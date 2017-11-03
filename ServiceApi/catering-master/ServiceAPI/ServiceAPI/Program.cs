using Microsoft.AspNetCore.Hosting;
using System;
using System.Threading.Tasks;
using ServiceAPI.Dal;
using System.Linq;
namespace ServiceAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CateringsDbContext())
            {
                // Create database
                context.Database.EnsureCreated();



                /*inserimento*//*
                context.Students.Add(s);
                context.Students.Add(n);
                context.Students.Add(f);
                context.Corsi.Add(cor);
                context.SaveChanges();

                /*cancellazione*/
                               /*var a=context.Students.FirstOrDefault(x => x.Name == "Giovanni");
                               context.Students.Remove(a);
                               context.SaveChanges();

                               /*update*/
                               /*var b= context.Students.FirstOrDefault(x => x.Name == "Piero");
                               b.DateOfBirth = new DateTime(2017, 2, 1);
                               context.Students.Update(b);
                               context.SaveChanges();*/
               
            }
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            Task restService = host.RunAsync();

            //System.Diagnostics.Process.Start("chrome.exe", "http://localhost/netcoreapp2.0/corsoing/");
           // System.Diagnostics.Process.Start("cmd", "/C start http://localhost/netcoreapp2.0/corsoing/");
            restService.Wait();
            Console.ReadKey();
        }
    }
}
