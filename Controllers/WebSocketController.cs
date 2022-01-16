using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoftwareFullComponents.Product2Component.Data;
using SoftwareFullComponents.Product2Component.DTO;

namespace SoftwareFullComponents.Product2Component.Controllers
{
    [ApiController]
    [Route("[controller]/ws")]
    public class WebSocketController : ControllerBase
    {

        private readonly ILogger<WebSocketController> _logger;
        private readonly IProductRepository _productRepository;

        public WebSocketController(ILogger<WebSocketController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpGet("Product")]
        public async Task GetLicenses()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using (var ws = await HttpContext.WebSockets.AcceptWebSocketAsync())
                {
                    var products = await _productRepository.GetProducts();

                    foreach (var product in products)
                    {
                        var msg = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(product));
                        await ws.SendAsync(new ArraySegment<byte>(msg, 0, msg.Length), WebSocketMessageType.Text, false,
                            CancellationToken.None);
                    }
                    
                    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure,"Finished", CancellationToken.None);
                }
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }
        
        [HttpGet("Product/{productSlug}")]
        public async Task GetProduct(string productSlug)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using (var ws = await HttpContext.WebSockets.AcceptWebSocketAsync())
                {
                    var product = await _productRepository.GetProductBySlug(productSlug);

                    var msg = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(product));
                    await ws.SendAsync(new ArraySegment<byte>(msg, 0, msg.Length), WebSocketMessageType.Text, false, CancellationToken.None);
                    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure,"Finished", CancellationToken.None);                    
                }
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }
    }
}