using System;
using System.Collections.Generic;
using MongoDB.Driver;
using back_community_report.DAL.Models;
using back_community_report.DAL.DTO;

namespace back_community_report.DAL.DAO
{
    public class ChamadoDao : IChamadoDao
    {
        public readonly IMongoContext _context;
        public readonly IHistoricoDao _historicoDao;

        public ChamadoDao(IMongoContext context, IHistoricoDao historicoDao)
        {
            _context = context;
            _historicoDao = historicoDao;
        }

        public void Inserir(Chamado objChamado)
        {
            var categoria = _context.CollectionCategoria.Find<Categoria>(cat => cat.Id == objChamado.Id_Categoria).FirstOrDefault();
            var setor = new Setor();
            
            if (categoria != null) {
                setor = _context.CollectionSetor.Find<Setor>(set => set.Id == categoria.Id_Setor).FirstOrDefault();
            } else {
                setor = null;
            }
            
            // Inserindo o chamado
            Chamado novoChamado = new Chamado {
                Privacidade = objChamado.Privacidade,
                Id_Categoria = objChamado.Id_Categoria,
                Id_Tipo = objChamado.Id_Tipo,
                Observacao = objChamado.Observacao,
                Endereco = objChamado.Endereco,
                Status = "Em Análise",
                Data_Abertura = DateTime.Now,
                Data_Conclusao = null,
                Id_Prefeitura = objChamado.Id_Prefeitura,
                Id_Setor = setor!=null ? setor.Id : null, // (condição de aceitação) ? (se verdadeiro) : (se falso)
                Justificativa = "",
                Id_Usuario_Publico = objChamado.Id_Usuario_Publico,
                Imagem = ""
            };

            _context.CollectionChamado.InsertOne(novoChamado);

            // Inserindo o Arquivo
            if (objChamado.Imagem != "") {

                Arquivo file = new Arquivo {
                    Id_Chamado = novoChamado.Id,
                    Arquivo_Hash = objChamado.Imagem
                };

                _context.CollectionArquivo.InsertOne(file);
            }

            // Inserindo o Historico
            Historico objHistorico = new Historico {
                Id_Chamado = novoChamado.Id,
                Status_Atual = novoChamado.Status,
                Setor_Responsavel = setor!=null ? setor.Nome : "",
                Data_Atual = DateTime.Now,
                Justificativa = "",
                Responsavel_Gestao = ""
            };

            _historicoDao.Inserir(objHistorico);
        }

        public List<ChamadoDto> ObterTodos()
        {
            List<ChamadoDto> resultados = new List<ChamadoDto>();
            
            var chamados = _context.CollectionChamado.Find<Chamado>(chamado => true).ToList();
            
            foreach (var item in chamados)
            {
                var categoria = new Categoria();
                var tipo = new Tipo();
                var prefeitura = new Prefeitura();
                var setor = new Setor();
                var user_public = new UsuarioPublico();
                
                if (item.Id_Categoria != "" && item.Id_Categoria != null) {
                    categoria = _context.CollectionCategoria.Find<Categoria>(categ => categ.Id == item.Id_Categoria).FirstOrDefault();
                }
                if (item.Id_Tipo != "" && item.Id_Tipo != null) {
                    tipo = _context.CollectionTipo.Find<Tipo>(tipo => tipo.Id == item.Id_Tipo).FirstOrDefault();
                }
                if (item.Id_Prefeitura != "" && item.Id_Prefeitura != null) {
                    prefeitura = _context.CollectionPrefeitura.Find<Prefeitura>(pref => pref.Id == item.Id_Prefeitura).FirstOrDefault();
                }
                if (item.Id_Setor != "" && item.Id_Setor != null) {
                    setor = _context.CollectionSetor.Find<Setor>(setor => setor.Id == item.Id_Setor).FirstOrDefault();
                }
                if (item.Id_Usuario_Publico != "" && item.Id_Usuario_Publico != null) {
                    user_public = _context.CollectionUsuarioPublico.Find<UsuarioPublico>(user => user.Id == item.Id_Usuario_Publico).FirstOrDefault();
                }

                var file = _context.CollectionArquivo.Find<Arquivo>(arq => arq.Id_Chamado == item.Id).FirstOrDefault();

                ChamadoDto objChamado = new ChamadoDto {
                    Id = item.Id,
                    Privacidade = item.Privacidade,
                    Id_Categoria = item.Id_Categoria,
                    Categoria = categoria.Descricao,
                    Id_Tipo = item.Id_Tipo,
                    Tipo = tipo.Descricao,
                    Observacao = item.Observacao,
                    Endereco = item.Endereco,
                    Status = item.Status,
                    Data_Abertura = item.Data_Abertura,
                    Data_Conclusao = item.Data_Conclusao,
                    Id_Prefeitura = item.Id_Prefeitura,
                    Prefeitura = prefeitura.Nome,
                    Id_Setor = setor.Id,
                    Setor = setor.Nome,
                    Justificativa = item.Justificativa,
                    Id_Usuario_Publico = item.Id_Usuario_Publico,
                    Usuario_Publico = user_public.Nome,
                    Imagem = file!=null ? file.Arquivo_Hash : "" // (condição de aceitação) ? (se verdadeiro) : (se falso)
                };

                resultados.Add(objChamado);
            }
            
            return resultados;
        }

