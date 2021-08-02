using System;
using System.Collections.Generic;
using MongoDB.Driver;
using back_community_report.DAL.Models;
using back_community_report.DAL.DTO;

namespace back_community_report.DAL.DAO
{
    public class SetorDao : ISetorDao
    {
        public readonly IMongoContext _context;

        public SetorDao(IMongoContext context)
        {
            _context = context;
        }

        public void Inserir(Setor objSetor)
        {
            Setor novoObj = new Setor {
                Id_Prefeitura = objSetor.Id_Prefeitura,
                Nome = objSetor.Nome.TrimStart().TrimEnd().ToUpper(),
                Responsavel = objSetor.Responsavel.TrimStart().TrimEnd().ToUpper(),
                Email = objSetor.Email.TrimStart().TrimEnd(),
                Site = objSetor.Site.TrimStart().TrimEnd(),
                Telefone = objSetor.Telefone,
                Endereco = objSetor.Endereco,
                Status = "ativo"
            };

            _context.CollectionSetor.InsertOne(novoObj);
        }

        public List<Setor> ObterTodos()
        {
            var resultados = _context.CollectionSetor.Find<Setor>(setor => setor.Status == "ativo").ToList();
            return resultados;
        }

        public List<Setor> ObterPorPrefeitura(string idPrefeitura)
        {
            var resultados = _context.CollectionSetor.Find<Setor>(setor => (setor.Id_Prefeitura == idPrefeitura) && (setor.Status == "ativo")).ToList();
            return resultados;
        }

        public Setor ObterPorId(string id)
        {
            var resultado = _context.CollectionSetor.Find<Setor>(setor => setor.Id == id).FirstOrDefault();
            return resultado;
        }

        public void Atualizar(string id, Setor objSetor)
        {
            Setor novoObj = new Setor {
                Id = id,
                Id_Prefeitura = objSetor.Id_Prefeitura,
                Nome = objSetor.Nome.TrimStart().TrimEnd().ToUpper(),
                Responsavel = objSetor.Responsavel.TrimStart().TrimEnd().ToUpper(),
                Email = objSetor.Email.TrimStart().TrimEnd(),
                Site = objSetor.Site.TrimStart().TrimEnd(),
                Telefone = objSetor.Telefone,
                Endereco = objSetor.Endereco,
                Status = "ativo"
            };

            _context.CollectionSetor.ReplaceOne(setor => setor.Id == id, novoObj);
        }

        public void Deletar(string id)
        {
            // Ao "deletar" um setor não a removemos do banco, o que fazemos é setar o seu status para "inativo"
            // Isso é feito para que a referencia do setor com o chamado e com a categoria não seja perdido
            _context.CollectionSetor.UpdateOne(setor =>
                setor.Id == id,
                Builders<Setor>.Update.Set(setor => setor.Status, "inativo"),
                new UpdateOptions { IsUpsert = false }
            );
            //_context.CollectionSetor.DeleteOne(setor => setor.Id == id);
        }

        public string ObterIdPorNome(string nome)
        {
            var id = "";
            var setor = _context.CollectionSetor.Find<Setor>(set => set.Nome == nome.ToUpper()).FirstOrDefault();

            if (setor != null) {
                id = setor.Id;
            } else {
                id = null;
            }

            return id;
        }
    }
}