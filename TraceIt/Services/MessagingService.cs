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
        string RefreshDataSourceMessage = "Refresh datasource";

        public enum MessageType
        {
            PushStandard,
            RefreshStandards,
            RefreshEndorsements,
            RepositoryInitialisationComplete,
            RefreshDataSource
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

                case MessageType.RefreshDataSource:
                    finalMessage = RefreshDataSourceMessage;
                    break;

                default:
                    finalMessage = null;
                    break;
            }

            return finalMessage;
        }
    }
}
