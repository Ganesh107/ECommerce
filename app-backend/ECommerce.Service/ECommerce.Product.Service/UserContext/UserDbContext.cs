using Microsoft.EntityFrameworkCore;

namespace ECommerce.User.Service.UserContext
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        
        }

        // DbSets
    }
}
