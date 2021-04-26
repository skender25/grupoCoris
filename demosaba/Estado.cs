using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demosaba
{
    public class Estado
    {

        private static bool d_estado_session = false;
        private static DateTime d_fecha_i = DateTime.Parse("01-01-2021");
        private static DateTime d_fecha_f = DateTime.Parse("04-04-2021");
        public static bool estado_session
        {
            get { return d_estado_session; }
            set { d_estado_session = value; }
        }
        public static DateTime fecha_i
        {
            get { return d_fecha_i; }
            set { d_fecha_i = value; }
        }
        public static DateTime fecha_f
        {
            get { return d_fecha_f; }
            set { d_fecha_f = value; }
        }
    }
}