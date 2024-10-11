using formulaire.Models;
using Microsoft.EntityFrameworkCore;

namespace formulaire.Data.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Type_emp> type_emp { get; set; }
        public DbSet<Selection> selection { get; set; }
        public DbSet<Header_selection> header_selection { get; set; }
        public DbSet<Fiche> fiche { get; set; }
        public DbSet<Th> th { get; set; }
        public DbSet<Td_principale> td_principale { get; set; }
        
    }
}