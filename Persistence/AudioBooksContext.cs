using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BananaReader_audiobooks_ms.Model;
using Microsoft.EntityFrameworkCore;

namespace BananaReader_audiobooks_ms.Persistence
{
    public class AudioBooksContext : DbContext
    {
        public AudioBooksContext(DbContextOptions<AudioBooksContext> options) : base(options)
        { }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AudioBook>().ToTable("AudioBook");            
        }
        //entities
        public DbSet<AudioBook> AudioBooks { get; set; }
    }
}