        public List<ChamadoDto> ObterPorPrefeitura(string idPrefeitura)
        {
            List<ChamadoDto> resultados = new List<ChamadoDto>();

            var sort = Builders<Chamado>.Sort.Descending(cham => cham.Data_Abertura);
            var chamados = _context.CollectionChamado.Find<Chamado>(cham => (cham.Id_Prefeitura == idPrefeitura) && cham.Status != "Rejeitado").Sort(sort).ToList();
            
            foreach (var item in chamados)
            {
                var categoria = new Categoria();
                var tipo = new Tipo();
                var prefeitura = new Prefeitura();
                var setor = new Setor();
                var user_public = new UsuarioPublico();
                
                if (item.Id_Categoria != "" && item.Id_Categoria != null) {
                    categoria = _context.CollectionCategoria.Find<Categoria>(categ => categ.Id == item.Id_Categoria).FirstOrDefault();
                }
                if (item.Id_Tipo != "" && item.Id_Tipo != null) {
                    tipo = _context.CollectionTipo.Find<Tipo>(tipo => tipo.Id == item.Id_Tipo).FirstOrDefault();
                }
                if (item.Id_Prefeitura != "" && item.Id_Prefeitura != null) {
                    prefeitura = _context.CollectionPrefeitura.Find<Prefeitura>(pref => pref.Id == item.Id_Prefeitura).FirstOrDefault();
                }
                if (item.Id_Setor != "" && item.Id_Setor != null) {
                    setor = _context.CollectionSetor.Find<Setor>(setor => setor.Id == item.Id_Setor).FirstOrDefault();
                }
                if (item.Id_Usuario_Publico != "" && item.Id_Usuario_Publico != null) {
                    user_public = _context.CollectionUsuarioPublico.Find<UsuarioPublico>(user => user.Id == item.Id_Usuario_Publico).FirstOrDefault();
                }

                var file = _context.CollectionArquivo.Find<Arquivo>(arq => arq.Id_Chamado == item.Id).FirstOrDefault();

                ChamadoDto objChamado = new ChamadoDto {
                    Id = item.Id,
                    Privacidade = item.Privacidade,
                    Id_Categoria = item.Id_Categoria,
                    Categoria = categoria.Descricao,
                    Id_Tipo = item.Id_Tipo,
                    Tipo = tipo.Descricao,
                    Observacao = item.Observacao,
                    Endereco = item.Endereco,
                    Status = item.Status,
                    Data_Abertura = item.Data_Abertura,
                    Data_Conclusao = item.Data_Conclusao,
                    Id_Prefeitura = item.Id_Prefeitura,
                    Prefeitura = prefeitura.Nome,
                    Id_Setor = setor.Id,
                    Setor = setor.Nome,
                    Justificativa = item.Justificativa,
                    Id_Usuario_Publico = item.Id_Usuario_Publico,
                    Usuario_Publico = user_public.Nome,
                    Imagem = file!=null ? file.Arquivo_Hash : "" // (condição de aceitação) ? (se verdadeiro) : (se falso)
                };

                resultados.Add(objChamado);
            }
            
            return resultados;
        }
        
