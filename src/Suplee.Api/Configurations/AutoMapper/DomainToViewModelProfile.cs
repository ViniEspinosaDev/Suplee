using AutoMapper;
using Suplee.Api.Controllers.Catalogo.ViewModels;
using Suplee.Api.Controllers.Identidade.ViewModels;
using Suplee.Catalogo.Domain.DTO;
using Suplee.Catalogo.Domain.Models;
using Suplee.Identidade.Domain.Models;
using System.Linq;

namespace Suplee.Catalogo.Api.Configurations.AutoMapper
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            MapeiaContextoCatalogo();
            MapeiaContextoIdentidade();
        }

        private void MapeiaContextoIdentidade()
        {
            CreateMap<Usuario, UsuarioViewModel>()
                .ForMember(i => i.UsuarioId, opt => opt.MapFrom(m => m.Id))
                .ForMember(i => i.Nome, opt => opt.MapFrom(m => m.Nome))
                .ForMember(i => i.Email, opt => opt.MapFrom(m => m.Email))
                .ForMember(i => i.CPF, opt => opt.MapFrom(m => m.CPF))
                .ForMember(i => i.Celular, opt => opt.MapFrom(m => m.Celular))
                .ForMember(i => i.Tipo, opt => opt.MapFrom(m => m.Tipo))
                .ForMember(i => i.DataCadastro, opt => opt.MapFrom(m => m.DataCadastro))
                .ForMember(i => i.Enderecos, opt => opt.MapFrom(m => m.Enderecos));

            CreateMap<Endereco, EnderecoViewModel>()
                .ForMember(i => i.EnderecoId, opt => opt.MapFrom(m => m.Id))
                .ForMember(i => i.NomeDestinatario, opt => opt.MapFrom(m => m.NomeDestinatario))
                .ForMember(i => i.CEP, opt => opt.MapFrom(m => m.CEP))
                .ForMember(i => i.Estado, opt => opt.MapFrom(m => m.Estado))
                .ForMember(i => i.Cidade, opt => opt.MapFrom(m => m.Cidade))
                .ForMember(i => i.Bairro, opt => opt.MapFrom(m => m.Bairro))
                .ForMember(i => i.Rua, opt => opt.MapFrom(m => m.Rua))
                .ForMember(i => i.Numero, opt => opt.MapFrom(m => m.Numero))
                .ForMember(i => i.Complemento, opt => opt.MapFrom(m => m.Complemento))
                .ForMember(i => i.TipoLocal, opt => opt.MapFrom(m => m.TipoLocal))
                .ForMember(i => i.Telefone, opt => opt.MapFrom(m => m.Telefone))
                .ForMember(i => i.InformacaoAdicional, opt => opt.MapFrom(m => m.InformacaoAdicional))
                .ForMember(i => i.EnderecoPadrao, opt => opt.MapFrom(m => m.EnderecoPadrao));
        }

        private void MapeiaContextoCatalogo()
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
                .ForMember(i => i.Categoria, opt => opt.MapFrom(m => m.Categoria))
                .ForMember(i => i.Imagens, opt => opt.MapFrom(m => m.Imagens))
                .ForMember(i => i.Efeitos, opt => opt.MapFrom(m => m.Efeitos.ToList().Select(x => x.Efeito)))
                .ForMember(i => i.InformacaoNutricional, opt => opt.MapFrom(m => m.InformacaoNutricional));

            CreateMap<Produto, ProdutoResumidoViewModel>()
                .ForMember(i => i.Id, opt => opt.MapFrom(m => m.Id))
                .ForMember(i => i.Nome, opt => opt.MapFrom(m => m.Nome))
                .ForMember(i => i.Preco, opt => opt.MapFrom(m => m.Preco))
                .ForMember(i => i.NomeCategoria, opt => opt.MapFrom(m => m.Categoria.Nome))
                .ForMember(i => i.NomeEfeito, opt => opt.MapFrom(m => m.Efeitos.Select(x => x.Efeito.Nome)))
                .ForMember(i => i.Imagens, opt => opt.MapFrom(m => m.Imagens))
                .ForMember(i => i.QuantidadeDisponivel, opt => opt.MapFrom(m => m.QuantidadeDisponivel));

            CreateMap<ProdutoImagem, ProdutoImagemViewModel>()
                .ForMember(i => i.NomeImagem, opt => opt.MapFrom(m => m.NomeImagem))
                .ForMember(i => i.UrlImagemOriginal, opt => opt.MapFrom(m => m.UrlImagemOriginal))
                .ForMember(i => i.UrlImagemReduzida, opt => opt.MapFrom(m => m.UrlImagemReduzida))
                .ForMember(i => i.UrlImagemMaior, opt => opt.MapFrom(m => m.UrlImagemMaior));

            CreateMap<Efeito, EfeitoViewModel>()
                .ForMember(i => i.Id, opt => opt.MapFrom(m => m.Id))
                .ForMember(i => i.Nome, opt => opt.MapFrom(m => m.Nome));

            CreateMap<Categoria, CategoriaViewModel>()
                .ForMember(i => i.Id, opt => opt.MapFrom(m => m.Id))
                .ForMember(i => i.Nome, opt => opt.MapFrom(m => m.Nome));

            CreateMap<FreteDTO, FreteViewModel>()
                .ForMember(i => i.Preco, opt => opt.MapFrom(m => m.Preco))
                .ForMember(i => i.PrazoDias, opt => opt.MapFrom(m => m.PrazoDias))
                .ForMember(i => i.DataEstimada, opt => opt.MapFrom(m => m.DataEstimada));
        }
    }
}
