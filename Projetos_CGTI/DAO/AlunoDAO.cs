using Projetos_CGTI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;


namespace Projetos_CGTI.DAO
{
    public class AlunoDAO
    {
        public SqlConnection con = Conexao.getconection();

        public void Salvar_Aluno(Models.AlunoViewModel Aluno)
        {
            SqlCommand comand = new SqlCommand();

            string sql = "";

            if (Aluno.ID == 0)
            {
                sql = "INSERT INTO Cadastro_Aluno" +
                           "(Projeto,Nome_Aluno,Data_Dascimento,Sexo,Email_Aluno,Peso,Altura,RG_Aluno,CPF_Aluno,Deficiencia," +
                           "Obs,Nome_Escola,Serie,Tipo_Escola,Periodo,Tamanho_Camisa,Tamanho_short,Numero_Tenis,Nome_Responsavel,RG_Responsavel," +
                           "Parentesco,Email_Responsavel,CEP,Endereco,Numero,Bairro,Cidade,Estado,Tel_Fixo,Tel_Celular) " +
                       "Values" +
                           "(@Projeto,@Nome_Aluno, @Data_Dascimento, @Sexo, @Email_Aluno, @Peso, @Altura, @RG_Aluno, @CPF_Aluno, @Deficiencia," +
                           "@Obs, @Nome_Escola, @Serie, @Tipo_Escola, @Periodo,@Tamanho_Camisa,@Tamanho_short,@Numero_Tenis," +
                           "@Nome_Responsavel, @RG_Responsavel,@Parentesco, @Email_Responsavel, @CEP,@Endereco,@Numero," +
                           "@Bairro,@Cidade,@Estado, @Tel_Fixo, @Tel_Celular)";
            }
            else
            {
                sql = "update Cadastro_Aluno set Projeto = @Projeto , Nome_Aluno = @Nome_Aluno, Data_Dascimento = @Data_Dascimento," +
                            "Sexo = @Sexo, Email_Aluno = @Email_Aluno, Peso = @Peso, Altura = @Altura ,RG_Aluno = @RG_Aluno, CPF_Aluno = @CPF_Aluno," +
                            "Deficiencia = @Deficiencia, OBS = @OBS, Nome_Escola = @Nome_Escola, Serie = @Serie, Periodo = @Periodo," +
                            "Tamanho_Camisa = @Tamanho_Camisa ,Tamanho_short = @Tamanho_short,Numero_Tenis = @Numero_Tenis," +
                            "Nome_Responsavel = @Nome_Responsavel, RG_Responsavel = @RG_Responsavel, Parentesco = @Parentesco," +
                            "Email_Responsavel = @Email_Responsavel, CEP = @CEP, Endereco = @Endereco, Numero = @Numero," +
                            "Bairro = @Bairro, Cidade = @Cidade, Estado = @Estado, Tel_Fixo = @Tel_Fixo, Tel_Celular = @Tel_Celular " +
                      "where ID = @ID";

                comand.Parameters.AddWithValue("@ID", Aluno.ID);
            }

            comand.Connection = con;
            comand.CommandText = sql;

            // Continuar configuração para salvar
            comand.Parameters.AddWithValue("@Projeto", Aluno.Nome_Projeto);
            comand.Parameters.AddWithValue("@Nome_Aluno", Aluno.Nome_Aluno);
            comand.Parameters.AddWithValue("@Data_Dascimento", Aluno.DataNascimento);
            comand.Parameters.AddWithValue("@Sexo", Aluno.Sexo);
            comand.Parameters.AddWithValue("@Email_Aluno", Aluno.Email);
            comand.Parameters.AddWithValue("@Peso", Convert.ToDecimal(Aluno.Peso));
            comand.Parameters.AddWithValue("@Altura", Convert.ToDecimal(Aluno.Alttura));
            comand.Parameters.AddWithValue("@RG_Aluno", Aluno.RG);
            comand.Parameters.AddWithValue("@CPF_Aluno", Aluno.CPF.Replace(".", "").Replace("-", ""));
            comand.Parameters.AddWithValue("@Deficiencia", Aluno.Deficiencia);
            comand.Parameters.AddWithValue("@Obs", Aluno.Problema_Saude);
            comand.Parameters.AddWithValue("@Nome_Escola", Aluno.Nome_Escola);
            comand.Parameters.AddWithValue("@Serie", Convert.ToInt64(Aluno.Seri));
            comand.Parameters.AddWithValue("@Tipo_Escola", Aluno.Tipo_Escola);
            comand.Parameters.AddWithValue("@Periodo", Aluno.Periodo);
            comand.Parameters.AddWithValue("@Tamanho_Camisa", Aluno.T_Camiseta);
            comand.Parameters.AddWithValue("@Tamanho_short", Aluno.T_Short);
            comand.Parameters.AddWithValue("@Numero_Tenis", Aluno.T_Tenis);
            comand.Parameters.AddWithValue("@Nome_Responsavel", Aluno.Nome_responsavel);
            comand.Parameters.AddWithValue("@RG_Responsavel", Aluno.RG_Responsagel);
            comand.Parameters.AddWithValue("@Parentesco", Aluno.Parentesco);
            comand.Parameters.AddWithValue("@Email_Responsavel", Aluno.Email_Responsavel);
            comand.Parameters.AddWithValue("@CEP", Convert.ToInt64(Aluno.CEP.Replace("-", "")));
            comand.Parameters.AddWithValue("@Endereco", Aluno.Endereco);
            comand.Parameters.AddWithValue("@Numero", Aluno.Numero);
            comand.Parameters.AddWithValue("@Bairro", Aluno.Bairro);
            comand.Parameters.AddWithValue("@Cidade", Aluno.Cidade);
            comand.Parameters.AddWithValue("@Estado", Aluno.Estado);
            comand.Parameters.AddWithValue("@Tel_Fixo", Aluno.Telefone);
            comand.Parameters.AddWithValue("@Tel_Celular", Aluno.Celular);

            con.Open();
            comand.ExecuteNonQuery();
            con.Close();
        }

