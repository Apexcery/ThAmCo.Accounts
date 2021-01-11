using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThAmCo.Accounts.Models.Profile;

namespace ThAmCo.Accounts.Interfaces
{
    public interface IProfileService
    {
        public Task AddProfile(ProfileDto profile);
    }
}
