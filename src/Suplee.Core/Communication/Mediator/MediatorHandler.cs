﻿using MediatR;
using Suplee.Core.Data.EventSourcing;
using Suplee.Core.Messages;
using Suplee.Core.Messages.CommonMessages.DomainEvents;
using Suplee.Core.Messages.CommonMessages.Notifications;
using System.Threading.Tasks;

namespace Suplee.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        //private readonly IEventSourcingRepository _eventSourcingRepository;

        public MediatorHandler(IMediator mediator,
                               IEventSourcingRepository eventSourcingRepository)
        {
            _mediator = mediator;
            //_eventSourcingRepository = eventSourcingRepository;
        }

        public async Task<R> EnviarComando<R>(Command<R> comando)
        {
            return await _mediator.Send(comando);
        }

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
            //await _eventSourcingRepository.SalvarEvento(evento);
        }

        public async Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification
        {
            await _mediator.Publish(notificacao);
        }

        public async Task PublicarDomainEvent<T>(T notificacao) where T : DomainEvent
        {
            await _mediator.Publish(notificacao);
        }
    }
}
