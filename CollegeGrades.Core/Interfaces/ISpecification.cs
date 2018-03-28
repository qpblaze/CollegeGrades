﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CollegeGrades.Core.Interfaces
{
    public interface ISpecification<TEntity>
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
        List<Expression<Func<TEntity, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
    }
}