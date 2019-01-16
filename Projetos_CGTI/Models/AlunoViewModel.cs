using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projetos_CGTI.Models
{
    public class AlunoViewModel
    {
        public int ID { get; set; }

        [DisplayName("Nome")]
        public String Nome_Aluno { get; set; }

        [DisplayName("Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DataNascimento { get; set; }

        [DisplayName("Sexo")]
        public string Sexo { get; set; }
        public List<SelectListItem> SexoList { get; set; }

        [DisplayName("E-Mail do Aluno")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Peso")]
        public string Peso { get; set; }

        [DisplayName("Altura")]
        public string Alttura { get; set; }

        [DisplayName("RG")]
        public string RG { get; set; }

        [DisplayName("CPF")]
        public string CPF { get; set; }

        [DisplayName("Deficiencia")]
        public string Deficiencia { get; set; }

        [DisplayName("Problema de Saúde")]
        public string Problema_Saude { get; set; }

        [DisplayName("Nome da Escola")]
        public string Nome_Escola { get; set; }
        
        [DisplayName("Série")]
        public string Seri { get; set; }
        public List<SelectListItem> SeieList { get; set; }

        [DisplayName("Tipo da Escola")]
        public string Tipo_Escola { get; set; }
        public List<SelectListItem> EscolaList { get; set; }

        [DisplayName("Período")]
        public string Periodo { get; set; }
        public List<SelectListItem> PeriodoList { get; set; }

        [DisplayName("Tamanho da Camiseta")]
        public string T_Camiseta{ get; set; }
        public List<SelectListItem> CamisetaLista { get; set; }

        [DisplayName("Tamanho do Short")]
        public string T_Short { get; set; }
        public List<SelectListItem> ShortLista { get; set; }

        [DisplayName("Tamanho do Tênis")]
        public string T_Tenis { get; set; }
        public List<SelectListItem> TenisList { get; set; }

        [DisplayName("Nome do Responsável")]
        public string Nome_responsavel { get; set; }

        [DisplayName("RG")]
        public string RG_Responsagel { get; set; }

        [DisplayName("Grau de Parentesco")]
        public string Parentesco { get; set; }
        public List<SelectListItem> ParentescoList { get; set; }

        [DisplayName("E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email_Responsavel { get; set; }

        [DisplayName("Endereço")]
        public string Endereco { get; set; }

        [DisplayName("Número")]
        public string Numero { get; set; }

        [DisplayName("Bairro")]
        public string Bairro { get; set; }

        [DisplayName("Cep")]
        public string CEP { get; set; }

        [DisplayName("Cidade")]
        public string Cidade { get; set; }

        [DisplayName("Estado")]
        public string Estado { get; set; }

        [DisplayName("Telefone Residêncial")]
        public string Telefone { get; set; }

        [DisplayName("Celular")]
        public string Celular { get; set; }

        [DisplayName("Projeto")]
        public string Nome_Projeto { get; set; }
        public List<Projeto> listProjeto { get; set; }

        public AlunoViewModel()
        {
            listProjeto = new List<Projeto>();
        }
    }

    public class Projeto
    {
        public int ID { get; set; }

        public string Nome { get; set; }
    }
}