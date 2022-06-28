using AutoMapper;
using Suplee.Catalogo.Api.Controllers.ViewModels;
using Suplee.Catalogo.Domain.Models;
using System.Linq;

namespace Suplee.Catalogo.Api.Configurations.AutoMapper
{
    /// <summary>
    /// Mapeamento das entidades para View Model
    /// </summary>
    public class DomainToViewModelProfile : Profile
    {
        /// <summary>
        /// Construtor do mapeamento
        /// </summary>
        public DomainToViewModelProfile()
        {
            CreateMap<CompostoNutricional, CompostoNutricionalViewModel>()
                .ForMember(i => i.Composto, opt => opt.MapFrom(m => m.Composto))
                .ForMember(i => i.Porcao, opt => opt.MapFrom(m => m.Porcao))
                .ForMember(i => i.ValorDiario, opt => opt.MapFrom(m => m.ValorDiario))
                .ForMember(i => i.Ordem, opt => opt.MapFrom(m => m.Ordem));


            CreateMap<InformacaoNutricional, InformacaoNutricionalViewModel>()
                .ForMember(i => i.Cabecalho, opt => opt.MapFrom(m => m.Cabecalho))
                .ForMember(i => i.Legenda, opt => opt.MapFrom(m => m.Legenda))
                .ForMember(i => i.CompostosNutricionais, opt => opt.MapFrom(m => m.CompostosNutricionais));

            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(i => i.Id, opt => opt.MapFrom(m => m.Id))
                .ForMember(i => i.Nome, opt => opt.MapFrom(m => m.Nome))
                .ForMember(i => i.Descricao, opt => opt.MapFrom(m => m.Descricao))
                .ForMember(i => i.Composicao, opt => opt.MapFrom(m => m.Composicao))
                .ForMember(i => i.QuantidadeDisponivel, opt => opt.MapFrom(m => m.QuantidadeDisponivel))
                .ForMember(i => i.Preco, opt => opt.MapFrom(m => m.Preco))
                .ForMember(i => i.CategoriaId, opt => opt.MapFrom(m => m.CategoriaId))
                .ForMember(i => i.Imagens, opt => opt.MapFrom(m => m.Imagens.ToList().Select(x => x.Url)))
                .ForMember(i => i.Efeitos, opt => opt.MapFrom(m => m.Efeitos.ToList().Select(x => x.EfeitoId.ToString())))
                .ForMember(i => i.InformacaoNutricional, opt => opt.MapFrom(m => m.InformacaoNutricional));

            CreateMap<Produto, ProdutoResumidoViewModel>()
                .ForMember(i => i.Id, opt => opt.MapFrom(m => m.Id))
                .ForMember(i => i.Nome, opt => opt.MapFrom(m => m.Nome))
                .ForMember(i => i.Preco, opt => opt.MapFrom(m => m.Preco))
                .ForMember(i => i.NomeCategoria, opt => opt.MapFrom(m => m.Categoria.Nome))
                .ForMember(i => i.NomeEfeito, opt => opt.MapFrom(m => m.Efeitos.Select(x => x.Efeito.Nome)))
                .ForMember(i => i.Imagem, opt => opt.MapFrom(m => m.Imagens.FirstOrDefault().Url));

        }
    }
}
