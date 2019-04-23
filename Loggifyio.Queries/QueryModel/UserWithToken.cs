using System;
using Loggifyio.Data.Model;

namespace Loggifyio.Queries.QueryModel
{
    public class UserWithToken
    {
        public User User { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}