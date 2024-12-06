using FeriasTJ.Domain.Entities;

namespace FeriasTJ.Infra.Interface
{
    public interface IRabbitMqEnvia
    {
        public void SendFerias(Ferias ferias);
    }
}
