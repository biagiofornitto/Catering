using Microsoft.AspNetCore.Mvc;
using ServiceAPI.Dal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ServiceAPI
{
    [Route("api")]
    public class ServiceApiRistorante : Controller
    {
        static readonly object setupLock = new object();
        static readonly SemaphoreSlim parallelism = new SemaphoreSlim(2);

        [HttpGet("setup")]
        public IActionResult SetupDatabase()
        {
            lock (setupLock)
            {
                using (var context = new CateringsDbContext())
                {
                    // Create database
                    context.Database.EnsureCreated();
                }
                return Ok("database created");
            }
        }


        [HttpGet("ristoranti")]
        public async Task<IActionResult> GetRistorante()
        {
            try
            {
                await parallelism.WaitAsync();

                using (var context = new CateringsDbContext())
                {
                    return Ok(await context.Ristorante.ToListAsync());
                }
            }
            finally
            {
                parallelism.Release();
            }
        }

        [HttpGet("ristorante")]
        public async Task<IActionResult> GetRistorante([FromQuery]string piva)
        {
            using (var context = new CateringsDbContext())
            {
                return Ok(await context.Ristorante.FirstOrDefaultAsync(x => x.Piva == piva)); /*tale che ha l'id uguale a quello che ti sto passando*/
            }
        }

        [HttpPut("ristorante")]
        public async Task<IActionResult> CreateRistorante([FromBody]Ristorante ristorante)
        {
            using (var context = new CateringsDbContext())
            {
                context.Ristorante.Add(ristorante);

                await context.SaveChangesAsync();

                return Ok();
            }
        }

        [HttpPost("ristorante")]
        public async Task<IActionResult> UpdateRistorante([FromBody]Ristorante ristorante)
        {
            using (var context = new CateringsDbContext())
            {
                context.Ristorante.Update(ristorante);
                await context.SaveChangesAsync();
                return Ok();
            }
        }


        [HttpDelete("ristorante")]
        public async Task<IActionResult> DeleteRistorante([FromQuery]int id)
        {
            using (var context = new CateringsDbContext())
            {
                var ristorante = await context.Ristorante.FirstOrDefaultAsync(x => x.Id == id);
                context.Ristorante.Remove(ristorante);
                await context.SaveChangesAsync();
                return Ok();


            }
        }
    }
}


