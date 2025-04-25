using System;
using System.Collections.Generic;
using System.Linq; 

using System.Web.Mvc;
using Examen2.Data;
using Examen2.Models;

namespace Examen2.Controllers
{
    public class VuelosController : Controller
    {

        private VuelosEntitiesConnection db = new VuelosEntitiesConnection();

        // GET: Vuelos
        public ActionResult Index()
        {
            return RedirectToAction("ListaVuelos");
        }

        // GET: Ciudades
        public ActionResult Ciudades()
        {
            return View();
        }

        // GET: Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }

        // Debe re-direccionar a una vista llamada “ListaVuelos”. Dicha vista será creada en el punto 6
        // GET: Vuelos
        public ActionResult ListaVuelos()
        {
            return View();
        }

        public ActionResult CrearVuelo()
        {
            return View();
        }

        

        /**
         * Debe retornar en un objeto JSON todos los registros haciendo uso del
         * procedimiento almacenado llamado “sp_Vuelo_Retorna”, el método debe
         * recibir todos los parámetros necesarios para ejecutar el procedimiento antes
         * mencionado. (0.5pts)
         */
        //GET: Vuelos/RetornaVuelos 
        public JsonResult RetornaVuelos()
        {
            List<sp_Vuelo_Retorna_Result > result_VueloRetorna = db.sp_Vuelo_Retorna().ToList(); 
            return Json(result_VueloRetorna, JsonRequestBehavior.AllowGet);
        }

        /**
         * Debe retornar en un objeto JSON el resultado del procedimiento almacenado
         * llamado “sp_Ciudad_Origen_Retorna”, el método debe recibir todos los
         * parámetros necesarios para ejecutar el procedimiento antes mencionado.
         * (0.5pts)
         */
        //GET: Vuelos/RetornaCiudadOrigen 
        public JsonResult RetornaCiudadOrigen()
        {
            List<sp_Ciudad_Origen_Retorna_Result> result_CiudadOrigenRetorna = db.sp_Ciudad_Origen_Retorna().ToList();
            return Json(result_CiudadOrigenRetorna, JsonRequestBehavior.AllowGet);
        }

        /**
         * Debe recibir como parámetro un número que permita invocar el
         * procedimiento almacenado “sp_Ciudad_Destino_Retorna” (0.5pts)
         * Debe retornar en un objeto JSON el resultado del procedimiento almacenado
         * llamado “sp_Ciudad_Destino_Retorna” utilizando los parámetros recibidos
         * en el método. (0.5pts)
         */
        //GET: Vuelos/RetornaCiudadDestino 
        public JsonResult RetornaCiudadDestino(int idCiudadOrigen)
        {
            List<sp_Ciudad_Destino_Retorna_Result> result_CiudadDestinoRetorna = db.sp_Ciudad_Destino_Retorna(idCiudadOrigen).ToList();
            return Json(result_CiudadDestinoRetorna, JsonRequestBehavior.AllowGet);
        }


        /**
         * Debe recibir como parámetros dos números que permitan invocar el
         * procedimiento almacenado “sp_Piloto_Origen_Destino_Retorna” (0.5pts)
         * Debe retornar en un objeto JSON el resultado del procedimiento almacenado
         * llamado “sp_Ciudad_Destino_Retorna” utilizando los parámetros recibidos
         * en el método. (0.5pts)
         */
        //GET: Vuelos/RetornaPilotoPorCiudadDestino 
        public JsonResult RetornaPilotoPorCiudadDestino(int idCiudadOrigin, int idCiudadDestino)
        {
            List<sp_Piloto_Origen_Destino_Retorna_Result> result_PilotoOrigenDestinoRetorna = db.sp_Piloto_Origen_Destino_Retorna(idCiudadOrigin,idCiudadDestino).ToList();
            return Json(result_PilotoOrigenDestinoRetorna, JsonRequestBehavior.AllowGet);
        }

