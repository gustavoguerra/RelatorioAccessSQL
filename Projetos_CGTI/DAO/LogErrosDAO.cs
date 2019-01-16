using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Projetos_CGTI.Models;

namespace Projetos_CGTI.DAO
{
    public class LogErrosDAO
    {
        public SqlConnection con = Conexao.getconection();

        public void Salvar_Log(string Local,string Erro)
        {
            string sql = "INSERT INTO LogErros (LOCAL_ERRO,ERRO,DATA_ERRO)Values(@LOCAL,@ERRO,GETDATE())";

            SqlCommand comand = new SqlCommand();

            comand.Connection = con;
            comand.CommandText = sql;

            comand.Parameters.AddWithValue("@LOCAL", Local);
            comand.Parameters.AddWithValue("@ERRO", Erro);
            
            con.Open();
            comand.ExecuteNonQuery();
            con.Close();
        }
    }
}