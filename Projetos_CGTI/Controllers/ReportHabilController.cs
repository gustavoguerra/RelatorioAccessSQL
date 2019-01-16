using Projetos_CGTI.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projetos_CGTI.Models;
using System.Data;
using System.Text;
using System.IO;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Web.Routing;

namespace Projetos_CGTI.Controllers
{
    public class ReportHabilController : Controller
    {
        // Verifica qual a letra do Alfabeto para planilha
        public static string RetornarCaractere(int index)
        {
            string alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            return alfabeto.ToCharArray()[index].ToString();
        }

        public static DataRow row;

        private void loadEmpresa(ConsultarReportHabil model)
        {
            //carrega empresas e ultima data de Update
            DataTable Empresa = new DataTable();
            ReportHabilDAO report = new ReportHabilDAO();

            Empresa = report.ListaEmpresa();

            var listEmpresa = new List<Empresa>();

            foreach (DataRow dr in Empresa.Rows)
            {
                listEmpresa.Add(new Empresa
                {
                    ID = Convert.ToInt32(dr["Codigo"].ToString()),
                    Nome = dr["Nome_Fantasia"].ToString()
                });
            }
            model.listEmpresa = listEmpresa;
        }

        private void loadContaBancaria(ConsultarReportHabil model)
        {
            DataTable ContaBancaria = new DataTable();
            ReportHabilDAO reporte = new ReportHabilDAO();

            if (model.ID_Relatorio != 1) // verifica qual relarotio
            {
                model.ID_Empresa = string.Join(",", model.ID_Empresa_Get);
            }

            ContaBancaria = reporte.ListaContaBancaria(model); // passar o model para fazer a consulta e retornar o datatable com

            var listaContaBancaria = new List<ContaBancaria>();

            foreach (DataRow dr in ContaBancaria.Rows)
            {
                listaContaBancaria.Add(new ContaBancaria
                {
                    ID = Convert.ToInt32(dr["ID_ContaBancaria"].ToString()),
                    ID_Empresa = dr["Empresa"].ToString(),
                    Nome = dr["Descrição"].ToString()
                });
            }
            model.listContaBancaria = listaContaBancaria;

            // model.listContaBancaria = listaContaBancaria.Where(prop => prop.ID_Empresa == model.ID_Empresa).ToList();
        }

        private void loadFornecedor(ConsultarReportHabil model)
        {
            DataTable Fornecedor = new DataTable();
            ReportHabilDAO report = new ReportHabilDAO();

            Fornecedor = report.ListaFornecedores(model);

            var listFornecedor = new List<Fornecedor>();

            row = Fornecedor.NewRow();

            row["ID_Fornecedor"] = 0;
            row["Nome_Fantasia"] = "Todos";

            Fornecedor.Rows.Add(row);

            foreach (DataRow dr in Fornecedor.Rows)
            {
                listFornecedor.Add(new Fornecedor
                {
                    ID = Convert.ToInt32(dr["ID_Fornecedor"].ToString()),
                    Nome = dr["Nome_Fantasia"].ToString()                  
                });
            }

            model.listFornecedor = listFornecedor;
        }

        private void loadRelatorio(ConsultarReportHabil model)
        {
            DataTable Relatorio = new DataTable();
            ReportHabilDAO report = new ReportHabilDAO();

            Relatorio = report.ListaRelatorio();

            var listRelatorio = new List<Relatorio>();

            listRelatorio.Add(new Relatorio { ID = 0, Nome = "Selecione Relatorio" });

            foreach (DataRow dr in Relatorio.Rows)
            {
                listRelatorio.Add(new Relatorio
                {
                    ID = Convert.ToInt32(dr["ID"].ToString()),
                    Nome = dr["Nome"].ToString()
                });
            }

            model.DataUltimoUpdate = report.ConsultaUltimaAtualizacao();

            model.listRelatorio = listRelatorio;
        }

        private void loadCentroCusto(ConsultarReportHabil model)
        {
            DataTable CentroCusto = new DataTable();
            ReportHabilDAO report = new ReportHabilDAO();

            CentroCusto = report.ListaCentroCusto(model);

            var listCentroCusto = new List<CentroCusto>();

            row = CentroCusto.NewRow();

            row["ID_Centro_Custo"] = 0;
            row["Descrição"] = "Todos";

            CentroCusto.Rows.Add(row);

            foreach (DataRow dr in CentroCusto.Rows)
            {
                listCentroCusto.Add(new CentroCusto
                {
                    ID = Convert.ToInt32(dr["ID_Centro_Custo"].ToString()),
                    Nome = dr["Descrição"].ToString()
                });
            }
            model.listCentroCusto = listCentroCusto;
        }

