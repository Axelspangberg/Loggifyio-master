using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using loggifyio.Data.Access.Constants;
using loggifyio.Data.Access.DAL;
using loggifyio.Data.Model;
using Loggifyio.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace loggifyio
{
    public class SecurityContext : ISecurityContext
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _uow;
        private User _user;

        public SecurityContext(IHttpContextAccessor contextAccessor, IUnitOfWork uow)
        {
            _contextAccessor = contextAccessor;
            _uow = uow;
        }

        public User User
        {
            get
            {
                if (_user != null) return _user;

                if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    throw new UnauthorizedAccessException();
                }

                var username = _contextAccessor.HttpContext.User.Identity.Name;
                _user = _uow.Query<User>()
                    .Where(x => x.Username == username)
                    .Include(x => x.Roles)
                    .ThenInclude(x => x.Role)
                    .FirstOrDefault();

                if (_user == null)
                {
                    throw new UnauthorizedAccessException("User is not found");
                }

                return _user;
            }
        }

        public bool IsAdministrator
        {
            get { return User.Roles.Any(x => x.Role.Name == Roles.Administrator); }
        }
    }
}
