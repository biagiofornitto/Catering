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
    public class ServiceApiCliente : Controller
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


        [HttpGet("clienti")]
        public async Task<IActionResult> GetCliente()
        {
            try
            {
                await parallelism.WaitAsync();

                using (var context = new CateringsDbContext())
                {
                    return Ok(await context.Cliente.ToListAsync());
                }
            }
            finally
            {
                parallelism.Release();
            }
        }

        [HttpGet("cliente")]
        public async Task<IActionResult> GetCliente([FromQuery]string cf)
        {
            using (var context = new CateringsDbContext())
            {
                return Ok(await context.Cliente.FirstOrDefaultAsync(x => x.Codice_Fiscale == cf)); /*tale che ha l'id uguale a quello che ti sto passando*/
            }
        }

        [HttpPut("cliente")]
        public async Task<IActionResult> CreateCliente([FromBody]Cliente cliente)
        {
            using (var context = new CateringsDbContext())
            {
                context.Cliente.Add(cliente);

                await context.SaveChangesAsync();

                return Ok();
            }
        }

        [HttpPost("cliente")]
        public async Task<IActionResult> UpdateCliente([FromBody]Cliente cliente)
        {
            using (var context = new CateringsDbContext())
            {
                context.Cliente.Update(cliente);
                await context.SaveChangesAsync();
                return Ok();
            }
        }


        [HttpDelete("cliente")]
        public async Task<IActionResult> DeleteCliente([FromQuery]int id)
        {
            using (var context = new CateringsDbContext())
            {
                var cliente = await context.Cliente.FirstOrDefaultAsync(x => x.Id == id);
                context.Cliente.Remove(cliente);
                await context.SaveChangesAsync();
                return Ok();


            }
        }
    }
}
