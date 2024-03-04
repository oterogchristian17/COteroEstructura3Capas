using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Negocio
{
    public class Empleado
    {
        //ML 
        //Propiedades

        public int EmpleadoID { get; set; }
        public string Nombre { get; set; }
        public List<Empleado> Empleados { get; set; }

        //Navegacion

        public Negocio.Departamento Departamento { get; set; }
        public Negocio.Puesto Puesto { get; set; }

        //Constructores
        public Empleado(string nombre)
        {
            Nombre = nombre;
            
        }

        public Empleado() { }   

        //Metodos BL

        public static Dictionary<string, object> Delete(int EmpleadoID)
        {
            bool resultadoDelete = false;
            string excepcion = "";
            Empleado empleado = new Empleado();
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Empleado", EmpleadoID }, { "Exepcion", excepcion }, { "Resultado", false } };

            try
            {


                using (SqlConnection context = new SqlConnection(AccesoDatos.Conexion.ObtenerConecctionString()))
                {
                    var sentencia = "EmpleadoDelete";

                    SqlParameter[] parametros = new SqlParameter[1];

                    parametros[0] = new SqlParameter("@EmpleadoID", SqlDbType.Int);
                    parametros[0].Value = EmpleadoID;

                    SqlCommand command = new SqlCommand(sentencia, context);
                    //leer la informacion
                    command.Parameters.AddRange(parametros);
                    command.Connection.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    int filasAfectadas = command.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }
            catch (Exception ex)
            {

                resultadoDelete = false;
            }

            return diccionario;
        }

        public static Dictionary<string, object> GetAll(Empleado emp)
        {
            //Negocio.Empleado emp = new Negocio.Empleado();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Empleado", emp }, { "Exepcion", excepcion }, { "Resultado", false } };

            try
            {
                using (AccesoDatos.COteroEstructuraEntities1 context = new AccesoDatos.COteroEstructuraEntities1())

                {
                    var registros = context.EmpleadoGetAll(emp.Nombre).ToList();

                    if (registros != null)
                    {
                        emp.Empleados = new List<Negocio.Empleado>();

                        foreach (var registro in registros)
                        {
                            //Instanciar el objeto
                            Empleado empleado = new Negocio.Empleado();

                            empleado.EmpleadoID = registro.EmpleadoID;
                            empleado.Nombre = registro.Nombre;
   
                            //AQUI VA LA PROPIEDAD DE NAVEGACION

                            empleado.Departamento = new Negocio.Departamento();
                            empleado.Departamento.Descripcion = registro.DescripcionDepa;

                            empleado.Puesto = new Negocio.Puesto();
                            empleado.Puesto.Descripcion = registro.DescripcionPuesto;

                            emp.Empleados.Add(empleado);
                        }
                        diccionario["Resultado"] = true;
                        diccionario["Usuario"] = emp;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario;
        }

        //public static Dictionary<string, object> GetByName()
        //{
        //    Negocio.Empleado empleado = new Negocio.Empleado();
        //    Negocio.Empleado emp = new Negocio.Empleado();

        //    string excepcion = "";
        //    Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Empleado", nombre }, { "Exepcion", excepcion }, { "Resultado", false } };

        //    try
        //    {
        //        using (AccesoDatos.COteroEstructuraEntities context = new AccesoDatos.COteroEstructuraEntities())
        //        {
        //            var objeto = context.EmpleadoGetAll(Empleado empleado).SingleOrDefault();
        //            //SingleOrDefault -- devuelve el unico valor 
        //            //FirstOrDefault -- devuelve el primero devuleve un valor por defecto

        //            if (objeto != null)
        //            {

        //                emp.Empleados = new List<Negocio.Empleado>();

        //                empleado.EmpleadoID = objeto.EmpleadoID;
        //                empleado.Nombre = objeto.Nombre;

        //                empleado.Departamento = new Negocio.Departamento();
        //                empleado.Departamento.Descripcion = objeto.Departamento;

        //                empleado.Puesto = new Negocio.Puesto();
        //                empleado.Puesto.Descripcion = objeto.Puesto;


        //                emp.Empleados.Add(empleado);

        //                diccionario["Resultado"] = true;
        //                diccionario["Empleado"] = empleado;
        //            }
        //            else
        //            {
        //                diccionario["Resultado"] = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        diccionario["Resultado"] = false;
        //        diccionario["Exepcion"] = ex.Message;
        //    }
        //    return diccionario;
        //}


    }
}
