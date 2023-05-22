using DotNetCore.CAP;
using HKSH.Common.Repository.Database;
using Microsoft.EntityFrameworkCore;

namespace MicroService.Repository
{
    /// <summary>
    /// CAPExtensions
    /// </summary>
    public static class CapExtension
    {
        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="publisher">The publisher.</param>
        /// <returns></returns>
        public static void BeginTransaction(this IUnitOfWork unitOfWork, ICapPublisher publisher)
        {
            if (unitOfWork.DbContext.Database.CurrentTransaction == null)
            {
                unitOfWork.DbContext.Database.BeginTransaction(publisher);
            }
        }
    }
}