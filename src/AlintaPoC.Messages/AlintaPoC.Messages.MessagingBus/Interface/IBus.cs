using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlintaPoC.Messages.MessagingBus.Interface
{
    public interface IBus
    {
        void Send(string message, string topicName);
    }
}
