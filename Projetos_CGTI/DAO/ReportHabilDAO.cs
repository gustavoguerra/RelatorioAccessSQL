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
    public class ReportHabilDAO
    {
        public SqlConnection con = Conexao.getconectionHabil();

        public string destFile;

        #region Load Dados do DB Access para SQL

        public void ResetarTabelas()
        {
            try
            {
                SqlCommand command = new SqlCommand();

                command.Connection = con;

                command.CommandText = "truncate table ANEXOS " +
                                  "truncate table Caixa_Configuracao " +
                                  "truncate table Caixa_Contas " +
                                  "truncate table Caixa_Lancamentos " +
                                  "truncate table Caixa_Lancamentos_Detalhes " +
                                  "truncate table CAIXAS " +
                                  "truncate table Centros_Custos " +
                                  "truncate table Cliente " +
                                  "truncate table Codigo_Bancario_Padrao " +
                                  "truncate table Codigos " +
                                  "truncate table Contas_Bancarias " +
                                  "truncate table Contas_Bancarias_Lancamentos " +
                                  "truncate table Contas_Pagar " +
                                  "truncate table Contas_Receber " +
                                  "truncate table DETALHES_CONTACAIXA " +
                                  "truncate table DetalhesLctos " +
                                  "truncate table Empresa " +
                                  "truncate table Fonte_Sql " +
                                  "truncate table FORMAS_PGTO " +
                                  "truncate table Fornecedor " +
                                  "truncate table IMAGENS " +
                                  "truncate table RapidAction " +
                                  "truncate table Usuarios";

                con.Open();
                command.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                LogErrosDAO log = new LogErrosDAO();
                log.Salvar_Log("ReportHabilDAO ResetarTabelas", ex.Message);
            }
        } //Fazer verificação para remover arquivos antigos.

        public void Copiar_Arquivo()
        {
            try
            {
                /*destFile = @"D:\Habil_to_SQL\habil.mdb";

                // Verifica se existe um arquivo Se existir apaga para recriar
                if (System.IO.File.Exists(destFile))
                {
                    System.IO.File.Delete(destFile);
                }

                System.Diagnostics.Process.Start(@"D:\Habil_to_SQL\Copiar.bat");

                */
                string fileName = "habil.mdb";
                string diretorio = Convert.ToString(System.DateTime.Today.Date);
                string sourcePath = @"D:\Habil Empresarial"; //@"D:\Habil Empresarial\Habil Empresarial\Dados";
                string targetPath = @"D:\Temp_Habil\Habil_TO_SQL";// + diretorio.Replace("/", "-").Replace("00:00:00", "");
               // string targetPath = @"D:\Habil_to_SQL\" + diretorio;
                string targetPathFile = targetPath + @"\" + fileName;

                string sourceFile = System.IO.Path.Combine(sourcePath, fileName);

                destFile = System.IO.Path.Combine(targetPath, fileName);

                //Criar Diretorio se nao existir
                if (!System.IO.Directory.Exists(targetPath))
                {
                    System.IO.Directory.CreateDirectory(targetPath);
                }
                // Verifica se existe um arquivo Se existir apaga para recriar
                if (System.IO.File.Exists(targetPathFile))
                {
                    System.IO.File.Delete(targetPathFile);
                }
                //Copiar Arquivo
                System.IO.File.Copy(sourceFile, destFile, true);
            }
            catch (Exception ex)
            {
                LogErrosDAO log = new LogErrosDAO();
                log.Salvar_Log("ReportHabilDAO Copiar_Arquivo", ex.Message);
            }
        }

        public void Carregar_Lista(List<string> lista)
        {

            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandText = "select * from Lista_Tabelas";

                con.Open();
                SqlDataReader Lista = command.ExecuteReader();
                int progs = 0;

                while (Lista.Read())
                {
                    lista.Add(Lista[1].ToString());
                    progs++;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                LogErrosDAO log = new LogErrosDAO();
                log.Salvar_Log("ReportHabilDAO Carregar_Lista", ex.Message);
            }
        }

        public void IniciarAccess(string tabela)
        {
            try
            {
                // Conectar com habil
                // string conectarAcess = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + destFile.Replace(" ", "") + ";Jet OLEDB:Database Password = amoragape;Persist Security Info=True";              
                string conectarAcess = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + destFile.Replace(" ", "") + ";Jet OLEDB:Database Password = amoragape;Persist Security Info=True";             
                string localizar = "select * from " + tabela;

                OleDbConnection conexaoAcess = new OleDbConnection(conectarAcess);
                OleDbCommand Acess = new OleDbCommand(localizar, conexaoAcess);

                DataTable dtaccess = null;

                conexaoAcess.Open();

                Acess.CommandText = localizar;

                dtaccess = new DataTable();
                dtaccess.Load(Acess.ExecuteReader());

                conexaoAcess.Close();

                IniciarSQL(dtaccess, tabela);
            }
            catch (Exception ex)
            {
                LogErrosDAO log = new LogErrosDAO();
                log.Salvar_Log("ReportHabilDAO IniciarAccess", ex.Message);
            }
        }

        public void IniciarSQL(DataTable dataAccess, string tabela)
        {
            try
            {
                SqlCommand command = new SqlCommand();

                //   conexaoSQL.ConnectionString = @"Data Source=192.168.1.4;Initial Catalog=Habil;User=sa;Password=ti@sup0rt3";

                command.Connection = con;

                con.Open();

                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    sqlBulkCopy.DestinationTableName = tabela;

                    foreach (var column in dataAccess.Columns)
                        sqlBulkCopy.ColumnMappings.Add(column.ToString(), column.ToString());

                    sqlBulkCopy.WriteToServer(dataAccess);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                LogErrosDAO log = new LogErrosDAO();
                log.Salvar_Log("ReportHabilDAO IniciarSQL", ex.Message);
            }
        }

        public void SalvaUltimaAtualizacao()
        {
            try
            {
                SqlCommand command = new SqlCommand();

                //  con.ConnectionString = @"Data Source=192.168.1.4;Initial Catalog=Habil;User=sa;Password=ti@sup0rt3";

                command.Connection = con;

                command.CommandText = "INSERT INTO ULTIMA_ATUALIZACAO (DATA) VALUES (GETDATE())";
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                LogErrosDAO log = new LogErrosDAO();
                log.Salvar_Log("ReportHabilDAO SalvaUltimaAtualizacao", ex.Message);
            }
        }

        public string ConsultaUltimaAtualizacao()
        {
            string data = "";
            try
            {
                SqlCommand command = new SqlCommand();

                command.Connection = con;

                command.CommandText = "select top 1 format(data, 'dd/MM/yyyy HH:mm:ss') from ULTIMA_ATUALIZACAO order by data desc";

                con.Open();
                data = (string)command.ExecuteScalar();
                con.Close();
            }
            catch (Exception ex)
            {
                LogErrosDAO log = new LogErrosDAO();
                log.Salvar_Log("ReportHabilDAO ConsultarUltimaAtualização", ex.Message);
            }
            return data;
        }

        #endregion

        #region Listas Entradas

        public DataTable ListaEmpresa()
        {
            SqlCommand command = new SqlCommand();

            command.Connection = con;

            command.CommandText = "select Codigo,Nome_Fantasia from Empresa";

            DataTable empresa = new DataTable();

            con.Open();
            empresa.Load(command.ExecuteReader());
            con.Close();

            return empresa;
        }

        public DataTable ListaContaBancaria(ConsultarReportHabil model)
        {
            SqlCommand command = new SqlCommand();

            command.Connection = con;

            //  command.CommandText = "select Codigo,Empresa,Descrição from Contas_Bancarias";
            command.CommandText = "select ID_ContaBancaria,Empresa,Descrição from Contas_Bancarias where Empresa in (SELECT * FROM STRING_SPLIT(@Empresa, ','))";
            command.Parameters.AddWithValue("@Empresa", model.ID_Empresa);

            DataTable Conta = new DataTable();

            con.Open();
            Conta.Load(command.ExecuteReader());
            con.Close();

            return Conta;
        }

        public DataTable ListaFornecedores(ConsultarReportHabil model)
        {
            SqlCommand command = new SqlCommand();

            command.Connection = con;

            command.CommandText = "select ID_Fornecedor,Nome_Fantasia from Fornecedor where Empresa in (SELECT * FROM STRING_SPLIT(@Empresa, ',')) order by Nome_Fantasia";
            command.Parameters.AddWithValue("@Empresa", model.ID_Empresa);

            DataTable Fornecedor = new DataTable();

            con.Open();
            Fornecedor.Load(command.ExecuteReader());
            con.Close();

            return Fornecedor;
        }

        public DataTable ListaCentroCusto(ConsultarReportHabil model)
        {
            SqlCommand command = new SqlCommand();

            command.Connection = con;

            command.CommandText = "select ID_Centro_Custo,Descrição from Centros_Custos where Empresa = 1 order by Codigo";
            //command.Parameters.AddWithValue("@Empresa", model.ID_Empresa);

            DataTable CentroCusto = new DataTable();

            con.Open();
            CentroCusto.Load(command.ExecuteReader());
            con.Close();

            return CentroCusto;
        }

        public DataTable ListaContaCaixa(ConsultarReportHabil model)
        {
            SqlCommand command = new SqlCommand();

            command.Connection = con;

            command.CommandText = "select ID_Conta_Caixa,Código_Conta+ '   ' + Descrição as Descrição from Caixa_Contas where empresa = 1 order by Código_Conta";
            // command.Parameters.AddWithValue("@Empresa", model.ID_Empresa);

            DataTable ContaCaixa = new DataTable();

            con.Open();
            ContaCaixa.Load(command.ExecuteReader());
            con.Close();

            return ContaCaixa;
        }

        public DataTable ListaRelatorio()
        {
            SqlCommand command = new SqlCommand();

            command.Connection = con;

            command.CommandText = "select ID,Nome from Relatorio order by Nome";

            DataTable Relatorio = new DataTable();

            con.Open();
            Relatorio.Load(command.ExecuteReader());
            con.Close();

            return Relatorio;
        }

        public DataTable ListaCliente(ConsultarReportHabil model)
        {
            SqlCommand command = new SqlCommand();

            command.Connection = con;

            command.CommandText = "select ID_Cliente,Cliente_Nome from Cliente where Empresa in (SELECT * FROM STRING_SPLIT(@Empresa, ',')) order by Cliente_Nome";
            command.Parameters.AddWithValue("@Empresa", model.ID_Empresa);

            DataTable Relatorio = new DataTable();

            con.Open();
            Relatorio.Load(command.ExecuteReader());
            con.Close();

            return Relatorio;
        }

        #endregion

        public DataTable RelatorioHabil(ConsultarReportHabil model)
        {
            //List<RetornoRelatorioHabil> relatorio = new List<RetornoRelatorioHabil>();
            DataTable Relatorio = new DataTable();
            DataTable Empresa = new DataTable();
            DataTable ContaBancaria = new DataTable();
            DataTable Fornecedor = new DataTable();
            DataTable CentroCustro = new DataTable();
            DataTable ContaCaixa = new DataTable();
            DataTable Cliente = new DataTable();

            Empresa.Columns.Add("Valor",typeof(int));
            ContaBancaria.Columns.Add("Valor", typeof(int));
            Fornecedor.Columns.Add("Valor", typeof(int));
            CentroCustro.Columns.Add("Valor", typeof(int));
            ContaCaixa.Columns.Add("Valor", typeof(int));
            Cliente.Columns.Add("Valor", typeof(int));

            if (model.ID_Relatorio == 1) // se o relatorio for extrato
            {
              //  model.ID_Empresa = model.ID_Empresa_Get[0].ToString();
             //   model.ID_ContaBancaria = model.ID_ContaBancaria_Get[0].ToString();
                Empresa.Rows.Add(0);
                ContaBancaria.Rows.Add(0);
                Fornecedor.Rows.Add(0);
                CentroCustro.Rows.Add(0);
                ContaCaixa.Rows.Add(0);
                Cliente.Rows.Add(0);
            }
            else
            {
                foreach (string dr in model.ID_Empresa_Get)
                {
                    Empresa.Rows.Add(Convert.ToInt32(dr.ToString()));
                }

                foreach (string dr in model.ID_ContaBancaria_Get)
                {
                    ContaBancaria.Rows.Add(Convert.ToInt32(dr.ToString()));
                }

                //Analisar Fonecedor
                if ((model.ID_Relatorio != 1) && (model.ID_Relatorio != 5))
                {
                    model.ID_Fornecedor = "1";
                    foreach (string dr in model.ID_Fornecedor_Get)
                    {
                        if (Convert.ToInt32(dr.ToString()) == 0)
                        {
                            model.ID_Fornecedor = dr.ToString();
                            break;
                        }
                    }
                    if (string.IsNullOrEmpty(model.ID_Fornecedor) || model.ID_Fornecedor == "1")
                    {
                        foreach (string dr in model.ID_Fornecedor_Get)
                        {
                            Fornecedor.Rows.Add(Convert.ToInt32(dr.ToString()));
                        }
                    }
                }
                //Analisar Centro de Custo   
                model.ID_Centro_Custo = "1";             
                foreach (string dr in model.ID_Centro_Custo_Get)
                {
                    if(Convert.ToUInt32(dr.ToString()) == 0)
                    {
                        model.ID_Centro_Custo = dr.ToString();
                        break;
                    }                    
                }
                if(string.IsNullOrEmpty(model.ID_Centro_Custo) || model.ID_Centro_Custo == "1")
                {
                    foreach (string dr in model.ID_Centro_Custo_Get)
                    {
                        CentroCustro.Rows.Add(Convert.ToInt32(dr.ToString()));
                    }
                }
                //Analise de Conta Caixa
                model.ID_Conta_Caixa = "1";
                foreach (string dr in model.ID_Conta_Caixa_Get)
                {
                    if(Convert.ToInt32(dr.ToString()) == 0)
                    {
                        model.ID_Conta_Caixa = dr.ToString();
                        break;
                    }                    
                }
                if(string.IsNullOrEmpty(model.ID_Conta_Caixa) || model.ID_Conta_Caixa == "1")
                {
                    foreach (string dr in model.ID_Conta_Caixa_Get)
                    {
                        ContaCaixa.Rows.Add(Convert.ToInt32(dr.ToString()));
                    }
                }
                //Analise de Cliente   
                if ((model.ID_Relatorio != 4) && (model.ID_Relatorio != 3) && (model.ID_Relatorio != 1))
                {
                    model.ID_Cliente = "1";
                    foreach (string dr in model.ID_Cliente_Get)
                    {
                        if (Convert.ToInt32(dr.ToString()) == 0)
                        {
                            model.ID_Cliente = dr.ToString();
                            break;
                        }
                    }

                    //if (string.IsNullOrEmpty(model.ID_Cliente))
                    if (model.ID_Cliente == "1")
                    {
                        foreach (string dr in model.ID_Cliente_Get)
                        {
                            Cliente.Rows.Add(Convert.ToInt32(dr.ToString()));
                        }
                    }
                }
            }

            SqlCommand command = new SqlCommand();
            command.Connection = con;

            command.CommandType = CommandType.StoredProcedure;

            command.CommandText = "C#_Relatorio_Habil";

            command.Parameters.AddWithValue("@ID_RELATORIO", model.ID_Relatorio);
            command.Parameters.AddWithValue("@DATAIN", model.Datainicio);
            command.Parameters.AddWithValue("@DATAFIM", model.Datafim);
            command.Parameters.AddWithValue("@ID_EMPRESA_EX", model.ID_Empresa);
            command.Parameters.AddWithValue("ID_CONTABANCARIA_EX", model.ID_ContaBancaria);
            command.Parameters.AddWithValue("@ID_FORNECEDOR_EX",Convert.ToInt32(model.ID_Fornecedor));
            command.Parameters.AddWithValue("@ID_CLIENTE_EX",Convert.ToInt32(model.ID_Cliente));
            command.Parameters.AddWithValue("@ID_CONTACAIXA_EX",Convert.ToInt32(model.ID_Conta_Caixa));
            command.Parameters.AddWithValue("@ID_CENTROCUSTO_EX",Convert.ToInt32(model.ID_Centro_Custo));
            command.Parameters.Add("ID_EMPRESA", SqlDbType.Structured, -1).Value = Empresa; 
            command.Parameters.Add("ID_CONTABANCARIA", SqlDbType.Structured, -1).Value = ContaBancaria;
            command.Parameters.Add("ID_FORNECEDOR", SqlDbType.Structured, -1).Value = Fornecedor;
            command.Parameters.Add("ID_CENTROCUSTO", SqlDbType.Structured, -1).Value = CentroCustro;
            command.Parameters.Add("ID_CONTACAIXA", SqlDbType.Structured, -1).Value = ContaCaixa;
            command.Parameters.Add("ID_CLIENTE", SqlDbType.Structured, -1).Value = Cliente;

            //SqlDataReader ler = command.ExecuteReader();
            con.Open();
            Relatorio.Load(command.ExecuteReader());
            con.Close();

            return Relatorio;
        }

        public string NomeRelatorioHabil(ConsultarReportHabil model)
        {
            SqlCommand command = new SqlCommand();

            command.Connection = con;

            command.CommandText = "select Nome from Relatorio where ID = @ID";

            command.Parameters.AddWithValue("@ID", model.ID_Relatorio);

            con.Open();
            string Relatorio = (string)command.ExecuteScalar();
            con.Close();

            return Relatorio;
        }

    }
}