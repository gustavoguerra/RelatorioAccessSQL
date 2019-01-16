using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projetos_CGTI.Models;
using System.Threading.Tasks;

namespace Projetos_CGTI.Controllers
{
    public class ChequeController : Controller
    {
        // GET: Cheque
        public ActionResult ChequeView()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult ChequeView(ChequeModel cheque)
        {
            try
            {
                ChequeDAO Insert = new ChequeDAO();
                Insert.Salvar(cheque);
                return View();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", $"{ex}");
                return View();
            }
        }

        public PartialViewResult ChequePartial()
        {
            List<Models.ChequeModel> lista = new List<Models.ChequeModel>();

            ChequeDAO valores = new ChequeDAO();

            return PartialView("_ChequeView", valores.Lista_Cheque(lista));            
        }

    }
}