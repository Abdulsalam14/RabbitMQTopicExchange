using Microsoft.AspNetCore.Mvc;
using RabbitMQTopicExchange.Services;
using System.Diagnostics;

namespace RabbitMQTopicExchange.Controllers
{
    public class HomeController : Controller
    {
        private readonly TopicConsumerService _consumerService;

        public HomeController(TopicConsumerService consumerService)
        {
            _consumerService=consumerService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> SaveSelects(string[] selects)
        {
            try
            {
                await _consumerService.ConsumeMessage(selects);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


    }
}
