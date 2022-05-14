using System;

namespace Suplee.Core.Data.EventSourcing
{
    public class StoredEvent
    {
        public StoredEvent(Guid id, string tipo, DateTime dataOcorrencia, string dados)
        {
            Id = id;
            Tipo = tipo;
            DataOcorrencia = dataOcorrencia;
            Dados = dados;
        }

        public Guid Id { get; protected set; }
        public string Tipo { get; protected set; }
        public DateTime DataOcorrencia { get; protected set; }
        public string Dados { get; protected set; }
    }
}