        public List<AlunoViewModel> Listar_Aluno()
        {
            string sql = "select * from Cadastro_Aluno";

            SqlCommand comand = new SqlCommand();
            List<AlunoViewModel> lista = new List<AlunoViewModel>();

            comand.Connection = con;
            comand.CommandText = sql;

            con.Open();
            SqlDataReader ler = comand.ExecuteReader();
            while (ler.Read())
            {
                AlunoViewModel Aluno = new AlunoViewModel();

                Aluno.ID = Convert.ToInt32(ler[0].ToString());
                Aluno.Nome_Projeto = ler[1].ToString();
                Aluno.Nome_Aluno = ler[2].ToString();
                Aluno.DataNascimento = Convert.ToDateTime(ler[3].ToString());
                Aluno.Sexo = ler[4].ToString();
                Aluno.Email = ler[5].ToString();
                Aluno.Peso = ler[6].ToString();
                Aluno.Alttura = ler[7].ToString();
                Aluno.RG = ler[8].ToString();
                Aluno.CPF = ler[9].ToString();
                Aluno.Deficiencia = ler[10].ToString();
                Aluno.Problema_Saude = ler[11].ToString();
                Aluno.Nome_Escola = ler[12].ToString();
                Aluno.Seri = ler[13].ToString();
                Aluno.Tipo_Escola = ler[14].ToString();
                Aluno.Periodo = ler[15].ToString();
                Aluno.Nome_responsavel = ler[16].ToString();
                Aluno.RG_Responsagel = ler[17].ToString();
                Aluno.Parentesco = ler[18].ToString();
                Aluno.Email_Responsavel = ler[19].ToString();
                Aluno.CEP = ler[20].ToString();
                Aluno.Endereco = ler[21].ToString();
                Aluno.Numero = ler[22].ToString();
                Aluno.Bairro = ler[23].ToString();
                Aluno.Cidade = ler[24].ToString();
                Aluno.Estado = ler[25].ToString();
                Aluno.Telefone = ler[26].ToString();
                Aluno.Celular = ler[27].ToString();

                lista.Add(Aluno);
            }
            con.Close();

            return lista;
        }

