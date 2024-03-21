using AsteriaMP.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using WpfApp6.Services;
using WpfApp6.Services.Launch;
using System.Diagnostics;
using System.IO.Compression;

namespace AsteriaMP.Pages
{
	/// <summary>
	/// Interaction logic for Home.xaml
	/// </summary>
	public partial class Home : UserControl
	{
		public Home()
		{
			InitializeComponent();
        }

        private void Button_Click(object sender, EventArgs e)
        {

            if (UpdateINI.ReadValue("Auth", "Path") == "" || UpdateINI.ReadValue("Auth", "Path") == null || !File.Exists(UpdateINI.ReadValue("Auth", "Path") + "\\FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe"))
            {
                System.Windows.MessageBox.Show("Please select a valid Fortnite Path first", "INFORMATION");
            }
            else
            {
                using (WebClient webClient = new WebClient())
                {
                    var mc = new Button();
                    mc.IsEnabled = false;
                    if (!File.Exists(Path.GetTempPath() + "\\FortniteClient-Win64-Shipping_BE.exe"))
                        webClient.DownloadFile(new Uri("https://github.com/SkyAlumny/fortnite-launcher-for-og-servers/raw/main/FortniteClient-Win64-Shipping_BE.exe"), Path.GetTempPath() + "\\FortniteClient-Win64-Shipping_BE.exe");
                    if (!File.Exists(Path.GetTempPath() + "\\FortniteLauncher.exe"))
                        webClient.DownloadFile(new Uri("https://github.com/SkyAlumny/fortnite-launcher-for-og-servers/raw/main/FortniteLauncher.exe"), Path.GetTempPath() + "\\FortniteLauncher.exe");

                }
                // Dont ask what i did here i thought i look smart
                ProcessStartInfo launcherSimulatorInfo = new ProcessStartInfo
                {
                    FileName = Path.GetTempPath() + "\\FortniteLauncher.exe",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };
                ProcessStartInfo beSimulatorInfo = new ProcessStartInfo
                {
                    FileName = Path.GetTempPath() + "\\FortniteClient-Win64-Shipping_BE.exe",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };
                ProcessStartInfo actualFortnitePathInfo = new ProcessStartInfo
                {
                    FileName = System.IO.Path.Combine(UpdateINI.ReadValue("GS", "Path") + "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping-Reboot.exe"),
                    Arguments = $"-log -epicapp=Fortnite -epicenv=Prod -epicportal -skippatchcheck -nobe -fromfl=eac -fltoken=3db3ba5dcbd2e16703f3978d -nosplash -caldera=eyJhbGciOiJFUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2NvdW50X2lkIjoiYmU5ZGE1YzJmYmVhNDQwN2IyZjQwZWJhYWQ4NTlhZDQiLCJnZW5lcmF0ZWQiOjE2Mzg3MTcyNzgsImNhbGRlcmFHdWlkIjoiMzgxMGI4NjMtMmE2NS00NDU3LTliNTgtNGRhYjNiNDgyYTg2IiwiYWNQcm92aWRlciI6IkVhc3lBbnRpQ2hlYXQiLCJub3RlcyI6IiIsImZhbGxiYWNrIjpmYWxzZX0.VAWQB67RTxhiWOxx7DBjnzDnXyyEnX7OljJm-j2d88G_WgwQ9wrE6lwMEHZHjBd1ISJdUO1UVUqkfLdU5nofBQ -AUTH_LOGIN={UpdateINI.ReadValue("Auth", "Email")} -AUTH_PASSWORD={UpdateINI.ReadValue("Auth", "Password")} -AUTH_TYPE=epic",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };
                Process procesLauncher = new Process
                {
                    StartInfo = launcherSimulatorInfo
                };
                Process processBe = new Process
                {
                    StartInfo = beSimulatorInfo
                };
                Process processFortnite = new Process
                {
                    StartInfo = actualFortnitePathInfo
                };
                procesLauncher.Start();
                processBe.Start();
                processFortnite.Start();
                AsteriaMP.Services.Launch.Inject.InjectProtection(processFortnite.Id, UpdateINI.ReadValue("GS", "Redirect")); // change to your dll name :)
                AsteriaMP.Services.Launch.Inject.InjectProtection(processFortnite.Id, UpdateINI.ReadValue("GS", "DLL"));
                Environment.Exit(0); // exit because it does not load correctly 
            }
        }
    }
}
