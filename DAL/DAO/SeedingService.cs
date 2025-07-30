using System;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using back_community_report.DAL.Models;
using back_community_report.DAL.DTO;

namespace back_community_report.DAL.DAO
{
    public class SeedingService
    {
        private readonly IMongoContext _context;
        public readonly IUsuarioRestritoDao _usuarioRestritoDao;
        public readonly ICategoriaDao _categoriaDao;
        public readonly IPrefeituraDao _prefeituraDao;
        public readonly ISetorDao _setorDao;

        public SeedingService(IMongoContext context, IUsuarioRestritoDao usuarioRestritoDao, ICategoriaDao categoriaDao, IPrefeituraDao prefeituraDao, ISetorDao setorDao)
        {
            _context = context;
            _usuarioRestritoDao = usuarioRestritoDao;
            _categoriaDao = categoriaDao;
            _prefeituraDao = prefeituraDao;
            _setorDao = setorDao;
        }

        public void Seed()
        {
            // Seeding de Prefeitura e de Setor
            if (_context.CollectionPrefeitura.Find(pref => true).ToList().Count == 0) {

                EnderecoDto endereco = new EnderecoDto {
                    CEP = "38700-900",
                    Rua = "Rua Dr. José Olympio de Mello",
                    Numero = 151,
                    Bairro = "Eldorado",
                    Complemento = "",
                    Cidade = "Patos de Minas",
                    Estado = "MG"
                };
                
                Prefeitura pref_01 = new Prefeitura();
                pref_01.Nome = "PREFEITURA MUNICIPAL DE PATOS DE MINAS";
                pref_01.Email = "teste@email.com";
                pref_01.Site = "http://patosdeminas.mg.gov.br/home/";
                pref_01.Telefone = "(34) 3822-9800";
                pref_01.Endereco = endereco;

                _context.CollectionPrefeitura.InsertOne(pref_01);

                List<Setor> setores = new List<Setor>();
                
                Setor set_01 = new Setor {
                    Id_Prefeitura = _prefeituraDao.ObterIdPorNome("Prefeitura Municipal de Patos de Minas"),
                    Nome = "Obras Públicas".ToUpper(),
                    Responsavel = "Eduarda Analu Heloise Ramos",
                    Email = "teste_secobras@patosdeminas.mg.gov.br",
                    Site = "http://patosdeminas.mg.gov.br/home/prefeitura-de-patos-de-minas/secretaria-municipal-de-obras-publicas/",
                    Telefone = "(34)3822-9867",
                    Endereco = endereco,
                    Status = "ativo"
                };

                Setor set_02 = new Setor {
                    Id_Prefeitura = _prefeituraDao.ObterIdPorNome("Prefeitura Municipal de Patos de Minas"),
                    Nome = "Planejamento".ToUpper(),
                    Responsavel = "Letícia Caroline Carolina da Conceição",
                    Email = "teste_planejamento@patosdeminas.mg.gov.br",
                    Site = "http://patosdeminas.mg.gov.br/home/prefeitura-de-patos-de-minas/secretaria-municipal-de-planejamento/",
                    Telefone = "(34)3822-9725",
                    Endereco = endereco,
                    Status = "ativo"
                };

                Setor set_03 = new Setor {
                    Id_Prefeitura = _prefeituraDao.ObterIdPorNome("Prefeitura Municipal de Patos de Minas"),
                    Nome = "Saúde".ToUpper(),
                    Responsavel = "Isabelle Bianca Alves",
                    Email = "teste_saude@patosdeminas.mg.gov.br",
                    Site = "http://patosdeminas.mg.gov.br/home/prefeitura-de-patos-de-minas/secretaria-municipal-de-saude-sms/",
                    Telefone = "",
                    Endereco = endereco,
                    Status = "ativo"
                };

                Setor set_04 = new Setor {
                    Id_Prefeitura = _prefeituraDao.ObterIdPorNome("Prefeitura Municipal de Patos de Minas"),
                    Nome = "Trânsito e Transporte".ToUpper(),
                    Responsavel = "Breno Fernando Cavalcanti",
                    Email = "teste_settram@patosdeminas.mg.gov.br",
                    Site = "http://patosdeminas.mg.gov.br/home/prefeitura-de-patos-de-minas/secretaria-municipal-de-transito-transporte-e-mobilidade/",
                    Telefone = "(34)3822-9712",
                    Endereco = endereco,
                    Status = "ativo"
                };

                Setor set_05 = new Setor {
                    Id_Prefeitura = _prefeituraDao.ObterIdPorNome("Prefeitura Municipal de Patos de Minas"),
                    Nome = "Controladoria Geral".ToUpper(),
                    Responsavel = "Julio Henrique Almeida",
                    Email = "teste_controladoria@patosdeminas.mg.gov.br",
                    Site = "http://patosdeminas.mg.gov.br/home/prefeitura-de-patos-de-minas/controladoria-geral/",
                    Telefone = "(34)3822-9834",
                    Endereco = endereco,
                    Status = "ativo"
                };

                Setor set_06 = new Setor {
                    Id_Prefeitura = _prefeituraDao.ObterIdPorNome("Prefeitura Municipal de Patos de Minas"),
                    Nome = "Ouvidoria do Município".ToUpper(),
                    Responsavel = "Francisco Davi Cavalcanti",
                    Email = "teste_ouvidoriamunicipio@patosdeminas.mg.gov.br",
                    Site = "http://patosdeminas.mg.gov.br/home/prefeitura-de-patos-de-minas/ouvidoria-do-municipio/",
                    Telefone = "(34)3822-9115",
                    Endereco = endereco,
                    Status = "ativo"
                };

                setores.Add(set_01);
                setores.Add(set_02);
                setores.Add(set_03);
                setores.Add(set_04);
                setores.Add(set_05);
                setores.Add(set_06);

                _context.CollectionSetor.InsertMany(setores);
            }
            
            // Seeding de Categorias
            if (_context.CollectionCategoria.Find(categ => true).ToList().Count == 0)
            {
                List<Categoria> categorias = new List<Categoria>();

                Categoria categoria_a = new Categoria();
                categoria_a.Descricao = "Vigilância Sanitária".ToUpper();
                categoria_a.Id_Setor = _setorDao.ObterIdPorNome("Saúde".ToUpper());
                categoria_a.Status = "ativo";

                Categoria categoria_b = new Categoria();
                categoria_b.Descricao = "Pragas Urbanas".ToUpper();
                categoria_b.Id_Setor = _setorDao.ObterIdPorNome("Ouvidoria do Município".ToUpper());
                categoria_b.Status = "ativo";

                Categoria categoria_c = new Categoria();
                categoria_c.Descricao = "Dengue".ToUpper();
                categoria_c.Id_Setor = _setorDao.ObterIdPorNome("Saúde".ToUpper());
                categoria_c.Status = "ativo";

                Categoria categoria_d = new Categoria();
                categoria_d.Descricao = "Recolha de animais feridos em via pública".ToUpper();
                categoria_d.Id_Setor = _setorDao.ObterIdPorNome("Ouvidoria do Município".ToUpper());
                categoria_d.Status = "ativo";

                Categoria categoria_e = new Categoria();
                categoria_e.Descricao = "Patrulhamento da Cidade".ToUpper();
                categoria_e.Id_Setor = _setorDao.ObterIdPorNome("Ouvidoria do Município".ToUpper());
                categoria_e.Status = "ativo";

                Categoria categoria_f = new Categoria();
                categoria_f.Descricao = "Proteção de patrimônio público".ToUpper();
                categoria_f.Id_Setor = _setorDao.ObterIdPorNome("Ouvidoria do Município".ToUpper());
                categoria_f.Status = "ativo";

                Categoria categoria_g = new Categoria();
                categoria_g.Descricao = "Transporte Escolar".ToUpper();
                categoria_g.Id_Setor = _setorDao.ObterIdPorNome("Trânsito e Transporte".ToUpper());
                categoria_g.Status = "ativo";

                Categoria categoria_h = new Categoria();
                categoria_h.Descricao = "Área de Risco".ToUpper();
                categoria_h.Id_Setor = _setorDao.ObterIdPorNome("Ouvidoria do Município".ToUpper());
                categoria_h.Status = "ativo";

                Categoria categoria_i = new Categoria();
                categoria_i.Descricao = "Ocupação Irregular".ToUpper();
                categoria_i.Id_Setor = _setorDao.ObterIdPorNome("Planejamento".ToUpper());
                categoria_i.Status = "ativo";

                Categoria categoria_j = new Categoria();
                categoria_j.Descricao = "Buraco na Rua".ToUpper();
                categoria_j.Id_Setor = _setorDao.ObterIdPorNome("Obras Públicas".ToUpper());
                categoria_j.Status = "ativo";

                Categoria categoria_k = new Categoria();
                categoria_k.Descricao = "Infração no trânsito".ToUpper();
                categoria_k.Id_Setor = _setorDao.ObterIdPorNome("Trânsito e Transporte".ToUpper());
                categoria_k.Status = "ativo";

                Categoria categoria_l = new Categoria();
                categoria_l.Descricao = "Estacionado Irregularmente".ToUpper();
                categoria_l.Id_Setor = _setorDao.ObterIdPorNome("Trânsito e Transporte".ToUpper());
                categoria_l.Status = "ativo";

                Categoria categoria_m = new Categoria();
                categoria_m.Descricao = "Iluminação Pública".ToUpper();
                categoria_m.Id_Setor = _setorDao.ObterIdPorNome("Ouvidoria do Município".ToUpper());
                categoria_m.Status = "ativo";

                Categoria categoria_n = new Categoria();
                categoria_n.Descricao = "Manutenção de Praças".ToUpper();
                categoria_n.Id_Setor = _setorDao.ObterIdPorNome("Ouvidoria do Município".ToUpper());
                categoria_n.Status = "ativo";

                Categoria categoria_o = new Categoria();
                categoria_o.Descricao = "Placas de Sinalização".ToUpper();
                categoria_o.Id_Setor = _setorDao.ObterIdPorNome("Trânsito e Transporte".ToUpper());
                categoria_o.Status = "ativo";

                Categoria categoria_p = new Categoria();
                categoria_p.Descricao = "Sinal de Trânsito".ToUpper();
                categoria_p.Id_Setor = _setorDao.ObterIdPorNome("Trânsito e Transporte".ToUpper());
                categoria_p.Status = "ativo";

                Categoria categoria_q = new Categoria();
                categoria_q.Descricao = "Denúncia Descarte de Lixo".ToUpper();
                categoria_q.Id_Setor = _setorDao.ObterIdPorNome("Obras Públicas".ToUpper());
                categoria_q.Status = "ativo";

                Categoria categoria_r = new Categoria();
                categoria_r.Descricao = "Manutenção de Bueiro".ToUpper();
                categoria_r.Id_Setor = _setorDao.ObterIdPorNome("Obras Públicas".ToUpper());
                categoria_r.Status = "ativo";

                Categoria categoria_s = new Categoria();
                categoria_s.Descricao = "Limpeza urbana".ToUpper();
                categoria_s.Id_Setor = _setorDao.ObterIdPorNome("Obras Públicas".ToUpper());
                categoria_s.Status = "ativo";

                Categoria categoria_t = new Categoria();
                categoria_t.Descricao = "Poda de árvore".ToUpper();
                categoria_t.Id_Setor = _setorDao.ObterIdPorNome("Obras Públicas".ToUpper());
                categoria_t.Status = "ativo";

                Categoria categoria_u = new Categoria();
                categoria_u.Descricao = "Poluição Ambiental".ToUpper();
                categoria_u.Id_Setor = _setorDao.ObterIdPorNome("Ouvidoria do Município".ToUpper());
                categoria_u.Status = "ativo";

                Categoria categoria_v = new Categoria();
                categoria_v.Descricao = "Lote Sujo".ToUpper();
                categoria_v.Id_Setor = _setorDao.ObterIdPorNome("Obras Públicas".ToUpper());
                categoria_v.Status = "ativo";

                Categoria categoria_w = new Categoria();
                categoria_w.Descricao = "Acolhimento de População de Rua".ToUpper();
                categoria_w.Id_Setor = _setorDao.ObterIdPorNome("Ouvidoria do Município".ToUpper());
                categoria_w.Status = "ativo";

                Categoria categoria_x = new Categoria();
                categoria_x.Descricao = "Ouvidoria".ToUpper();
                categoria_x.Id_Setor = _setorDao.ObterIdPorNome("Ouvidoria do Município".ToUpper());
                categoria_x.Status = "ativo";

                Categoria categoria_y = new Categoria();
                categoria_y.Descricao = "Alvará".ToUpper();
                categoria_y.Id_Setor = _setorDao.ObterIdPorNome("Ouvidoria do Município".ToUpper());
                categoria_y.Status = "ativo";

                categorias.Add(categoria_a);
                categorias.Add(categoria_b);
                categorias.Add(categoria_c);
                categorias.Add(categoria_d);
                categorias.Add(categoria_e);
                categorias.Add(categoria_f);
                categorias.Add(categoria_g);
                categorias.Add(categoria_h);
                categorias.Add(categoria_i);
                categorias.Add(categoria_j);
                categorias.Add(categoria_k);
                categorias.Add(categoria_l);
                categorias.Add(categoria_m);
                categorias.Add(categoria_n);
                categorias.Add(categoria_o);
                categorias.Add(categoria_p);
                categorias.Add(categoria_q);
                categorias.Add(categoria_r);
                categorias.Add(categoria_s);
                categorias.Add(categoria_t);
                categorias.Add(categoria_u);
                categorias.Add(categoria_v);
                categorias.Add(categoria_w);
                categorias.Add(categoria_x);
                categorias.Add(categoria_y);

                _context.CollectionCategoria.InsertMany(categorias);
            }

            // Seeding de Tipos
            if (_context.CollectionTipo.Find(tipo => true).ToList().Count == 0)
            {
                List<Tipo> tipos = new List<Tipo>();

                Tipo tipo_01 = new Tipo();
                tipo_01.Id_Categoria = _categoriaDao.ObterPorDescricao("Pragas Urbanas");
                tipo_01.Descricao = "Roedores".ToUpper();
                tipo_01.Status = "ativo";

                Tipo tipo_02 = new Tipo();
                tipo_02.Id_Categoria = _categoriaDao.ObterPorDescricao("Pragas Urbanas");
                tipo_02.Descricao = "Pombas".ToUpper();
                tipo_02.Status = "ativo";

                Tipo tipo_03 = new Tipo();
                tipo_03.Id_Categoria = _categoriaDao.ObterPorDescricao("Dengue");
                tipo_03.Descricao = "Água parada".ToUpper();
                tipo_03.Status = "ativo";

                Tipo tipo_04 = new Tipo();
                tipo_04.Id_Categoria = _categoriaDao.ObterPorDescricao("Dengue");
                tipo_04.Descricao = "Larva de Mosquito".ToUpper();
                tipo_04.Status = "ativo";

                Tipo tipo_05 = new Tipo();
                tipo_05.Id_Categoria = _categoriaDao.ObterPorDescricao("Dengue");
                tipo_05.Descricao = "Mosquito Voando".ToUpper();
                tipo_05.Status = "ativo";

                Tipo tipo_06 = new Tipo();
                tipo_06.Id_Categoria = _categoriaDao.ObterPorDescricao("Recolha de animais feridos em via pública");
                tipo_06.Descricao = "Sujeira de Cachorro".ToUpper();
                tipo_06.Status = "ativo";

                Tipo tipo_07 = new Tipo();
                tipo_07.Id_Categoria = _categoriaDao.ObterPorDescricao("Recolha de animais feridos em via pública");
                tipo_07.Descricao = "Animal Solto".ToUpper();
                tipo_07.Status = "ativo";

                Tipo tipo_08 = new Tipo();
                tipo_08.Id_Categoria = _categoriaDao.ObterPorDescricao("Proteção de patrimônio público");
                tipo_08.Descricao = "Pixação ao patrimônio público".ToUpper();
                tipo_08.Status = "ativo";

                Tipo tipo_09 = new Tipo();
                tipo_09.Id_Categoria = _categoriaDao.ObterPorDescricao("Proteção de patrimônio público");
                tipo_09.Descricao = "Depredação ao patrimônio público".ToUpper();
                tipo_09.Status = "ativo";

                Tipo tipo_10 = new Tipo();
                tipo_10.Id_Categoria = _categoriaDao.ObterPorDescricao("Área de Risco");
                tipo_10.Descricao = "Área de Risco".ToUpper();
                tipo_10.Status = "ativo";

                Tipo tipo_11 = new Tipo();
                tipo_11.Id_Categoria = _categoriaDao.ObterPorDescricao("Ocupação Irregular");
                tipo_11.Descricao = "Ocupação Irregular".ToUpper();
                tipo_11.Status = "ativo";

                Tipo tipo_12 = new Tipo();
                tipo_12.Id_Categoria = _categoriaDao.ObterPorDescricao("Buraco na Rua");
                tipo_12.Descricao = "Buraco pequeno".ToUpper();
                tipo_12.Status = "ativo";

                Tipo tipo_13 = new Tipo();
                tipo_13.Id_Categoria = _categoriaDao.ObterPorDescricao("Buraco na Rua");
                tipo_13.Descricao = "Buraco médio".ToUpper();
                tipo_13.Status = "ativo";

                Tipo tipo_14 = new Tipo();
                tipo_14.Id_Categoria = _categoriaDao.ObterPorDescricao("Buraco na Rua");
                tipo_14.Descricao = "Buraco grande".ToUpper();
                tipo_14.Status = "ativo";

                Tipo tipo_15 = new Tipo();
                tipo_15.Id_Categoria = _categoriaDao.ObterPorDescricao("Infração no trânsito");
                tipo_15.Descricao = "Infração no trânsito".ToUpper();
                tipo_15.Status = "ativo";

                Tipo tipo_16 = new Tipo();
                tipo_16.Id_Categoria = _categoriaDao.ObterPorDescricao("Estacionado Irregularmente");
                tipo_16.Descricao = "Estacionado irregular".ToUpper();
                tipo_16.Status = "ativo";

                Tipo tipo_17 = new Tipo();
                tipo_17.Id_Categoria = _categoriaDao.ObterPorDescricao("Iluminação Pública");
                tipo_17.Descricao = "Lâmpada Queimada".ToUpper();
                tipo_17.Status = "ativo";

                Tipo tipo_18 = new Tipo();
                tipo_18.Id_Categoria = _categoriaDao.ObterPorDescricao("Iluminação Pública");
                tipo_18.Descricao = "Lâmpada Piscando".ToUpper();
                tipo_18.Status = "ativo";

                Tipo tipo_19 = new Tipo();
                tipo_19.Id_Categoria = _categoriaDao.ObterPorDescricao("Iluminação Pública");
                tipo_19.Descricao = "Lâmpada Acesa".ToUpper();
                tipo_19.Status = "ativo";

                Tipo tipo_20 = new Tipo();
                tipo_20.Id_Categoria = _categoriaDao.ObterPorDescricao("Iluminação Pública");
                tipo_20.Descricao = "Árvore Caída na rede elétrica".ToUpper();
                tipo_20.Status = "ativo";

                Tipo tipo_21 = new Tipo();
                tipo_21.Id_Categoria = _categoriaDao.ObterPorDescricao("Iluminação Pública");
                tipo_21.Descricao = "Outros".ToUpper();
                tipo_21.Status = "ativo";

                Tipo tipo_22 = new Tipo();
                tipo_22.Id_Categoria = _categoriaDao.ObterPorDescricao("Manutenção de Praças");
                tipo_22.Descricao = "Manutenção de Praças".ToUpper();
                tipo_22.Status = "ativo";

                Tipo tipo_23 = new Tipo();
                tipo_23.Id_Categoria = _categoriaDao.ObterPorDescricao("Placas de Sinalização");
                tipo_23.Descricao = "Reposição de Placa".ToUpper();
                tipo_23.Status = "ativo";

                Tipo tipo_24 = new Tipo();
                tipo_24.Id_Categoria = _categoriaDao.ObterPorDescricao("Placas de Sinalização");
                tipo_24.Descricao = "Quebra Molas".ToUpper();
                tipo_24.Status = "ativo";

                Tipo tipo_25 = new Tipo();
                tipo_25.Id_Categoria = _categoriaDao.ObterPorDescricao("Placas de Sinalização");
                tipo_25.Descricao = "Ponto de Ónibus".ToUpper();
                tipo_25.Status = "ativo";

                Tipo tipo_26 = new Tipo();
                tipo_26.Id_Categoria = _categoriaDao.ObterPorDescricao("Placas de Sinalização");
                tipo_26.Descricao = "Outros".ToUpper();
                tipo_26.Status = "ativo";

                Tipo tipo_27 = new Tipo();
                tipo_27.Id_Categoria = _categoriaDao.ObterPorDescricao("Sinal de Trânsito");
                tipo_27.Descricao = "Sinal de Trânsito".ToUpper();
                tipo_27.Status = "ativo";

                Tipo tipo_28 = new Tipo();
                tipo_28.Id_Categoria = _categoriaDao.ObterPorDescricao("Denúncia Descarte de Lixo");
                tipo_28.Descricao = "Denúncia Lixo".ToUpper();
                tipo_28.Status = "ativo";

                Tipo tipo_29 = new Tipo();
                tipo_29.Id_Categoria = _categoriaDao.ObterPorDescricao("Manutenção de Bueiro");
                tipo_29.Descricao = "Manutenção de Bueiro".ToUpper();
                tipo_29.Status = "ativo";

                Tipo tipo_30 = new Tipo();
                tipo_30.Id_Categoria = _categoriaDao.ObterPorDescricao("Limpeza urbana");
                tipo_30.Descricao = "Varrição".ToUpper();
                tipo_30.Status = "ativo";

                Tipo tipo_31 = new Tipo();
                tipo_31.Id_Categoria = _categoriaDao.ObterPorDescricao("Limpeza urbana");
                tipo_31.Descricao = "Coleta de Lixo".ToUpper();
                tipo_31.Status = "ativo";

                Tipo tipo_32 = new Tipo();
                tipo_32.Id_Categoria = _categoriaDao.ObterPorDescricao("Limpeza urbana");
                tipo_32.Descricao = "Capina".ToUpper();
                tipo_32.Status = "ativo";

                Tipo tipo_33 = new Tipo();
                tipo_33.Id_Categoria = _categoriaDao.ObterPorDescricao("Limpeza urbana");
                tipo_33.Descricao = "Outros".ToUpper();
                tipo_33.Status = "ativo";

                Tipo tipo_34 = new Tipo();
                tipo_34.Id_Categoria = _categoriaDao.ObterPorDescricao("Poda de árvore");
                tipo_34.Descricao = "Remoção de galho".ToUpper();
                tipo_34.Status = "ativo";

                Tipo tipo_35 = new Tipo();
                tipo_35.Id_Categoria = _categoriaDao.ObterPorDescricao("Poda de árvore");
                tipo_35.Descricao = "Poda de árvore".ToUpper();
                tipo_35.Status = "ativo";

                Tipo tipo_36 = new Tipo();
                tipo_36.Id_Categoria = _categoriaDao.ObterPorDescricao("Poda de árvore");
                tipo_36.Descricao = "Árvore caída".ToUpper();
                tipo_36.Status = "ativo";

                Tipo tipo_37 = new Tipo();
                tipo_37.Id_Categoria = _categoriaDao.ObterPorDescricao("Poda de árvore");
                tipo_37.Descricao = "Remoção de Árvore".ToUpper();
                tipo_37.Status = "ativo";

                Tipo tipo_38 = new Tipo();
                tipo_38.Id_Categoria = _categoriaDao.ObterPorDescricao("Poda de árvore");
                tipo_38.Descricao = "Árvore na pista".ToUpper();
                tipo_38.Status = "ativo";

                Tipo tipo_39 = new Tipo();
                tipo_39.Id_Categoria = _categoriaDao.ObterPorDescricao("Poda de árvore");
                tipo_39.Descricao = "Outros".ToUpper();
                tipo_39.Status = "ativo";

                Tipo tipo_40 = new Tipo();
                tipo_40.Id_Categoria = _categoriaDao.ObterPorDescricao("Poluição Ambiental");
                tipo_40.Descricao = "Poluição Ambiental".ToUpper();
                tipo_40.Status = "ativo";

                Tipo tipo_41 = new Tipo();
                tipo_41.Id_Categoria = _categoriaDao.ObterPorDescricao("Lote Sujo");
                tipo_41.Descricao = "Mato alto".ToUpper();
                tipo_41.Status = "ativo";

                Tipo tipo_42 = new Tipo();
                tipo_42.Id_Categoria = _categoriaDao.ObterPorDescricao("Lote Sujo");
                tipo_42.Descricao = "Lixo no lote".ToUpper();
                tipo_42.Status = "ativo";

                Tipo tipo_43 = new Tipo();
                tipo_43.Id_Categoria = _categoriaDao.ObterPorDescricao("Lote Sujo");
                tipo_43.Descricao = "Recolher entulhos".ToUpper();
                tipo_43.Status = "ativo";

                Tipo tipo_44 = new Tipo();
                tipo_44.Id_Categoria = _categoriaDao.ObterPorDescricao("Lote Sujo");
                tipo_44.Descricao = "Outros".ToUpper();
                tipo_44.Status = "ativo";

                Tipo tipo_45 = new Tipo();
                tipo_45.Id_Categoria = _categoriaDao.ObterPorDescricao("Ouvidoria");
                tipo_45.Descricao = "Informação".ToUpper();
                tipo_45.Status = "ativo";

                Tipo tipo_46 = new Tipo();
                tipo_46.Id_Categoria = _categoriaDao.ObterPorDescricao("Ouvidoria");
                tipo_46.Descricao = "Sugestão".ToUpper();
                tipo_46.Status = "ativo";

                Tipo tipo_47 = new Tipo();
                tipo_47.Id_Categoria = _categoriaDao.ObterPorDescricao("Ouvidoria");
                tipo_47.Descricao = "Reclamação".ToUpper();
                tipo_47.Status = "ativo";

                Tipo tipo_48 = new Tipo();
                tipo_48.Id_Categoria = _categoriaDao.ObterPorDescricao("Ouvidoria");
                tipo_48.Descricao = "Elogio".ToUpper();
                tipo_48.Status = "ativo";

                Tipo tipo_49 = new Tipo();
                tipo_49.Id_Categoria = _categoriaDao.ObterPorDescricao("Ouvidoria");
                tipo_49.Descricao = "Denúncia".ToUpper();
                tipo_49.Status = "ativo";

                Tipo tipo_50 = new Tipo();
                tipo_50.Id_Categoria = _categoriaDao.ObterPorDescricao("Ouvidoria");
                tipo_50.Descricao = "Solicitação".ToUpper();
                tipo_50.Status = "ativo";

                Tipo tipo_51 = new Tipo();
                tipo_51.Id_Categoria = _categoriaDao.ObterPorDescricao("Alvará");
                tipo_51.Descricao = "Alvará de Construção".ToUpper();
                tipo_51.Status = "ativo";

                Tipo tipo_52 = new Tipo();
                tipo_52.Id_Categoria = _categoriaDao.ObterPorDescricao("Alvará");
                tipo_52.Descricao = "Alvará de Funcionamento".ToUpper();
                tipo_52.Status = "ativo";

                tipos.Add(tipo_01);
                tipos.Add(tipo_02);
                tipos.Add(tipo_03);
                tipos.Add(tipo_04);
                tipos.Add(tipo_05);
                tipos.Add(tipo_06);
                tipos.Add(tipo_07);
                tipos.Add(tipo_08);
                tipos.Add(tipo_09);
                tipos.Add(tipo_10);
                tipos.Add(tipo_11);
                tipos.Add(tipo_12);
                tipos.Add(tipo_13);
                tipos.Add(tipo_14);
                tipos.Add(tipo_15);
                tipos.Add(tipo_16);
                tipos.Add(tipo_17);
                tipos.Add(tipo_18);
                tipos.Add(tipo_19);
                tipos.Add(tipo_20);
                tipos.Add(tipo_21);
                tipos.Add(tipo_22);
                tipos.Add(tipo_23);
                tipos.Add(tipo_24);
                tipos.Add(tipo_25);
                tipos.Add(tipo_26);
                tipos.Add(tipo_27);
                tipos.Add(tipo_28);
                tipos.Add(tipo_29);
                tipos.Add(tipo_30);
                tipos.Add(tipo_31);
                tipos.Add(tipo_32);
                tipos.Add(tipo_33);
                tipos.Add(tipo_34);
                tipos.Add(tipo_35);
                tipos.Add(tipo_36);
                tipos.Add(tipo_37);
                tipos.Add(tipo_38);
                tipos.Add(tipo_39);
                tipos.Add(tipo_40);
                tipos.Add(tipo_41);
                tipos.Add(tipo_42);
                tipos.Add(tipo_43);
                tipos.Add(tipo_44);
                tipos.Add(tipo_45);
                tipos.Add(tipo_46);
                tipos.Add(tipo_47);
                tipos.Add(tipo_48);
                tipos.Add(tipo_49);
                tipos.Add(tipo_50);
                tipos.Add(tipo_51);
                tipos.Add(tipo_52);

                _context.CollectionTipo.InsertMany(tipos);
            }

            // Seeding de Usuário Administrador (Para testes)
            if (_context.CollectionUsuarioRestrito.Find(user => true).ToList().Count == 0) {
                UsuarioRestrito user = new UsuarioRestrito();
                user.Nome = "Administrador do Community Report";
                user.Email = "communityreportsi@gmail.com";
                user.Login = "adm";
                user.Senha = _usuarioRestritoDao.CriptografarSenhaSHA256("123456");
                user.Id_Prefeitura = _prefeituraDao.ObterIdPorNome("Prefeitura Municipal de Patos de Minas");
                user.Id_Setor = "";
                user.Perfil = true;

                _context.CollectionUsuarioRestrito.InsertOne(user);
            }

            // Seeding de Usuários Públicos (Para testes)
            if (_context.CollectionUsuarioPublico.Find(user => true).ToList().Count == 0) {
                
                EnderecoDto endereco = new EnderecoDto();
                
                UsuarioPublico user_1 = new UsuarioPublico {
                    Nome = "Caio Vicente Rodrigues",
                    Email = "caiorodrigues1@unipam.edu.br",
                    Cpf = "196.731.638-47",
                    Endereco = endereco,
                    Prefeitura = _prefeituraDao.ObterIdPorNome("Prefeitura Municipal de Patos de Minas"),
                    Login = "caio",
                    Senha = _usuarioRestritoDao.CriptografarSenhaSHA256("123456")
                };

                _context.CollectionUsuarioPublico.InsertOne(user_1);

                UsuarioPublico user_2 = new UsuarioPublico {
                    Nome = "Rhuan Thales de Souza Trajano",
                    Email = "rhuantst@unipam.edu.br",
                    Cpf = "027.082.133-34",
                    Endereco = endereco,
                    Prefeitura = _prefeituraDao.ObterIdPorNome("Prefeitura Municipal de Patos de Minas"),
                    Login = "rhuan",
                    Senha = _usuarioRestritoDao.CriptografarSenhaSHA256("123456")
                };

                _context.CollectionUsuarioPublico.InsertOne(user_2);
            }
           
            return; // DB has been seeded
        }
    }
}
