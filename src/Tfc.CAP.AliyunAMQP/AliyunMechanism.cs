using System;
using System.Text;
using RabbitMQ.Client;

namespace Tfc.CAP.AliyunAMQP
{
    public class AliyunMechanism : IAuthMechanism
    {
        public byte[] handleChallenge(byte[] challenge, IConnectionFactory factory)
        {
            if (!(factory is ConnectionFactory))
            {
                throw new InvalidCastException("need ConnectionFactory");
            }

            ConnectionFactory cf = factory as ConnectionFactory;
            return Encoding.UTF8.GetBytes("\0" + getUserName(cf) + "\0" + AliyunUtils.GetPassword(cf.Password));
        }

        private string getUserName(ConnectionFactory cf)
        {
            string instanceId;
            try
            {
                string[] sArray = cf.HostName.Split('.');
                instanceId = sArray[0];
            }
            catch (Exception e)
            {
                throw new InvalidProgramException("hostName invalid", e);
            }

            return AliyunUtils.GetUserName(cf.UserName, instanceId);
        }
    }
}