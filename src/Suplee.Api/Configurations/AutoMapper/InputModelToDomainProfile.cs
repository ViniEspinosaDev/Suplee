using AutoMapper;
using Suplee.Api.Controllers.Identidade.InputModel;
using Suplee.Api.Controllers.Identidade.InputModels;
using Suplee.Api.Controllers.Vendas.InputModels;
using Suplee.Catalogo.Api.Controllers.Catalogo.InputModels;
using Suplee.Catalogo.Domain.Models;
using Suplee.Core.Tools;
using Suplee.Identidade.Domain.Autenticacao.Commands;
using Suplee.Identidade.Domain.Identidade.Commands;
using Suplee.Identidade.Domain.Models;
using Suplee.Vendas.Domain.Commands;
using System;
using static Suplee.Vendas.Domain.Commands.CadastrarCarrinhoCommand;

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
            MapeiaContextoVendas();
        }

        private void MapeiaContextoVendas()
        {
            CreateMap<CadastrarCarrinhoInputModel, CadastrarCarrinhoCommand>()
                .ForMember(f => f.UsuarioId, opt => opt.MapFrom(m => m.UsuarioId))
                .ForMember(f => f.Produtos, opt => opt.MapFrom(m => m.Produtos));

            CreateMap<CarrinhoProdutoInputModel, CadastrarCarrinhoCommandProduto>()
                .ForMember(f => f.ProdutoId, opt => opt.MapFrom(m => m.ProdutoId))
                .ForMember(f => f.NomeProduto, opt => opt.MapFrom(m => m.NomeProduto))
                .ForMember(f => f.Quantidade, opt => opt.MapFrom(m => m.Quantidade))
                .ForMember(f => f.ValorUnitario, opt => opt.MapFrom(m => m.ValorUnitario));

            CreateMap<InserirProdutoCarrinhoInputModel, InserirProdutoCarrinhoCommand>()
                .ForMember(f => f.UsuarioId, opt => opt.MapFrom(m => m.UsuarioId))
                .ForMember(f => f.ProdutoId, opt => opt.MapFrom(m => m.ProdutoId))
                .ForMember(f => f.NomeProduto, opt => opt.MapFrom(m => m.NomeProduto))
                .ForMember(f => f.Quantidade, opt => opt.MapFrom(m => m.Quantidade))
                .ForMember(f => f.ValorUnitario, opt => opt.MapFrom(m => m.ValorUnitario));

            CreateMap<AtualizarProdutoCarrinhoInputModel, AtualizarProdutoCarrinhoCommand>()
                .ForMember(f => f.UsuarioId, opt => opt.MapFrom(m => m.UsuarioId))
                .ForMember(f => f.ProdutoId, opt => opt.MapFrom(m => m.ProdutoId))
                .ForMember(f => f.Quantidade, opt => opt.MapFrom(m => m.Quantidade));

            CreateMap<ExcluirProdutoCarrinhoInputModel, ExcluirProdutoCarrinhoCommand>()
                .ForMember(f => f.UsuarioId, opt => opt.MapFrom(m => m.UsuarioId))
                .ForMember(f => f.ProdutoId, opt => opt.MapFrom(m => m.ProdutoId));

            CreateMap<RealizarPagamentoInputModel, IniciarPedidoCommand>()
                .ForMember(f => f.UsuarioId, opt => opt.MapFrom(m => m.UsuarioId))
                .ForMember(f => f.Sucesso, opt => opt.MapFrom(m => m.Sucesso));
        }

        private void MapeiaContextoIdentidade()
        {
            // Autenticação
            CreateMap<LoginEmailInputModel, RealizarLoginEmailCommand>()
                .ForMember(f => f.Email, opt => opt.MapFrom(m => m.Email.Trim()))
                .ForMember(f => f.Senha, opt => opt.MapFrom(m => m.Senha.Trim()));

            CreateMap<LoginCPFInputModel, RealizarLoginCPFCommand>()
                .ForMember(f => f.CPF, opt => opt.MapFrom(m => m.CPF.Trim()))
                .ForMember(f => f.Senha, opt => opt.MapFrom(m => m.Senha.Trim()));

            // Identidade
            CreateMap<CadastroUsuarioInputModel, CadastrarUsuarioCommand>()
                .ForMember(f => f.Nome, opt => opt.MapFrom(m => m.Nome.Trim()))
                .ForMember(f => f.Email, opt => opt.MapFrom(m => m.Email.Trim()))
                .ForMember(f => f.CPF, opt => opt.MapFrom(m => m.CPF.Trim()))
                .ForMember(f => f.Celular, opt => opt.MapFrom(m => m.Celular.Trim()))
                .ForMember(f => f.Senha, opt => opt.MapFrom(m => m.Senha.Trim()))
                .ForMember(f => f.ConfirmacaoSenha, opt => opt.MapFrom(m => m.ConfirmacaoSenha.Trim()));

            CreateMap<AlterarSenhaInputModel, AlterarSenhaCommand>()
                .ForMember(f => f.UsuarioId, opt => opt.MapFrom(m => m.UsuarioId))
                .ForMember(f => f.CodigoConfirmacao, opt => opt.MapFrom(m => m.CodigoConfirmacao.Trim()))
                .ForMember(f => f.Senha, opt => opt.MapFrom(m => m.Senha.Trim()))
                .ForMember(f => f.ConfirmacaoSenha, opt => opt.MapFrom(m => m.ConfirmacaoSenha.Trim()));

            CreateMap<EditarUsuarioInputModel, EditarUsuarioCommand>()
                .ForMember(f => f.UsuarioId, opt => opt.MapFrom(m => ConverterTextoEmGuid(m.UsuarioId.Trim())))
                .ForMember(f => f.Nome, opt => opt.MapFrom(m => m.Nome.Trim()))
                .ForMember(f => f.Celular, opt => opt.MapFrom(m => m.Celular.Trim()))
                .ForMember(f => f.Enderecos, opt => opt.MapFrom(m => m.Enderecos));

            CreateMap<EnderecoInputModel, Endereco>()
                .ForMember(f => f.Id, opt => opt.MapFrom(m => ConverterTextoEmGuid(m.Id.Trim())))
                .ForMember(f => f.UsuarioId, opt => opt.MapFrom(m => ConverterTextoEmGuid(m.UsuarioId.Trim())))
                .ForMember(f => f.NomeDestinatario, opt => opt.MapFrom(m => m.NomeDestinatario.Trim()))
                .ForMember(f => f.CEP, opt => opt.MapFrom(m => m.CEP.FormatarCEPApenasNumeros()))
                .ForMember(f => f.Estado, opt => opt.MapFrom(m => m.Estado.Trim()))
                .ForMember(f => f.Cidade, opt => opt.MapFrom(m => m.Cidade.Trim()))
                .ForMember(f => f.Bairro, opt => opt.MapFrom(m => m.Bairro.Trim()))
                .ForMember(f => f.Rua, opt => opt.MapFrom(m => m.Rua.Trim()))
                .ForMember(f => f.Numero, opt => opt.MapFrom(m => m.Numero.Trim()))
                .ForMember(f => f.Complemento, opt => opt.MapFrom(m => m.Complemento.Trim()))
                .ForMember(f => f.TipoLocal, opt => opt.MapFrom(m => m.TipoLocal))
                .ForMember(f => f.Telefone, opt => opt.MapFrom(m => m.Telefone.Trim()))
                .ForMember(f => f.InformacaoAdicional, opt => opt.MapFrom(m => m.InformacaoAdicional.Trim()));
        }

        private void MapeiaContextoCatalogo()
        {
            CreateMap<CompostoNutricionalInputModel, CompostoNutricional>()
                .ForMember(f => f.Composto, opt => opt.MapFrom(m => m.Composto.Trim()))
                .ForMember(f => f.Porcao, opt => opt.MapFrom(m => m.Porcao.Trim()))
                .ForMember(f => f.ValorDiario, opt => opt.MapFrom(m => m.ValorDiario.Trim()));

            CreateMap<InformacaoNutricionalInputModel, InformacaoNutricional>()
                .ForMember(f => f.Cabecalho, opt => opt.MapFrom(m => m.Cabecalho.Trim()))
                .ForMember(f => f.Legenda, opt => opt.MapFrom(m => m.Legenda.Trim()))
                .ForMember(f => f.CompostosNutricionais, opt => opt.MapFrom(m => m.CompostosNutricionais));
        }

        private Guid ConverterTextoEmGuid(string texto)
        {
            Guid.TryParse(texto, out Guid guid);

            return guid;
        }
    }
}
