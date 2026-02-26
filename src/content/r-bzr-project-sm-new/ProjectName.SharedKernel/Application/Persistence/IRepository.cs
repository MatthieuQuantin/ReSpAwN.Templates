// Based on Ardalis.SharedKernel v5.0.0
// https://github.com/ardalis/Ardalis.SharedKernel/tree/5.0.0
using Ardalis.Specification;
using ProjectName.SharedKernel.Domain;

namespace ProjectName.SharedKernel.Application.Persistence;

public interface IRepository<T> : IReadRepository<T>, IRepositoryBase<T> where T : class, IAggregateRoot;