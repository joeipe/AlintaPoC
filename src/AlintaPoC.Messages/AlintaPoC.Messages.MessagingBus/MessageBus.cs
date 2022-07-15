using AlintaPoC.Messages.MessagingBus.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlintaPoC.Messages.MessagingBus
{
    public class MessageBus
    {
        private readonly IBus _bus;

        public MessageBus(IBus bus)
        {
            _bus = bus;
        }

        public void SendMessage(object ev, string topicName)
        {
            _bus.Send(ev.ToString(), topicName);
        }
    }
}
