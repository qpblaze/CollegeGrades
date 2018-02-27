using System;
using System.Threading.Tasks;

namespace CollegeGrades.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountService Accounts { get; }

        Task CompletedAsync();
    }
}