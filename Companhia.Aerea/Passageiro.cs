using System;
using System.Text;

namespace Companhia.Aerea
{
    public class Passageiro
    {
        #region [+] Atributos e propriedades

        public int CPF { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Endereco { get; set; }

        public string Telefone { get; set; }

        public int NumeroPassagem { get; set; }

        public int NumeroVoo { get; set; }

        public int? NumeroPoltrona { get; set; }

        public DateTime HorarioVoo { get; set; }

        #endregion

        #region [+] Métodos

        public StringBuilder RetornarDadosPassageiro()
        {
            StringBuilder texto = new StringBuilder();

            texto.AppendLine("\n-----> Dados do passageiro\n");

            texto.AppendFormat("\tNome: {0} {1}\n", Nome, Sobrenome);
            texto.AppendFormat("\tCPF: {0}\n", CPF.ToString().PadLeft(11, '0'));
            texto.AppendFormat("\tEndereço: {0}\n", Endereco);
            texto.AppendFormat("\tNúmero da passagem: {0}\n", NumeroPassagem);
            texto.AppendFormat("\tNúmero da poltrona: {0}\n", NumeroPoltrona);

            return texto;
        }

        #endregion
    }
}