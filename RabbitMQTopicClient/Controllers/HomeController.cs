using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace RabbitMQTopicClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
