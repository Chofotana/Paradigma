using Tarefa2.Dominio.Enums;

namespace Tarefa2.Dominio.Entidades
{
    public class Galho
    {
        public Galho() { }
               
        public virtual int Valor { get; set; }

        public virtual int Altura { get; set; }

        /// <summary>
        /// Lado = NULL => Raiz
        /// </summary>
        public virtual LadoEnum? Lado { get; set; }
    }
}
