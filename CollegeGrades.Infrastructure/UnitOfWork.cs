using CollegeGrades.Core.Interfaces;
using CollegeGrades.Infrastructure.Data;
using System.Threading.Tasks;

namespace CollegeGrades.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
       // public IAccountService Accounts => throw new System.NotImplementedException();

        public Task CompletedAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        //private readonly ApplicationDbContext _context;



        //public UnitOfWork(ApplicationDbContext context)
        //{
        //    _context = context;

        //    Accounts = new AccountService(_context);
        //}

        //public async Task CompletedAsync()
        //{
        //    await _context.SaveChangesAsync();
        //}

    }
}