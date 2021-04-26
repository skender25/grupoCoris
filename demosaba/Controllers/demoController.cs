using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using demosaba.Models;
using PagedList.Mvc;
using PagedList;
using System.Globalization;
using System.Threading;



namespace demosaba.Controllers
{
    public class demoController : Controller
    {
        // GET: demo
        public ActionResult Index(int? i, DateTime? start, DateTime? end, string sortOrder, DateTime? currentFilter, DateTime? currentFilter2)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("uk");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("uk");
            ViewBag.CurrentSort = sortOrder;
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery;
            if (start == null)
            {

                start = Estado.fecha_i;
                end = Estado.fecha_f;
                sqlquery = "set dateformat dmy select top 200  * from [erpadmin].[P_APP_HACIENDA_DE] where FECHA between '" + start + "'and'" + end + "'";


            }
            else
            {

                sqlquery = "set dateformat dmy select  * from [erpadmin].[P_APP_HACIENDA_DE] where FECHA between '" + start + "'and'" + end + "'";
                ViewBag.CurrentFilter = start;
                ViewBag.CurrentFilter2 = end;
                Estado.fecha_i = Convert.ToDateTime(start);
                Estado.fecha_f = Convert.ToDateTime(end);
            }


            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            List<MVClass> lc = new List<MVClass>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                var con_err = Convert.ToString(dr["CONTIENE_ERRORES"]);
                var con_errws = Convert.ToString(dr["ERROR_WS"]);
                var con_soft = Convert.ToString(dr["ERROR_SOFTLAND"]);
                var acept = Convert.ToString(dr["ACEPTADO"]);
                var envia = Convert.ToString(dr["ENVIADO"]);
                var error = true;
                var error2 = true;
                var error3 = true;
                var var_env = false;
                var var_acep = false;
                if (con_err == "N")
                {
                    error = false;
                }
                if (con_errws == "N")
                {
                    error2 = false;
                }
                if (con_soft == "N")
                {
                    error3 = false;
                }
                if (envia == "S")
                {
                    var_env = true;
                }
                if (acept == "A")
                {
                    var_acep = true;
                }
                lc.Add(new MVClass
                {
                    DOCUMENTO = Convert.ToString(dr["DOCUMENTO"]),
                    CLIENTE = Convert.ToString(dr["CLIENTE"]),
                    FECHA = Convert.ToDateTime(dr["FECHA"]),
                    NOMBRE = Convert.ToString(dr["NOMBRE"]),
                    NIT_RECEPTOR = Convert.ToString(dr["NIT_RECEPTOR"]),
                    CODIGO_MONEDA = Convert.ToString(dr["CODIGO_MONEDA"]),
                    CLAVE = Convert.ToString(dr["CLAVE"]),
                    TOTALGRAVADO = Convert.ToString(dr["TOTALGRAVADO"]),
                    TOTALEXENTO = Convert.ToString(dr["TOTALEXENTO"]),
                    TOTALDESCUENTOS = Convert.ToString(dr["TOTALDESCUENTOS"]),
                    TOTALIMPUESTO = Convert.ToString(dr["TOTALIMPUESTO"]),
                    TOTALCOMPROBANTE = Convert.ToString(dr["TOTALCOMPROBANTE"]),
                    CONTIENE_ERRORES = error,
                    ERROR_WS = error2,
                    ERROR_SOFTLAND = error3,
                    ENVIADO = var_env,
                    ACEPTADO = var_acep,
                    RESPUESTA_XML = Convert.ToString(dr["RESPUESTA_XML"]).Remove(0,19),
                    PDF = Convert.ToString(dr["PDF"]).Remove(0,19),
                    XML = Convert.ToString(dr["XML"]).Remove(0, 19)


                });
                
            }

            int pagesize = 25;
            int pagenumber = (i ?? 1);
            sqlconn.Close();
            ModelState.Clear();
            return View(lc.ToPagedList(pagenumber, pagesize));
        }

       /* [HttpPost]
        public ActionResult Index(DateTime? start, DateTime? end , int? i)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("uk");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("uk");
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "set dateformat dmy select  * from [erpadmin].[P_APP_HACIENDA_DE] where FECHA between '" + start + "'and'" + end + "'";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            List<MVClass> lc = new List<MVClass>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                var con_err = Convert.ToString(dr["CONTIENE_ERRORES"]);
                var con_errws = Convert.ToString(dr["ERROR_WS"]);
                var con_soft = Convert.ToString(dr["ERROR_SOFTLAND"]);
                var acept = Convert.ToString(dr["ACEPTADO"]);
                var envia = Convert.ToString(dr["ENVIADO"]);
                var error = true;
                var error2 = true;
                var error3 = true;
                var var_env = false;
                var var_acep = false;
                if (con_err == "N")
                {
                    error = false;
                }
                if (con_errws == "N")
                {
                    error2 = false;
                }
                if (con_soft == "N")
                {
                    error3 = false;
                }
                if (envia == "S")
                {
                    var_env = true;
                }
                if (acept == "A")
                {
                    var_acep = true;
                }
                lc.Add(new MVClass
                {

                    DOCUMENTO = Convert.ToString(dr["DOCUMENTO"]),
                    CLIENTE = Convert.ToString(dr["CLIENTE"]),
                    FECHA = Convert.ToDateTime(dr["FECHA"]),
                    NOMBRE = Convert.ToString(dr["NOMBRE"]),
                    NIT_RECEPTOR = Convert.ToString(dr["NIT_RECEPTOR"]),
                    CODIGO_MONEDA = Convert.ToString(dr["CODIGO_MONEDA"]),
                    CLAVE = Convert.ToString(dr["CLAVE"]),
                    TOTALGRAVADO = Convert.ToString(dr["TOTALGRAVADO"]),
                    TOTALEXENTO = Convert.ToString(dr["TOTALEXENTO"]),
                    TOTALDESCUENTOS = Convert.ToString(dr["TOTALDESCUENTOS"]),
                    TOTALIMPUESTO = Convert.ToString(dr["TOTALIMPUESTO"]),
                    TOTALCOMPROBANTE = Convert.ToString(dr["TOTALCOMPROBANTE"]),
                    CONTIENE_ERRORES = error,
                    ERROR_WS = error2,
                    ERROR_SOFTLAND = error3,
                    ENVIADO = var_env,
                    ACEPTADO = var_acep,
                    RESPUESTA_XML = Convert.ToString(dr["RESPUESTA_XML"]).Remove(0,19),
                    PDF = Convert.ToString(dr["PDF"]).Remove(0, 19),
                    XML = Convert.ToString(dr["XML"]).Remove(0, 19)


                });
                
            }

            sqlconn.Close();
            ModelState.Clear();
            return View(lc.ToPagedList(i ?? 1, 25));
        }*/


    }
}