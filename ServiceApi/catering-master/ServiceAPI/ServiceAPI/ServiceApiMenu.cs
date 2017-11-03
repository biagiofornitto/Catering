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
    public class ServiceApiMenu : Controller
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


        [HttpGet("menu")]
        public async Task<IActionResult> GetMenu()
        {
            try
            {
                await parallelism.WaitAsync();

                using (var context = new CateringsDbContext())
                {
                    return Ok(await context.Menu.ToListAsync());
                }
            }
            finally
            {
                parallelism.Release();
            }
        }

        [HttpGet("menuid")]
        public async Task<IActionResult> GetMenu([FromQuery]int id)
        {
            using (var context = new CateringsDbContext())
            {
                return Ok(await context.Menu.FirstOrDefaultAsync(x => x.Id == id)); /*tale che ha l'id uguale a quello che ti sto passando*/
            }
        }

        [HttpPut("menu")]
        public async Task<IActionResult> CreateMenu([FromBody]Menu menu)
        {
            using (var context = new CateringsDbContext())
            {
                context.Menu.Add(menu);

                await context.SaveChangesAsync();

                return Ok();
            }
        }

        [HttpPost("menu")]
        public async Task<IActionResult> UpdateMenu([FromBody]Menu menu)
        {
            using (var context = new CateringsDbContext())
            {
                context.Menu.Update(menu);
                await context.SaveChangesAsync();
                return Ok();
            }
        }


        [HttpDelete("menu")]
        public async Task<IActionResult> DeleteMenu([FromQuery]int id)
        {
            using (var context = new CateringsDbContext())
            {
                var menu = await context.Menu.FirstOrDefaultAsync(x => x.Id == id);
                context.Menu.Remove(menu);
                await context.SaveChangesAsync();
                return Ok();


            }
        }
    }
}
