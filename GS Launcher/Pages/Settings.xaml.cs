using AsteriaMP.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowsAPICodePack.Dialogs;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using AsteriaMP.Pages;
using Microsoft.Win32;

namespace AsteriaMP.Pages
{
	/// <summary>
	/// Interaction logic for Settings.xaml
	/// </summary>
	public partial class Settings : UserControl
	{
		public Settings()
		{
			InitializeComponent();
        }

		private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			//Regex r = new Regex(@"^[a-zA-Z@]+$");
			//if (!r.IsMatch(e.Text))
				//e.Handled = true;
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{

        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
			commonOpenFileDialog.IsFolderPicker = true;
			commonOpenFileDialog.Title = "Select A Fortnite Build";
			commonOpenFileDialog.Multiselect = false;
			CommonFileDialogResult commonFileDialogResult = commonOpenFileDialog.ShowDialog();

			
			bool flag = commonFileDialogResult == CommonFileDialogResult.Ok;
			if (flag)
			{
				if (File.Exists(System.IO.Path.Join(commonOpenFileDialog.FileName, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping-Reboot.exe")))
				{
					this.PathBox.Text = commonOpenFileDialog.FileName;
				}
				else
				{
					MessageBox.Show("Please make sure that your the folder contains FortniteGame and Engine In");

				}
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			UpdateINI.WriteToConfig("GS","Email", EmailBox.Text);
			UpdateINI.WriteToConfig("GS", "Password", PasswordBox.Password);
			UpdateINI.WriteToConfig("GS", "Path", PathBox.Text);
            UpdateINI.WriteToConfig("GS", "DLL", DLLPathBox.Text);
            UpdateINI.WriteToConfig("GS", "Redirect", RedirectPathBox.Text);
        }

		private void PathBox_TextChanged(object sender, TextChangedEventArgs e)
		{
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog commonOpenFileDialog2 = new CommonOpenFileDialog();
            commonOpenFileDialog2.Title = "Select your Gameserver DLL";
            commonOpenFileDialog2.Multiselect = false;
            CommonFileDialogResult commonFileDialogResult2 = commonOpenFileDialog2.ShowDialog();


            bool flag = commonFileDialogResult2 == CommonFileDialogResult.Ok;
            if (flag)
            {
                    this.DLLPathBox.Text = commonOpenFileDialog2.FileName;
            }
        }
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog commonOpenFileDialog1 = new CommonOpenFileDialog();
            commonOpenFileDialog1.IsFolderPicker = false;
            commonOpenFileDialog1.Title = "Select your Redirect DLL";
            commonOpenFileDialog1.Multiselect = false;
            CommonFileDialogResult commonFileDialogResult1 = commonOpenFileDialog1.ShowDialog();


            bool flag = commonFileDialogResult1 == CommonFileDialogResult.Ok;
            if (flag)
            {
                this.RedirectPathBox.Text = commonOpenFileDialog1.FileName;
            }
        }
    }
}
