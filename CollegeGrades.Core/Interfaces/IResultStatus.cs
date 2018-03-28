using System.Collections.Generic;

namespace CollegeGrades.Core.Interfaces
{
    public interface IResultStatus : IEnumerable<string>
    {
        bool Succeeded { get; }
    }

    
}