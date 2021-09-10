using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICurrentUserService
    {
        List<OperationClaim> GetClaims(CurrentUser currentUser);
        void Add(CurrentUser currentUser);
        CurrentUser GetByMail(string email);
    }
}
