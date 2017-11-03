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
    public class ServiceApiPortata : Controller
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


        [HttpGet("portate")]
        public async Task<IActionResult> GetPortata()
        {
            try
            {
                await parallelism.WaitAsync();

                using (var context = new CateringsDbContext())
                {
                    return Ok(await context.Portata.ToListAsync());
                }
            }
            finally
            {
                parallelism.Release();
            }
        }

        [HttpGet("portata")]
        public async Task<IActionResult> GetPortata([FromQuery]int id)
        {
            using (var context = new CateringsDbContext())
            {
                return Ok(await context.Portata.FirstOrDefaultAsync(x => x.Id == id)); /*tale che ha l'id uguale a quello che ti sto passando*/
            }
        }

        [HttpPut("portata")]
        public async Task<IActionResult> CreatePortata([FromBody]Portata portata)
        {
            using (var context = new CateringsDbContext())
            {
                context.Portata.Add(portata);

                await context.SaveChangesAsync();

                return Ok();
            }
        }

        [HttpPost("portata")]
        public async Task<IActionResult> UpdatePortata([FromBody]Portata portata)
        {
            using (var context = new CateringsDbContext())
            {
                context.Portata.Update(portata);
                await context.SaveChangesAsync();
                return Ok();
            }
        }


        [HttpDelete("portata")]
        public async Task<IActionResult> DeletePortata([FromQuery]int id)
        {
            using (var context = new CateringsDbContext())
            {
                var portata = await context.Portata.FirstOrDefaultAsync(x => x.Id == id);
                context.Portata.Remove(portata);
                await context.SaveChangesAsync();
                return Ok();


            }
        }
    }
}

