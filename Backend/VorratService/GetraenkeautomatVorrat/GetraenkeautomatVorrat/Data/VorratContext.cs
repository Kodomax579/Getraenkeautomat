using GetraenkeautomatVorrat.Models;
using Microsoft.EntityFrameworkCore;

namespace GetraenkeautomatVorrat.Data;

public class VorratContext : DbContext
{
    public VorratContext(DbContextOptions<VorratContext> options) : base(options) { }

    public DbSet<Vorrat> Vorraete => Set<Vorrat>();
}
