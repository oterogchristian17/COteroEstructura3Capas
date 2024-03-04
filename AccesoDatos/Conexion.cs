using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class Conexion
    {
        //Metodo para obtener la cadena de conexion
        //Data Source=.;Initial Catalog=COteroProgramacionNCapas;User ID=sa;Password=*********** 
        //se debe guardar en Appconfig
        public static string ObtenerConecctionString()
        {
            return ConfigurationManager.ConnectionStrings["COteroEstructura"].ToString();
        }
    }
}
