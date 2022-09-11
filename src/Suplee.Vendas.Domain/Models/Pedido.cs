using Suplee.Core.DomainObjects;
using Suplee.Core.Tools;
using Suplee.Vendas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Suplee.Vendas.Domain.Models
{
    public class Pedido : Entity, IAggregateRoot
    {
        private readonly List<PedidoProduto> _produtos;

        public Pedido(Guid usuarioId)
        {
            UsuarioId = usuarioId;
            Codigo = "#" + HashPassword.GenerateRandomCode(6);
            Status = EPedidoStatus.Rascunho;
            DataCadastro = DateTime.Now;
            _produtos = new List<PedidoProduto>();

            CalcularValorTotal();
        }

        public string Codigo { get; protected set; }
        public Guid UsuarioId { get; protected set; }
        public EPedidoStatus Status { get; protected set; }
        public decimal ValorTotal { get; protected set; }
        public DateTime DataCadastro { get; protected set; }
        public IReadOnlyCollection<PedidoProduto> Produtos => _produtos;

        public void CalcularValorTotal()
        {
            ValorTotal = Produtos.Sum(p => p.CalcularValor());
        }

        public bool ProdutoJaExiste(PedidoProduto produto)
        {
            return _produtos.Any(p => p.ProdutoId == produto.ProdutoId);
        }

        public void AdicionarProdutos(List<PedidoProduto> produtos)
        {
            if (produtos is null) return;

            _produtos.RemoveAll(x => true);

            produtos.ForEach(produto => AdicionarProduto(produto));
        }

        public void AdicionarProduto(PedidoProduto produto)
        {
            if (!produto.EstaValido()) return;

            produto.AssociarPedido(Id);

            if (ProdutoJaExiste(produto))
            {
                var produtoExistente = _produtos.FirstOrDefault(p => p.ProdutoId == produto.ProdutoId);
                produtoExistente.AdicionarUnidade(produto.Quantidade);
                produto = produtoExistente;

                _produtos.Remove(produtoExistente);
            }

            produto.CalcularValor();
            _produtos.Add(produto);

            CalcularValorTotal();
        }

        public void RemoverProduto(PedidoProduto produto)
        {
            if (!produto.EstaValido()) return;

            var produtoExistente = _produtos.FirstOrDefault(p => p.ProdutoId == produto.ProdutoId);

            if (produtoExistente == null)
                throw new DomainException("O produto não pertence ao pedido");

            _produtos.Remove(produtoExistente);

            CalcularValorTotal();
        }

        public void AtualizarProduto(PedidoProduto produto)
        {
            if (!produto.EstaValido()) return;

            produto.AssociarPedido(Id);

            var produtoExistente = _produtos.FirstOrDefault(p => p.ProdutoId == produto.ProdutoId);

            if (produtoExistente == null)
                throw new DomainException("O produto não pertence ao pedido");

            _produtos.Remove(produtoExistente);
            _produtos.Add(produto);

            CalcularValorTotal();
        }

        public void TornarRascunho() => Status = EPedidoStatus.Rascunho;
        public void Iniciar() => Status = EPedidoStatus.Iniciado;
        public void Finalizar() => Status = EPedidoStatus.Pago;
        public void Cancelar() => Status = EPedidoStatus.Cancelado;
        public void Entregar() => Status = EPedidoStatus.Entregue;

        public static class PedidoFactory
        {
            public static Pedido NovoPedidoRascunho(Guid usuarioId)
            {
                var pedido = new Pedido(usuarioId);

                return pedido;
            }
        }
    }
}
