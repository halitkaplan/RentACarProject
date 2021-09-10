using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CurrentUserManager : ICurrentUserService
    {
       
            ICurrentUserDal _currentUserDal;

            public CurrentUserManager(ICurrentUserDal currentUserDal)
            {
                _currentUserDal = currentUserDal;
            }

            public List<OperationClaim> GetClaims(CurrentUser currentUser)
            {
                return _currentUserDal.GetClaims(currentUser);
            }

            public void Add(CurrentUser currentUser)
            {
                _currentUserDal.Add(currentUser);
            }

        public CurrentUser GetByMail(string email)
        {
            //return _currentUserDal.Get(u => u.Email == email);
            return _currentUserDal.Get(e => e.Email == email);
        }

    }
}
