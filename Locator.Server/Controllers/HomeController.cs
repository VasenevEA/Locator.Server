using Nancy;

namespace Locator.Server.Controllers
{
    public class HomeController : NancyModule
    {
        public HomeController()
        {
            Get["/"] = x =>
            {
                return View["Index.html"];
            };
        }
    }
}
