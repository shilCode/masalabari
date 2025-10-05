using System;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using Core.Interfaces;

namespace Core.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDesc { get; }
    
    bool isDistinct{ get; }

}

public interface ISpecification<T, TResult> : ISpecification<T>
{
    Expression<Func<T,TResult>>? Select{ get; }
}