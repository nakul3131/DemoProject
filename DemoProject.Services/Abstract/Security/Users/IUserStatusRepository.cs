using DemoProject.Services.ViewModel.Security.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Security.Users
{
    public interface IUserStatusRepository
    {
        Task<IEnumerable<UserStatusViewModel>> GetUserStatusIndex(string UserProfileStatus, DateTime EffectiveDate);
    }
}
