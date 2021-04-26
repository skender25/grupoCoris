using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demosaba.Controllers
{
    public class cerrarController : Controller
    {
        // GET: cerrar
        public ActionResult LogOff()
        {
            Estado.estado_session = false;
            return RedirectToAction("Index", "login"); ;
        }
    }
}