using Microsoft.EntityFrameworkCore;

namespace WebApplication6.Data
{
    public class LinkStoreContext:DbContext
    {
        public LinkStoreContext(DbContextOptions<LinkStoreContext> options):base(options) 
        {
        
        }
        public DbSet<Link> Link { get; set;}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=Link;Integrated Security=True;TrustServerCertificate=True");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }



}
