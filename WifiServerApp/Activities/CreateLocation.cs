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
    [Activity(Label = "CreateLocation")]
    public class CreateLocation : Activity
    {
        private EditText edtLocation, edtMAC, edtMaxSignalStrength, edtMinSignalStrength;
        private Button btnSend;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.CreateLocation);

            edtLocation = FindViewById<EditText>(Resource.Id.edtLocation);
            edtMAC = FindViewById<EditText>(Resource.Id.edtMAC);
            edtMaxSignalStrength = FindViewById<EditText>(Resource.Id.edtMaxSignalStrength);
            edtMinSignalStrength = FindViewById<EditText>(Resource.Id.edtMinSignalStrength);
            btnSend = FindViewById<Button>(Resource.Id.btnSend);

            btnSend.Click += async delegate 
            { 
                LocationInfo locationInfo = new LocationInfo();
                locationInfo.Location = edtLocation.Text;
                locationInfo.MAC = edtMAC.Text;
                locationInfo.MaxSignalStrength = float.Parse(edtMaxSignalStrength.Text);
                locationInfo.MinSignalStrength = float.Parse(edtMinSignalStrength.Text);
                HttpClient client = new HttpClient();
                string url = "";
                var uri = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response;
                var json = JsonConvert.SerializeObject(locationInfo);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.PostAsync(url, content);
                Clear();
                if(response.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    Toast.MakeText(this, "Your Location Data Has Been Saved", ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(this, "Your Location Data Has NOT Been Saved", ToastLength.Short).Show();
                }
            };
        }

        void Clear()
        {
            edtLocation.Text = "";
            edtMAC.Text = "";
            edtMaxSignalStrength = (EditText)"";
            edtMinSignalStrength = (EditText)"";
        }
    }
}