        private void loadContaCaixa(ConsultarReportHabil model)
        {
            DataTable ContaCaixa = new DataTable();
            ReportHabilDAO report = new ReportHabilDAO();

            ContaCaixa = report.ListaContaCaixa(model);

            var listContaCaixa = new List<ContaCaixa>();

            row = ContaCaixa.NewRow();

            row["ID_Conta_Caixa"] = 0;
            row["Descrição"] = "Todos";

            ContaCaixa.Rows.Add(row);

            foreach (DataRow dr in ContaCaixa.Rows)
            {
                listContaCaixa.Add(new ContaCaixa
                {
                    ID = Convert.ToInt32(dr["ID_Conta_Caixa"].ToString()),
                    Nome = dr["Descrição"].ToString()
                });
            }
            model.listContaCaixa = listContaCaixa;
        }

        private void loadCliente(ConsultarReportHabil model)
        {
            DataTable Cliente = new DataTable();
            ReportHabilDAO report = new ReportHabilDAO();

            Cliente = report.ListaCliente(model);

            var listCliente = new List<Cliente>();

            row = Cliente.NewRow();

            row["ID_Cliente"] = 0;
            row["Cliente_Nome"] = "Todos";

            Cliente.Rows.Add(row);

            foreach (DataRow dr in Cliente.Rows)
            {
                listCliente.Add(new Cliente
                {
                    ID = Convert.ToInt32(dr["ID_Cliente"].ToString()),
                    Nome = dr["Cliente_Nome"].ToString()
                });
            }

            model.listCliente = listCliente;
        }

