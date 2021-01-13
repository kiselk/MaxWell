using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MaxWell.Services
{
    public interface IVkService
    {
        Task<LoginResult> Login();
        Task<string[]> GetFingerprints();
        bool IsLoggedIn();
        void Logout();
        Task<LoginResult> GetLoginResult();
        Task<LoginResult> GetUserProfile();
        Task<string> WakeUp();

        Task<string> GetVkIdFromPrefs();
    }
  
}
