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
        
        [HttpGet("MultiProductCall")]
        public async Task RequestedProducts()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using (var ws = await HttpContext.WebSockets.AcceptWebSocketAsync())
                {
                    var cts = new System.Threading.CancellationTokenSource();
                    ArraySegment<byte> byteToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes("Shaking hands: Waiting for product slugs"));
                    await ws.SendAsync(byteToSend, WebSocketMessageType.Text, false, cts.Token);
                    
                    var responseBuffer = new byte[1024];
                    var offset = 0;
                    var packet = 1024;

                    while (true)
                    {
                        ArraySegment<byte> byteRecieved = new ArraySegment<byte>(responseBuffer, offset, packet);
                        WebSocketReceiveResult response = await ws.ReceiveAsync(byteRecieved, cts.Token);
                        var responseMessage = Encoding.UTF8.GetString(responseBuffer, offset, response.Count);

                        if (response.EndOfMessage)
                        {
                            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Agreeing to end this",
                                CancellationToken.None);
                            break;
                        }
                        
                        var productSlug = responseMessage;
                        var product = await _productRepository.GetProductBySlug(productSlug);
                        var productbytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(product));
                        ArraySegment<byte> productToSendInBytes = new ArraySegment<byte>(productbytes);
                        await ws.SendAsync(productToSendInBytes, WebSocketMessageType.Text, false, cts.Token);
                    }
                }
            }
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