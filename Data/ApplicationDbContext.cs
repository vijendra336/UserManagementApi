using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UserManagementApi.Models;


namespace UserManagementApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
    }
}
