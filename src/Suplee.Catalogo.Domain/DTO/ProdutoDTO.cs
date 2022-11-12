using System;
using System.Collections.Generic;

namespace Suplee.Catalogo.Domain.DTO
{
    public class ProdutoDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Composicao { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public decimal Preco { get; set; }
        public Guid CategoriaId { get; set; }
        public Guid InformacaoNutricionalId { get; set; }
        public CategoriaDTO Categoria { get; set; }
        public List<ProdutoImagemDTO> Imagens { get; set; }
        public List<EfeitoDTO> Efeitos { get; set; }
        public InformacaoNutricionalDTO InformacaoNutricional { get; set; }
    }

    public class CategoriaDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }

    public class EfeitoDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
    }

    public class ProdutoImagemDTO
    {
        public Guid Id { get; set;}
        public string NomeImagem { get; set; }
        public string UrlImagemOriginal { get; set; }
        public string UrlImagemReduzida { get; set; }
        public string UrlImagemMaior { get; set; }

    }

    public class InformacaoNutricionalDTO
    {
        public Guid Id { get; set; }
        public string Cabecalho { get; set; }
        public string Legenda { get; set; }
        public List<CompostoNutricionalDTO> CompostosNutricionais { get; set; }
    }

    public class CompostoNutricionalDTO
    {
        public Guid Id { get; set; }
        public string Composto { get; set; }
        public string Porcao { get; set; }
        public string ValorDiario { get; set; }
        public int Ordem { get; set; }
    }
}
