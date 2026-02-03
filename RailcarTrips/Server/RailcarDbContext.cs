using System;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server
{
	public class RailcarDbContext:DbContext
	{
		public RailcarDbContext(DbContextOptions<RailcarDbContext> options)
			:base(options)
		{
		}
        public DbSet<EquipmentEvent> EquipmentEvents => Set<EquipmentEvent>();
       
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Trip> Trips => Set<Trip>();
        public DbSet<Event_Code_Definations> Event_Code_Definations => Set<Event_Code_Definations>();
        public DbSet<canadian_cities> canadian_cities => Set<canadian_cities>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<canadian_cities>()
                .HasNoKey();
         
        }

    }
}

