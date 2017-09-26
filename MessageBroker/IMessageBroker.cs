using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace ExpertDayDemo
{
    public interface IMessageBroker
    {
        IObservable<string> Messages { get;}

        void QueueMessage(string message);
    }

    public class MessageBroker : IMessageBroker
    {
        public IObservable<string> Messages => MessageSubject;


        private Subject<string> MessageSubject { get; set; } = new Subject<string>();



        public void QueueMessage(string message)
        {
            MessageSubject.OnNext(message);
        }
    }
}
