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
        public DbSet<Header_selection> header_selection { get; set; }
        public DbSet<Form> form {get; set;}
        public DbSet<Header_form> header_form {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Header_form>()
            .HasOne(hf => hf.form)
            .WithMany(f => f.header_form)
            .HasForeignKey(hf => hf.form_id)
            .OnDelete(DeleteBehavior.Cascade);

        }
    }
}