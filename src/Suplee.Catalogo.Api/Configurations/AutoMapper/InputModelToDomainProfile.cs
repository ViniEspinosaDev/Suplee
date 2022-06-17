using AutoMapper;
using Suplee.Catalogo.Api.Controllers.InputModels;
using Suplee.Catalogo.Domain.Models;

namespace Suplee.Catalogo.Api.Configurations.AutoMapper
{
    /// <summary>
    /// Mapeamento dos inputs de entrada para entidade
    /// </summary>
    public class InputModelToDomainProfile : Profile
    {
        /// <summary>
        /// Construtor do mapeamento
        /// </summary>
        public InputModelToDomainProfile()
        {
            CreateMap<CompostoNutricionalInputModel, CompostoNutricional>()
                .ForMember(f => f.Composto, opt => opt.MapFrom(m => m.Composto))
                .ForMember(f => f.Porcao, opt => opt.MapFrom(m => m.Porcao))
                .ForMember(f => f.ValorDiario, opt => opt.MapFrom(m => m.ValorDiario));

            CreateMap<InformacaoNutricionalInputModel, InformacaoNutricional>()
                .ForMember(f => f.Cabecalho, opt => opt.MapFrom(m => m.Cabecalho))
                .ForMember(f => f.Legenda, opt => opt.MapFrom(m => m.Legenda))
                .ForMember(f => f.CompostosNutricionais, opt => opt.MapFrom(m => m.CompostosNutricionais));
        }
    }
}
