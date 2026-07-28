using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MzadService.Entities;

namespace MzadService.Data;

public class MzadDbContext : DbContext
{
    public MzadDbContext(DbContextOptions<MzadDbContext> options) : base(options)
    {

    }

    public DbSet<Mzad> Mzadat { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
    }
}
