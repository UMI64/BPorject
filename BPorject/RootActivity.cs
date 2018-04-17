using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using KdGoldAPI;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Json;

namespace BPorject
{
    [Activity(Label = "快递查询")]
    public class RootActivity : Activity
    {
        int count = 0;
        string result = "", Comp = "";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Root);
            Button RootButton = FindViewById<Button>(Resource.Id.RootButton);
            ListView ListText = FindViewById<ListView>(Resource.Id.listView1);
            EditText NumberText = FindViewById<EditText>(Resource.Id.number);
            // Create your application here
            Button showPopupMenu = FindViewById<Button>(Resource.Id.SelateButtom);

            showPopupMenu.Click += (s, arg) =>
            {

                PopupMenu menu = new PopupMenu(this, showPopupMenu);

                menu.Inflate(Resource.Menu.popup_menu);
                menu.MenuItemClick += (s1, arg1) =>
                {
                    switch (arg1.Item.ItemId)
                    {
                        case Resource.Id.sf: showPopupMenu.Text = "顺风"; Comp = "SF"; break;
                        case Resource.Id.yd: showPopupMenu.Text = "韵达"; Comp = "YD"; break;
                        case Resource.Id.zt: showPopupMenu.Text = "中通"; Comp = "ZT"; break;
                    }
                };
                menu.DismissEvent += (s2, arg2) =>
                {

                };

                menu.Show();
            };
            RootButton.Click += (sender, e) =>
            {
                if (count < 1)
                {
                    RootButton.Text = "查询中...";
                    if (Regex.IsMatch(NumberText.Text, @"^\d+$") && Comp.Length != 0)
                    {
                        Task ChaXun = new Task(() => { KuaiDiChaXun(); });
                        ChaXun.Start();
                    }
                }
                else RootButton.Text = "你点了" + count++ + "下";
            };
             async void KuaiDiChaXun()
            {
                KuaiDiNiaoAPI kuaidi = new KuaiDiNiaoAPI();
                await Task.Delay(10);
                result = kuaidi.getOrderTracesByJson(NumberText.Text, Comp);
                var JSONresult=(JsonObject)JsonObject.Parse(result);
                String[] Traces = (from item in (JsonArray)JSONresult["Traces"] select item.ToString()).ToArray();
            this.RunOnUiThread(() =>
                {
                    ListText.Adapter=new MyCustomeAdapter(this,Traces);
                    RootButton.Text = "你点了" + count++ + "下";
                });
            };
        }
    }
    public class MyCustomeAdapter : BaseAdapter<string>
    {
        string[] items;
        Activity activity;

        public MyCustomeAdapter(Activity context, string[] values): base()
        {
            activity = context;
            items = values;
        }

        public override string this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Length; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View v = convertView;
            if (v == null)
                v = activity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            v.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items[position];
            return v;
        }
    }
}