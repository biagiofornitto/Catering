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
    public class ServiceApiCatering : Controller
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


        [HttpGet("caterings")]
        public async Task<IActionResult> GetCaterings()
        {
            try
            {
                await parallelism.WaitAsync();

                using (var context = new CateringsDbContext())
                {
                    
                    return  Ok(await context.Catering.ToListAsync());
                }
            }
            finally
            {
                parallelism.Release();
            }
        }

        [HttpGet("catering")]
        public async Task<IActionResult> GetCatering([FromQuery]int id)
        {
            using (var context = new CateringsDbContext())
            {
                return Ok(await context.Catering.FirstOrDefaultAsync(x => x.Codice == id)); 
            }
        }

        [HttpPut("catering")]
        public async Task<IActionResult> CreateCatering([FromBody]Catering catering)
        {
            using (var context = new CateringsDbContext())
            {
                context.Catering.Add(catering);

                await context.SaveChangesAsync();

                return Ok();
            }
        }

        [HttpPost("catering")]
        public async Task<IActionResult> UpdateCatering([FromBody]Catering catering)
        {
            using (var context = new CateringsDbContext())
            {
                context.Catering.Update(catering);
                await context.SaveChangesAsync();
                return Ok();
            }
        }


        [HttpDelete("catering")]
        public async Task<IActionResult> DeleteCatering([FromQuery]int id)
        {
            using (var context = new CateringsDbContext())
            {
                var catering = await context.Catering.FirstOrDefaultAsync(x => x.Id == id);
                context.Catering.Remove(catering);
                await context.SaveChangesAsync();
                return Ok();


            }
        }
    }
}