        public void Edita_Aluno(AlunoViewModel Aluno)
        {
            string sql = "INSERT INTO Cadastro_Aluno" +
                            "(Projeto,Nome_Aluno,Data_Dascimento,Sexo,Email_Aluno,Peso,Altura,RG_Aluno,CPF_Aluno,Deficiencia," +
                            "Obs,Nome_Escola,Serie,Tipo_Escola,Periodo,Nome_Responsavel,RG_Responsavel," +
                            "Parentesco,Email_Responsavel,CEP,Endereco,Numero,Bairro,Cidade,Estado,Tel_Fixo,Tel_Celular) " +
                        "Values" +
                            "(@Projeto,@Nome_Aluno, @Data_Dascimento, @Sexo, @Email_Aluno, @Peso, @Altura, @RG_Aluno, @CPF_Aluno, @Deficiencia," +
                            "@Obs, @Nome_Escola, @Serie, @Tipo_Escola, @Periodo, @Nome_Responsavel, @RG_Responsavel, " +
                            "@Parentesco, @Email_Responsavel, @CEP,@Endereco, @Numero,@Bairro,@Cidade,@Estado, @Tel_Fixo, @Tel_Celular)";

            SqlCommand comand = new SqlCommand();

            comand.Connection = con;
            comand.CommandText = sql;
            // Continuar configuração para salvar
            comand.Parameters.AddWithValue("@ID", Aluno.ID);
            comand.Parameters.AddWithValue("@Projeto", Aluno.Nome_Projeto);
            comand.Parameters.AddWithValue("@Nome_Aluno", Aluno.Nome_Aluno);
            comand.Parameters.AddWithValue("@Data_Dascimento", Aluno.DataNascimento);
            comand.Parameters.AddWithValue("@Sexo", Aluno.Sexo);
            comand.Parameters.AddWithValue("@Email_Aluno", Aluno.Email);
            comand.Parameters.AddWithValue("@Peso", Convert.ToDecimal(Aluno.Peso));
            comand.Parameters.AddWithValue("@Altura", Convert.ToDecimal(Aluno.Alttura));
            comand.Parameters.AddWithValue("@RG_Aluno", Aluno.RG);
            comand.Parameters.AddWithValue("@CPF_Aluno", Aluno.CPF.Replace(".", "").Replace("-", ""));
            comand.Parameters.AddWithValue("@Deficiencia", Aluno.Deficiencia);
            comand.Parameters.AddWithValue("@Obs", Aluno.Problema_Saude);
            comand.Parameters.AddWithValue("@Nome_Escola", Aluno.Nome_Escola);
            comand.Parameters.AddWithValue("@Serie", Convert.ToInt64(Aluno.Seri));
            comand.Parameters.AddWithValue("@Tipo_Escola", Aluno.Tipo_Escola);
            comand.Parameters.AddWithValue("@Periodo", Aluno.Periodo);
            comand.Parameters.AddWithValue("@Nome_Responsavel", Aluno.Nome_responsavel);
            comand.Parameters.AddWithValue("@RG_Responsavel", Aluno.RG_Responsagel);
            comand.Parameters.AddWithValue("@Parentesco", Aluno.Parentesco);
            comand.Parameters.AddWithValue("@Email_Responsavel", Aluno.Email_Responsavel);
            comand.Parameters.AddWithValue("@CEP", Convert.ToInt64(Aluno.CEP.Replace("-", "")));
            comand.Parameters.AddWithValue("@Endereco", Aluno.Endereco);
            comand.Parameters.AddWithValue("@Numero", Aluno.Numero);
            comand.Parameters.AddWithValue("@Bairro", Aluno.Bairro);
            comand.Parameters.AddWithValue("@Cidade", Aluno.Cidade);
            comand.Parameters.AddWithValue("@Estado", Aluno.Estado);
            comand.Parameters.AddWithValue("@Tel_Fixo", Aluno.Telefone);
            comand.Parameters.AddWithValue("@Tel_Celular", Aluno.Celular);

            con.Open();
            comand.ExecuteNonQuery();
            con.Close();

        }

        public AlunoViewModel Localizar_Aluno(int ID)
        {
            string sql = "select * from Cadastro_Aluno where ID = @ID";

            SqlCommand comand = new SqlCommand();

            comand.Connection = con;
            comand.CommandText = sql;
            comand.Parameters.AddWithValue("@ID", ID);

            con.Open();

            SqlDataReader ler = comand.ExecuteReader();

            AlunoViewModel Aluno = new AlunoViewModel();

            ler.Read();

            Aluno.ID = Convert.ToInt32(ler[0].ToString());
            Aluno.Nome_Projeto = ler[1].ToString();
            Aluno.Nome_Aluno = ler[2].ToString();
            Aluno.DataNascimento = Convert.ToDateTime(ler[3].ToString()).Date;
            Aluno.Sexo = ler[4].ToString();
            Aluno.Email = ler[5].ToString();
            Aluno.Peso = ler[6].ToString();
            Aluno.Alttura = ler[7].ToString();
            Aluno.RG = ler[8].ToString();
            Aluno.CPF = ler[9].ToString();
            Aluno.Deficiencia = ler[10].ToString();
            Aluno.Problema_Saude = ler[11].ToString();
            Aluno.Nome_Escola = ler[12].ToString();
            Aluno.Seri = ler[13].ToString();
            Aluno.Tipo_Escola = ler[14].ToString();
            Aluno.Periodo = ler[15].ToString();
            Aluno.T_Camiseta = ler[16].ToString();
            Aluno.T_Short = ler[17].ToString();
            Aluno.T_Tenis = ler[18].ToString();
            Aluno.Nome_responsavel = ler[19].ToString();
            Aluno.RG_Responsagel = ler[20].ToString();
            Aluno.Parentesco = ler[21].ToString();
            Aluno.Email_Responsavel = ler[22].ToString();
            Aluno.CEP = ler[23].ToString();
            Aluno.Endereco = ler[24].ToString();
            Aluno.Numero = ler[25].ToString();
            Aluno.Bairro = ler[26].ToString();
            Aluno.Cidade = ler[27].ToString();
            Aluno.Estado = ler[28].ToString();
            Aluno.Telefone = ler[29].ToString();
            Aluno.Celular = ler[30].ToString();

            con.Close();

            return Aluno;
        }

        public void Apagar_Aluno(int id)
        {
            string sql = "delete Cadastro_Aluno where ID = @ID";

            SqlCommand comand = new SqlCommand();
            comand.Connection = con;
            comand.CommandText = sql;

            // Continuar configuração para salvar
            comand.Parameters.AddWithValue("@ID", id);

            con.Open();
            comand.ExecuteNonQuery();
            con.Close();
        }

        public DataTable Lista_Projeto()
        {
            SqlCommand command = new SqlCommand();

            command.Connection = con;

            command.CommandText = "select ID,Nome from Esporte_Projeto";

            DataTable Projeto = new DataTable();

            con.Open();
            Projeto.Load(command.ExecuteReader());
            con.Close();

            return Projeto;
        }

    }
}