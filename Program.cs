using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Pessoa alguem = new Pessoa();

            int Menu()
            {
            repetir:
                Console.Clear();
                Console.WriteLine($"Escolha uma ação para {alguem.nome} fazer:");
                Console.WriteLine("0 (dormir) | 1 (desocupar) | 2 (comer) | 3 (correr) | 4 (falar)");
                int op;
                if (int.TryParse(Console.ReadLine(), out op))
                {
                    if (op > 4 || op < 0)
                        goto repetir;
                    else
                        return op;
                }
                else
                    goto repetir;
            } // exibe um menu com as opções de ações que o usuário pode ordenar
            string OrdemEscolhida(int opmenu)
            {
                string[] acoes = { "dormir", "desocupar", "comer", "correr", "falar" };

                string seletor = acoes[opmenu];

                return seletor;
            } // filtra o dado e interpreta como uma ordem para a pessoa virtual
            void MotivacaoEscolhida()
            {
                string[] motivacoes = { "normalmente", "mais", "muito mais", "exageradamente", "desesperadamente" };
                alguem.vontade = motivacoes[alguem.fadiga];
            } // seleciona a motivação com base na ordem dada
            void OcupacaoEscolhida(int opmenu)
            {
                string[] ocupacoes = { "dormindo", "desocupado(a)", "comendo", "correndo", "falando" };
                alguem.ocupacao = ocupacoes[opmenu];
            } // seleciona a ocupação conforme a ordem dada

            Console.WriteLine("Escolha um nome para essa pessoa:");
            alguem.nome = Console.ReadLine();

            do
            {
            Inicio:
                Console.Clear();
                int escolha = Menu();
                string ordem = OrdemEscolhida(escolha);

                if (ordem == "desocupar") // verifica se a ordem é para desocupar
                {
                    if (alguem.ocupacao == "desocupado(a)") // se a pessoa já estiver desocupada...
                    {
                        Console.Clear();
                        Console.WriteLine($"{alguem.nome} já está {alguem.ocupacao}...");
                        Console.ReadKey();
                        goto Inicio;
                    }
                    else // se a pessoa não estiver desocupada...
                    {
                        Console.Clear();
                        alguem.acaoAtual = ordem;
                        alguem.fadiga = 0;
                        OcupacaoEscolhida(escolha);
                        MotivacaoEscolhida();
                        Console.WriteLine($"Agora {alguem.nome} está {alguem.ocupacao}.");
                        Console.ReadKey();
                    }
                }
                else // caso a ordem seja diferente de "desocupar"
                {
                    if (ordem == alguem.acaoAtual) // se for para continuar o que já está fazendo
                    {
                        alguem.fadiga++;
                        if (alguem.fadiga > 4) alguem.saude = "morto"; // testa o limite da motivacao da pessoa
                        else
                        {
                            MotivacaoEscolhida();
                            OcupacaoEscolhida(escolha);
                            Console.Clear();
                            Console.WriteLine($"{alguem.nome} está {alguem.ocupacao} {alguem.vontade}.");
                            Console.ReadKey();
                        }
                    }
                    else // se for pra fazer algo diferente do que está fazendo
                    {
                        if (alguem.ocupacao == "desocupado(a)")
                        {
                            Console.Clear();
                            MotivacaoEscolhida();
                            OcupacaoEscolhida(escolha);
                            alguem.acaoAtual = ordem;
                            Console.WriteLine($"{alguem.nome} agora está {alguem.ocupacao} {alguem.vontade}.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"{alguem.nome} não pode {ordem} enquanto está {alguem.ocupacao}.");
                            Console.ReadKey();
                        }
                    }
                }
            } while (alguem.saude == "vivo" && alguem.acaoAtual != "dormir");

            Console.Clear();
            if (alguem.saude == "vivo") Console.WriteLine($"Vamos deixar {alguem.nome} descansar um pouco agora...");
            else 
            {
                if (alguem.acaoAtual == "comer") 
                    Console.WriteLine($"{alguem.nome} morreu, pois explodiu de tanto comer!");
                else if (alguem.acaoAtual == "correr") 
                    Console.WriteLine($"De tanto correr, {alguem.nome} quebrou as pernas e morreu! ");
                else if (alguem.acaoAtual == "falar")
                    Console.WriteLine($"{alguem.nome} mordeu a língua enquanto falava, teve uma hemorragia e morreu!");
            }
        }
    }
}
