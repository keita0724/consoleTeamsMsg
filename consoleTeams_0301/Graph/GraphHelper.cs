using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace consoleTeams_0301
{
    public interface GraphHelper
    {
        private static GraphServiceClient graphClient;
        public static void Initialize(IAuthenticationProvider authProvider)
        {
            graphClient = new GraphServiceClient(authProvider);
        }

        public static async Task<ChatMessage> PostMessageAsync()
        {
            try
            {
                var chatMessage = new ChatMessage
                {
                    Body = new ItemBody
                    {
                        Content = "Hello world"
                    }
                };

                return await graphClient.Chats["19:860caee54eaa4f2e822d38350c07b7fa@thread.v2"].Messages
                    .Request()
                    .AddAsync(chatMessage);
            }
            catch (ServiceException ex)
            {
                Console.WriteLine($"Error getting signed-in user: {ex.Message}");
                return null;
            }
        }

        public static async Task<IChatMessagesCollectionPage> GetMessageAsync()
        {
            try
            {
                var messages = await graphClient.Me.Chats["19:860caee54eaa4f2e822d38350c07b7fa@thread.v2"].Messages
                    .Request()
                    .GetAsync();

                return messages;
            }
            catch (ServiceException ex)
            {
                Console.WriteLine($"Error getting signed-in user: {ex.Message}");
                return null;
            }
        }
    }
}
