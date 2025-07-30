using MongoDB.Driver;
using Microsoft.Extensions.Options;
using back_community_report.DAL.Models;

namespace back_community_report.DAL.DAO
{
    public class MongoContext:IMongoContext
    {
        private readonly IMongoDatabase _db;

        public MongoContext(IOptions<Configuracoes> options, IMongoClient client)
        {
            _db = client.GetDatabase(options.Value.Database);
        }
        
        public IMongoCollection<UsuarioRestrito> CollectionUsuarioRestrito => _db.GetCollection<UsuarioRestrito>("UsuarioRestrito");
        public IMongoCollection<UsuarioPublico> CollectionUsuarioPublico => _db.GetCollection<UsuarioPublico>("UsuarioPublico");
        public IMongoCollection<Chamado> CollectionChamado => _db.GetCollection<Chamado>("Chamado");
        public IMongoCollection<Arquivo> CollectionArquivo => _db.GetCollection<Arquivo>("Arquivo");
        public IMongoCollection<Historico> CollectionHistorico => _db.GetCollection<Historico>("Historico");
        public IMongoCollection<Prefeitura> CollectionPrefeitura => _db.GetCollection<Prefeitura>("Prefeitura");
        public IMongoCollection<Setor> CollectionSetor => _db.GetCollection<Setor>("Setor");
        public IMongoCollection<Categoria> CollectionCategoria => _db.GetCollection<Categoria>("Categoria");
        public IMongoCollection<Tipo> CollectionTipo => _db.GetCollection<Tipo>("Tipo");
    }
}