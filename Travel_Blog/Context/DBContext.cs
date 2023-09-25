using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Travel_Blog.Models;

namespace Travel_Blog.Context;

public class DBContext : IdentityDbContext<User>
{
    public DBContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Destination> Destinations { get; set; }
    public DbSet<Image> Images { get; set; }
}
