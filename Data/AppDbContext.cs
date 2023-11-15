using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SWA.API.Properties;
using Microsoft.EntityFrameworkCore;


namespace SWA.API.Data
{
	public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
    }
}

