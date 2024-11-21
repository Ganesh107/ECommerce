using Microsoft.EntityFrameworkCore;

namespace ECommerce.User.Service.UserContext
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptionsBuilder<UserDbContext> _dbContext) : base(_dbContext.Options)
        {
                
        }
    }
}
