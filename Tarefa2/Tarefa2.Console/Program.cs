using System;
using System.Collections.Generic;
using System.Linq;
using Tarefa2.Aplicacao;
using Tarefa2.Dominio.Entidades;
using Tarefa2.Dominio.Enums;

namespace Tarefa2.Console
{
    public static class Program
    {
        /// <summary>
        /// Console apenas para testar a aplicação
        /// </summary>
        public static void Main(string[] args)
        {
            try
            {
                //Cenário 1
                DesenharArvore([3, 2, 1, 6, 0, 5]);
                System.Console.WriteLine();
                System.Console.WriteLine();

                //Cenário 2 
                DesenharArvore([7, 5, 13, 9, 1, 6, 4]);
                System.Console.WriteLine();
                System.Console.WriteLine();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        public static void DesenharArvore(IEnumerable<int> valores)
        {
            System.Console.WriteLine($"Array de entrada: {string.Join(", ", valores)}");
            ControladorGalho controlador = new();
            DesenharArvore(controlador.ObterGalhos(valores));
        }

        public static void DesenharArvore(IEnumerable<Galho> galhos)
        {
            System.Console.WriteLine($"Raiz: {galhos.First(galho => galho.Lado is null).Valor}");
            System.Console.WriteLine($"Galhos da esquerda: {string.Join(", ", galhos.Where(galho => galho.Lado.Equals(LadoEnum.Esquerda)).Select(galho => galho.Valor))}");
            System.Console.WriteLine($"Galhos da direita: {string.Join(", ", galhos.Where(galho => galho.Lado.Equals(LadoEnum.Direita)).Select(galho => galho.Valor))}");

            var maiorAlturaEsquerda = ObterMaiorAltura(galhos.Where(galho => galho.Lado.Equals(LadoEnum.Esquerda))) * 2;
            var maiorAlturaDireita = ObterMaiorAltura(galhos.Where(galho => galho.Lado.Equals(LadoEnum.Direita))) * 2;

            var posicaoInicial = maiorAlturaEsquerda;
            int meio = 1;
            foreach (var grupoAltura in galhos.GroupBy(galho => galho.Altura).OrderBy(grupo => grupo.Key))
            {
                //Raiz
                if (grupoAltura.Key == 0)
                    EscreverLinha(grupoAltura.First().Valor.ToString(), posicaoInicial);
                //Ramificações
                else
                {
                    var galhoEsquerda = grupoAltura.FirstOrDefault(grupo => grupo.Lado.Equals(LadoEnum.Esquerda));
                    var galhoDireita = grupoAltura.FirstOrDefault(grupo => grupo.Lado.Equals(LadoEnum.Direita));

                    if (galhoEsquerda is not null && galhoDireita is not null)
                    {
                        var alturaEsquerda = maiorAlturaEsquerda - (grupoAltura.Key * 2 - 1);

                        Escrever("/" , alturaEsquerda--);
                        EscreverLinha("\\" , meio);
                        meio += 2;

                        Escrever(galhoEsquerda.Valor.ToString(), alturaEsquerda);
                        EscreverLinha(galhoDireita.Valor.ToString(), meio);
                        meio += 2;
                    }
                    else if (galhoEsquerda is not null)
                    {
                        var alturaEsquerda = maiorAlturaEsquerda - (grupoAltura.Key * 2 - 1);

                        EscreverLinha("/", alturaEsquerda--);
                        EscreverLinha(galhoEsquerda.Valor.ToString(), alturaEsquerda);
                    }
                    else if (galhoDireita is not null)
                    {
                        EscreverLinha("\\", posicaoInicial + (grupoAltura.Key * 2 - 1));
                        EscreverLinha(galhoDireita.Valor.ToString(), posicaoInicial + (grupoAltura.Key * 2));
                    }
                }
            }

            static int ObterMaiorAltura(IEnumerable<Galho> galhosLado) =>
                galhosLado.Any() ? galhosLado.MaxBy(galho => galho.Altura).Altura : 0;

            void Escrever(string texto, int quantidadePular) =>
                System.Console.Write(texto.PadLeft(quantidadePular + 1, ' '));

            void EscreverLinha(string texto, int quantidadePular) =>
                System.Console.WriteLine(texto.PadLeft(quantidadePular + 1, ' '));
        }
    }
}
