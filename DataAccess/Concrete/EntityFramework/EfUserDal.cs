using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework {
    // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/DataAccess/Concrete/EntityFramework/EfUserDal.cs
    public class EfUserDal : EfEntityRepositoryBase<User, NorthwindContext>, IUserDal {
        public List<OperationClaim> GetOperationClaims(User user) {
            using (var context = new NorthwindContext()) {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { 
                                 Id = operationClaim.Id, 
                                 Name = operationClaim.Name 
                             };
                return result.ToList();
            }
        }
    }
}