        public List<ChamadoDto> ObterPorSetor(string idPrefeitura, string idSetor)
        {
            List<ChamadoDto> resultados = new List<ChamadoDto>();
            
            var chamados = _context.CollectionChamado.Find<Chamado>(cham => (cham.Id_Prefeitura == idPrefeitura) && (cham.Id_Setor == idSetor)).ToList();
            
            foreach (var item in chamados)
            {
                var categoria = new Categoria();
                var tipo = new Tipo();
                var prefeitura = new Prefeitura();
                var setor = new Setor();
                var user_public = new UsuarioPublico();
                
                if (item.Id_Categoria != "" && item.Id_Categoria != null) {
                    categoria = _context.CollectionCategoria.Find<Categoria>(categ => categ.Id == item.Id_Categoria).FirstOrDefault();
                }
                if (item.Id_Tipo != "" && item.Id_Tipo != null) {
                    tipo = _context.CollectionTipo.Find<Tipo>(tipo => tipo.Id == item.Id_Tipo).FirstOrDefault();
                }
                if (item.Id_Prefeitura != "" && item.Id_Prefeitura != null) {
                    prefeitura = _context.CollectionPrefeitura.Find<Prefeitura>(pref => pref.Id == item.Id_Prefeitura).FirstOrDefault();
                }
                if (item.Id_Setor != "" && item.Id_Setor != null) {
                    setor = _context.CollectionSetor.Find<Setor>(setor => setor.Id == item.Id_Setor).FirstOrDefault();
                }
                if (item.Id_Usuario_Publico != "" && item.Id_Usuario_Publico != null) {
                    user_public = _context.CollectionUsuarioPublico.Find<UsuarioPublico>(user => user.Id == item.Id_Usuario_Publico).FirstOrDefault();
                }

                var file = _context.CollectionArquivo.Find<Arquivo>(arq => arq.Id_Chamado == item.Id).FirstOrDefault();

                ChamadoDto objChamado = new ChamadoDto {
                    Id = item.Id,
                    Privacidade = item.Privacidade,
                    Id_Categoria = item.Id_Categoria,
                    Categoria = categoria.Descricao,
                    Id_Tipo = item.Id_Tipo,
                    Tipo = tipo.Descricao,
                    Observacao = item.Observacao,
                    Endereco = item.Endereco,
                    Status = item.Status,
                    Data_Abertura = item.Data_Abertura,
                    Data_Conclusao = item.Data_Conclusao,
                    Id_Prefeitura = item.Id_Prefeitura,
                    Prefeitura = prefeitura.Nome,
                    Id_Setor = setor.Id,
                    Setor = setor.Nome,
                    Justificativa = item.Justificativa,
                    Id_Usuario_Publico = item.Id_Usuario_Publico,
                    Usuario_Publico = user_public.Nome,
                    Imagem = file!=null ? file.Arquivo_Hash : "" // (condição de aceitação) ? (se verdadeiro) : (se falso)
                };

                resultados.Add(objChamado);
            }
            
            return resultados;
        }

