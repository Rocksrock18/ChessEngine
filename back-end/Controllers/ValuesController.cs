using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChessEngine;
using Microsoft.AspNetCore.Mvc;

namespace ChessEngineApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get([FromQuery] string fen)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            int max_depth = 8;
            int stop_time = 3000;
            BoardData board = new BoardData(fen);
            Conversion c = new Conversion();
            Zobrist z = new Zobrist();
            Engine com = new Engine(board);
            int move = com.PerformBestMove(max_depth, stop_time);
            return c.NumToMove(move);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
