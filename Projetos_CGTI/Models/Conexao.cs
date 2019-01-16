using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Projetos_CGTI.Models
{
    public class Conexao
    {
        public static SqlConnection getconection()
        {
            SqlConnection con = new SqlConnection();
            try
            {
                con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CgtiConnectionString"].ConnectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha na conexão: ", ex);
            }
            return con;
        }
        public static SqlConnection getconectionHabil()
        {
            SqlConnection con = new SqlConnection();
            try
            {
                con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CgtiConnectionStringHabil"].ConnectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha na conexão: ", ex);
            }
            return con;
        }

    }
}