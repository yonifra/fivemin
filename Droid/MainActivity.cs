using Android.App;
using Android.Widget;
using Android.OS;
using Auth0.SDK;

namespace FiveMin.Core.Droid
{
    [Activity (Label = "FiveMin", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity{

        private Auth0Client auth0;
        protected override void OnCreate (Bundle savedInstanceState)
        {
            auth0 = new Auth0Client ("cryptocodes.auth0.com", "nYc5cfTehBlni7yCB4QpfgJQOu5876ce");
            base.OnCreate (savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button> (Resource.Id.myButton);

            button.Click += delegate {
                ShowLogin ();
            };;
        }

        private async void ShowLogin ()
        {
            var user = await auth0.LoginAsync (this);

            var usernameTv = FindViewById<TextView> (Resource.Id.usernameTextView);

            usernameTv.Text = user.Profile ["email"].ToString ();
        }
    }


}


