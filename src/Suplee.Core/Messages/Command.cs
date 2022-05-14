using FluentValidation.Results;
using MediatR;
using System;

namespace Suplee.Core.Messages
{
    public abstract class Command : Message, IRequest<bool>
    {
        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; protected set; }
        public ValidationResult ValidationResult { get; set; }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
