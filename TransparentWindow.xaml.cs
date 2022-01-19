using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Runtime.InteropServices;

namespace TappedKeyDisplay
{
    /// <summary>
    /// Interaction logic for TransparentWindow.xaml
    /// </summary>
    public partial class TransparentWindow : Window, IDisposable
    {
        private string TWFont { get; set; }
        private int TWFontSize { get; set; }
        private double TWIntervalTime { get; set; }
        private bool TWEnableLogging { get; set; }
        private DateTime timerStart { get; set; }

        private bool setup = false;
        private double dWAHeight = SystemParameters.WorkArea.Height;
        private double oriSize;
        private double changedSize;

        private bool writefilesetup = false;
        private DateTime writefileTime;
        private string filename;

        private GlobalKeyboardHook _globalKeyboardHook;
        VirtualCodeToKeyboardKey vc2kk = new VirtualCodeToKeyboardKey();
        GlobalKeyboardHookEventArgs a;

        private bool IsClosed { get; set; }

        private Point MouseDownLocation;
        private double tbMarginLastX;
        private double tbMarginLastY;

        DispatcherTimer _timer = new DispatcherTimer();
        public TransparentWindow()
        {
            InitializeComponent();
            SetupKeyboardHooks();

            tbText.Text = "";

            this.tbText.MouseDown += new MouseButtonEventHandler(this.tbText_MouseDown);
            this.tbText.MouseMove += new MouseEventHandler(this.tbText_MouseMove);
            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Interval = TimeSpan.FromMilliseconds(1);
            this.Closed += new EventHandler(OnClosed);

            AllocConsole();
            this.ShowInTaskbar = false;
        }

        private void OnClosed(object sender, EventArgs e)
        {
            Process p = Process.GetCurrentProcess();
            p.CloseMainWindow();
            p.Close();
        }

        private void tbText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                MouseDownLocation = Application.Current.MainWindow.PointToScreen(Mouse.GetPosition(Application.Current.MainWindow));

                tbMarginLastX = tbText.Margin.Left;
                tbMarginLastY = tbText.Margin.Top;
            }
        }

        private void tbText_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //Two ways to get MouseEventArgs e Position
                /*
                Point p = Application.Current.MainWindow.PointToScreen(Mouse.GetPosition(Application.Current.MainWindow));
                p.X or p.Y
                ==========================================================================================================
                e.GetPosition(this).X or e.GetPosition(this).Y
                */

                //Ways to set the object location
                /*
                tbText.TranslatePoint(new Point(0, 0), tbText).X
                =================================================
                tbText.Margin = new Thickness(0, 0, 0, 0);
                =================================================
                Thickness m = tbText.Margin;
                m.Left = 10;
                m.Top = 10;
                tbText.Margin = m;

                P.S. we can't directly set Margin.Left or Margin.Top of the object because it is Thickness Type not double or int.
                */

                Point DragPos = Application.Current.MainWindow.PointToScreen(Mouse.GetPosition(Application.Current.MainWindow));

                double moveX = DragPos.X + tbMarginLastX - MouseDownLocation.X;
                double moveY = DragPos.Y + tbMarginLastY - MouseDownLocation.Y;

                //Use for Debug
                ///*
                IntPtr defaultStdout = new IntPtr(7);
                IntPtr currentStdout = new IntPtr(7);

                Console.WriteLine("MouseDownLocation : X = {0}, Y = {1}", MouseDownLocation.X, MouseDownLocation.Y);
                Console.WriteLine("DragLocation : X = {0}, Y = {1}", DragPos.X, DragPos.Y);
                Console.WriteLine("tbLocation : X = {0}, Y = {1}", tbText.Margin.Left, tbText.Margin.Top);
                Console.WriteLine("MoveLocation : X = {0}, Y = {1}", moveX, moveY);
                Console.SetCursorPosition(0, 0);
                //*/

                tbText.Margin = new Thickness(moveX , moveY, 0, 0);
                Thickness m = tbText.Margin;
                m.Left = moveX;
                m.Top = moveY;
                tbText.Margin = m;

            }
        }

        public void SetupKeyboardHooks()
        {
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
        }

        private void OnKeyPressed(Object sender, GlobalKeyboardHookEventArgs e)
        {
            a = (GlobalKeyboardHookEventArgs)e;
            _timer.Stop();
            timerStart = DateTime.Now;
            _timer.Start();

            if (a.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
            {
                tbText.Text += vc2kk.vc2key(a.KeyboardData.VirtualCode);
                tbText.Text += " ";
                if (!setup)
                {
                    oriSize = tbText.ActualHeight;
                    changedSize = oriSize;
                    setup = true;
                }
                if (tbText.ActualHeight != changedSize)
                {
                    tbText.Margin = new Thickness(10, dWAHeight - tbText.ActualHeight, 0, 0);
                    changedSize = tbText.ActualHeight;
                }
                if (TWEnableLogging)
                {
                    if (!writefilesetup)
                    {
                        writefileTime = DateTime.Now;
                        filename = writefileTime.ToString("yyyy'-'MM'-'dd HH.mm.ss.ffff") + ".log";
                        using (StreamWriter sw = new StreamWriter(filename))
                        {
                            sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff") + ": " + vc2kk.vc2key(a.KeyboardData.VirtualCode));
                            sw.AutoFlush = true;
                        }
                        writefilesetup = true;
                    } else
                    {
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(filename, true))
                            {
                                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff") + ": " + vc2kk.vc2key(a.KeyboardData.VirtualCode));
                                sw.AutoFlush = true;
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }

                }
            }

            if (a.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp)
            {
                if (tbText.ActualHeight != changedSize)
                {
                    tbText.Margin = new Thickness(10, dWAHeight - tbText.ActualHeight, 0, 0);
                    changedSize = tbText.ActualHeight;
                }
            }

            if (e.KeyboardData.VirtualCode != GlobalKeyboardHook.VkSnapshot)
            return;

            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
            {
                e.Handled = true;
            }

        }

        public void ChangeFont(string fontName, int fontSize)
        {
            TWFont = fontName;
            TWFontSize = fontSize;

            tbText.FontFamily = new FontFamily(TWFont);
            tbText.FontSize = TWFontSize;
            tbText.Margin = new Thickness(10, SystemParameters.WorkArea.Height - tbText.ActualHeight, 0, 0);
        }

        public void ChangeIntervalTime(double it)
        {
            TWIntervalTime = it;
        }

        public void SetEnableLogging(bool etl)
        {
            TWEnableLogging = etl;
        }

        public void ChangeTextColour(Color tc)
        {
            tbText.Foreground = new SolidColorBrush(tc);
        }

        public void ChangeBGColour(Color bgc)
        {
            tbText.Background = new SolidColorBrush(bgc);
        }

        public void Dispose()
        {
            _globalKeyboardHook?.Dispose();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var elpasedTime = DateTime.Now - timerStart;
            if (elpasedTime.TotalMilliseconds >= TWIntervalTime)
            {
                tbText.Text = "";
                _timer.Stop();
                changedSize = oriSize;
                tbText.Margin = new Thickness(10, dWAHeight - tbText.FontSize, 0, 0);
            } 
        }


        private const UInt32 StdOutputHandle = 0xFFFFFFF5;
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetStdHandle(UInt32 nStdHandle);
        [DllImport("kernel32.dll")]
        private static extern void SetStdHandle(UInt32 nStdHandle, IntPtr handle);
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();
    }

}
