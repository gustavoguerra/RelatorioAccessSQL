using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Projetos_CGTI.Models
{
    public class ReembolsoDAO
    {
        public SqlConnection con = Conexao.getconection();

        public void SalvarUsuario(SalvarUsuarioViewModel usuario)
        {
            string sql = "INSERT INTO UsuarioReembolso(NOME,CPF,PROJETO,VEICULO,CARGO,ENDERECO) VALUES (@NOME,@CPF,@PROJETO,@VEICULO,@CARGO,@ENDERECO)";

            SqlCommand comand = new SqlCommand();

            comand.Connection = con;
            comand.CommandText = sql;

            comand.Parameters.AddWithValue("@NOME", usuario.Nome);
            comand.Parameters.AddWithValue("@CPF", usuario.CPF);
            comand.Parameters.AddWithValue("@PROJETO", usuario.Projeto);
            comand.Parameters.AddWithValue("@VEICULO", usuario.Veiculo);
            comand.Parameters.AddWithValue("@CARGO", usuario.Cargo);
            comand.Parameters.AddWithValue("@ENDERECO", usuario.Endereco);
            
            con.Open();
                comand.ExecuteNonQuery();
            con.Close();
        }

        public List<EditarUsuarioViewModel> ListaUsuario(List<EditarUsuarioViewModel> listaUsuario)
        {
            string sql = "select * from UsuarioReembolso";

            SqlCommand comand = new SqlCommand();

            comand.Connection = con;
            comand.CommandText = sql;

            con.Open();
            SqlDataReader ler = comand.ExecuteReader();
            while (ler.Read())
            {
                EditarUsuarioViewModel LerUsuario = new EditarUsuarioViewModel();
                LerUsuario.id = ler[0].ToString();
                LerUsuario.Nome = ler[1].ToString(); 
                LerUsuario.CPF = ler[2].ToString();
                LerUsuario.Projeto = ler[3].ToString();
                LerUsuario.Veiculo = ler[4].ToString();
                LerUsuario.Cargo = ler[5].ToString();
                LerUsuario.Endereco = ler[6].ToString();
                                
                listaUsuario.Add(LerUsuario);
            }

            con.Close();

            return listaUsuario;
        }

    }
}