using Microsoft.EntityFrameworkCore;

namespace MVC_060223.Classes
{
    public class UygulamaDbContext:DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext>options):base(options) 
        {
        
        }
        public DbSet<Film>Filmler =>Set<Film>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>().HasData(
                new Film() { Id=1,Ad="Fight Clup",Yil=1999,Puan=8.7m},
                new Film() { Id=2,Ad="Inception",Yil=2010,Puan=8.7m},
                new Film() { Id=3,Ad="The Matrix",Yil=1999,Puan=8.7m},
                new Film() { Id=4,Ad="Joker",Yil=2019,Puan=8.3m},
                new Film() { Id=5,Ad="Die Hard",Yil=1988,Puan=8.2m},
                new Film() { Id=6,Ad="Green Book",Yil=2018,Puan=8.2m}
               
                );
        }
    }
}
