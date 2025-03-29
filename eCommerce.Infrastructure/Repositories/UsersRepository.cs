using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using eCommerce.Core.Entities;
using eCommerce.Core.Entities.DTO;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;


namespace eCommerce.Infrastructure.Repositories
{
    public class UsersRepository : IUserRepository
    {
        private readonly DapperDbContext _dbContext;
        public UsersRepository(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApplicationUser?> AddUser(ApplicationUser user)
        {
            if (_dbContext == null || _dbContext.DbConnection == null)
            {
                throw new InvalidOperationException("Database connection is not initialized");
            }
            user.UserId = Guid.NewGuid();

            string query = "INSERT INTO public.\"Users\"(\"UserId\", \"Email\", \"Password\", \"PersonName\", \"Gender\") VALUES(@UserId, @Email,  @Password, @PersonName, @Gender)";

           int rowCountAffected = await _dbContext.DbConnection.ExecuteAsync(query, user);

           if (rowCountAffected > 0)
           {
               return user;
           }
           else
           {
               return null;
           }

                
        }

        public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? Email, string? Password)
        {
            string query = "SELECT * FROM public.\"Users\" WHERE \"Email\"=@Email AND \"Password\"=@Password";

            var parameters = new { Email = Email, Password = Password };

            ApplicationUser? user = await _dbContext.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, parameters);

            //new ApplicationUser()
            //{
            //    UserId = Guid.NewGuid(),
            //    Email = Email,
            //    Password = Password,
            //    PersonName = "Person Name",
            //    Gender = GenderOptions.Female.ToString()

            //};

           
            //var parameters = new { Email = Email, Password = Password };

           

            return user;
        }
    }
}
