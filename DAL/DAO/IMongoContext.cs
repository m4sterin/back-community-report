using back_community_report.DAL.Models;
using MongoDB.Driver;

namespace back_community_report.DAL.DAO
{
    public interface IMongoContext
    {
        IMongoCollection<UsuarioPublico> CollectionUsuarioPublico { get; }
        IMongoCollection<UsuarioRestrito> CollectionUsuarioRestrito { get; }
        IMongoCollection<Chamado> CollectionChamado { get; }
        IMongoCollection<Arquivo> CollectionArquivo { get; }
        IMongoCollection<Historico> CollectionHistorico { get; }
        IMongoCollection<Prefeitura> CollectionPrefeitura { get; }
        IMongoCollection<Setor> CollectionSetor { get; }
        IMongoCollection<Categoria> CollectionCategoria { get; }
        IMongoCollection<Tipo> CollectionTipo { get; }
    }
}