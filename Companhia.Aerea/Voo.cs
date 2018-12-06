using System;
using System.Collections;
using System.Linq;
using System.Text;
using Companhia.Aerea.Enumeradores;

namespace Companhia.Aerea
{
    public class Voo
    {
        #region [+] Construtores

        public Voo(NomeVooEnum nomeVoo)
        {
            TipoVoo = nomeVoo;

            _numeroVooContador++;
            NumeroVoo = _numeroVooContador;

            Passageiros = new ArrayList();
            FilaEspera = new Queue();
        }

        #endregion

        #region [+] Atributos e propriedades

        /// <summary>
        /// Atributo estático para armazenar o último número do voo
        /// </summary>
        private static int _numeroVooContador;

        /// <summary>
        /// Número do voo da classe
        /// </summary>
        public int NumeroVoo { get; }

        private static int _numeroPoltronas;

        /// <summary>
        /// Propriedade que guarda o tipo do voo para melhor identificação no sistema (BH/Rio, BH/SP, BH/Recife)
        /// </summary>
        public NomeVooEnum TipoVoo { get; }

        /// <summary>
        /// Propriedade para retornar o nome do voo
        /// </summary>
        public string NomeVoo
        {
            get
            {
                switch (TipoVoo)
                {
                    case NomeVooEnum.BHRio:
                        return "BH/Rio";

                    case NomeVooEnum.BHSP:
                        return "BH/SP";

                    case NomeVooEnum.BHRecife:
                        return "BH/Recife";

                    default:
                        return string.Empty;
                }
            }
        }

        /// <summary>
        /// Propriedade que possui o nome da cidade de origem
        /// </summary>
        public string CidadeOrigem
        {
            get
            {
                switch (TipoVoo)
                {
                    case NomeVooEnum.BHRio:
                    case NomeVooEnum.BHSP:
                    case NomeVooEnum.BHRecife:
                        return "Belo Horizonte";

                    default:
                        return string.Empty;
                }
            }
        }

        /// <summary>
        /// Propriedade que possui o nome da cidade de destino
        /// </summary>
        public string CidadeDestino
        {
            get
            {
                switch (TipoVoo)
                {
                    case NomeVooEnum.BHRio:
                        return "Rio de Janeiro";

                    case NomeVooEnum.BHSP:
                        return "São Paulo";

                    case NomeVooEnum.BHRecife:
                        return "Recife";

                    default:
                        return string.Empty;
                }
            }
        }

        /// <summary>
        /// Proprieade que armazena a data e horário do voo
        /// </summary>
        public DateTime HorarioVoo { get; set; }

        /// <summary>
        /// Lista com os passageiros do voo
        /// </summary>
        public ArrayList Passageiros { get; set; }

        /// <summary>
        /// Fila com os passageiros na espera do voo
        /// </summary>
        public Queue FilaEspera { get; set; }

        #endregion

        #region [+] Métodos


        public static StringBuilder ListarVoos(ArrayList listaVoos)
        {
            StringBuilder texto = new StringBuilder();

            texto.AppendLine("-----------> Lista de Voos\n");

            if (listaVoos.Count > 0)
            {
                foreach (var voo in listaVoos.Cast<Voo>().GroupBy(a => new { a.NomeVoo, a.CidadeOrigem, a.CidadeDestino }))
                {
                    texto.AppendFormat("\n-----> Números Voo {0} - Cidade Origem {1} - Cidade Destino {2}:\n", voo.Key.NomeVoo, voo.Key.CidadeOrigem, voo.Key.CidadeDestino);

                    foreach (var detalhesVoo in voo.Select(s => new { s.NumeroVoo, s.HorarioVoo }))
                    {
                        texto.AppendFormat("-----> Número do Voo: {0} - Horário do voo: {1}\n", detalhesVoo.NumeroVoo, detalhesVoo.HorarioVoo.ToString("dd/MM/yyyy HH:mm"));
                    }
                }
            }
            else
                texto.AppendLine("-----> Não possui voos cadastrados no sistema.\n");

            return texto;
        }