        /**
         * Debe recibir como parámetro un número que permita invocar el
         * procedimiento almacenado “sp_Piloto_Retorna_ID” (0.5pts)
         * Debe retornar en un objeto JSON el resultado del procedimiento almacenado
         * llamado “sp_Piloto_Retorna_ID”, el método debe recibir todos los
         * parámetros necesarios para ejecutar el procedimiento antes mencionado.
         * (0.5pts)
         */
        //GET: Vuelos/RetornaPilotoID
        public JsonResult RetornaPilotoID(int idPiloto)
        {
            List<sp_Piloto_Retorna_ID_Result> result_PilotoRetornaID = db.sp_Piloto_Retorna_ID(idPiloto).ToList();
            return Json(result_PilotoRetornaID, JsonRequestBehavior.AllowGet);
        }

        //GET: Vuelos/RetornaCiudades
        public JsonResult RetornaCiudades()
        {
            List<CiudadView> result_Ciudades = db.tbl_Ciudad.Select(c => new Models.CiudadView
            {
                IdCiudad = c.id_Ciudad,
                Nombre = c.Ciudad,
                Descripcion = c.Descripcion
            }).ToList();
            ;
            return Json(result_Ciudades, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DashboardObtenerHorasVuelo()
        {
            var horasPorPiloto = (from p in db.VW_PILOTO_ORIGEN_DESTINO
                                  group p by p.nombreCompleto into grupo
                                  select new
                                  {
                                      nombre = grupo.Key,
                                      totalHoras = grupo.Sum(p => p.cantidadHoras)
                                  }).ToList();

            return Json(horasPorPiloto, JsonRequestBehavior.AllowGet);
        }

        /**
         * Debe utilizar el verbo httppost y recibir como parámetro un modelo que
         * permita ejecutar el procedimiento almacenado: sp_Vuelo_Inserta (0.5pts).
         * Debe calcular las siguientes propiedades:
         *  1. montoSalarioPiloto: debe evaluar el valor de la propiedad
            cantidadHoras del modelo enviado como parámetro de la siguiente
            manera:
         * 
         *  a.Debe obtener el salario por hora de cada piloto, por lo que,
         *      debe invocar el procedimiento almacenado
         *      sp_Piloto_Retorna_ID enviando como parámetro el valor
         *      id_Piloto del modelo recibido como parámetro en el método.
         *      (0.5pts)
         *  b. Si es menor o igual a 6 cada hora se paga normal del monto
         *      calculado en el punto “a”. (2pts)
         *  c. Si es mayor a 6 pero no superior a 12, cada hora entre la 1 y las
         *      6 son horas normales, cada hora adicional después de las 6
         *      horas se paga como hora y media del monto calculado en el
         *      punto “a” (4pts)
         *  d. Si es mayor a 12, cada hora entre la 1 y las 6 son horas
         *      normales, pero cada hora adicional después de las 6 horas se
         *      paga como hora y media y cada hora posterior a las 12 se paga
         *      como doble
         *  e. Debe validar que el piloto que se esté incluyendo no tenga
         *      registrado en la base de datos otro vuelo para la misma fecha.
         *      Esta validación deberá ser hecha mediante el uso de LINQMethod
         *      sintax. (2pts)
         */
        [HttpPost]
        public JsonResult InsertaVuelos(tbl_Vuelo dataVuelo)
        {
            try
            {
                int idVuelo = db.sp_Vuelo_Inserta(dataVuelo.id_CiudadOrigen, dataVuelo.id_CiudadDestino, dataVuelo.id_Piloto, dataVuelo.cantidadHoras, dataVuelo.montoSalarioPiloto, dataVuelo.fecha);
                
                return Json(new { msgError = "" });
            }
            catch (Exception ex)
            {
                return Json(new { msgError = ex.Message });
            }

        }


        [HttpPost]
        public JsonResult CiudadAgregar(Models.CiudadView dataCiudad)
        {
            try
            {
                tbl_Ciudad dataCiudadAdd = new tbl_Ciudad();
                dataCiudadAdd.Ciudad = dataCiudad.Nombre;
                dataCiudadAdd.Descripcion = dataCiudad.Descripcion;
                db.tbl_Ciudad.Add(dataCiudadAdd);
                db.SaveChanges();
                return Json(new { msgError = "" });
            }
            catch (Exception ex)
            {
                return Json(new { msgError = ex.Message });
            }

        }
        
    }
}