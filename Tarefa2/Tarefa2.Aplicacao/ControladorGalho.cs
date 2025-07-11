using System;
using System.Collections.Generic;
using System.Linq;
using Tarefa2.Dominio.Entidades;
using Tarefa2.Dominio.Enums;

namespace Tarefa2.Aplicacao
{
    public class ControladorGalho
    {
        public ControladorGalho() { }

        public List<Galho> ObterGalhos(string valores) =>
            ObterGalhos(valores.Split(",").Where(valor => !string.IsNullOrEmpty(valor)).Select(valor => Convert.ToInt32(valor.Trim())));

        public List<Galho> ObterGalhos(IEnumerable<int> valores)
        {
            if (!valores.Any())
                throw new Exception("Precisa de pelo menos 1 valor para gerar a árvore!");

            List<Galho> galhos = [];

            var maiorValor = valores.Max();
            var lado = LadoEnum.Esquerda;
            foreach (var valor in valores)
            {
                if (valor != maiorValor)
                {
                    galhos.Add(new()
                    {
                        Valor = valor,
                        Lado = lado
                    });
                }
                else
                {
                    lado = LadoEnum.Direita;
                    galhos.Add(new()
                    {
                        Valor = valor,
                        Altura = 0,
                        Lado = null
                    });
                }
            }
            OrdenarGalhos(galhos);

            return galhos;
        }

        private void OrdenarGalhos(List<Galho> galhos)
        {
            Ordenar(galhos.Where(galho => galho.Lado.Equals(LadoEnum.Esquerda)));
            Ordenar(galhos.Where(galho => galho.Lado.Equals(LadoEnum.Direita)));

            static void Ordenar(IEnumerable<Galho> galhosLado)
            {
                int altura = 1;
                //'OrderByDescending' para ordenar do galho de maior valor até o menor
                foreach (var galho in galhosLado.OrderByDescending(galho => galho.Valor))
                    galho.Altura = altura++;
            }
        }
    }
}
