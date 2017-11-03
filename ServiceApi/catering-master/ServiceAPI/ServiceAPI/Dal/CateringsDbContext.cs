using Microsoft.EntityFrameworkCore;

namespace ServiceAPI.Dal
{
    public class CateringsDbContext : DbContext
    {

        public DbSet<Catering> Catering { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Portata> Portata { get; set; }
        public DbSet<Ristorante> Ristorante { get; set; }
        public DbSet<Utente> Utente { get; set; }
        public DbSet<Invitato> Invitato { get; set; }

        /*crea un nuovo database solo se non esiste*/
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                //.UseMySql(@"Server=localhost;database=corso;uid=corso;pwd=unict;");
                .UseMySql(@"Server=localhost;database=agenziacatcorso;uid=root;"); /*nel progetto crea un utente con password*/

        /*creazione del modello*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Skip shadow types
                if (entityType.ClrType == null)
                    continue;

                entityType.Relational().TableName = entityType.ClrType.Name;
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}

