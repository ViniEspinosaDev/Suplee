using AutoMapper;
using Suplee.Api.Controllers.Identidade.InputModel;
using Suplee.Api.Controllers.Identidade.InputModels;
using Suplee.Catalogo.Api.Controllers.Catalogo.InputModels;
using Suplee.Catalogo.Domain.Models;
using Suplee.Identidade.Domain.Commands;

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
            MapeiaContextoCatalogo();
            MapeiaContextoIdentidade();
        }

        private void MapeiaContextoIdentidade()
        {
            CreateMap<CadastroUsuarioInputModel, CadastrarUsuarioCommand>()
                .ForMember(f => f.Nome, opt => opt.MapFrom(m => m.Nome))
                .ForMember(f => f.Email, opt => opt.MapFrom(m => m.Email))
                .ForMember(f => f.CPF, opt => opt.MapFrom(m => m.CPF))
                .ForMember(f => f.Celular, opt => opt.MapFrom(m => m.Celular))
                .ForMember(f => f.Senha, opt => opt.MapFrom(m => m.Senha))
                .ForMember(f => f.ConfirmacaoSenha, opt => opt.MapFrom(m => m.ConfirmacaoSenha));

            CreateMap<LoginInputModel, RealizarLoginEmailCommand>()
                .ForMember(f => f.Email, opt => opt.MapFrom(m => m.Email))
                .ForMember(f => f.Senha, opt => opt.MapFrom(m => m.Senha));
        }

        private void MapeiaContextoCatalogo()
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
