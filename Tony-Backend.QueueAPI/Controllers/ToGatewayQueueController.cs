using Amazon;
using Amazon.SQS;
using Microsoft.AspNetCore.Mvc;
using Tony_Backend.Shared.Entities;
using Tony_Backend.Shared.Helpers;

namespace Tony_Backend.QueueAPI.Controllers
{

    //[Authorize(Roles = "Administrator")]
    [ApiController]
    [Route("[controller]")]
    public class ToGatewayQueueController : ControllerBase
    {
        private readonly IQueueOperations _queueOperations;

        public ToGatewayQueueController(IQueueOperations queueOperations)
        {
            _queueOperations = queueOperations;
        }

        [HttpPost("{number}/{gatewayId}/Connect")]
        public async Task<IActionResult> Connect(int number, int gatewayId, string userId)
        {
            Console.WriteLine($"Message received from queue API server: \n    Number: {number}\n    GatewayId: {gatewayId}\n    UserId: {userId}");

            var message = new 
            {
                Number = number,
                GatewayId = gatewayId,
                UserId = userId
            }.ToString();

            _queueOperations.SendMessage( message);

            return Ok();
        }
    }
}