        // GET: ReportHabil
        [HttpGet]
        public ActionResult ReportHabilView()
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    ConsultarReportHabil listaEmpresa = new ConsultarReportHabil();
                    loadEmpresa(listaEmpresa);
                    loadRelatorio(listaEmpresa);
                    return View(listaEmpresa);
                }
                catch (Exception ex)
                {
                    return View("Error");
                }

            }
            return RedirectToAction("Index", "Home");
        }

        // GET: ReportHabil/HabilReportView
        [NonAction]
        //public ActionResult HabilReportView(ConsultarReportHabil model)
        public RelatorioTelaExelHabil HabilReportView(ConsultarReportHabil model)
        {
            ReportHabilDAO relatorio = new ReportHabilDAO();
            RelatorioTelaExelHabil Tela = new RelatorioTelaExelHabil();
            DataTable tabela = new DataTable();
            StringBuilder htmlCabecalho = new StringBuilder();
            StringBuilder htmlCorpo = new StringBuilder();

            if (model.ID_Relatorio != 1)
            {
                model.ID_Empresa = string.Join(",", model.ID_Empresa_Get);
                model.ID_ContaBancaria = string.Join(",", model.ID_ContaBancaria_Get);
            }

            try
            {
                tabela = relatorio.RelatorioHabil(model);

                // monta o cabeçalho
                //   htmlCabecalho.Append("<thead>");
                htmlCabecalho.Append("<tr>");

                foreach (DataColumn coluna in tabela.Columns)
                {
                    htmlCabecalho.Append("<th>");
                    htmlCabecalho.Append(coluna.ColumnName);
                    htmlCabecalho.Append("</th>");
                }
                htmlCabecalho.Append("</tr>");
                //    htmlCabecalho.Append("</thead>");

                // Preenche com os dados
                //  htmlCorpo.Append("<tbody>");   
                double Total = 0.00;                           
                foreach (DataRow row in tabela.Rows)
                {
                    htmlCorpo.Append("<tr>");
                    foreach (DataColumn coluna in tabela.Columns)
                    {
                        htmlCorpo.Append("<td>");
                        htmlCorpo.Append(row[coluna.ColumnName]);
                        htmlCorpo.Append("</td>");
                        // Calcula o Valor Total da pequisa
                        if(coluna.ColumnName == "Valor")
                        {
                            Total += Convert.ToDouble(row[coluna.ColumnName]);
                        }
                    }
                    htmlCorpo.Append("</tr>");
                }
                //   htmlCorpo.Append("</tbody>");                

                //ViewBag.ReportCabecalho = htmlCabecalho.ToString();
                //ViewBag.ReportCorpo = htmlCorpo.ToString();
                Tela.Report_Total = Total.ToString("C");
                Tela.ReportCabecalho = htmlCabecalho.ToString();
                Tela.ReportCorpo = htmlCorpo.ToString();
            }
            catch (Exception ex)
            {
                // return View();
                RedirectToAction("Erro", ex.ToString());
            }

            return Tela;
        }

        [NonAction]
        //public ActionResult ExportReport(ConsultarReportHabil model)
        public RelatorioTelaExelHabil ExportReport(ConsultarReportHabil model)
        {

            ReportHabilDAO relatorio = new ReportHabilDAO();
            RelatorioTelaExelHabil excel = new RelatorioTelaExelHabil();
            DataTable tabela = new DataTable();
            try
            {
                if (model.ID_Relatorio != 1) // Se nao for Extrato Carregar a lista de empresa e Conta Bancaria
                {
                    model.ID_Empresa = string.Join(",", model.ID_Empresa_Get);
                    model.ID_ContaBancaria = string.Join(",", model.ID_ContaBancaria_Get);
                }
                //  model.ID_Fornecedor = string.Join(",", model.ID_Fornecedor_Get);
                //  model.ID_Conta_Caixa = string.Join(",", model.ID_Conta_Caixa_Get);
                //  model.ID_Centro_Custo = string.Join(",", model.ID_Centro_Custo_Get);

                tabela = relatorio.RelatorioHabil(model);

                string NomeRelatorio = relatorio.NomeRelatorioHabil(model);

                // captura o tamanho da tabela para formatar
                int coluna = 0;
                int linha = 3;
                string LetraValor = "";
                string LetraDebito = "";
                string LetraCredito = "";
                string LetraSaldo = "";
                int NumeroValor = 0;
                int NumeroDebito = 0;
                int NumeroCredito = 0;
                int NumeroSaldo = 0;

                foreach (DataColumn dtc in tabela.Columns)
                {
                    if (dtc.ColumnName == "Valor")
                    {
                        LetraValor = RetornarCaractere(coluna);
                        NumeroValor = coluna;
                    }
                    if (dtc.ColumnName == "Debito")
                    {
                        LetraDebito = RetornarCaractere(coluna);
                        NumeroDebito = coluna;
                    }
                    if (dtc.ColumnName == "Credito")
                    {
                        LetraCredito = RetornarCaractere(coluna);
                        NumeroCredito = coluna;
                    }
                    if (dtc.ColumnName == "Saldo")
                    {
                        LetraSaldo = RetornarCaractere(coluna);
                        NumeroSaldo = coluna;
                    }
                    coluna++;
                }
                coluna++;
                foreach (DataRow dtl in tabela.Rows)
                {
                    linha++;
                }

                // Cria arquivo em memoria
                byte[] byteArray = null;

                using (var package = new ExcelPackage())
                {
                    var ws = package.Workbook.Worksheets.Add("Sheet1");
                    // Coloca cabeçalho no formato
                    ws.Cells["A1"].Value = NomeRelatorio; 
                    ws.Cells["A1"].Style.Font.Bold = true; 
                    ws.Cells["A1"].Style.Font.Size = 25;
                    ws.Cells[1, 1, 1, 3].Merge = true;

                    // Coloca Corpo no formato Tamanho e Font
                    ws.Cells[4, 1, linha, coluna].Style.Font.Name = "Tahoma";
                    ws.Cells[4, 1, linha, coluna].Style.Font.Size = 8;

                    //carrega tabela no excell
                    ws.Cells[3, 1].LoadFromDataTable(tabela, true);


                    //Formata Campo de Saldo
                    if (NumeroSaldo != 0)
                    {
                        ws.Cells[(LetraSaldo + 4) + ":" + (LetraSaldo + linha)].Style.Numberformat.Format = @"_-R$ * #,##0.00_-;-R$ * #,##0.00_-;_-R$ * "" - ""??_-;_-@_-";
                    }

                    //Verifica se tem Campo Valor para calcular total
                    if ((NumeroValor != 0) && (linha != 3))
                    {
                        //Variaveis para fazer calculo
                        string colFormIni = LetraValor + 4;// 4 onde começa na tabela Execel
                        string colFormFim = LetraValor + linha;

                        //formato para todos os campos
                        ws.Cells[colFormIni + ":" + colFormFim].Style.Numberformat.Format = @"_-R$ * #,##0.00_-;-R$ * #,##0.00_-;_-R$ * "" - ""??_-;_-@_-";

                        // Formulas para ultimo campo               
                        ws.Cells[LetraValor + (linha + 1)].Formula = "=SUM(" + colFormIni + ":" + colFormFim + ")";
                        ws.Cells[LetraValor + (linha + 1)].Style.Numberformat.Format = @"_-R$ * #,##0.00_-;-R$ * #,##0.00_-;_-R$ * "" - ""??_-;_-@_-";
                    }

                    //Calculo Total para Debito
                    if (NumeroDebito != 0)
                    {
                        ws.Cells[(LetraDebito + 4) + ":" + (LetraDebito + linha)].Style.Numberformat.Format = @"_-R$ * #,##0.00_-;-R$ * #,##0.00_-;_-R$ * "" - ""??_-;_-@_-";

                        // Formulas para ultimo campo               
                        ws.Cells[LetraDebito + (linha + 1)].Formula = "=SUM(" + (LetraDebito + 4) + ":" + (LetraDebito + linha) + ")";
                        ws.Cells[LetraDebito + (linha + 1)].Style.Numberformat.Format = @"_-R$ * #,##0.00_-;-R$ * #,##0.00_-;_-R$ * "" - ""??_-;_-@_-";
                    }

                    //Calculo Total para Credito
                    if (NumeroCredito != 0)
                    {
                        ws.Cells[(LetraCredito + 4) + ":" + (LetraCredito + linha)].Style.Numberformat.Format = @"_-R$ * #,##0.00_-;-R$ * #,##0.00_-;_-R$ * "" - ""??_-;_-@_-";

                        // Formulas para ultimo campo               
                        ws.Cells[LetraCredito + (linha + 1)].Formula = "=SUM(" + (LetraCredito + 4) + ":" + (LetraCredito + linha) + ")";
                        ws.Cells[LetraCredito + (linha + 1)].Style.Numberformat.Format = @"_-R$ * #,##0.00_-;-R$ * #,##0.00_-;_-R$ * "" - ""??_-;_-@_-";
                    }

                    //Cestraliza o nome e o cabeçalho da tabela
                    ws.Cells[1, 1, 3, coluna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //Alinha as informações do relatorio a esquerda
                    ws.Cells[4, 1, linha + 1, coluna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    //Ajusta tabela para se auto ajustar o tamanho
                    ws.Cells[3, 1, linha + 1, coluna - 1].AutoFitColumns();

                    //Borda fina toda tabela
                    ws.Cells[3, 1, linha, coluna].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    ws.Cells[3, 1, linha, coluna].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    ws.Cells[3, 1, linha, coluna].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    ws.Cells[3, 1, linha, coluna].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    // Ajusta bordas media cabeçalho
                    ws.Cells[3, 1, 3, coluna].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                    ws.Cells[3, 1, 3, coluna].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                    //Borda media geral
                    ws.Cells[3, 1, linha, 1].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                    ws.Cells[linha, 1, linha, coluna].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                    ws.Cells[3, coluna, linha, coluna].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                    ws.Cells[3, coluna, linha, coluna].Style.Border.Left.Style = ExcelBorderStyle.Medium;

                    //tamanho da ultima celula
                    ws.Column(coluna).Width = 3;

                    //byteArray = package.GetAsByteArray();
                    // string fileName = NomeRelatorio + ".xlsx";

                    excel.ArrayExcell = package.GetAsByteArray();
                    excel.NomeArquivo = NomeRelatorio + ".xlsx";

                    //return File(byteArray, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                }
            }
            catch (Exception ex)
            {
                RedirectToAction("Erro", ex.ToString());
            }
            return excel;
        }

        //POST: return combobox and report
        [HttpPost]
        public ActionResult ReportHabilView(ConsultarReportHabil model, string button)
        {
            if (button == "Pesquisa")
            {
                var tela = HabilReportView(model);

                ViewBag.ReportCabecalho = tela.ReportCabecalho;
                ViewBag.ReportCorpo = tela.ReportCorpo;
                ViewBag.Report_Total = tela.Report_Total;
                return View("HabilReportView");
            }
            else
            if (button == "Exportar")
            {
                var excel = ExportReport(model);

                return File(excel.ArrayExcell, System.Net.Mime.MediaTypeNames.Application.Octet, excel.NomeArquivo);
            }
            else
            {
                if (button != "Carregar")
                {
                    model.ID_Empresa = null;
                    loadEmpresa(model);
                    loadRelatorio(model);
                    return View(model);
                }
                else
                {
                    loadEmpresa(model);
                    loadContaBancaria(model);
                    loadFornecedor(model);
                    loadContaCaixa(model);
                    loadCentroCusto(model);
                    loadRelatorio(model);
                    loadCliente(model);
                }
            }
            return View(model);
        }

        // GET: LOAD tabelas do Habil
        public ActionResult LoadHabil()
        {
            try
            {
                ReportHabilDAO report = new ReportHabilDAO();
                List<string> lista = new List<string>();
                report.ResetarTabelas();
                report.Copiar_Arquivo();
                report.Carregar_Lista(lista);

                foreach (string Item in lista)
                {
                    report.IniciarAccess(Item.ToString());
                }

                report.SalvaUltimaAtualizacao();

                return RedirectToAction("ReportHabilView", "ReportHabil");

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.End();
                // return View(ex.ToString());
                return RedirectToAction("ReportHabilView", "ReportHabil");
            }
        }


        // GET: ReportHabil/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: ReportHabil/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: ReportHabil/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: ReportHabil/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReportHabil/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ReportHabil/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReportHabil/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
