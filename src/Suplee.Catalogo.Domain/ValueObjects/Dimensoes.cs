namespace Suplee.Catalogo.Domain.ValueObjects
{
    public class Dimensoes
    {
        public Dimensoes() { }
        public Dimensoes(decimal profundidade, decimal altura, decimal largura)
        {
            Profundidade = profundidade;
            Altura = altura;
            Largura = largura;
        }

        public decimal Profundidade { get; protected set; }
        public decimal Altura { get; protected set; }
        public decimal Largura { get; protected set; }
    }
}
