using CollegeGrades.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CollegeGrades.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IAccountRepository Accounts { get; private set; }

        public UnitOfWork(ApplicationDbContext context, IOptions<AppSecrets> options = null, IHostingEnvironment hostingEnvironment = null)
        {
            _context = context;

            Accounts = new AccountRepository(_context, options);
        }

        public async Task CompletedAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}