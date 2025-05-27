using System;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using ZenBlaze.Server.DataModels;

namespace ZenBlaze.Server.Controllers
{
 
    [ApiController]
    [Route("api/pun")]
    public class PunController : ControllerBase
    {
        private readonly IDatabase _redisDb;

        public PunController(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }

        // GET: api/pun
        [HttpGet]
        public async Task<IActionResult> GetPun([FromQuery]  string punKey)
        {
            string pun = "";
            RedisValue punText = await _redisDb.StringGetAsync(punKey);
            if (!punText.HasValue)
                _ = "Pun not found.";
             pun = punText.ToString();
            return Ok(pun);

        }

        // POST: api/pun
        [HttpPost]
        public async Task<IActionResult> SetPun([FromBody] PunData newPun)
        {
            if (string.IsNullOrWhiteSpace(newPun.PunKey )|| string.IsNullOrWhiteSpace(newPun.PunText))
                return BadRequest("Pun cannot be empty!");

            string json = JsonSerializer.Serialize(newPun);
           // await _redisDb.StringSetAsync("pun:today", json);

            await _redisDb.StringSetAsync(newPun.PunKey, newPun.PunText, TimeSpan.FromHours(24));
            return Ok("Pun saved successfully! 😄");
        }

        // DELETE: api/pun  
        [HttpDelete]
        public async Task<IActionResult> DeletePun([FromQuery] string punKey)
        {
            if (string.IsNullOrWhiteSpace(punKey))
                return BadRequest("punKey is required.");

            bool deleted = await _redisDb.KeyDeleteAsync(punKey);
            return deleted ? Ok() : NotFound();

         
        }
    }   
}
