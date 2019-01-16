using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projetos_CGTI.Models;

namespace Projetos_CGTI.DAO
{
    public class EnderecoDAO
    {
        public ConsultaCEPModel ConsultaCEP(string CEP)
        {
            var ws = new WSCorreios.AtendeClienteClient();
            var resposta = ws.consultaCEP(CEP);
            ConsultaCEPModel model = new ConsultaCEPModel();

            model.Endereco = resposta.end;
            model.Complemento = resposta.complemento;
            model.Bairro = resposta.bairro;
            model.Cidade = resposta.cidade;
            model.Estado = resposta.uf;
            model.CEP = resposta.cep;

            return model;
        }
    }
}