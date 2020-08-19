using RabbitMQ.Client;

namespace Tfc.CAP.AliyunAMQP
{
    public class AliyunMechanismFactory : IAuthMechanismFactory
    {
        public IAuthMechanism GetInstance()
        {
            return new AliyunMechanism();
        }

        public string Name => "PLAIN";
    }
}