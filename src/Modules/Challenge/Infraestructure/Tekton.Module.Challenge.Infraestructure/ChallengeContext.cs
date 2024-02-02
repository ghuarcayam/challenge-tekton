using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Tekton.Module.Challenge.Domain.Products;

namespace Tekton.Module.Challenge.Infraestructure
{
    internal class ChallengeContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ChallengeContext(DbContextOptions options)
    : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
