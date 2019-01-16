﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projetos_CGTI.Models
{
    public class ConsultaCEPModel
    {
        public int ID { get; set; }

        public string CEP { get; set; }

        public string Endereco { get; set; }

        public string Bairro { get; set; }

        public string Complemento { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string  Pais { get; set; }
    }
}