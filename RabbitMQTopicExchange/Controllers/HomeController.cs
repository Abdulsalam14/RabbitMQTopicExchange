using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace RabbitMQTopicExchange.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}
