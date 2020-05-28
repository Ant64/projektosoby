﻿using ProjektOsoby.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ProjektOsoby.DAL
{
    public class MPSContext : DbContext
    {
        public MPSContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<Person> Person { get; set; }
        public DbSet<UserNew> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}