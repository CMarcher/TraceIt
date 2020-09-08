using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace TraceIt.Services
{
    public class MessagingService
    {
        string UpdateStandardMessage = "Update standard";

        public enum MessageType
        {
            UpdateStandard
        }

        public MessagingService()
        {

        }

        public void Send(MessageType message) 
            => MessagingCenter.Send(this, GetMessage(message));
        

        public void Subscribe(object subscriber, MessageType message, Action<MessagingService> action)
            => MessagingCenter.Subscribe(subscriber, GetMessage(message), action);
        

        public string GetMessage(MessageType message)
        {
            string finalMessage;

            switch (message)
            {
                case MessageType.UpdateStandard:
                    finalMessage = UpdateStandardMessage;
                    break;
                default:
                    finalMessage = null;
                    break;
            }

            return finalMessage;
        }
    }
}
