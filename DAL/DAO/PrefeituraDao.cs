using System.Collections.Generic;
using MongoDB.Driver;
using back_community_report.DAL.Models;
using back_community_report.DAL.DTO;

namespace back_community_report.DAL.DAO
{
    public class PrefeituraDao : IPrefeituraDao
    {
        public readonly IMongoContext _context;

        public PrefeituraDao(IMongoContext context)
        {
            _context = context;
        }

        public void Inserir(Prefeitura objPrefeitura)
        {
            Prefeitura novaPrefeitura = new Prefeitura {
                Nome = objPrefeitura.Nome.TrimStart().TrimEnd().ToUpper(),
                Email = objPrefeitura.Email,
                Site = objPrefeitura.Site,
                Telefone = objPrefeitura.Telefone,
                Endereco = objPrefeitura.Endereco
            };

            _context.CollectionPrefeitura.InsertOne(novaPrefeitura);
        }

        public List<Prefeitura> ObterTodas()
        {
            var colecaoPrefeitura = _context.CollectionPrefeitura.Find<Prefeitura>(pref => true).ToList();
            return colecaoPrefeitura;
        }

        public Prefeitura ObterPorNome(string nome)
        {
            var prefeitura = _context.CollectionPrefeitura.Find<Prefeitura>(pref => pref.Nome == nome.ToUpper()).FirstOrDefault();
            return prefeitura;
        }

        public Prefeitura ObterPorId(string id)
        {
            var prefeitura = _context.CollectionPrefeitura.Find<Prefeitura>(pref => pref.Id == id).FirstOrDefault();
            return prefeitura;
        }

        public void Atualizar(string id, Prefeitura objPrefeitura)
        {
            Prefeitura novaPrefeitura = new Prefeitura {
                Id = objPrefeitura.Id,
                Nome = objPrefeitura.Nome.TrimStart().TrimEnd().ToUpper(),
                Email = objPrefeitura.Email,
                Site = objPrefeitura.Site,
                Telefone = objPrefeitura.Telefone,
                Endereco = objPrefeitura.Endereco
            };

            _context.CollectionPrefeitura.ReplaceOne(pref => pref.Id == id, novaPrefeitura);
        }

        public void Deletar(string id)
        {
            _context.CollectionPrefeitura.DeleteOne(pref => pref.Id == id);
        }

        public string ObterIdPorNome(string nome)
        {
            var id = "";
            
            var resultado = _context.CollectionPrefeitura.Find<Prefeitura>(pref => pref.Nome == nome.ToUpper()).FirstOrDefault();
            
            if (resultado != null) {
                id = resultado.Id;
            }

            return id;
        }
    }
}