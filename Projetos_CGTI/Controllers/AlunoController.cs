using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projetos_CGTI.Models;
using Projetos_CGTI.DAO;
using System.Data;

namespace Projetos_CGTI.Controllers
{  
    public class AlunoController : Controller
    {
        private void loadDropdown(AlunoViewModel model)
        {
            model.SexoList = new List<SelectListItem>
            {
                new SelectListItem{ Text = "Masculino" , Value = "Masculino" },
                new SelectListItem{ Text = "Feminino" , Value = "Feminino" }
            };

            model.EscolaList = new List<SelectListItem>
            {
                new SelectListItem{ Text = "Publica" , Value = "Publica" },
                new SelectListItem{ Text = "Particular" , Value = "Particular" }
            };

            model.CamisetaLista = new List<SelectListItem>
            {
                new SelectListItem{ Text = "Pequena" , Value = "Pequena" },
                new SelectListItem{ Text = "Media" , Value = "Media" },
                new SelectListItem{ Text = "Grande" , Value = "Grande" }
            };

            model.ShortLista = new List<SelectListItem>
            {
                new SelectListItem{ Text = "Pequena" , Value = "Pequena" },
                new SelectListItem{ Text = "Media" , Value = "Media" },
                new SelectListItem{ Text = "Grande" , Value = "Grande" }
            };

            model.PeriodoList = new List<SelectListItem>
            {
                new SelectListItem{ Text = "Matutino" , Value = "Matutino" },
                new SelectListItem{ Text = "Vespertino" , Value = "Vespertino" },
                new SelectListItem{ Text = "Noturno" , Value = "Noturno" }
            };

            model.SeieList = new List<SelectListItem>
            {
                new SelectListItem{ Text = "Quinta" , Value = "Quinta" },
                new SelectListItem{ Text = "Sexta" , Value = "Sexta" },
                new SelectListItem{ Text = "Setima" , Value = "Setima" },
                new SelectListItem{ Text = "Oitava" , Value = "Oitava" },
                new SelectListItem{ Text = "Nona" , Value = "Nona" },
                new SelectListItem{ Text = "Primeiro" , Value = "Primeiro" },
                new SelectListItem{ Text = "Segundo" , Value = "Segundo" },
                new SelectListItem{ Text = "Terceiro" , Value = "Terceiro" }
            };
            model.TenisList = new List<SelectListItem>
            {
                new SelectListItem{ Text = "34" , Value = "34" },
                new SelectListItem{ Text = "35" , Value = "35" },
                new SelectListItem{ Text = "36" , Value = "36" },
                new SelectListItem{ Text = "37" , Value = "37" },
                new SelectListItem{ Text = "38" , Value = "38" },
                new SelectListItem{ Text = "39" , Value = "39" },
                new SelectListItem{ Text = "40" , Value = "40" },
                new SelectListItem{ Text = "41" , Value = "41" },
                new SelectListItem{ Text = "42" , Value = "42" },
                new SelectListItem{ Text = "43" , Value = "43" },
                new SelectListItem{ Text = "44" , Value = "44" }
            };

            model.ParentescoList = new List<SelectListItem>
            {
                new SelectListItem{ Text = "Pais" , Value = "Pais" },
                new SelectListItem{ Text = "Avós" , Value = "Avós" },
                new SelectListItem{ Text = "Irmãos" , Value = "Irmãos" },
                new SelectListItem{ Text = "Outros" , Value = "Outros" }
            };          
        }


        private void loadProjeto(AlunoViewModel model)
        {
            DataTable Projeto = new DataTable();
            AlunoDAO aluno = new AlunoDAO();

            Projeto = aluno.Lista_Projeto();

            var listaProjeto = new List<Projeto>();

            foreach (DataRow dr in Projeto.Rows)
            {
                listaProjeto.Add(new Projeto
                {
                    ID = Convert.ToInt32(dr["ID"].ToString()),
                    Nome = dr["Nome"].ToString()
                });
            }
            model.listProjeto = listaProjeto;
        }

        // GET: Aluno
        public ActionResult AlunoView()
        {
            List<AlunoViewModel> model = new List<AlunoViewModel>();

            AlunoDAO lista = new AlunoDAO();


            return View(lista.Listar_Aluno());
        }

        // GET: Aluno/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Aluno/Create
        public ActionResult AlunoEditView(int ID)
        {
            AlunoDAO aluno = new AlunoDAO();
            
            return View("AlunoCreateView", aluno.Localizar_Aluno(ID));
        }

        public ActionResult AlunoCreateView()
        {
            AlunoViewModel lista = new AlunoViewModel();

            loadProjeto(lista);
            loadDropdown(lista);

            return View(lista);
        }
        // POST: Aluno/Create
        [HttpPost]
        public ActionResult AlunoCreateView(AlunoViewModel model, string Opcao)
        {
            try
            {
                /*  if (Opcao == null)
                  {
                      EnderecoDAO CEP = new EnderecoDAO();
                      ConsultaCEPModel ReturnEndereco = CEP.ConsultaCEP(model.CEP);

                      model.Endereco = ReturnEndereco.Endereco;
                      model.Bairro = ReturnEndereco.Bairro;
                      model.Cidade = ReturnEndereco.Cidade;
                      model.Estado = ReturnEndereco.Estado;
                  }
                  else
                  { */
                AlunoDAO Salvar = new AlunoDAO();

             //   Salvar.Salvar_Aluno(model);

                //   }
                ViewBag.Menssagem = "Aluno Salvo com Sucesso !!";
                return RedirectToAction("AlunoCreateView");
            }
            catch (Exception ex)
            {
                ViewBag.Menssagem = "Erro ao salvar aluno: "+ ex.ToString();
                return View();
            }
        }

        // GET: Aluno/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Aluno/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Aluno/Delete/5
        public ActionResult DeleteAluno(int id)
        {
            try
            {
                AlunoDAO aluno = new AlunoDAO();

                aluno.Apagar_Aluno(id);

                ViewBag.Menssagem = "Aluno Excluido com sucesso !!";
                return RedirectToAction("AlunoView");
            }
            catch(Exception ex)
            {
                //menssagem de erro
                ViewBag.Menssagem = ("Erro: "+ex.ToString());
                return RedirectToAction("AlunoView");
            }
        }

        // POST: Aluno/Delete/5
       //[HttpPost]
        public ActionResult Delete(AlunoViewModel model)
        {
            try
            {

                return RedirectToAction("AlunoView");
            }
            catch
            {
                //menssagem de erro
                return RedirectToAction("AlunoView");
            }
        }
    }
}
