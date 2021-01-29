using System;

namespace Probabilidade {
    class Program {
        static int Fatorial (int num) {
            int fat = num;

            if (num == 0)
                return 1;

            while(num < 0) {
                Console.Write("Digite um número natural: ");
                num = int.Parse(Console.ReadLine());
            }

            for (int x = num - 1; x > 1; x--)
                fat *= x;

            return fat;
        }

        static double Somatoria (int i, int n) {
            double somatoria = 0;
            
            for(int x = i; x <= n; x++) {
                somatoria += (Math.Pow((-1), x) / Fatorial(x));
                // somatoria = {[(-1) ^ x] / x!} + {[(-1) ^ x2] / x2!}... {[(-1) ^ xn] / xn!}
            }
            
            return somatoria;
        }

        static void Main(string[] args) {
            string s = "Análise Combinatória e Probabilidade";
            string escolha = "s";
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            // centraliza o texto horizontalmente
            Console.WriteLine(s);

            while (escolha.ToLower() == "s") {
                try {
                    Console.Write("Deseja calcular a probabilidade diretamente? [S/N] ");
                    escolha = Console.ReadLine();
                    float probabilidade;
                    float casos_favoraveis = 0;
                    float espaco_amostral = 0;

                    if (escolha.ToLower() == "s") {
                        Console.Write("Digite o número de casos favoráveis ao evento desejado: n(E) = ");
                        casos_favoraveis = int.Parse(Console.ReadLine());

                        Console.Write("Digite o número total de possibilidades do evento: n(S) = ");
                        espaco_amostral = int.Parse(Console.ReadLine());

                        probabilidade = casos_favoraveis / espaco_amostral;

                        if (probabilidade != float.Parse(probabilidade.ToString("F")))
                            Console.WriteLine("A probabilidade desse evento ocorrer é aproximadamente de {0}%.", (probabilidade * 100).ToString("F"));
                        else
                            Console.WriteLine("A probabilidade desse evento ocorrer é de {0}%.", (probabilidade * 100).ToString("F"));
                    } else {
                        Console.Write("Digite o número total de elementos do evento: ");
                        int qtd_agrupamento = int.Parse(Console.ReadLine());

                        Console.Write("O número total de casos é somente uma troca de posições, ou seja, é uma permutação? [S/N] ");
                        escolha = Console.ReadLine();

                        if (escolha.ToLower() == "s")
                            espaco_amostral = Permutacao(qtd_agrupamento);
                        else {
                            int total_elementos;
                            Console.Write("Indique o total de elementos que originam o conjunto de possibilidades do evento: ");
                            total_elementos = int.Parse(Console.ReadLine());

                            Console.Write("É um arranjo, ou seja, a ordem importa? [S/N] ");
                            escolha = Console.ReadLine();

                            if(escolha.ToLower() == "s") {
                                // arranjo
                                Console.Write("O conjunto de elementos do evento pode ter repetições? [S/N] ");
                                escolha = Console.ReadLine();

                                if (escolha.ToLower() == "s") // arranjo com repetição
                                    espaco_amostral = Convert.ToInt32(Math.Pow(total_elementos, qtd_agrupamento));
                                else                          // arranjo simples
                                    espaco_amostral = Fatorial(total_elementos) / Fatorial(total_elementos - qtd_agrupamento);
                            } else {
                                // combinação
                                int numero_elementos = total_elementos + qtd_agrupamento - 1;
                                Console.Write("O conjunto de elementos do evento pode ter repetições? [S/N] ");
                                escolha = Console.ReadLine();

                                if (escolha.ToLower() == "s") // combinação com repetição
                                    espaco_amostral = Fatorial(numero_elementos) / (Fatorial(qtd_agrupamento) * Fatorial(numero_elementos - qtd_agrupamento));
                                else                          // combinação simples
                                    espaco_amostral = Fatorial(total_elementos) / (Fatorial(qtd_agrupamento) * Fatorial(total_elementos - qtd_agrupamento));
                            }
                        }

                        Console.WriteLine("O número total de casos possíveis para este evento é igual a {0}.", espaco_amostral);
                    }
                } catch(DivideByZeroException) {
                    Console.WriteLine("Não é possível haver um número de possibilidades totais igual a 0. Tente novamente!!");
                } catch (FormatException) {
                    Console.WriteLine("Você digitou um formato errado no campo. Tente novamente!!");
                } catch (Exception e) {
                    Console.WriteLine("Exceção " + e.ToString());
                }

                Console.Write("Deseja continuar calculando? [S/N] ");
                escolha = Console.ReadLine();
            }

            Console.ReadKey();
        }

        static int Permutacao(int numero_elementos) {
            int permutacao;
            Console.Write("É uma permutação circular, ou seja, o primeiro elemento do conjunto está ao lado do último? [S/N] ");
            string escolha = Console.ReadLine();

            if (escolha.ToLower() == "s") {
                // permutação circular
                permutacao = Fatorial(numero_elementos) / numero_elementos;
            } else {
                Console.Write("É uma permutação caótica, ou seja, os elementos do conjunto não podem ocupar o seu lugar anterior? [S/N] ");
                escolha = Console.ReadLine();

                if (escolha.ToLower() == "s") {
                    // desarranjo
                    permutacao = Convert.ToInt32(Fatorial(numero_elementos) * Somatoria(0, numero_elementos));
                } else {
                    // permutação simples
                    Console.Write("O conjunto de elementos do evento possui repetições? [S/N] ");
                    escolha = Console.ReadLine();

                    if (escolha.ToLower() != "s") {
                        // permutação simples sem repetição
                        permutacao = Fatorial(numero_elementos);
                    } else {
                        // permutação simples com repetição
                        int repeticoes = 1;
                        do {
                            Console.Write("Digite o número de repetições semelhantes: ");
                            repeticoes *= Fatorial(int.Parse(Console.ReadLine()));

                            Console.Write("O conjunto possui mais repetições diferentes? [S/N] ");
                            escolha = Console.ReadLine();
                        } while (escolha.ToLower() == "s");

                        permutacao = Fatorial(numero_elementos) / repeticoes;
                    }
                }
            }

            return permutacao;
        }
    }
}