        public StringBuilder ListarFilaEspera()
        {
            StringBuilder texto = new StringBuilder();

            texto.AppendLine("-----------> Fila de Espera de Voos\n");

            if (FilaEspera.Count > 0)
            {
                int contador = 1;

                foreach (Passageiro passageiro in FilaEspera.Cast<Passageiro>())
                {
                    texto.AppendFormat("\n-----> {0} posição: {1} {2}\n", contador, passageiro.Nome, passageiro.Sobrenome);

                    contador++;
                }
            }
            else
                texto.AppendLine("-----> A fila de espera de voos está vazia.\n");

            return texto;
        }

        public StringBuilder ListarPassageiros()
        {
            StringBuilder texto = new StringBuilder();

            texto.AppendFormat("-----------> Lista de Passageiros do Voo\n", NomeVoo, NumeroVoo);

            if (Passageiros.Count > 0)
            {
                foreach (Passageiro passageiro in Passageiros.Cast<Passageiro>())
                {
                    texto.AppendFormat("\n-----> Passageiro: {0} {1}\n", passageiro.Nome, passageiro.Sobrenome);
                    texto.AppendFormat("\tCPF: {0}\n", passageiro.CPF.ToString().PadLeft(11, '0'));
                    texto.AppendFormat("\tNúmero da passagem: {0}\n", passageiro.NumeroPassagem);
                    texto.AppendFormat("\tNúmero da poltrona: {0}\n", passageiro.NumeroPoltrona);
                }
            }
            else
                texto.AppendLine("\n-----> Não possui passageiros cadastrados no voo.\n");

            return texto;
        }

        public StringBuilder ExcluirPassageiro(Passageiro passageiro)
        {
            StringBuilder mensagemRetorno = new StringBuilder();

            if (passageiro != null)
            {
                Passageiros.Remove(passageiro);

                mensagemRetorno.AppendFormat("\n-----> O passageiro {0} {1} foi removido com sucesso do voo.\n", passageiro.Nome, passageiro.Sobrenome);

                if (FilaEspera.Count > 0)
                {
                    Passageiro passageiroFilaEspera = FilaEspera.Dequeue() as Passageiro;

                    passageiroFilaEspera.NumeroPoltrona = passageiro.NumeroPoltrona;

                    Passageiros.Add(passageiroFilaEspera);

                    mensagemRetorno.AppendFormat("\n-----> O passageiro {0} {1} saiu da fila de espera e foi adicionado no voo {2}, com horário de {3} e a poltrona de número {4}.\n", 
                                                 passageiroFilaEspera.Nome, passageiroFilaEspera.Sobrenome, passageiroFilaEspera.NumeroVoo, 
                                                 passageiroFilaEspera.HorarioVoo, passageiroFilaEspera.NumeroPoltrona);
                }
            }
            else
                mensagemRetorno.AppendLine("\n-----> O passageiro informado não foi localizado no voo.\n");

            return mensagemRetorno;
        }

        public static int? BuscarProximaPoltronaVazia()
        {
            if (_numeroPoltronas <= 5)
            {
                _numeroPoltronas++;
                return _numeroPoltronas;
            }

            return null;
        }

        public string AdicionarPassageiroVoo(Passageiro passageiro)
        {
            string mensagemRetorno = string.Empty;

            if (Passageiros.Count < 5)
            {
                Passageiros.Add(passageiro);
                mensagemRetorno = "\nPassageiro adicionado no voo com sucesso!\n";
            }
            else if (FilaEspera.Count < 5)
            {
                FilaEspera.Enqueue(passageiro);
                mensagemRetorno = string.Format("\nO número de passageiros do voo está completo, por esse motivo você foi adicionado na {0} posição da lista de espera e caso algum passageiro desista do voo você poderá ser incluído.\n",
                                                FilaEspera.Count);
            }

            return mensagemRetorno;
        }

        public bool VerificarSeFilaEsperaPossuiEspaco()
        {
            return FilaEspera.Count < 5;
        }

        #endregion
    }
}