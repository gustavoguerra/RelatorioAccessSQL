using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Projetos_CGTI.Models
{

    public class SalvarUsuarioViewModel
    {
        [DisplayName("Nome Completo")]
        [Required(ErrorMessage = "Nome")]
        public string Nome { get; set; }

        [DisplayName("CPF")]
        [Required(ErrorMessage = "CPF")]
        //[DataType(DataType.Custom)]
        public string CPF { get; set; }

        [DisplayName("Projeto")]
        [Required(ErrorMessage = "Veiculo")]
        public string Projeto { get; set; }

        [DisplayName("Veiculo e Placa")]
        [Required(ErrorMessage = "Veiculo")]
        public string Veiculo { get; set; }

        [DisplayName("Cargo")]
        [Required(ErrorMessage = "Cargo")]
        public string Cargo { get; set; }

        [DisplayName("Endereço Completo")]
        [Required(ErrorMessage = "Enredeço")]
        public string Endereco { get; set; }
    }


    public class SalvarDespesaVewModel
    {
        [Required(ErrorMessage = "ID")]
        public int Id_Usuario { get; set; }

        [DisplayName("Data")]
        [Required(ErrorMessage = "Data")]
        [DataType(DataType.Date)]
        public DateTime Datalanca { get; set; }

        [DisplayName("Km")]
        [Required(ErrorMessage = "Km")]
        public string km { get; set; }

        [DisplayName("Origem")]
        [Required(ErrorMessage = "Origem")]
        public string Origem { get; set; }

        [DisplayName("Destino")]
        [Required(ErrorMessage = "Destino")]
        public string Destino { get; set; }

        [DisplayName("Locomoção")]
        [Required(ErrorMessage = "Locomoção")]
        public string LocomocaoKm { get; set; }

        [DisplayName("Mudança de Rota")]
        public bool TrocaRota { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Descrição")]
        public string Descricao { get; set; }

        [DisplayName("Alimentação")]
        [Required(ErrorMessage = "Alimentação")]
        public string Alimentacao { get; set; }

        [DisplayName("Estacionamento")]
        [Required(ErrorMessage = "Estacionamento")]
        public string Estacionamento { get; set; }

        [DisplayName("Pedagio")]
        [Required(ErrorMessage = "Pedagio")]
        public string Pedagio { get; set; }

        [DisplayName("Servoço Terceiros")]
        [Required(ErrorMessage = "Servoço Terceiros")]
        public string ServicoTerceiros { get; set; }

        [DisplayName("Materiais")]
        [Required(ErrorMessage = "Materiais")]
        public string Materiais { get; set; }

        [DisplayName("Diversos")]
        [Required(ErrorMessage = "Diversos")]
        public string Diversos { get; set; }

        [DisplayName("Observação")]
        [Required(ErrorMessage = "Observação")]
        public string Obs { get; set; }
    }
    
    public class EditarUsuarioViewModel
    {
        public string id { get; set; }

        [DisplayName("Nome Completo")]
        [Required(ErrorMessage = "Nome")]
        public string Nome { get; set; }

        [DisplayName("CPF")]
        [Required(ErrorMessage = "CPF")]
        //[DataType(DataType.Custom)]
        public string CPF { get; set; }

        [DisplayName("Projeto")]
        [Required(ErrorMessage = "Veiculo")]
        public string Projeto { get; set; }

        [DisplayName("Veiculo")]
        [Required(ErrorMessage = "Veiculo")]
        public string Veiculo { get; set; }

        [DisplayName("Cargo")]
        [Required(ErrorMessage = "Cargo")]
        public string Cargo { get; set; }

        [DisplayName("Endereço")]
        [Required(ErrorMessage = "Enredeço")]
        public string Endereco { get; set; }
    }

    public class CheckUsuario
    {
        public int Id { get; set; }

    }

    public class ModificarDespesaVewModel
    {
        public int Id { get; set; }


    }

    public class ApagarDespesaViewModel
    {

    }




}