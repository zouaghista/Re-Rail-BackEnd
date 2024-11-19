using Microsoft.AspNetCore.SignalR;

namespace ReRailBackEnd.Hubs
{
    public class DataEntryHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("waaa");
            return base.OnConnectedAsync();
        }
        public async Task SendImageAndLocation(List<byte> data, string info)
        {
            string imageBase64 = data.Count.ToString();
            Console.WriteLine("Received image and text.");
            Console.WriteLine("Image (Base64): " + imageBase64 + info);
        }

    }
}
