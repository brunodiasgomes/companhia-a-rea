using System;
using System.Collections;
using System.Linq;
using System.Text;
using Companhia.Aerea.Enumeradores;

namespace Companhia.Aerea
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList listaVoos = new ArrayList();
            ConsoleKeyInfo teclaPressionada;

            try
            {
                do
                {
                    Console.WriteLine("---------------------------> EMPRESA AÉREA CRUZEIRO 6/1 <---------------------------\n");
                    Console.WriteLine("-----------> MENU DE OPÇÕES DO VOO\n");
                    Console.WriteLine(" [F1] Lista de Voos");
                    Console.WriteLine(" [F2] Acessar Voo");
                    Console.WriteLine(" [F3] Cadastrar Voo");
                    Console.WriteLine("[ESC] SAIR");

                    teclaPressionada = Console.ReadKey();

                    switch (teclaPressionada.Key)
                    {
                        case ConsoleKey.F1:
                            Console.Clear();

                            StringBuilder textoListaVoos = Voo.ListarVoos(listaVoos);
                            Console.WriteLine(textoListaVoos.ToString());
                            textoListaVoos.Clear();
                            break;

                        case ConsoleKey.F2:
                            Console.Clear();

                            if (listaVoos.Count > 0)
                            {
                                textoListaVoos = Voo.ListarVoos(listaVoos);
                                Console.WriteLine(textoListaVoos.ToString());

                                Console.Write("Selecione o número do voo que deseja acessar: ");

                                int numeroVooSelecionado;

                                if (int.TryParse(Console.ReadLine(), out numeroVooSelecionado))
                                {
                                    Voo vooSelecionado = listaVoos.Cast<Voo>().FirstOrDefault(a => a.NumeroVoo == numeroVooSelecionado);

                                    if (vooSelecionado != null)
                                        ProcessarVoos(vooSelecionado);
                                    else
                                        Console.WriteLine("\n-----> Voo não encontrado.\n");
                                }
                                else
                                    Console.WriteLine("\n-----> Número do voo está no formato incorreto, por favor tente novamente.\n");

                                textoListaVoos.Clear();
                            }
                            else
                                Console.WriteLine("-----> Não possui voos cadastrados no sistema.\n");

                            break;

                        case ConsoleKey.F3:
                            Console.Clear();

                            Voo voo = null;
                            DateTime horarioVoo = DateTime.MinValue;
                            bool dataConvertida = false;

                            Console.WriteLine("Selecione o voo que deseja criar: \n");
                            Console.WriteLine("[F1] BH/Rio");
                            Console.WriteLine("[F2] BH/SP");
                            Console.WriteLine("[F3] BH/Recife");
                            Console.WriteLine("[F12] Cancelar");

                            teclaPressionada = Console.ReadKey();

                            switch (teclaPressionada.Key)
                            {
                                case ConsoleKey.F1:
                                    Console.Clear();

                                    Console.Write("\nInforme a data e horário do voo (no seguinte formato dd/MM/yyyy HH:mm): ");
                                    dataConvertida = DateTime.TryParse(Console.ReadLine(), out horarioVoo);

                                    if (dataConvertida)
                                    {
                                        if (horarioVoo >= DateTime.Now)
                                            voo = new Voo(NomeVooEnum.BHRio);
                                        else
                                            Console.WriteLine("\n-----> Horário do voo não pode ser menor o igual a data atual.\n");
                                    }
                                    else
                                        Console.WriteLine("\n-----> Horário do voo está no formato incorreto, por favor tente novamente.\n");

                                    break;

                                case ConsoleKey.F2:
                                    Console.Clear();

                                    Console.Write("\nInforme a data e horário do voo: \n");
                                    dataConvertida = DateTime.TryParse(Console.ReadLine(), out horarioVoo);

                                    if (dataConvertida)
                                    {
                                        if (horarioVoo >= DateTime.Now)
                                            voo = new Voo(NomeVooEnum.BHRio);
                                        else
                                            Console.WriteLine("\n-----> Horário do voo não pode ser menor o igual a data atual.\n");
                                    }
                                    else
                                        Console.WriteLine("\n-----> Horário do voo está no formato incorreto, por favor tente novamente.\n");

                                    break;

                                case ConsoleKey.F3:
                                    Console.Clear();

                                    Console.Write("\nInforme a data e horário do voo: ");
                                    dataConvertida = DateTime.TryParse(Console.ReadLine(), out horarioVoo);

                                    if (dataConvertida)
                                    {
                                        if (horarioVoo >= DateTime.Now)
                                            voo = new Voo(NomeVooEnum.BHRio);
                                        else
                                            Console.WriteLine("\n-----> Horário do voo não pode ser menor o igual a data atual.\n");
                                    }
                                    else
                                        Console.WriteLine("\n-----> Horário do voo está no formato incorreto, por favor tente novamente.\n");

                                    break;

                                case ConsoleKey.F12:
                                    Console.Clear();
                                    break;

                                default:
                                    Console.Clear();
                                    Console.WriteLine("Opção inválida, tente novamente!\n");
                                    break;
                            }

                            if (teclaPressionada.Key != ConsoleKey.F12)
                            {
                                if (dataConvertida && voo != null && horarioVoo != DateTime.MinValue)
                                {
                                    voo.HorarioVoo = horarioVoo;
                                    listaVoos.Add(voo);
                                    Console.WriteLine("Voo {0} - Número do voo {1} criado com sucesso!\n", voo.NomeVoo, voo.NumeroVoo);
                                }
                            }

                            break;

                        default:
                            Console.Clear();
                            Console.WriteLine("Opção incorreta, por favor insira uma opção válida e tente novamente!\n");
                            break;
                    }
                } while (teclaPressionada.Key != ConsoleKey.Escape);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro no sistema, por favor entre em contato com o administrador do sistema: {0}", ex.Message);
            }
        }

        static void ProcessarVoos(Voo vooSelecionado)
        {
            Passageiro passageiro;
            ConsoleKeyInfo teclaPressionada;

            Console.Clear();

            do
            {
                Console.WriteLine("---------------------------> EMPRESA AÉREA CRUZEIRO 6/1\t\t {0} <---------------------------\n", vooSelecionado.NomeVoo);
                Console.WriteLine("-----------> MENU DE OPÇÕES\n");
                Console.WriteLine(" [F1] Lista de Passageiros");
                Console.WriteLine(" [F2] Pesquisar");
                Console.WriteLine(" [F3] Cadastrar Passageiros");
                Console.WriteLine(" [F4] Excluir Passageiro da Lista");
                Console.WriteLine(" [F5] Mostrar Fila de Espera");
                Console.WriteLine(" [F6] Selecionar outro voo");
                Console.WriteLine("[ESC] SAIR");

                teclaPressionada = Console.ReadKey();

                switch (teclaPressionada.Key)
                {
                    case ConsoleKey.F1:
                        Console.Clear();

                        StringBuilder textoListaVoos = vooSelecionado.ListarPassageiros();
                        Console.WriteLine(textoListaVoos.ToString());
                        textoListaVoos.Clear();
                        break;

                    case ConsoleKey.F2:
                        Console.Clear();

                        int cpf;

                        Console.Write("Informe o CPF do passageiro que deseja buscar: ");

                        if (int.TryParse(Console.ReadLine(), out cpf))
                        {
                            Passageiro passageiroPesquisa = vooSelecionado.Passageiros.Cast<Passageiro>().FirstOrDefault(a => a.CPF == cpf);

                            if (passageiroPesquisa != null)
                            {
                                StringBuilder dadosPassageiro = passageiroPesquisa.RetornarDadosPassageiro();

                                Console.WriteLine(dadosPassageiro.ToString());
                                dadosPassageiro.Clear();
                            }
                            else
                                Console.WriteLine("Passageiro não localizado neste voo!");
                        }
                        else
                        {
                            Console.WriteLine("O CPF do passageiro está no formato incorreto, por favor preencha novamente.");
                            break;
                        }

                        break;

                    case ConsoleKey.F3:
                        Console.Clear();

                        if (vooSelecionado.VerificarSeFilaEsperaPossuiEspaco())
                        {
                            int inteiroAuxiliar;
                            string textoAuxiliar = string.Empty;
                            passageiro = new Passageiro();

                            passageiro.NumeroVoo = vooSelecionado.NumeroVoo;
                            passageiro.HorarioVoo = vooSelecionado.HorarioVoo;
                            passageiro.NumeroPoltrona = Voo.BuscarProximaPoltronaVazia();

                            Console.Write("Informe o CPF do passageiro: ");

                            if (int.TryParse(Console.ReadLine(), out inteiroAuxiliar))
                            {
                                if (vooSelecionado.Passageiros.Cast<Passageiro>().Any(a => a.CPF == inteiroAuxiliar))
                                {
                                    Console.WriteLine("Já existe um passageiro cadastrado no voo com o CPF informado, por favor informe outro CPF válido.");
                                    break;
                                }
                                else
                                    passageiro.CPF = inteiroAuxiliar;
                            }
                            else
                            {
                                Console.WriteLine("O CPF do passageiro está no formato incorreto, por favor preencha novamente.");
                                break;
                            }

                            Console.Write("Informe o nome do passageiro: ");
                            textoAuxiliar = Console.ReadLine();

                            if (!string.IsNullOrWhiteSpace(textoAuxiliar))
                                passageiro.Nome = textoAuxiliar;
                            else
                            {
                                Console.WriteLine("O nome do passageiro é de preenchimento obrigatório.");
                                break;
                            }

                            Console.Write("Informe o sobrenome do passageiro: ");
                            textoAuxiliar = Console.ReadLine();

                            if (!string.IsNullOrWhiteSpace(textoAuxiliar))
                                passageiro.Sobrenome = textoAuxiliar;
                            else
                            {
                                Console.WriteLine("O sobrenome do passageiro é de preenchimento obrigatório.");
                                break;
                            }

                            Console.Write("Informe o endereço do passageiro: ");
                            textoAuxiliar = Console.ReadLine();

                            if (!string.IsNullOrWhiteSpace(textoAuxiliar))
                                passageiro.Endereco = textoAuxiliar;
                            else
                            {
                                Console.WriteLine("O endereço do passageiro é de preenchimento obrigatório.");
                                break;
                            }

                            Console.Write("Informe o telefone do passageiro: ");
                            textoAuxiliar = Console.ReadLine();

                            if (!string.IsNullOrWhiteSpace(textoAuxiliar))
                                passageiro.Telefone = textoAuxiliar;
                            else
                            {
                                Console.WriteLine("O telefone do passageiro é de preenchimento obrigatório.");
                                break;
                            }

                            Console.Write("Informe o número da passagem do passageiro: ");

                            if (int.TryParse(Console.ReadLine(), out inteiroAuxiliar))
                            {
                                if (vooSelecionado.Passageiros.Cast<Passageiro>().Any(a => a.CPF == inteiroAuxiliar))
                                {
                                    Console.WriteLine("Já existe um passageiro cadastrado no voo com o número de passagem informado, por favor informe outro número de passagem válido.");
                                    break;
                                }
                                else
                                    passageiro.NumeroPassagem = inteiroAuxiliar;
                            }
                            else
                            {
                                Console.WriteLine("O número da passagem do passageiro está no formato incorreto, por favor tente novamente.");
                                break;
                            }

                            Console.WriteLine(vooSelecionado.AdicionarPassageiroVoo(passageiro));
                        }
                        else
                            Console.WriteLine("\nA reserva do voo não pode ser feita, pois o voo e a lista de espera estão completos!\n");

                        break;

                    case ConsoleKey.F4:
                        Console.Clear();

                        int cpfExcluir;

                        Console.Write("Informe o CPF do passageiro que deseja cancelar o voo: ");

                        if (int.TryParse(Console.ReadLine(), out cpfExcluir))
                        {
                            Passageiro passageiroExcluir = vooSelecionado.Passageiros.Cast<Passageiro>().FirstOrDefault(a => a.CPF == cpfExcluir);
                            StringBuilder mensagemExclusao = vooSelecionado.ExcluirPassageiro(passageiroExcluir);

                            Console.WriteLine(mensagemExclusao.ToString());
                            mensagemExclusao.Clear();
                        }
                        else
                        {
                            Console.WriteLine("O CPF do passageiro está no formato incorreto, por favor preencha novamente.");
                            break;
                        }
                        break;

                    case ConsoleKey.F5:
                        Console.Clear();

                        StringBuilder textoFilaEspera = vooSelecionado.ListarFilaEspera();
                        Console.WriteLine(textoFilaEspera.ToString());
                        textoFilaEspera.Clear();
                        break;

                    case ConsoleKey.F6:
                        Console.Clear();

                        return;

                    default:
                        Console.Clear();
                        Console.WriteLine("\nOpção incorreta, por favor insira uma opção válida e tente novamente!");
                        break;
                }
            } while (teclaPressionada.Key != ConsoleKey.Escape);
        }
    }
}