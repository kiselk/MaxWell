using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Preferences;
using MaxWell;
using MaxWell.Services;
using Plugin.CurrentActivity;
using VKontakte;
using VKontakte.API;
using VKontakte.Utils;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidVkService))]
namespace MaxWell
{
    public class AndroidVkService : Java.Lang.Object, IVkService
    {
        public static AndroidVkService Instance => DependencyService.Get<IVkService>() as AndroidVkService;

        readonly string[] _permissions = {
            VKScope.Email,
            VKScope.Offline
        };

        TaskCompletionSource<LoginResult> _completionSource;
        LoginResult _loginResult;

        public Task<LoginResult> Login()
        {
            _completionSource = new TaskCompletionSource<LoginResult>();
            VKSdk.Login(CrossCurrentActivity.Current.Activity as Activity, _permissions);
            return _completionSource.Task;
        }


        public async Task<string[]> GetFingerprints()
        {
            MainApplication application = MainApplication.getInstance();
          
            return application.fingerprints;
        }

        public bool IsLoggedIn()
        {
            if (!VKSdk.IsLoggedIn)
            {
                return false;
            }

            return true;
        }

        public void Logout()
        {
            _loginResult = null;
            _completionSource = null;
            VKSdk.Logout();
;            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(CrossCurrentActivity.Current.Activity);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.Remove("maxwell_vk_email");
            editor.Remove("maxwell_vk_id");
            editor.Remove("maxwell_vk_token");
          //  editor.Commit();    // applies changes synchronously on older APIs
            editor.Apply();        // applies changes asynchronously on newer APIs
        }

        public void SetUserToken(VKAccessToken token)
        {
            _loginResult = new LoginResult
            {
                Email = token.Email,
                Token = token.AccessToken,
                UserId = token.UserId,
                ExpireAt = Utils.FromMsDateTime(token.ExpiresIn)
            };

            Task.Run(GetUserInfo);
        }
        public async Task<string> WakeUp()
        {
            string output = "";
            try
            {
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(CrossCurrentActivity.Current.Activity);

            //    if (prefs.Contains("maxwell_vk_token"))
              //  {
             //   //     UserDialogs.Instance.AlertAsync( prefs.GetString("maxwell_vk_token", "Token debug prefs"));
               // }

                if (prefs.Contains("maxwell_vk_email"))
                {

                    var email = prefs.GetString("maxwell_vk_email", "none");
                    var userid = prefs.GetString("maxwell_vk_id", "none");
                 //   UserDialogs.Instance.AlertAsync("Token debug vkemail", email + " " + userid);
                    var token = VKAccessToken.TokenFromSharedPreferences(CrossCurrentActivity.Current.Activity, "maxwell_vk_token");


                    VKSdk.WakeUpSession(CrossCurrentActivity.Current.Activity);


                     token = VKAccessToken.TokenFromSharedPreferences(CrossCurrentActivity.Current.Activity, "maxwell_vk_token");

                  //  await UserDialogs.Instance.AlertAsync("Token debug email from token", token.Email);
                    AndroidVkService.Instance.SetUserToken(token);
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.AlertAsync(ex.Message, this.GetType() +" WakeUp Exception");
            }
  

            return output;
        }

        public async Task<string> GetVkIdFromPrefs()
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(CrossCurrentActivity.Current.Activity);
            return prefs.GetString("maxwell_vk_id", null);

        }

        async Task GetUserInfo()
        {
            var request = VKApi.Users.Get(VKParameters.From(VKApiConst.Fields, @"photo_400_orig,"));
            var response = await request.ExecuteAsync();
            var jsonArray = response.Json.OptJSONArray(@"response");
            var account = jsonArray?.GetJSONObject(0);
            if (account != null && _loginResult != null)
            {
                _loginResult.FirstName = account.OptString(@"first_name");
                _loginResult.LastName = account.OptString(@"last_name");
                _loginResult.ImageUrl = account.OptString(@"photo_400_orig");
                _loginResult.LoginState = LoginState.Success;
                try
                {
                    _loginResult.City = account.OptString(@"city");
                     _loginResult.Phone = account.OptString(@"phone");
                     _loginResult.Phone += account.OptString(@"mobile_phone");
                    _loginResult.Gender = account.OptString(@"sex");
                     _loginResult.Birthday = account.OptString(@"bdate");
                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.AlertAsync(ex.Message, this.GetType() + " GetUserInfo props");
                }

                try
                {
                    ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(CrossCurrentActivity.Current.Activity);
                    _loginResult.Email = prefs.GetString("maxwell_vk_email", "none");
                    _loginResult.UserId = prefs.GetString("maxwell_vk_id", "none");
                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.AlertAsync(ex.Message, this.GetType() + " GetUserInfo prefs");
                }

                SetResult(_loginResult);
            }
            else
                SetErrorResult(@"Unable to complete the request of user info");
        }

        public void SetErrorResult(string errorMessage)
        {
            SetResult(new LoginResult { LoginState = LoginState.Failed, ErrorString = errorMessage });
        }

        public void SetCanceledResult()
        {
            SetResult(new LoginResult { LoginState = LoginState.Canceled });
        }

        void SetResult(LoginResult result)
        {
            _completionSource?.TrySetResult(result);
            _loginResult = null;
            _completionSource = null;
        }
        public async Task<LoginResult> GetLoginResult()
        {
            var request = VKApi.Users.Get(VKParameters.From(VKApiConst.Fields, @"photo_400_orig,"));
            var response = await request.ExecuteAsync();
            var jsonArray = response.Json.OptJSONArray(@"response");
            var account = jsonArray?.GetJSONObject(0);
        //     LoginResult result = new LoginResult();
           if (account != null && _loginResult != null)
            {
                _loginResult.FirstName = account.OptString(@"first_name");
                _loginResult.LastName = account.OptString(@"last_name");
                _loginResult.ImageUrl = account.OptString(@"photo_400_orig");
                _loginResult.LoginState = LoginState.Success;
                if (_loginResult != null)
                {
                    _loginResult.Email = _loginResult.Email;
                }
               SetResult(_loginResult);
            }

            return _loginResult;
        }

        public async Task<LoginResult> GetUserProfile()
        {
          
            var profile = "";
             LoginResult result = new LoginResult();
            try
            {

                var request = VKApi.Users.Get(VKParameters.From(VKApiConst.Fields, @"photo_400_orig,"));
                var response = await request.ExecuteAsync();
                var jsonArray = response.Json.OptJSONArray(@"response");
                var account = jsonArray?.GetJSONObject(0);
                //
                if (account != null )
                {
                    result.FirstName = account.OptString(@"first_name");
                    result.LastName = account.OptString(@"last_name");
                    result.ImageUrl = account.OptString(@"photo_400_orig");
                    result.LoginState = LoginState.Success;
                    result.City = account.OptString(@"city");
                    result.Phone = account.OptString(@"phone") +" "+ account.OptString(@"mobile_phone");
                    result.Gender = account.OptString(@"sex");
                    result.Birthday = account.OptString(@"bdate");    //    result.Email = account.OptString(@"Email");
                    ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(CrossCurrentActivity.Current.Activity);
                    result.Email = prefs.GetString("maxwell_vk_email", "none");
                    result.UserId = prefs.GetString("maxwell_vk_id", "none");

                }

                return result;
            }
            catch (Exception  e)
            {
                UserDialogs.Instance.AlertAsync(e.Message, this.GetType() + " GetUserProfile Exception");
                result.ErrorString = e.Message;
            }

            return result;
        }
    }
}