        public List<ChamadoDto> ObterPorUsuarioPublico(string idUsuario)
        {
            List<ChamadoDto> resultados = new List<ChamadoDto>();
            
            var sort = Builders<Chamado>.Sort.Descending(cham => cham.Data_Abertura);
            var chamados = _context.CollectionChamado.Find<Chamado>(u => u.Id_Usuario_Publico == idUsuario).Sort(sort).ToList();
            
            foreach (var item in chamados)
            {
                var categoria = new Categoria();
                var tipo = new Tipo();
                var prefeitura = new Prefeitura();
                var setor = new Setor();
                var user_public = new UsuarioPublico();
                
                if (item.Id_Categoria != "" && item.Id_Categoria != null) {
                    categoria = _context.CollectionCategoria.Find<Categoria>(categ => categ.Id == item.Id_Categoria).FirstOrDefault();
                }
                if (item.Id_Tipo != "" && item.Id_Tipo != null) {
                    tipo = _context.CollectionTipo.Find<Tipo>(tipo => tipo.Id == item.Id_Tipo).FirstOrDefault();
                }
                if (item.Id_Prefeitura != "" && item.Id_Prefeitura != null) {
                    prefeitura = _context.CollectionPrefeitura.Find<Prefeitura>(pref => pref.Id == item.Id_Prefeitura).FirstOrDefault();
                }
                if (item.Id_Setor != "" && item.Id_Setor != null) {
                    setor = _context.CollectionSetor.Find<Setor>(setor => setor.Id == item.Id_Setor).FirstOrDefault();
                }
                if (item.Id_Usuario_Publico != "" && item.Id_Usuario_Publico != null) {
                    user_public = _context.CollectionUsuarioPublico.Find<UsuarioPublico>(user => user.Id == item.Id_Usuario_Publico).FirstOrDefault();
                }

                var file = _context.CollectionArquivo.Find<Arquivo>(arq => arq.Id_Chamado == item.Id).FirstOrDefault();

                ChamadoDto objChamado = new ChamadoDto {
                    Id = item.Id,
                    Privacidade = item.Privacidade,
                    Id_Categoria = item.Id_Categoria,
                    Categoria = categoria.Descricao,
                    Id_Tipo = item.Id_Tipo,
                    Tipo = tipo.Descricao,
                    Observacao = item.Observacao,
                    Endereco = item.Endereco,
                    Status = item.Status,
                    Data_Abertura = item.Data_Abertura,
                    Data_Conclusao = item.Data_Conclusao,
                    Id_Prefeitura = item.Id_Prefeitura,
                    Prefeitura = prefeitura.Nome,
                    Id_Setor = setor.Id,
                    Setor = setor.Nome,
                    Justificativa = item.Justificativa,
                    Id_Usuario_Publico = item.Id_Usuario_Publico,
                    Usuario_Publico = user_public.Nome,
                    Imagem = file!=null ? file.Arquivo_Hash : "" // (condição de aceitação) ? (se verdadeiro) : (se falso)
                };

                resultados.Add(objChamado);
            }
            
            return resultados;
        }

        public List<ChamadoDto> ObterPorCategoria(string idCategoria)
        {
            List<ChamadoDto> resultados = new List<ChamadoDto>();
            
            var chamados = _context.CollectionChamado.Find<Chamado>(cham => cham.Id_Categoria == idCategoria).ToList();
            
            foreach (var item in chamados)
            {
                var categoria = new Categoria();
                var tipo = new Tipo();
                var prefeitura = new Prefeitura();
                var setor = new Setor();
                var user_public = new UsuarioPublico();
                
                if (item.Id_Categoria != "" && item.Id_Categoria != null) {
                    categoria = _context.CollectionCategoria.Find<Categoria>(categ => categ.Id == item.Id_Categoria).FirstOrDefault();
                }
                if (item.Id_Tipo != "" && item.Id_Tipo != null) {
                    tipo = _context.CollectionTipo.Find<Tipo>(tipo => tipo.Id == item.Id_Tipo).FirstOrDefault();
                }
                if (item.Id_Prefeitura != "" && item.Id_Prefeitura != null) {
                    prefeitura = _context.CollectionPrefeitura.Find<Prefeitura>(pref => pref.Id == item.Id_Prefeitura).FirstOrDefault();
                }
                if (item.Id_Setor != "" && item.Id_Setor != null) {
                    setor = _context.CollectionSetor.Find<Setor>(setor => setor.Id == item.Id_Setor).FirstOrDefault();
                }
                if (item.Id_Usuario_Publico != "" && item.Id_Usuario_Publico != null) {
                    user_public = _context.CollectionUsuarioPublico.Find<UsuarioPublico>(user => user.Id == item.Id_Usuario_Publico).FirstOrDefault();
                }

                var file = _context.CollectionArquivo.Find<Arquivo>(arq => arq.Id_Chamado == item.Id).FirstOrDefault();

                ChamadoDto objChamado = new ChamadoDto {
                    Id = item.Id,
                    Privacidade = item.Privacidade,
                    Id_Categoria = item.Id_Categoria,
                    Categoria = categoria.Descricao,
                    Id_Tipo = item.Id_Tipo,
                    Tipo = tipo.Descricao,
                    Observacao = item.Observacao,
                    Endereco = item.Endereco,
                    Status = item.Status,
                    Data_Abertura = item.Data_Abertura,
                    Data_Conclusao = item.Data_Conclusao,
                    Id_Prefeitura = item.Id_Prefeitura,
                    Prefeitura = prefeitura.Nome,
                    Id_Setor = setor.Id,
                    Setor = setor.Nome,
                    Justificativa = item.Justificativa,
                    Id_Usuario_Publico = item.Id_Usuario_Publico,
                    Usuario_Publico = user_public.Nome,
                    Imagem = file!=null ? file.Arquivo_Hash : "" // (condição de aceitação) ? (se verdadeiro) : (se falso)
                };

                resultados.Add(objChamado);
            }
            
            return resultados;
        }

