using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppLog.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        [HttpPost("save")]
        public async Task Save(BlazorAppLog.Shared.Log log)
        {
            Console.WriteLine($" registro desde WS {log.Data}  {log.Level}");
        }
    }
}
