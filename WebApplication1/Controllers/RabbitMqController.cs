using Microsoft.AspNetCore.Mvc;

namespace RabbitMqProducerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMqController : ControllerBase
    {
        private readonly RabbitMqProducer _rabbitMqProducer;

        public RabbitMqController()
        {
            // Создаем экземпляр RabbitMqProducer
            _rabbitMqProducer = new RabbitMqProducer();
        }

        // POST api/rabbitmq/send
        [HttpPost("send")]
        public IActionResult SendMessage([FromBody] string message)
        {
            // Отправляем сообщение в RabbitMQ
            _rabbitMqProducer.SendMessage(message);

            // Возвращаем успешный ответ
            return Ok($"Message '{message}' sent to RabbitMQ!");
        }
    }
}