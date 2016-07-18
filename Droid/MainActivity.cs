using Android.App;
using Android.Widget;
using Android.OS;
using Auth0.SDK;
using System;
using Android.Support.Design.Widget;
using Android.Views;

namespace FiveMin.Core.Droid
{
    [Activity (Label = "FiveMin", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private Auth0Client _auth0;
        private View _view;

        protected override void OnCreate (Bundle savedInstanceState)
        {
            _auth0 = new Auth0Client ("cryptocodes.auth0.com", "nYc5cfTehBlni7yCB4QpfgJQOu5876ce");
            base.OnCreate (savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            _view = FindViewById<LinearLayout> (Resource.Id.mainView);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button> (Resource.Id.loginButton);

            button.Click += delegate {
                ShowLogin ();
            };;
        }

        private async void ShowLogin ()
        {
            Auth0User user = null;
            try
            {
                user = await _auth0.LoginAsync (this);

                if (user != null && user.Profile != null)
                {
                    var usernameTv = FindViewById<TextView> (Resource.Id.usernameTextView);
                    var emailTv = FindViewById<TextView> (Resource.Id.emailTextView);
                 //   var picTv = FindViewById<TextView> (Resource.Id.pictureTextView);

                    var email = user.Profile ["email"].ToString ();
                    var name = user.Profile ["name"].ToString ();
               //     var profile = user.Profile ["profile"]?.ToJson ();

                    usernameTv.SetText (name, TextView.BufferType.Normal);
                    emailTv.SetText (email, TextView.BufferType.Normal);

                    Snackbar.Make (_view, string.Format ("Logged in as {0}", name), Snackbar.LengthLong).Show();
                    //  picTv.Text = user.Profile
                }
            }
            catch (Exception ex)
            {
                Snackbar.Make (_view, "Login failed", Snackbar.LengthLong).Show ();
            }
        }
    }
}


