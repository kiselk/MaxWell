using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreGraphics;
using Facebook.CoreKit;
using Facebook.LoginKit;
using Foundation;
using MaxWell;
using MaxWell.iOS;
using MaxWell.Services;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: Dependency(typeof(AppleFbService))]
namespace MaxWell.iOS
{
    public class AppleFbService : NSObject, IFbService
    {
        readonly LoginManager _loginManager = new LoginManager();
        readonly string[] _permissions = { @"public_profile", @"email" };

        LoginResult _loginResult;
        TaskCompletionSource<LoginResult> _completionSource;

        public Task<LoginResult> Login()
        {
            _completionSource = new TaskCompletionSource<LoginResult>();
            _loginManager.LogInWithReadPermissionsAsync(_permissions);
            return _completionSource.Task;
        }

        public Task<LoginResult> GetUserProfile()
        {
            throw new NotImplementedException();
        }

        public Task<string[]> GetFingerprints()
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            _loginManager.LogOut();
        }
        public bool IsLoggedIn()
        {
            AccessToken accessToken = AccessToken.CurrentAccessToken;
            return accessToken != null;
        }
        void LoginManagerLoginHandler(LoginManagerLoginResult result, NSError error)
        {
            if (result.IsCancelled)
                _completionSource.TrySetResult(new LoginResult { LoginState = LoginState.Canceled });
            else if (error != null)
                _completionSource.TrySetResult(new LoginResult { LoginState = LoginState.Failed, ErrorString = error.LocalizedDescription });
            else
            {
                _loginResult = new LoginResult
                {
                    Token = result.Token.TokenString,
                    UserId = result.Token.UserID,
                    ExpireAt = result.Token.ExpirationDate.ToDateTime()
                };

                var request = new GraphRequest(@"me", new NSDictionary(@"fields", @"email"));
                request.Start(GetEmailRequestHandler);
            }
        }

        void GetEmailRequestHandler(GraphRequestConnection connection, NSObject result, NSError error)
        {
            if (error != null)
                _completionSource.TrySetResult(new LoginResult { LoginState = LoginState.Failed, ErrorString = error.LocalizedDescription });
            else
            {
                _loginResult.FirstName = Profile.CurrentProfile.FirstName;
                _loginResult.LastName = Profile.CurrentProfile.LastName;
                _loginResult.ImageUrl = Profile.CurrentProfile.ImagePath(ProfilePictureMode.Square, new CGSize()).ToString();

                var dict = result as NSDictionary;
                var emailKey = new NSString(@"email");
                if (dict != null && dict.ContainsKey(emailKey))
                    _loginResult.Email = dict[emailKey]?.ToString();

                _loginResult.LoginState = LoginState.Success;
                _completionSource.TrySetResult(_loginResult);
            }
        }

        static UIViewController GetCurrentViewController()
        {
            var viewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
            while (viewController.PresentedViewController != null)
                viewController = viewController.PresentedViewController;
            return viewController;
        }
    }
}