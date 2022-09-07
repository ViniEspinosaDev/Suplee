using System;
using System.Collections.Generic;
using System.Text;

namespace Suplee.Test.Commands.Vendas
{
    // Iniciar pedido. Usuário ao clicar em pagar. Deve iniciar o pedido e realizar pagamento (Não sei se é melhor deixar como
    // Realizar pagamento ou iniciar Pedido.. Não sei
    internal class PagarPedidoCommandTest
    {
        // Validar se usuario tem pedido rascunho (carrinho)

        // Se tiver marcar como pedido iniciado e lançar evento para debitar do estoque

        // Caso dê erro o débito de estoque lançar comando para TornarPedidoUmCarrinho

        // Se der sucesso no debito de estoque lançar comando para RealizarPagamento (Outro contexto (Pagamento))

        // Dando certo o comando de pagamento do outro contexto, lançar evento de PagamentoRealizadoEvent e alterar status do pedido (Cancelado, pago ou enviado)
    }
}
