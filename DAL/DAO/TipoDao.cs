using System;
using System.Collections.Generic;
using MongoDB.Driver;
using back_community_report.DAL.Models;
using back_community_report.DAL.DTO;

namespace back_community_report.DAL.DAO
{
    public class TipoDao : ITipoDao
    {
        public readonly IMongoContext _context;

        public TipoDao(IMongoContext context)
        {
            _context = context;
        }

        public void Inserir(Tipo objTipo)
        {
            Tipo novoObj = new Tipo {
                Id_Categoria = objTipo.Id_Categoria,
                Descricao = objTipo.Descricao.TrimStart().TrimEnd().ToUpper(),
                Status = "ativo"
            };

            _context.CollectionTipo.InsertOne(novoObj);
        }

        public List<TipoDto> ObterTodos()
        {
            List<TipoDto> resultados = new List<TipoDto>();

            var tipos = _context.CollectionTipo.Find<Tipo>(tipo => tipo.Status == "ativo").ToList();

            foreach (var item in tipos)
            {
                var categoria = _context.CollectionCategoria.Find<Categoria>(cat => cat.Id == item.Id_Categoria).FirstOrDefault();
            
                TipoDto tipo = new TipoDto {
                    Id = item.Id,
                    Descricao = item.Descricao,
                    Id_Categoria = item.Id_Categoria,
                    Categoria = categoria!=null ? categoria.Descricao : "",
                    Status = item.Status
                };

                resultados.Add(tipo);
            }
            
            return resultados;
        }

        public List<TipoDto> ObterPorCategoria(string idCategoria)
        {
            List<TipoDto> resultados = new List<TipoDto>();

            var tipos = _context.CollectionTipo.Find<Tipo>(tipo => (tipo.Id_Categoria == idCategoria) && (tipo.Status == "ativo")).ToList();
            
            foreach (var item in tipos)
            {
                var categoria = _context.CollectionCategoria.Find<Categoria>(cat => cat.Id == item.Id_Categoria).FirstOrDefault();
            
                TipoDto tipo = new TipoDto {
                    Id = item.Id,
                    Descricao = item.Descricao,
                    Id_Categoria = item.Id_Categoria,
                    Categoria = categoria!=null ? categoria.Descricao : "",
                    Status = item.Status
                };

                resultados.Add(tipo);
            }
            
            return resultados;
        }

        public Tipo ObterPorId(string id)
        {
            var resultado = _context.CollectionTipo.Find<Tipo>(tipo => tipo.Id == id).FirstOrDefault();
            return resultado;
        }

        public void Atualizar(string id, Tipo objTipo)
        {
            Tipo novoObj = new Tipo {
                Id = id,
                Id_Categoria = objTipo.Id_Categoria,
                Descricao = objTipo.Descricao.TrimStart().TrimEnd().ToUpper(),
                Status = "ativo"
            };

            _context.CollectionTipo.ReplaceOne(tipo => tipo.Id == id, novoObj);
        }

        public void Deletar(string id)
        {            
            // Ao "deletar" um tipo não a removemos do banco, o que fazemos é setar o seu status para "inativo"
            // Isso é feito para que a referencia do tipo com o chamado não seja perdida
            _context.CollectionTipo.UpdateOne(tipo =>
                tipo.Id == id,
                Builders<Tipo>.Update.Set(tipo => tipo.Status, "inativo"),
                new UpdateOptions { IsUpsert = false }
            );
            //_context.CollectionTipo.DeleteOne(tipo => tipo.Id == id);
        }
    }
}