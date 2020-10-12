using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace TraceIt.Services
{
    public class MessagingService
    {
        string PushStandardMessage = "Push standard changes";
        string RefreshStandardsSourceMessage = "Refresh standards source";
        string RefreshEndorsementsMessage = "Refresh endorsements";
        string RepositoryInitialisationCompleteMessage = "Repository initialisation complete";

        public enum MessageType
        {
            PushStandard,
            RefreshStandards,
            RefreshEndorsements,
            RepositoryInitialisationComplete
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
                case MessageType.PushStandard:
                    finalMessage = PushStandardMessage;
                    break;

                case MessageType.RefreshStandards:
                    finalMessage = RefreshStandardsSourceMessage;
                    break;

                case MessageType.RefreshEndorsements:
                    finalMessage = RefreshEndorsementsMessage;
                    break;

                case MessageType.RepositoryInitialisationComplete:
                    finalMessage = RepositoryInitialisationCompleteMessage;
                    break;

                default:
                    finalMessage = null;
                    break;
            }

            return finalMessage;
        }
    }
}
