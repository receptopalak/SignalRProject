using Microsoft.AspNetCore.SignalR;
using SignalRProject.Hubs;

namespace SignalRProject.Business
{
    public class MyBusiness
    {
        readonly IHubContext<MyHub> _hubContext;

        public MyBusiness(IHubContext<MyHub> hubContext)
        {
            _hubContext = hubContext;
        }


    }
}
