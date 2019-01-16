using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projetos_CGTI.Models
{
    public class ConsultarReportHabil
    {
        [DisplayName("Data Inicio")]
        [DataType(DataType.Date)]
        public DateTime Datainicio { get; set; }

        [DisplayName("Data Fim")]
        [DataType(DataType.Date)]
        public DateTime Datafim { get; set; }

        [DisplayName("Empresa")]
        public string ID_Empresa { get; set; } // Convertido para String para receber grupo de numeros
        public List<Empresa> listEmpresa { get; set; }
        public string[] ID_Empresa_Get { get; set; }

        [DisplayName("Conta Bancaria")]
        public string ID_ContaBancaria { get; set; }
        public List<ContaBancaria> listContaBancaria { get; set; }
        public string[] ID_ContaBancaria_Get { get; set; }

        [DisplayName("Fornecedor")]
        public string ID_Fornecedor { get; set; }
        public List<Fornecedor> listFornecedor { get; set; }
        public string[] ID_Fornecedor_Get { get; set; }
        
        [DisplayName("Centro de Custo")]
        public string ID_Centro_Custo { get; set; }
        public List<CentroCusto> listCentroCusto { get; set; }
        public string[] ID_Centro_Custo_Get { get; set; }

        [DisplayName("Conta Caixa")]
        public string ID_Conta_Caixa { get; set; }
        public List<ContaCaixa> listContaCaixa { get; set; }
        public string[] ID_Conta_Caixa_Get { get; set; }

        [DisplayName("Tipo de Relatorio")]
        public int ID_Relatorio { get; set; }

        [DisplayName("Cliente")]
        public string ID_Cliente { get; set; }
        public List<Cliente> listCliente { get; set; }
        public string[] ID_Cliente_Get { get; set; }

        public List<Relatorio> listRelatorio { get; set; }
        
        public string DataUltimoUpdate { get; set; }

        public string ReportCabecalho { get; set; }

        public string ReportCorpo { get; set; }


        public ConsultarReportHabil()
        {
            listEmpresa = new List<Empresa>();

            listContaBancaria = new List<ContaBancaria>();

            listFornecedor = new List<Fornecedor>();

            listRelatorio = new List<Relatorio>();

            listContaCaixa = new List<ContaCaixa>();

            listCentroCusto = new List<CentroCusto>();

            listCliente = new List<Cliente>();

        }

        // public string SelEmpresa { get; set; }

        // public SelectList RetornoTeste { get; set; }

    }

    public class RelatorioTelaExelHabil
    {
        public string ReportCabecalho { get; set; }

        public string ReportCorpo { get; set; }

        public string Report_Total { get; set; }

        public byte[] ArrayExcell { get; set; }

        public string NomeArquivo { get; set; }
    }

    public class Empresa
    {
        public int ID { get; set; }

        public string Nome { get; set; }
    }

    public class Fornecedor
    {
        public int ID { get; set; }

        public string Nome { get; set; }
    }

    public class ContaBancaria
    {
        public int ID { get; set; }

        public string ID_Empresa { get; set; }

        public string Nome { get; set; }
    }

    public class ContaCaixa
    {
        public int ID { get; set; }

        public string Nome { get; set; }
    }

    public class CentroCusto
    {
        public int ID { get; set; }

        public string Nome { get; set; }
    }

    public class Relatorio
    {
        public int ID { get; set; }

        public string Nome { get; set; }
    }

    public class RetornoRelatorioHabil
    {
        [DisplayName("Tipo de Relatorio")]
        public string TipoRelatorio { get; set; }

        [DisplayName("Data")]
        public DateTime Data { get; set; }

        [DisplayName("Conta Bancaria")]
        public string ContaBancaria { get; set; }

        [DisplayName("Conta Caixa")]
        public string ContaCaixa { get; set; }

        [DisplayName("Centro de Custo")]
        public string CentroCusto { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [DisplayName("Nome do Cliente")]
        public string NomeCliente { get; set; }

        [DisplayName("Nome do Fornecedor")]
        public string nomefornecedor { get; set; }

        [DisplayName("CPF_CNPJ")]
        public string CPF_CNPJ { get; set; }

        [DisplayName("Documento")]
        public string Documento { get; set; }

        [DisplayName("Valor")]
        public decimal Valor { get; set; }
    }

    public class Cliente
    {
        public int ID { get; set; }
        public string Nome { get; set; }
    }

}