﻿using OpenAI.Chat;
using Syncfusion.Blazor.SmartComponents;

namespace TreeGrid.Services
{
    public class AzureAIService
    {
        private OpenAIConfiguration _openAIConfiguration;
        private ChatParameters chatParameters_history = new ChatParameters();

        public AzureAIService(OpenAIConfiguration openAIConfiguration)
        {
            _openAIConfiguration = openAIConfiguration;
        }

        /// <summary>
        /// Gets a text completion from the Azure OpenAI service.
        /// </summary>
        /// <param name="prompt">The user prompt to send to the AI service.</param>
        /// <param name="returnAsJson">Indicates whether the response should be returned in JSON format. Defaults to <c>true</c></param>
        /// <param name="appendPreviousResponse">Indicates whether to append previous responses to the conversation history. Defaults to <c>false</c></param>
        /// <param name="systemRole">Specifies the systemRole that is sent to AI Clients. Defaults to <c>null</c></param>
        /// <returns>The AI-generated completion as a string.</returns>
        public async Task<string> GetCompletionAsync(string prompt, bool returnAsJson = true, bool appendPreviousResponse = false, string systemRole = null)
        {
            string systemMessage = returnAsJson ? "You are a helpful assistant that only returns and replies with valid, iterable RFC8259 compliant JSON in your responses unless I ask for any other format. Do not provide introductory words such as 'Here is your result' or '```json', etc. in the response" : !string.IsNullOrEmpty(systemRole) ? systemRole : "You are a helpful assistant";
            try
            {
                ChatParameters chatParameters = appendPreviousResponse ? chatParameters_history : new ChatParameters();
                if (appendPreviousResponse)
                {
                    if (chatParameters.Messages == null)
                    {
                        chatParameters.Messages = new List<ChatMessage>() {
                            new SystemChatMessage(systemMessage),
                        };
                    }
                    chatParameters.Messages.Add(new UserChatMessage(prompt));
                }
                else
                {
                    chatParameters.Messages = new List<ChatMessage>(2) {
                        new SystemChatMessage(systemMessage),
                        new UserChatMessage(prompt)
                    };
                }
                var completion = await _openAIConfiguration.GetChatResponseAsync(chatParameters);
                if (appendPreviousResponse)
                {
                    chatParameters_history?.Messages?.Add(new AssistantChatMessage(completion.ToString()));
                }
                return completion.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception has occurred: {ex.Message}");
                return "";
            }
        }
    }
}
