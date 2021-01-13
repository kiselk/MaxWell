using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MaxWell.iOS;
using MaxWell.Services;
using Foundation;
using UIKit;
using VKontakte;
using VKontakte.API;
using VKontakte.API.Methods;
using VKontakte.API.Models;
using VKontakte.Core;
using VKontakte.Views;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppleVkService))]

namespace MaxWell.iOS
{
    public class AppleVkService : NSObject, IVkService, IVKSdkDelegate, IVKSdkUIDelegate
    {
        readonly string[] _permissions = {VKPermissions.Email, VKPermissions.Offline};
        private VKAccessToken tok;
        LoginResult _loginResult;
        TaskCompletionSource<LoginResult> _completionSource;
        private static AppleVkService Instance;
        public AppleVkService()
        {
            VKSdk.Instance.RegisterDelegate(this);
            VKSdk.Instance.UiDelegate = this;
            Instance = this;
          
          
        }

        public async Task<string> WakeUp()
        {
            string output = "";
            try
            {


                var plist = NSUserDefaults.StandardUserDefaults;
                if (plist != null)
                {
                    var Email = plist.StringForKey("maxwell_vk_email");
                    var UserId = plist.StringForKey("maxwell_vk_id");
                    var Token = plist.StringForKey("maxwell_vk_token");
                //    Utils.PopupMessage("VKAuthorizationState", "UserId " + UserId);
         
                    if (UserId != null)
                        if (UserId != "")
                            VKSdk.WakeUpSession(_permissions, wakeUpBlock);
                }

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.AlertAsync(ex.Message, this.GetType() + " WakeUp Exception");
            }

            return output;
        }

        private void wakeUpBlock(VKAuthorizationState state, NSError error)
        {
            try
            {
             //   Utils.PopupMessage("VKAuthorizationState", error.Code.ToString());
             //   Utils.PopupMessage("VKAuthorizationState", state.ToString());
            }catch(Exception e) { }
        }
        public static AppleVkService getInstance()
        {
            return Instance;
        }
        public Task<LoginResult> Login()
        {
            _completionSource = new TaskCompletionSource<LoginResult>();
            VKSdk.Authorize(_permissions);
            return _completionSource.Task;
        }

        public async Task<string[]> GetFingerprints()
        {
            return new string[] {"1", "2"};
        //    throw new System.NotImplementedException();
        }

        public void Logout()
        {
            _loginResult = null;
            _completionSource = null;
            VKSdk.ForceLogout();
            var plist = NSUserDefaults.StandardUserDefaults;

            plist.RemoveObject("maxwell_vk_token");
            plist.RemoveObject("maxwell_vk_email");
            plist.RemoveObject("maxwell_vk_id");
            plist.Synchronize();

        }

        public async Task<LoginResult> GetLoginResult()
        {
            return _loginResult;
        }

    

        [Export("vkSdkTokenHasExpired:")]
        public void TokenHasExpired(VKAccessToken expiredToken)
        {
            VKSdk.Authorize(_permissions);
        }

        public new void Dispose()
        {
            VKSdk.Instance.UnregisterDelegate(this);
            VKSdk.Instance.UiDelegate = null;
            SetCancelledResult();
        }

        public void AccessAuthorizationFinished(VKAuthorizationResult result)
        {
            if (result?.Token == null)
                SetErrorResult(result?.Error?.LocalizedDescription ?? @"VK authorization unknown error");
            else
            {
                tok = result.Token;
                _loginResult = new LoginResult
                {
                    Token = result.Token.AccessToken, UserId = result.Token.UserId, Email = result.Token.Email, ExpireAt = Utils.FromMsDateTime(result.Token.ExpiresIn),
                };
                try
                {
                    var Email = _loginResult.Email;
                    var UserId = _loginResult.UserId;
                    if (Email==null) Email = "";
                  //   if (Email.Equals("")) Email = "noemail";
                    var plist = NSUserDefaults.StandardUserDefaults;
              
                    plist.SetString(UserId, "maxwell_vk_id");
                    plist.SetString(Email, "maxwell_vk_email");
                    plist.Synchronize();   }
                catch (Exception ex)
                {
                    UserDialogs.Instance.AlertAsync(ex.Message, "No email exception 1");
                }
                try
                {
                    Task.Run(GetUserInfo);
                    result.Token.SaveTokenToDefaults("maxwell_vk_token");
               
                }
                catch (Exception ex)
                {
                     UserDialogs.Instance.AlertAsync( ex.Message,"No email exception 2");
                }
            }
        }

