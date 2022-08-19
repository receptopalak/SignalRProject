using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRProject.Business;
using SignalRProject.Hubs;
using System.Threading.Tasks;

namespace SignalRProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        readonly MyBusiness _myBusiness;// Buradan mybusiness a dependecy enjeciton ile istenilen işlmler yapılabilir.
        readonly IHubContext<MyHub> _hubContext; //  Buradan ise başka bir class a enjection yapmadan direk ıhıbcontexte dependecy injection yaparak istenlien işlemler yapılabilir. 

        public HomeController(MyBusiness myBusiness, IHubContext<MyHub> hubContext)
        {
            _myBusiness = myBusiness;
            _hubContext = hubContext;
        }

        [HttpGet("{message}")]
        public async Task<IActionResult> Index(string message)
        {
            _myBusiness.SendMessageAsync(message);
            return Ok();
        }
    }
}
