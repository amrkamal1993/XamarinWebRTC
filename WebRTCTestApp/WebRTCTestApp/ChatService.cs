
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Zeyarat.Mobile.Services.Meeting
{
    public class ChatService
    {
        private readonly HubConnection hubConnection;
        public ChatService()
        {

            hubConnection = new HubConnectionBuilder().WithUrl("https://zyarat.azurewebsites.net/chathub", (opts) =>
            {
                opts.HttpMessageHandlerFactory = (message) =>
                {
                    if (message is HttpClientHandler clientHandler)
                        // always verify the SSL certificate
                        clientHandler.ServerCertificateCustomValidationCallback +=
                              (sender, certificate, chain, sslPolicyErrors) => { return true; };
                    return message;
                };
            }).Build();
        }

        public async Task Connect(string userEmail)
        {
            await hubConnection.StartAsync();
            await hubConnection.InvokeAsync("ConnectedAsync");
        }

        public async Task Disconnect(string userEmail)
        {
            await hubConnection.InvokeAsync("DisConnectedAsync");
            await hubConnection.StopAsync();

        }

        public async Task SendMessage(string userId, string message, bool isPrivate = false)
        {
            if (isPrivate)
            {
                await hubConnection.InvokeAsync("SendPrivateMessage", userId, message);
            }
            else
            {
                await hubConnection.InvokeAsync("SendMessage", userId, message);
            }
        }

        public void ReceiveMessage(Action<string, string> GetMessageAndUser, bool isPrivate = false)
        {
            if (isPrivate)
            {
                hubConnection.On("ReceivePrivateMessage", GetMessageAndUser);
            }
            else
            {
                hubConnection.On("ReceiveMessage", GetMessageAndUser);
            }
        }

        public async Task CallFriend(string friendEmail)
        {
            await hubConnection.InvokeAsync("AddMe", "Amr");
            await hubConnection.InvokeAsync("CallFriendAsync", "Test");
        }

        public void ReceivePrivateVideoCall(Action<string> GetVideoCall)
        {
            hubConnection.On("ReceivePrivateVideoCall", GetVideoCall);
        }

        public async Task RejectVideoCall(string currentUser, string friendUser)
        {
            await hubConnection.InvokeAsync("RejectVideoCall", currentUser, friendUser);
        }

        public async Task AcceptVideoCall(string currentUser, string friendUser)
        {
            await hubConnection.InvokeAsync("AcceptVideoCall", currentUser, friendUser);
        }

        public void AcceptVideoCallByFriend(Action<string, string> VideoCallAcceptedByFriend)
        {
            hubConnection.On("AcceptVideoCallByFriend", VideoCallAcceptedByFriend);
        }
    }
}
