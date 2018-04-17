using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Threading.Tasks;

namespace BPorject
{
    [Activity(Label = "BPorject")]
    public class MainActivity : Activity
    {
        static string RootName="YumiStudio";
        static string PassWord = "949426508";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Button button = FindViewById<Button>(Resource.Id.button);
            TextView Password = FindViewById<TextView>(Resource.Id.PassWord);
            TextView Username = FindViewById<TextView>(Resource.Id.UserName);
            button.Click += (sender, e) =>
            {  
                button.Text = "正在登录";
                if (string.Equals(Username.Text, RootName) == true && string.Equals(Password.Text, PassWord) == true)
                {
                    Task startupWork = new Task(() => { Login(); });
                    startupWork.Start();
                    button.Text = "确认";
                }
                else
                {
                    button.Text = "用户名或密码错误";
                }
            };
        }
        async void Login()
        {
            await Task.Delay(500);
            StartActivity(new Intent(this, typeof(RootActivity)));
        }


    }
}