        public Chamado ObterPorId(string id)
        {
            var chamado = _context.CollectionChamado.Find<Chamado>(cham => cham.Id == id).FirstOrDefault();
            return chamado;
        }

        public void Atualizar(string id, Chamado objChamado)
        {
            var categoria = _context.CollectionCategoria.Find<Categoria>(cat => cat.Id == objChamado.Id_Categoria).FirstOrDefault();
            var setor = new Setor();
            
            if (categoria != null) {
                setor = _context.CollectionSetor.Find<Setor>(set => set.Id == categoria.Id_Setor).FirstOrDefault();
            } else {
                setor = null;
            }
            
            var chamadoAntigo = _context.CollectionChamado.Find<Chamado>(cham => cham.Id == id).FirstOrDefault();
            
            Chamado novoChamado = new Chamado {
                Id = id,
                Privacidade = objChamado.Privacidade,
                Id_Categoria = objChamado.Id_Categoria,
                Id_Tipo = objChamado.Id_Tipo,
                Observacao = objChamado.Observacao,
                Endereco = objChamado.Endereco,
                Status = chamadoAntigo.Status,
                Data_Abertura = chamadoAntigo.Data_Abertura,
                Data_Conclusao = null,
                Id_Prefeitura = objChamado.Id_Prefeitura,
                Id_Setor = setor!=null ? setor.Id : null, // (condição de aceitação) ? (se verdadeiro) : (se falso)
                Justificativa = "",
                Id_Usuario_Publico = objChamado.Id_Usuario_Publico,
                Imagem = objChamado.Imagem
            };

            _context.CollectionChamado.ReplaceOne(u => u.Id == id, novoChamado);

            // Atualizando arquivo do chamado
            if (objChamado.Imagem != "") {

                // 1º: Deve-se apagar a imagem antiga
                _context.CollectionArquivo.DeleteOne(arq => arq.Id_Chamado == id);
                
                // 2º: Cadastrar o novo arquivo
                Arquivo file = new Arquivo {
                    Id_Chamado = novoChamado.Id,
                    Arquivo_Hash = objChamado.Imagem
                };
                _context.CollectionArquivo.InsertOne(file);
            }
        }

