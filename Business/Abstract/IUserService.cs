using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract {
    // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/Business/Abstract/IUserService.cs
    public interface IUserService {
        IDataResult<List<OperationClaim>> GetOperationClaims(User user);
        IDataResult<User> GetByEmail(string email);
        IResult Add(User user);
    }
}
