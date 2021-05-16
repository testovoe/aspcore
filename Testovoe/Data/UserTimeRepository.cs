using System;
using System.Collections.Generic;
using System.Linq;
using Testovoe.Model;

namespace Testovoe.Data
{
    public class UserTimeRepository : IUserTimeRepository
    {
        protected readonly TestAppDbContext _testAppDbContext;

        public UserTimeRepository(TestAppDbContext testAppDbContext)
        {
            _testAppDbContext = testAppDbContext;
        }

        public IList<UserLife> GetHistogrammData()
        {
            return _testAppDbContext.UserTimes.Select(x => new UserLife() { UserId = x.UserId, LifeDays = (int)((x.DateLastActivity ?? x.DateRegistration) - x.DateRegistration).TotalDays }).ToList();
        }

        public double GetRollingRetention(int days)
        {
            var usersReturned = _testAppDbContext.UserTimes.Where(x => x.DateLastActivity != null).AsEnumerable().Where(x => ((DateTime)x.DateLastActivity - x.DateRegistration).TotalDays >= days).Count();
            var usersRegistered = _testAppDbContext.UserTimes.Count(x => x.DateRegistration <= DateTime.Now.AddDays(-days));

            return usersRegistered != 0 ? (double)usersReturned / usersRegistered : 0;
        }

        public void AddUserTimes(IEnumerable<UserTime> userTimes)
        {
            foreach (var userTime in userTimes)
            {
                if (_testAppDbContext.UserTimes.Any(x => x.UserId == userTime.UserId))
                {
                    var toUpdate = _testAppDbContext.UserTimes.First(x => x.UserId == userTime.UserId);
                    toUpdate.DateLastActivity = userTime.DateLastActivity;
                    toUpdate.DateRegistration = userTime.DateRegistration;
                }
                else
                {
                    _testAppDbContext.Add(userTime);
                }
            }
        }
    }
}
