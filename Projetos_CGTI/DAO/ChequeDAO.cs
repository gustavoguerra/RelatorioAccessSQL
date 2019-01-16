using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Projetos_CGTI.Models
{
    public class ChequeDAO
    {
        public SqlConnection con = Conexao.getconection();

        public void Salvar(ChequeModel cheque)
        {
            string sql = "INSERT INTO Controle_Cheque" +
                "(CHEQUE,TALAO,BANCO,REFERENCIA,FAVORECIDO,VALOR,LOCALURL,DATALAN)" +
                "VALUES(@ncheque, @ntalao, @banco, @referencia, @favorecido, @valor,@URL, @data)";

            SqlCommand comand = new SqlCommand();

            comand.Connection = con;
            comand.CommandText = sql;

            comand.Parameters.AddWithValue("@ntalao", cheque.NTalao);
            comand.Parameters.AddWithValue("@ncheque", cheque.NCheque);
            comand.Parameters.AddWithValue("@banco", cheque.Banco);
            comand.Parameters.AddWithValue("@favorecido", cheque.Favorecido);
            comand.Parameters.AddWithValue("@referencia", cheque.Referencia);
            comand.Parameters.AddWithValue("@valor", cheque.Valor);
            comand.Parameters.AddWithValue("@URL", cheque.URL);
            comand.Parameters.AddWithValue("@data", cheque.Datalanca);

            con.Open();
            comand.ExecuteNonQuery();
            con.Close();
        }

        public void Atualizar(ChequeModel cheque)
        {
            string sql = "UPDATE Controle_Cheque SET" +
                                "CHEQUE = @ncheque," +
                                "TALAO = @ntalao," +
                                "BANCO = @banco," +
                                "REFERENCIA = @referencia," +
                                "FAVORECIDO = @favorecido," +
                                "VALOR = @valor," +
                                "DATALAN = @data," +
                                "LOCALURL = @URL," +
                        "WHERE ID = @id";

            SqlCommand comand = new SqlCommand();
            comand.Connection = con;
            comand.CommandText = sql;

            comand.Parameters.AddWithValue("@id", cheque.Id);
            comand.Parameters.AddWithValue("@nchque", cheque.NCheque);
            comand.Parameters.AddWithValue("@ntalao", cheque.NTalao);
            comand.Parameters.AddWithValue("@banco", cheque.Banco);
            comand.Parameters.AddWithValue("@referencia", cheque.Referencia);
            comand.Parameters.AddWithValue("@favorecido", cheque.Favorecido);
            comand.Parameters.AddWithValue("@valor", cheque.Valor);
            comand.Parameters.AddWithValue("@URL", cheque.Datalanca);
            comand.Parameters.AddWithValue("@data", cheque.Datalanca);

            con.Open();
            comand.ExecuteNonQuery();
            con.Close();
        }

        public void Deletar(ChequeModel cheque)
        {
            string sql = "DELETE Controle_Cheque WHEQUE = @id";

            SqlCommand comand = new SqlCommand();

            comand.CommandText = sql;
            comand.Connection = con;
            comand.Parameters.AddWithValue("@id", cheque.Id);

            con.Open();
            comand.ExecuteNonQuery();
            con.Close();
        }

        public List<ChequeModel> Lista_Cheque(List<ChequeModel> lista)
        {
            string sql = "select * from Controle_Cheque";

            SqlCommand comand = new SqlCommand();

            comand.Connection = con;
            comand.CommandText = sql;

            con.Open();
            SqlDataReader ler = comand.ExecuteReader();
            while (ler.Read())
            {
                ChequeModel valores = new ChequeModel();
                valores.Id = Convert.ToInt32(ler[0]);
                valores.NTalao = ler[1].ToString();
                valores.NCheque = ler[2].ToString();
                valores.Referencia = ler[3].ToString();
                valores.Favorecido = ler[4].ToString();
                valores.Banco = ler[5].ToString();
                valores.Valor = ler[6].ToString();
                valores.URL = ler[7].ToString();
                valores.Datalanca = Convert.ToDateTime(ler[8].ToString());

                lista.Add(valores);
            }

            con.Close();

            return lista;
        }

        public string RetornarPorEmail(string Email)
        {
            string username = "";
            try
            {
                string sql = "select UserName from AspNetUsers where Email = @Email";

                SqlCommand comand = new SqlCommand();

                comand.Connection = con;
                comand.CommandText = sql;

                comand.Parameters.AddWithValue("@Email", Email);

                
                con.Open();
                SqlDataReader ler = comand.ExecuteReader();
                while (ler.Read())
                {
                    username = ler[0].ToString();
                }
            }
            catch(Exception)
            {
                return null;
            }
            return username;
        }
    }
}
