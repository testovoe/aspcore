using System.Collections.Generic;
using Testovoe.Model;

namespace Testovoe.Data
{
    public interface IUserTimeRepository
    {
        double GetRollingRetention(int days);
        IList<UserLife> GetHistogrammData();
        void AddUserTimes(IEnumerable<UserTime> userTimes);
    }
}
