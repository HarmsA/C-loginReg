using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LoginReg.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options){}
        public DbSet<User> Users {get;set;}
    }
}