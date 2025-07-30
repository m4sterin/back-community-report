using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using MongoDB.Driver;
using back_community_report.DAL.Models;
using back_community_report.DAL.DTO;
using back_community_report.BLL.Exceptions;

namespace back_community_report.DAL.DAO
{
    public class UsuarioPublicoDao : IUsuarioPublicoDao
    {
        private readonly IMongoContext _context;

        public UsuarioPublicoDao(IMongoContext context)
        {
            _context = context;
        }

        public void Inserir(UsuarioPublico objUsuario)
        {
            UsuarioPublico usuario = new UsuarioPublico {
                Nome = objUsuario.Nome.TrimStart().TrimEnd(),
                Email = objUsuario.Email.TrimStart().TrimEnd(),
                Cpf = objUsuario.Cpf.TrimStart().TrimEnd(),
                Endereco = objUsuario.Endereco,
                Prefeitura = objUsuario.Prefeitura.TrimStart().TrimEnd(),
                Login = objUsuario.Login.TrimStart().TrimEnd(),
                Senha = CriptografarSenhaSHA256(objUsuario.Senha)
            };
            
            _context.CollectionUsuarioPublico.InsertOne(usuario);
        }

        public List<UsuarioPublico> ObterTodos()
        {
            var usuarios = _context.CollectionUsuarioPublico.Find(users => true).ToList();
            return usuarios;
        }

        public UsuarioPublico ObterPorId(string id)
        {
            var usuario = _context.CollectionUsuarioPublico.Find<UsuarioPublico>(user => user.Id == id).FirstOrDefault();
            return usuario;
        }

        public UsuarioPublico ObterPorLogin(string login)
        {
            var usuario = _context.CollectionUsuarioPublico.Find<UsuarioPublico>(user => user.Login == login).FirstOrDefault();
            return usuario;
        }

        public UsuarioPublico ObterPorEmail(string email)
        {
            var usuario = _context.CollectionUsuarioPublico.Find<UsuarioPublico>(user => user.Email == email).FirstOrDefault();
            return usuario;
        }

        public void Atualizar(string id, UsuarioPublico objUsuario)
        {
            UsuarioPublico usuario = new UsuarioPublico {
                Id = id,
                Nome = objUsuario.Nome.TrimStart().TrimEnd(),
                Email = objUsuario.Email.TrimStart().TrimEnd(),
                Cpf = objUsuario.Cpf.TrimStart().TrimEnd(),
                Endereco = objUsuario.Endereco,
                Prefeitura = objUsuario.Prefeitura.TrimStart().TrimEnd(),
                Login = objUsuario.Login.TrimStart().TrimEnd(),
                Senha = objUsuario.Senha
            };
            
            _context.CollectionUsuarioPublico.ReplaceOne(user => user.Id == id, usuario);
        }

        public void AtualizarSenha(string id, string senhaAntiga, string senhaNova)
        {
            var validSenha = VerificarSenhaAntiga(id, senhaAntiga);
            
            if(validSenha == true){
                var senha = CriptografarSenhaSHA256(senhaNova);

                _context.CollectionUsuarioPublico.UpdateOne(user =>
                    user.Id == id,
                    Builders<UsuarioPublico>.Update.Set(user => user.Senha, senha),
                    new UpdateOptions { IsUpsert = false }
                );
            }
            else {
                throw new IncorrectPasswordException("A senha atual está incorreta!");
            }
        }

        public void RedefinirEndereco(string id, EnderecoDto endereco)
        {
            _context.CollectionUsuarioPublico.UpdateOne(user =>
                user.Id == id,
                Builders<UsuarioPublico>.Update.Set(user => user.Endereco, endereco),
                new UpdateOptions { IsUpsert = false }
            );
        }

        public void Deletar(string id)
        {
            _context.CollectionUsuarioPublico.DeleteOne(user => user.Id == id);
        }

        public bool VerificarSenhaAntiga(string id, string senhaAntiga)
        {
            var senhaCriptografada = CriptografarSenhaSHA256(senhaAntiga);

            var user = _context.CollectionUsuarioPublico.Find<UsuarioPublico>(u => u.Id == id).FirstOrDefault();

            if (user.Senha == senhaCriptografada){
                return true; // essa é a senha correta, então pode trocar
            }
            else {
                return false; // a senha está incorreta, então não pode trocar
            }
        }

        public string CriptografarSenhaSHA256(string senha)
        {
           // Criando a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                // ComputeHash - retorna array de bytes  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(senha));  
  
                // Converte array de bytes para uma string   
                StringBuilder builder = new StringBuilder();  
                for (int i = 0; i < bytes.Length; i++)  
                {  
                    builder.Append(bytes[i].ToString("x2"));  
                }  
                return builder.ToString();  
            }   
        }
    }
}