﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarvedRock.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarvedRock.Api.Data
{
    public class CarvedRockDbContext : DbContext
    {
        public CarvedRockDbContext(DbContextOptions<CarvedRockDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}