using Microsoft.AspNetCore.SignalR;
using ReRailBackEnd.Contexts;
using ReRailBackEnd.Entities;

namespace ReRailBackEnd.Hubs
{
    public class ClassificationHub : Hub
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHubContext<UIHub> _UIhubContext;
        public ClassificationHub(AppDbContext appDbContext, IHubContext<UIHub> uIhubContext)
        {
            _appDbContext = appDbContext;
            _UIhubContext = uIhubContext;
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("UserJoined total:");
            return base.OnConnectedAsync();
        }
        public void SetClassification(int id, string classification)
        {
            Console.WriteLine("GOT CLASSIFCATION");
            var snapshot = _appDbContext.Images.FirstOrDefault(e=>e.Id == id);
            if (snapshot != null) {
                TrackPoint trackPoint = new TrackPoint()
                {
                    Prediction = classification,
                    trackSnapShotId = snapshot.Id
                };
                _appDbContext.TrackPoints.Add(trackPoint);
                _appDbContext.SaveChanges();
                if(classification.Split(",").Contains("crack"))
                {
                    _UIhubContext.Clients.All.SendAsync("Notification", 
                        new {
                            snapshot.Id,
                            Predictiopn = "crack",
                            snapshot.Location,
                            date = snapshot.CreationDate
                    });
                }
                else if(classification.Split(",").Contains("flaking")){
                    _UIhubContext.Clients.All.SendAsync("Notification",
                        new
                        {
                            snapshot.Id,
                            Predictiopn = "flaking",
                            snapshot.Location,
                            date = snapshot.CreationDate
                        });
                }
            }
        }
        public void SubToResolver(string Token)
        {
            Console.WriteLine("Added a resolver");
            Groups.AddToGroupAsync(Context.ConnectionId, "Resolver");
        }
    }
}
