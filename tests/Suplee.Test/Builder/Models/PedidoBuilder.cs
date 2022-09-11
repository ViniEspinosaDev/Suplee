using Suplee.Vendas.Domain.Enums;
using Suplee.Vendas.Domain.Models;
using System;
using System.Collections.Generic;

namespace Suplee.Test.Builder.Models
{
    public class PedidoBuilder : Pedido
    {
        public PedidoBuilder(Guid usuarioId = default) : base(usuarioId)
        {
        }

        public PedidoBuilder PadraoValido()
        {
            Codigo = "#adwadq2";
            UsuarioId = Guid.NewGuid();
            Status = EPedidoStatus.Rascunho;

            return this;
        }

        public PedidoBuilder ComUsuarioId(Guid usuarioId)
        {
            UsuarioId = usuarioId;

            return this;
        }

        public PedidoBuilder Iniciado()
        {
            Status = EPedidoStatus.Iniciado;

            return this;
        }

        public PedidoBuilder Cancelado()
        {
            Status = EPedidoStatus.Cancelado;

            return this;
        }
        public PedidoBuilder Pago()
        {
            Status = EPedidoStatus.Pago;

            return this;
        }

        public PedidoBuilder Enviado()
        {
            Status = EPedidoStatus.Enviado;

            return this;
        }

        public PedidoBuilder Entregue()
        {
            Status = EPedidoStatus.Entregue;

            return this;
        }

        public PedidoBuilder ComProdutos(List<PedidoProduto> produtos)
        {
            produtos.ForEach(produto => AdicionarProduto(produto));

            return this;
        }

        public Pedido Build() => this;
    }
}
