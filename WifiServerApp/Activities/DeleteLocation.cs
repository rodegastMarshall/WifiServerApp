using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace WifiServerApp.Activities
{
    [Activity(Label = "DeleteLocation")]
    public class DeleteLocation : Activity
    {
        private EditText edtGUID;
        private Button btnDelete;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DeleteLocation);
            edtGUID = FindViewById<EditText>(Resource.Id.edtGUID);
            btnDelete = FindViewById<Button>(Resource.Id.btnDelete);
            btnDelete.Click += async delegate
            {
                LocationInfo locationInfo = new LocationInfo();
                locationInfo.Id = Guid.Parse(edtGUID.Text);
                HttpClient client = new HttpClient();
                string url = "";
                var uri = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response;
                var json = JsonConvert.SerializeObject(locationInfo);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.PostAsync(url, content);
                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    Toast.MakeText(this, "Your Location Data Has Been Deleted", ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(this, "Your Location Data Has NOT Been Deleted", ToastLength.Short).Show();
                }
            };
        }
    }
}