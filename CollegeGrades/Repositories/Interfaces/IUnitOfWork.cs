using System;
using System.Threading.Tasks;

namespace CollegeGrades.Repositories
{
    internal interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts { get; }

        Task CompletedAsync();
    }
}