        public void EmAtendimento(DadosGerenciaDto dados)
        {
            _context.CollectionChamado.UpdateOne(chamado =>
                chamado.Id == dados.idChamado,
                Builders<Chamado>.Update.Set(cham => cham.Status, "Em Atendimento"),
                new UpdateOptions { IsUpsert = false }
            );

            _context.CollectionChamado.UpdateOne(chamado =>
                chamado.Id == dados.idChamado,
                Builders<Chamado>.Update.Set(cham => cham.Justificativa, dados.Justificativa),
                new UpdateOptions { IsUpsert = false }
            );
            
            var objChamado = _context.CollectionChamado.Find<Chamado>(cham => cham.Id == dados.idChamado).FirstOrDefault();
            var setor = new Setor();
            if (objChamado.Id_Setor != "") {
                setor = _context.CollectionSetor.Find<Setor>(set => set.Id == objChamado.Id_Setor).FirstOrDefault();
            }
            var user = _context.CollectionUsuarioRestrito.Find<UsuarioRestrito>(user => user.Id == dados.idUsuario).FirstOrDefault();

            // Inserindo o Historico
            Historico objHistorico = new Historico {
                Id_Chamado = dados.idChamado,
                Status_Atual = "Em Atendimento",
                Setor_Responsavel = setor!=null ? setor.Nome : "",
                Data_Atual = DateTime.Now,
                Justificativa = dados.Justificativa,
                Responsavel_Gestao = user!=null ? user.Nome : ""
            };

            _historicoDao.Inserir(objHistorico);
        }

        public void Finalizado(DadosGerenciaDto dados)
        {
            _context.CollectionChamado.UpdateOne(chamado =>
                chamado.Id == dados.idChamado,
                Builders<Chamado>.Update.Set(cham => cham.Status, "Finalizado"),
                new UpdateOptions { IsUpsert = false }
            );

            _context.CollectionChamado.UpdateOne(chamado =>
                chamado.Id == dados.idChamado,
                Builders<Chamado>.Update.Set(cham => cham.Data_Conclusao, DateTime.Now),
                new UpdateOptions { IsUpsert = false }
            );

             _context.CollectionChamado.UpdateOne(chamado =>
                chamado.Id == dados.idChamado,
                Builders<Chamado>.Update.Set(cham => cham.Justificativa, dados.Justificativa),
                new UpdateOptions { IsUpsert = false }
            );
            
            var objChamado = _context.CollectionChamado.Find<Chamado>(cham => cham.Id == dados.idChamado).FirstOrDefault();
            var setor = new Setor();
            if (objChamado.Id_Setor != "") {
                setor = _context.CollectionSetor.Find<Setor>(set => set.Id == objChamado.Id_Setor).FirstOrDefault();
            }
            var user = _context.CollectionUsuarioRestrito.Find<UsuarioRestrito>(user => user.Id == dados.idUsuario).FirstOrDefault();

            // Inserindo o Historico
            Historico objHistorico = new Historico {
                Id_Chamado = dados.idChamado,
                Status_Atual = "Finalizado",
                Setor_Responsavel = setor!=null ? setor.Nome : "",
                Data_Atual = DateTime.Now,
                Justificativa = dados.Justificativa,
                Responsavel_Gestao = user!=null ? user.Nome : ""
            };

            _historicoDao.Inserir(objHistorico);
        }

        public void EnviadoOutroSetor(DadosGerenciaDto dados)
        {
            _context.CollectionChamado.UpdateOne(chamado =>
                chamado.Id == dados.idChamado,
                Builders<Chamado>.Update.Set(cham => cham.Status, "Enviado para o Setor Responsável"),
                new UpdateOptions { IsUpsert = false }
            );

            _context.CollectionChamado.UpdateOne(chamado =>
                chamado.Id == dados.idChamado,
                Builders<Chamado>.Update.Set(cham => cham.Id_Setor, dados.idNovoSetor),
                new UpdateOptions { IsUpsert = false }
            );

            _context.CollectionChamado.UpdateOne(chamado =>
                chamado.Id == dados.idChamado,
                Builders<Chamado>.Update.Set(cham => cham.Justificativa, dados.Justificativa),
                new UpdateOptions { IsUpsert = false }
            );
            
            var objChamado = _context.CollectionChamado.Find<Chamado>(cham => cham.Id == dados.idChamado).FirstOrDefault();
            var setor = new Setor();
            if (objChamado.Id_Setor != "") {
                setor = _context.CollectionSetor.Find<Setor>(set => set.Id == objChamado.Id_Setor).FirstOrDefault();
            }
            var user = _context.CollectionUsuarioRestrito.Find<UsuarioRestrito>(user => user.Id == dados.idUsuario).FirstOrDefault();

            // Inserindo o Historico
            Historico objHistorico = new Historico {
                Id_Chamado = dados.idChamado,
                Status_Atual = "Enviado para o Setor Responsável",
                Setor_Responsavel = setor!=null ? setor.Nome : "",
                Data_Atual = DateTime.Now,
                Justificativa = dados.Justificativa,
                Responsavel_Gestao = user!=null ? user.Nome : ""
            };

            _historicoDao.Inserir(objHistorico);
        }

