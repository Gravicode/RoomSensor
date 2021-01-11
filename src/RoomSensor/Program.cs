using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;
using GHI.Glide;

namespace RoomSensor
{
    public partial class Program
    {
        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            /*******************************************************************************************
            Modules added in the Program.gadgeteer designer view are used by typing 
            their name followed by a period, e.g.  button.  or  camera.
            
            Many modules generate useful events. Type +=<tab><tab> to add a handler to an event, e.g.:
                button.ButtonPressed +=<tab><tab>
            
            If you want to do something periodically, use a GT.Timer and handle its Tick event, e.g.:
                
            *******************************************************************************************/


            // Use Debug.Print to show messages in Visual Studio's "Output" window during debugging.
            Debug.Print("Program Started");
            setup();
        }
        static bool IsEmpty = true;
        GHI.Glide.Display.Window MainWindow;
        GHI.Glide.UI.Button txtTemp;
        GHI.Glide.UI.Button txtHumid;
        GHI.Glide.UI.Button txtLight;
        GHI.Glide.UI.Button txtMotivasi;

        string[] motivasi = new string[] { "Makan teros", "Tetep Semangat", "Jangan Bau", "Kerja + Maen", "Banyak Doa", "Jangan Mikir Jorok", "Hemat", "Inget Ma Alloh", "No Logay", "Ganteng Terus", "Wangi Donk", "Belajar Donk", "Jangan Males" };
        void setup()
        {
            GlideTouch.Initialize();
            
            MainWindow = GlideLoader.LoadWindow(Resources.GetString(Resources.StringResources.Form2));
            txtTemp = (GHI.Glide.UI.Button)MainWindow.GetChildByName("Btn1");
            txtHumid = (GHI.Glide.UI.Button)MainWindow.GetChildByName("Btn2");
            txtLight = (GHI.Glide.UI.Button)MainWindow.GetChildByName("Btn3");
            txtMotivasi = (GHI.Glide.UI.Button)MainWindow.GetChildByName("Btn4");
    
            Glide.MainWindow = MainWindow;
            Glide.FitToScreen = true;
            //GT.Timer timer = new GT.Timer(1000); // every second (1000ms)
            //timer.Tick += timer_Tick;
            //timer.Start();
            Thread th1 = new Thread(new ThreadStart(UpdateSensor));
            th1.Start();
        }
        void UpdateSensor()
        {
            Random rnd = new Random();
            while (true)
            {
                //clock signal on socket 6 is being held low
                txtHumid.Text = "HUMID: "+ tempHumidSI70.TakeMeasurement().RelativeHumidity.ToString("n2") + " %";
                txtLight.Text = "LIGHT: " + System.Math.Round( lightSense.GetIlluminance()) + " lux";
                txtTemp.Text = "TEMP: "+tempHumidSI70.TakeMeasurement().Temperature.ToString("n2") + " C";
                txtMotivasi.Text = motivasi[rnd.Next(motivasi.Length)];
                txtMotivasi.Invalidate();
                txtHumid.Invalidate();
                txtTemp.Invalidate();
                txtLight.Invalidate();
                //MainWindow.Invalidate();
                Thread.Sleep(1000);
            }

        }
    
    }
}
