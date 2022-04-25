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
    [Activity(Label = "UpdateLocation")]
    public class UpdateLocation : Activity
    {
        EditText edtGuid, edtLocation, edtMAC, edtMaxSignalStrength, edtMinSignalStrength;
        Button btnGetGuid, btnSend;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.UpdateLocation);
            edtGuid = FindViewById<EditText>(Resource.Id.edtGUID);
            edtLocation = FindViewById<EditText>(Resource.Id.edtLocation); 
            edtMAC = FindViewById<EditText>(Resource.Id.edtMAC);
            edtMaxSignalStrength = FindViewById<EditText>(Resource.Id.edtMaxSignalStrength);
            edtMinSignalStrength = FindViewById<EditText>(Resource.Id.edtMinSignalStrength);
            btnGetGuid = FindViewById<Button>(Resource.Id.btnGetGuid);
            btnSend = FindViewById<Button>(Resource.Id.btnSend);
            btnGetGuid.Click += async delegate
            {
                LocationInfo locationInfo = null;
                HttpClient client = new HttpClient();
                string url = "";
                var result = await client.GetAsync(url);
                var json = await result.Content.ReadAsStringAsync();
                try
                {
                    locationInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<LocationInfo>(json);
                }
                catch (Exception ex) { }
                if(locationInfo == null)
                {
                    Toast.MakeText(this, json, ToastLength.Short).Show();
                }
                else
                {
                    edtLocation = (EditText)locationInfo.Location;
                    edtMAC.Text = locationInfo.MAC;
                    edtMaxSignalStrength.Text = (string)(EditText)locationInfo.MaxSignalStrength;
                    edtMinSignalStrength.Text = (string)(EditText)locationInfo.MinSignalStrength;

                }
            };

            btnSend.Click += async delegate
            {
                LocationInfo locationInfo = new LocationInfo();
                locationInfo.Location = edtLocation.Text;
                locationInfo.MAC = edtMAC.Text;
                locationInfo.MaxSignalStrength = float.Parse(edtMaxSignalStrength.Text);
                locationInfo.MinSignalStrength = float.Parse(edtMinSignalStrength.Text);
                HttpClient client = new HttpClient();
                string url = $"";
                var uri = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response;
                var json = JsonConvert.SerializeObject(locationInfo);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.PostAsync(url, content);
                Clear();
                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    Toast.MakeText(this, "Your Location Data Has Been Updated", ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(this, "Your Location Data Has NOT Been Updated", ToastLength.Short).Show();
                }
            };

            void Clear()
            {
                edtLocation.Text = "";
                edtMAC.Text = "";
                edtMaxSignalStrength = (EditText)"";
                edtMinSignalStrength = (EditText)"";
            }

        }
    }
}