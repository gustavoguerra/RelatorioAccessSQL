using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Projetos_CGTI.Models
{
    public class ChequeModel
    {
        public int Id { get; set; }

        [DisplayName("Data*")]
        [Required(ErrorMessage = "Data")]
        [DataType(DataType.Date)]
        public DateTime Datalanca { get; set; }

        [DisplayName("Talao*")]
        [Required(ErrorMessage = "Numero Talão")]
        [MaxLength(10, ErrorMessage = "Máximo {0} caracteres")]
        public string NTalao { get; set; }

        [DisplayName("Cheque*")]
        [Required(ErrorMessage = "Numero Cheque")]
        [MaxLength(10, ErrorMessage = "Máximo {0} caracteres")]
        public string NCheque { get; set; }

        [DisplayName("Referencia*")]
        [Required(ErrorMessage = "Referencia")]
        [StringLength(250, ErrorMessage = "Máximo {0} caracteres")]
        public string Referencia { get; set; }

        [DisplayName("Banco*")]
        [Required(ErrorMessage = "Banco")]
        public string Banco { get; set; }

        [DisplayName("Valor")]
        [DataType(DataType.Currency)]
        public string Valor { get; set; }

        [DisplayName("Favorecido*")]
        [StringLength(250, ErrorMessage = "Máximo {0} caracteres")]
        [Required(ErrorMessage = "Favorecido")]
        public string Favorecido { get; set; }

        [DisplayName("URL*")]
        [Required(ErrorMessage = "Anexar o Arquivo")]
        [DataType(DataType.Url)]
        public string URL { get; set; }
    }
}