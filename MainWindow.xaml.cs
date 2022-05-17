using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using System.Configuration;
using System.Diagnostics;



namespace TappedKeyDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TransparentWindow tw = new TransparentWindow();
        private string TFont { get; set; }
        private int TFontSize { get; set; }
        private int TIntervalTime { get; set; }
        private bool EnableLogging { get; set; }
        private Color TTextColour { get; set; }
        private Color TBGColour { get; set; }
        private bool EnableDisplay { get; set; }

        int init = 0;

        public MainWindow()
        {
            try
            {
                Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                if (configFile.HasFile == false)
                {
                    List<string> fontList = new List<string>();
                    foreach (FontFamily ff in Fonts.SystemFontFamilies)
                    {
                        fontList.Add(ff.ToString());
                    }

                    Dictionary<string, string> configDictionary = new Dictionary<string, string>();
                    configDictionary.Add("Logging", "0");
                    configDictionary.Add("SizeMaster", "14");
                    configDictionary.Add("IntervalMaster", "500");
                    configDictionary.Add("FontMaster", fontList[0]);
                    configDictionary.Add("TextColour", "#FFFFFFFF");
                    configDictionary.Add("BGColour", "#FFEE82EE");

                    foreach(KeyValuePair<string, string> kvp in configDictionary)
                    {
                        configFile.AppSettings.Settings.Add(kvp.Key, kvp.Value);
                    }
                    
                    configFile.Save(ConfigurationSaveMode.Full);
                    System.Windows.Forms.Application.Restart();
                    Environment.Exit(0);
                }
            }
            catch (ConfigurationErrorsException)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Error writing app settings");
            }
            InitializeComponent();
            tw.Show();
            
        }
        void OnLoad(object sender, RoutedEventArgs e)
        {
            

            foreach (FontFamily ff in Fonts.SystemFontFamilies)
            {
                cbFont.Items.Add(ff.ToString());
            }
            TFont = ConfigurationManager.AppSettings["FontMaster"];
            cbFont.SelectedItem = TFont;
            TFontSize = Convert.ToInt32(ConfigurationManager.AppSettings["SizeMaster"]);
            iudSize.Value = TFontSize;
            TIntervalTime = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalMaster"]);
            iudResetTime.Value = TIntervalTime;
            EnableLogging = Convert.ToBoolean(Convert.ToInt32(ConfigurationManager.AppSettings["Logging"]));
            cbLogging.IsChecked = EnableLogging;
            TTextColour = ParseColor(ConfigurationManager.AppSettings["TextColour"]);
            clrpickerText.SelectedColor = TTextColour;
            TBGColour = ParseColor(ConfigurationManager.AppSettings["BGColour"]);
            clrpickerBG.SelectedColor = TBGColour;

            tw.ChangeIntervalTime(TIntervalTime);
            tw.SetEnableLogging(EnableLogging);
            tw.ChangeFont(TFont, TFontSize);
            tw.ChangeTextColour(TTextColour);
            tw.ChangeBGColour(TBGColour);

            this.Closed += new EventHandler(OnClosed);
        }

        private void cbFont_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TFont)){
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove("FontMaster");
                config.AppSettings.Settings.Add("FontMaster", cbFont.SelectedValue.ToString());
                config.Save(ConfigurationSaveMode.Modified);

                TFont = cbFont.SelectedValue.ToString();
                tw.ChangeFont(TFont, TFontSize);
            } 
            else {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove("FontMaster");
                config.AppSettings.Settings.Add("FontMaster", "Segoe UI");
                config.Save(ConfigurationSaveMode.Modified);

                TFont = "Segoe UI";
                tw.ChangeFont(TFont, TFontSize);
                cbFont.SelectedItem = TFont;
            }
        }

        private void iudSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if  (iudSize.Value != null)
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove("SizeMaster");
                config.AppSettings.Settings.Add("SizeMaster", iudSize.Value.ToString());
                config.Save(ConfigurationSaveMode.Modified);

                TFontSize = Convert.ToInt32(iudSize.Value);
                if (init == 0)
                    init++;
                else
                    tw.ChangeFont(TFont, TFontSize);
            } 
            else 
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove("SizeMaster");
                config.AppSettings.Settings.Add("SizeMaster", "14");
                config.Save(ConfigurationSaveMode.Modified);

                TFontSize = 14;
                if (init == 0)
                    init++;
                else
                    tw.ChangeFont(TFont, TFontSize);
            }
        }

        private void iudResetTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (iudResetTime.Value != null)
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove("IntervalMaster");
                config.AppSettings.Settings.Add("IntervalMaster", iudResetTime.Value.ToString());
                config.Save(ConfigurationSaveMode.Modified);

                TIntervalTime = Convert.ToInt32(iudResetTime.Value);
                tw.ChangeIntervalTime(Convert.ToDouble(TIntervalTime));
            } else
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove("IntervalMaster");
                config.AppSettings.Settings.Add("IntervalMaster", "500");
                config.Save(ConfigurationSaveMode.Modified);

                TIntervalTime = 500;
                tw.ChangeIntervalTime(Convert.ToDouble(TIntervalTime));
            }
        }

        private void cbLogging_CheckedChanged(object sender, RoutedEventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove("Logging");
            config.AppSettings.Settings.Add("Logging", Convert.ToInt32(cbLogging.IsChecked).ToString());
            config.Save(ConfigurationSaveMode.Modified);

            EnableLogging = Convert.ToBoolean(cbLogging.IsChecked);
            tw.SetEnableLogging(EnableLogging);
        }

        private void clrpickerText_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove("TextColour");
            config.AppSettings.Settings.Add("TextColour", clrpickerText.SelectedColor.ToString());
            config.Save(ConfigurationSaveMode.Modified);

            Color c = ParseColor(clrpickerText.SelectedColor.ToString());
            lblTest.Foreground = new SolidColorBrush(c);
            tw.ChangeTextColour(c);
            TTextColour = c;
        }

        private void clrpickerBG_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove("BGColour");
            config.AppSettings.Settings.Add("BGColour", clrpickerBG.SelectedColor.ToString());
            config.Save(ConfigurationSaveMode.Modified);

            Color c = ParseColor(clrpickerBG.SelectedColor.ToString());
            lblTest.Background = new SolidColorBrush(c);
            tw.ChangeBGColour(c);
            TBGColour = c;
        }

        public Color ParseColor(string clrpicker)
        {
            string a = clrpicker.Substring(1);
            byte[] b = new byte[4];
            for (int i = 0; i < a.Length; i += 2)
            {
                b[i / 2] = Convert.ToByte(a.Substring(i, 2), 16);
            }
            return Color.FromArgb(b[0], b[1], b[2], b[3]);
        }

        private void OnClosed(object sender, EventArgs e)
        {
            Process p = Process.GetCurrentProcess();
            p.Kill();
            p.WaitForExit();
            p.Close();
        }

        private void cbDisable_CheckedChanged(object sender, RoutedEventArgs e)
        {
            EnableDisplay = Convert.ToBoolean(cbDisable.IsChecked);
            tw.SetEnableDisplay(EnableDisplay);
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            tw.Close();
            System.Windows.Forms.Application.Restart();
            Environment.Exit(0);
        }

        private void btnOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo("explorer.exe");
            processStartInfo.Arguments = AppContext.BaseDirectory;
            Process.Start(processStartInfo);
        }
    }
}
