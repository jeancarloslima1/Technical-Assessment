using Microsoft.EntityFrameworkCore;
using System;
using TechAssess.AuthService.Models;

namespace TechAssess.AuthService.Data
{
    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; } = null!;
    }
}
