using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MaxWell.Services
{
    public interface IFbService
    {
        Task<LoginResult> Login();
        Task<LoginResult> GetUserProfile();
        Task<string[]> GetFingerprints();
        bool IsLoggedIn();
        void Logout();
    }

   
}
