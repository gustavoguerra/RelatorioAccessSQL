using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projetos_CGTI.Models;
using System.Threading.Tasks;

namespace Projetos_CGTI.Controllers
{
    public class ReembolsoController : Controller
    {
        // GET: Reembolso\Reembolso
        [HttpGet]
        public ActionResult ReembolsoView()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Reembolso\ReembolsoPartial
        public PartialViewResult ReembolsoPartial()
        {
            //    List<Models.ChequeModel> lista = new List<Models.ChequeModel>();

            //  ChequeDAO valores = new ChequeDAO();

            //return PartialView("_ChequeView", valores.Lista_Cheque(lista));
            return PartialView("_ReembolsoView", null);
        }
        
        [HttpPost]
        public ActionResult ReembolsoView(SalvarDespesaVewModel model)
        {
            
            ViewBag.Id_Usuario = model.Id_Usuario;
            return View();
            // return RedirectToAction("ReembolsoView", model);
        }

        // GET: Reembolso/Create
        public ActionResult CriarUsuarioView()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Reembolso/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reembolso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reembolso/Create Usuario
        [HttpPost]
        public ActionResult Create(SalvarUsuarioViewModel model)
        {
            try
            {
                ReembolsoDAO salvar = new ReembolsoDAO();

                salvar.SalvarUsuario(model);

                return RedirectToAction("ReembolsoView");
            }
            catch(Exception)
            {
                return View();
            }
        }

        // GET: Reembolso/Edit/5
        public ActionResult Edit(string id)
        {
            EditarUsuarioViewModel model = new EditarUsuarioViewModel();

            model.Nome = "Gustavo";

            return RedirectToAction("CriarUsuarioView", "Reembolso", model);
            //return View("CriarUsuarioView","Reembolso", id);
        }

        // POST: Reembolso/Edit/5
        //  [HttpPost]
        public ActionResult Edite(string id, FormCollection collection)
        {
            try
            {
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reembolso/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reembolso/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
       
        public PartialViewResult UsuarioPartial()
        {
            List<Models.EditarUsuarioViewModel> lista = new List<Models.EditarUsuarioViewModel>();

            ReembolsoDAO valores = new ReembolsoDAO();

            return PartialView("_CriarUsuarioView", valores.ListaUsuario(lista));
        }


    }
}
