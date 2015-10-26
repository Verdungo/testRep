using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using System.Threading;

namespace SimpleThread
{
    [Activity(Label = "SimpleThread", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        const string LOG_TAG = "MyLogs";

        const int STATUS_NONE = 0;
        const int STATUS_CONNECTING = 1;
        const int STATUS_CONNECTED = 2;
        const int STATUS_DOWNLOAD_START = 3;
        const int STATUS_DOWNLOAD_FILE = 4;
        const int STATUS_DOWNLOAD_END = 5;
        const int STATUS_DOWNLOAD_NONE = 6;
        

        Handler h;
        TextView tvStatus;
        Button btnConnect;
        ProgressBar pbDownload;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            tvStatus = FindViewById<TextView>(Resource.Id.tvStatus);
            btnConnect = FindViewById<Button>(Resource.Id.btnConnect);
            pbDownload = FindViewById<ProgressBar>(Resource.Id.pbDownLoad);

            btnConnect.Click += BtnClick;


            h = new Handler(new Action<Message>((Message msg) => 
            {
                switch (msg.What)
                {
                    case STATUS_NONE:
                        btnConnect.Enabled = true;
                        tvStatus.Text = "Not connected";
                        pbDownload.Visibility = ViewStates.Gone;
                        break;
                    case STATUS_CONNECTING:
                        btnConnect.Enabled = false;
                        tvStatus.Text = "Connecting";
                        break;
                    case STATUS_CONNECTED:
                        tvStatus.Text = "Connected";
                        break;
                    case STATUS_DOWNLOAD_START:
                        tvStatus.Text = String.Format("Start download {0} files.",msg.Arg1);
                        pbDownload.Max = msg.Arg1;
                        pbDownload.Progress = 0;
                        pbDownload.Visibility = ViewStates.Visible;
                        break;
                    case STATUS_DOWNLOAD_FILE:
                        tvStatus.Text = String.Format("Downloading. {0} files left.", msg.Arg2);
                        pbDownload.Progress = msg.Arg1;
                        SaveFile((byte[])msg.Obj);
                        break;
                    case STATUS_DOWNLOAD_END:
                        tvStatus.Text = String.Format("Download comlete.");
                        break;
                    case STATUS_DOWNLOAD_NONE:
                        tvStatus.Text = String.Format("No files for download.");
                        break;
                    default:
                        break;
                }
            }));
            h.SendEmptyMessage(STATUS_NONE);
        }

        private void BtnClick(object sender, EventArgs e)
        {
            Message m;
            byte[] file;
            Random rnd = new Random();

            Thread t = new Thread(new ThreadStart(() => 
            {
                try
                {
                    h.SendEmptyMessage(STATUS_CONNECTING);
                    Thread.Sleep(1000);

                    h.SendEmptyMessage(STATUS_CONNECTED);
                    Thread.Sleep(1000);

                    int filesToSend = rnd.Next(5);

                    if (filesToSend == 0)
                    {
                        h.SendEmptyMessage(STATUS_DOWNLOAD_NONE);
                        Thread.Sleep(2000);
                        h.SendEmptyMessage(STATUS_NONE);
                    }
                    else
                    {
                        m = h.ObtainMessage(STATUS_DOWNLOAD_START, filesToSend, 0);
                        h.SendMessage(m);

                        for (int i = 1; i <= filesToSend ; i++)
                        {
                            file = DownloadFile();
                            m = h.ObtainMessage(STATUS_DOWNLOAD_FILE, i, filesToSend - 1, file);
                            h.SendMessage(m);
                        }

                        h.SendEmptyMessage(STATUS_DOWNLOAD_END);

                        Thread.Sleep(2000);
                        h.SendEmptyMessage(STATUS_NONE);
                    }

                    h.SendEmptyMessage(STATUS_NONE  );
                }
                catch (Exception ex)
                {
                    Log.Debug(LOG_TAG, ex.Message);
                }
            }));
            t.Start();
        }

        private byte[] DownloadFile()
        {
            Thread.Sleep(3000);
            return new byte[1024];
        }

        private void SaveFile(byte[] obj)
        {

        }
    }
}

