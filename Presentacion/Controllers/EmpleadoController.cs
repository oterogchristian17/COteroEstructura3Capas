using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult EmpleadoGetAll(Negocio.Empleado empleado)
        {
            if (empleado.Nombre == null)
            {
                empleado.Nombre = "";
            }
            Dictionary<string, object> resultado = Negocio.Empleado.GetAll(empleado);
            bool correct = (bool)resultado["Resultado"];
            if (correct)
            {
                empleado = (Negocio.Empleado)resultado["Empleado"];
                return View(empleado);
            }

            return View();
    
        }


        [HttpGet]
        public ActionResult Delete(int EmpleadoID)
        {
            Dictionary<string, object> result = Negocio.Empleado.Delete(EmpleadoID);
            bool resultado = (bool)result["Resultado"];

            if (resultado == true)
            {

                ViewBag.Mensaje = "El Empleado ha sido eliminado";
                return PartialView("Modal");

            }
            else
            {
                //pendiente una alerta para la excepcion
                string excepcion = (string)result["Excepcion"];
                ViewBag.Mensaje = "El Empleado no se ha podido eliminar";
                return PartialView("Modal");

            }
        }
    }
}