        async Task GetUserInfo()
        {
            var request = VKApi.Users.Get(NSDictionary.FromObjectAndKey((NSString) @"photo_400_orig", VKApiConst.Fields));
            var response = await request.ExecuteAsync();
            var users = response.ParsedModel as VKUsersArray;
            var account = users?.FirstObject as VKUser;
            if (account != null && _loginResult != null)
            {
                _loginResult.FirstName = account.first_name;
                _loginResult.LastName = account.last_name;
                _loginResult.ImageUrl = account.photo_400_orig;
                _loginResult.LoginState = LoginState.Success;
                try {
                    if (account.city!=null) _loginResult.City = account.city.ToString();
                    if (account.phone != null) _loginResult.Phone = account.phone;
                    if (account.mobile_phone != null) _loginResult.Phone += account.mobile_phone;
                    if (account.sex != null) _loginResult.Gender = account.sex.ToString();
                    if (account.bdate != null) _loginResult.Birthday = account.bdate;
            }
                catch (Exception ex)
                {
                    UserDialogs.Instance.AlertAsync(ex.Message, this.GetType() + " GetUserInfo");
                }
                try
                {
                    var plist = NSUserDefaults.StandardUserDefaults;
                    if (plist != null)
                    {
                        if (!plist.StringForKey("maxwell_vk_id").Equals(null))
                        {
                            if (!plist.StringForKey("maxwell_vk_id").Equals(""))
                            {
                                _loginResult.UserId = plist.StringForKey("maxwell_vk_id");
                                _loginResult.Email = plist.StringForKey("maxwell_vk_email");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.AlertAsync(ex.Message, "GetUserInfo Error AppleVKService");
                }
                SetResult(_loginResult);
            }
            else
                SetErrorResult(@"Unable to complete the request of user info");
        }

        public void UserAuthorizationFailed()
        {
            SetErrorResult(@"VK authorization unknown error");
        }
        public bool IsLoggedIn()
        {
            if (!VKSdk.IsLoggedIn)
            {
                return false;
            }

            return true;
        }
        public void ShouldPresentViewController(UIViewController controller)
        {
            Device.BeginInvokeOnMainThread(() => Utils.GetCurrentViewController().PresentViewController(controller, true, null));
        }

        public void NeedCaptchaEnter(VKError captchaError)
        {
            Device.BeginInvokeOnMainThread(() => VKCaptchaViewController.Create(captchaError).PresentIn(Utils.GetCurrentViewController()));
        }

        void SetCancelledResult()
        {
            SetResult(new LoginResult {LoginState = LoginState.Canceled});
        }

        void SetErrorResult(string errorString)
        {
            SetResult(new LoginResult {LoginState = LoginState.Failed, ErrorString = errorString});
        }

        void SetResult(LoginResult result)
        {
            _completionSource?.TrySetResult(result);
            _loginResult = null;
            _completionSource = null;
        }


        public async Task<LoginResult> GetUserProfile()
        {

            var profile = "";
            LoginResult result = new LoginResult();
            try
            {

                var request = VKApi.Users.Get(NSDictionary.FromObjectAndKey((NSString)@"photo_400_orig", VKApiConst.Fields));
                var response = await request.ExecuteAsync();
                var users = response.ParsedModel as VKUsersArray;
                var account = users?.FirstObject as VKUser;
                //
                if (account != null)
                {
                    result.FirstName = account.first_name;
                    result.LastName = account.last_name;
                    result.ImageUrl = account.photo_400_orig;
                    result.LoginState = LoginState.Success;
                  

                    try
                    {
                        if (account.city != null) _loginResult.City = account.city.ToString();
                        if (account.phone != null) _loginResult.Phone = account.phone;
                        if (account.mobile_phone != null) _loginResult.Phone += account.mobile_phone;
                        if (account.sex != null) _loginResult.Gender = account.sex.ToString();
                        if (account.bdate != null) _loginResult.Birthday = account.bdate;
                    }
                    catch (Exception ex)
                    {
                        UserDialogs.Instance.AlertAsync(ex.Message, this.GetType() + " GetUserInfo");
                    }
                    try
                    {
                        var plist = NSUserDefaults.StandardUserDefaults;
                        if (plist != null)
                        {
                            string id = plist.StringForKey("maxwell_vk_id");
                            UserDialogs.Instance.AlertAsync(id, this.GetType()+"UserProfile DEBUG id");
                            if (!plist.StringForKey("maxwell_vk_id").Equals(null))
                            {

                                if (!plist.StringForKey("maxwell_vk_id").Equals(""))
                                {
                                    result.Email = plist.StringForKey("maxwell_vk_email");

                                    result.UserId = plist.StringForKey("maxwell_vk_id");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                      UserDialogs.Instance.AlertAsync( ex.Message,"UserProfile Error AppleVKService");
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                result.ErrorString = e.Message;
            }

            return result;
        }

        public async Task<string> GetVkIdFromPrefs()
        {
            var plist = NSUserDefaults.StandardUserDefaults;
            if (plist != null)
            {
                return plist.StringForKey("maxwell_vk_id");
            }

            return null;
                

        }
    }
}