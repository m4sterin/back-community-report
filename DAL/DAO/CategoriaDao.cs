using System;
using System.Collections.Generic;
using MongoDB.Driver;
using back_community_report.DAL.Models;
using back_community_report.DAL.DTO;

namespace back_community_report.DAL.DAO
{
    public class CategoriaDao : ICategoriaDao
    {
        public readonly IMongoContext _context;

        public CategoriaDao(IMongoContext context)
        {
            _context = context;
        }

        public void Inserir(Categoria objCategoria)
        {
            Categoria novoObj = new Categoria {
                Descricao = objCategoria.Descricao.TrimStart().TrimEnd().ToUpper(),
                Id_Setor = objCategoria.Id_Setor,
                Status = "ativo"
            };

            _context.CollectionCategoria.InsertOne(novoObj);
        }

        public List<CategoriaDto> ObterTodas()
        {
            List<CategoriaDto> resultados = new List<CategoriaDto>();
            
            var categorias = _context.CollectionCategoria.Find<Categoria>(categ => categ.Status == "ativo").ToList();

            foreach (var item in categorias)
            {
                var setor  = _context.CollectionSetor.Find<Setor>(set => set.Id == item.Id_Setor).FirstOrDefault();
                
                CategoriaDto cat = new CategoriaDto {
                    Id = item.Id,
                    Descricao = item.Descricao,
                    Id_Setor = item.Id_Setor,
                    Setor = setor!=null ? setor.Nome : "",
                    Status = item.Status
                };

                resultados.Add(cat);
            }
            
            return resultados;
        }

        public List<CategoriaDto> ObterPorSetor(string idSetor)
        {
            List<CategoriaDto> resultados = new List<CategoriaDto>();
            
            var categorias = _context.CollectionCategoria.Find<Categoria>(categ => (categ.Id_Setor == idSetor) && (categ.Status == "ativo")).ToList();
            
            foreach (var item in categorias)
            {
                var setor  = _context.CollectionSetor.Find<Setor>(set => set.Id == item.Id_Setor).FirstOrDefault();
                
                CategoriaDto cat = new CategoriaDto {
                    Id = item.Id,
                    Descricao = item.Descricao,
                    Id_Setor = item.Id_Setor,
                    Setor = setor!=null ? setor.Nome : "",
                    Status = item.Status
                };

                resultados.Add(cat);
            }
            
            return resultados;
        }

        public Categoria ObterPorId(string id)
        {
            var resultado = _context.CollectionCategoria.Find<Categoria>(categ => categ.Id == id).FirstOrDefault();
            return resultado;
        }

        public string ObterPorDescricao(string descricao)
        {
            var id = "";
            
            var resultado = _context.CollectionCategoria.Find<Categoria>(categ => categ.Descricao == descricao.ToUpper()).FirstOrDefault();
            
            if (resultado != null) {
                id = resultado.Id;
            }

            return id;
        }

        public void Atualizar(string id, Categoria objCategoria)
        {
            Categoria novoObj = new Categoria {
                Id = id,
                Descricao = objCategoria.Descricao.TrimStart().TrimEnd().ToUpper(),
                Id_Setor = objCategoria.Id_Setor,
                Status = "ativo"
            };

            _context.CollectionCategoria.ReplaceOne(categ => categ.Id == id, novoObj);
        }

        public void Deletar(string id)
        {            
            // Ao "deletar" uma categoria não a removemos do banco, o que fazemos é setar o seu status para "inativo"
            // Isso é feito para que a referencia da categoria com o chamado não seja perdida
            _context.CollectionCategoria.UpdateOne(cat =>
                cat.Id == id,
                Builders<Categoria>.Update.Set(cat => cat.Status, "inativo"),
                new UpdateOptions { IsUpsert = false }
            );
            // _context.CollectionCategoria.DeleteOne(categ => categ.Id == id);
        }
    }
}