        public void Rejeitado(DadosGerenciaDto dados)
        {
            _context.CollectionChamado.UpdateOne(chamado =>
                chamado.Id == dados.idChamado,
                Builders<Chamado>.Update.Set(cham => cham.Status, "Rejeitado"),
                new UpdateOptions { IsUpsert = false }
            );

            _context.CollectionChamado.UpdateOne(chamado =>
                chamado.Id == dados.idChamado,
                Builders<Chamado>.Update.Set(cham => cham.Data_Conclusao, DateTime.Now),
                new UpdateOptions { IsUpsert = false }
            );

             _context.CollectionChamado.UpdateOne(chamado =>
                chamado.Id == dados.idChamado,
                Builders<Chamado>.Update.Set(cham => cham.Justificativa, dados.Justificativa),
                new UpdateOptions { IsUpsert = false }
            );
            
            var objChamado = _context.CollectionChamado.Find<Chamado>(cham => cham.Id == dados.idChamado).FirstOrDefault();
            var setor = new Setor();
            if (objChamado.Id_Setor != "") {
                setor = _context.CollectionSetor.Find<Setor>(set => set.Id == objChamado.Id_Setor).FirstOrDefault();
            }
            var user = _context.CollectionUsuarioRestrito.Find<UsuarioRestrito>(user => user.Id == dados.idUsuario).FirstOrDefault();

            // Inserindo o Historico
            Historico objHistorico = new Historico {
                Id_Chamado = dados.idChamado,
                Status_Atual = "Rejeitado",
                Setor_Responsavel = setor!=null ? setor.Nome : "",
                Data_Atual = DateTime.Now,
                Justificativa = dados.Justificativa,
                Responsavel_Gestao = user!=null ? user.Nome : ""
            };

            _historicoDao.Inserir(objHistorico);
        }

        public void Deletar(string id)
        {
            // 1º: Limpar/Apagar históricos do chamado
            _context.CollectionHistorico.DeleteMany(hist => hist.Id_Chamado == id);

            // 2º: Limpar/Apagar Arquivos
            _context.CollectionArquivo.DeleteMany(arq => arq.Id_Chamado == id);
            
            // 3º: Deletar o chamado
            _context.CollectionChamado.DeleteOne(chamado => chamado.Id == id);
        }

        public int TotalChamados(string idPrefeitura)
        {
            var qtdChamados = _context.CollectionChamado.Find(cham => (cham.Id_Prefeitura == idPrefeitura) && cham.Status != "Rejeitado").CountDocuments();
            var total = Convert.ToInt32(qtdChamados);
            return total;
        }

        public int TotalChamadosEmAnalise(string idPrefeitura)
        {
            var qtdChamados = _context.CollectionChamado.Find(cham => (cham.Id_Prefeitura == idPrefeitura) && cham.Status == "Em Análise").CountDocuments();
            var total = Convert.ToInt32(qtdChamados);
            return total;
        }

        public int TotalChamadosEmAtendimento(string idPrefeitura)
        {
            var qtdChamados = _context.CollectionChamado.Find(cham => (cham.Id_Prefeitura == idPrefeitura) && cham.Status == "Em Atendimento").CountDocuments();
            var total = Convert.ToInt32(qtdChamados);
            return total;
        }

        public int TotalChamadosFinalizados(string idPrefeitura)
        {
            var qtdChamados = _context.CollectionChamado.Find(cham => (cham.Id_Prefeitura == idPrefeitura) && cham.Status == "Finalizado").CountDocuments();
            var total = Convert.ToInt32(qtdChamados);
            return total;
        }
    }
}