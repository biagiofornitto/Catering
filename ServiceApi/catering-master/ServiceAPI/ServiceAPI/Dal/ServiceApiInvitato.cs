
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
    public class ServiceApiInvitato : Controller
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


        [HttpGet("invitati")]
        public async Task<IActionResult> GetInvitato()
        {
            try
            {
                await parallelism.WaitAsync();

                using (var context = new CateringsDbContext())
                {
                    return Ok(await context.Invitato.ToListAsync());
                }
            }
            finally
            {
                parallelism.Release();
            }
        }

        [HttpGet("invitato")]
        public async Task<IActionResult> GetInvitato([FromQuery]int cf)
        {
            using (var context = new CateringsDbContext())
            {
                return Ok(await context.Invitato.FirstOrDefaultAsync(x => x.Id == cf)); /*tale che ha l'id uguale a quello che ti sto passando*/
            }
        }

        [HttpPut("invitato")]
        public async Task<IActionResult> CreateInvitato([FromBody]Invitato invitato)
        {
            using (var context = new CateringsDbContext())
            {
                context.Invitato.Add(invitato);

                await context.SaveChangesAsync();

                return Ok();
            }
        }

        [HttpPost("invitato")]
        public async Task<IActionResult> UpdateInvitato([FromBody]Invitato invitato)
        {
            using (var context = new CateringsDbContext())
            {
                context.Invitato.Update(invitato);
                await context.SaveChangesAsync();
                return Ok();
            }
        }


        [HttpDelete("invitato")]
        public async Task<IActionResult> DeleteInvitato([FromQuery]int id)
        {
            using (var context = new CateringsDbContext())
            {
                var invitato = await context.Invitato.FirstOrDefaultAsync(x => x.Id == id);
                context.Invitato.Remove(invitato);
                await context.SaveChangesAsync();
                return Ok();


            }
        }
    }
}
