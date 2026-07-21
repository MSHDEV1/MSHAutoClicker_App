using NHotkey;
using NHotkey.Wpf;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace autoclickermsh
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
      
        public bool auto = false;
        
        public MainWindow()
        {
            InitializeComponent();
            HotkeyManager.Current.AddOrReplace("openclose", Key.X, ModifierKeys.Shift, OnHotKeyPressed);
            HotkeyManager.Current.AddOrReplace("opencloseleft", Key.C, ModifierKeys.Shift, OnHotKeyPressedleft);
            Thread thread = new Thread(detect);
            thread.Start();
            Thread threadleft = new Thread(detectleft);
            threadleft.Start();
        }
        
      
       
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const int MOUSEEVENTF_LEFTUP = 0x0004;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        public const int VK_XBUTTON1 = 0x05;
        [DllImport("user32.dll")]
        public static extern short GetKeyState(int vKey);

        [DllImport("user32.dll")]
       
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        public void detect()
        {
            short oldState = GetKeyState(0x05);

            while (true)
            {
                short newState = GetKeyState(0x05);

                if (oldState != newState)
                {
                    oldState = newState;

                    if (newState < 0)
                    {
                        onMouseClick();
                    }
                }

                Thread.Sleep(50);
            }
        }
        public void detectleft()
        {
            short oldState = GetKeyState(0x06);

            while (true)
            {
                short newState = GetKeyState(0x06);

                if (oldState != newState)
                {
                    oldState = newState;

                    if (newState < 0)
                    {
                        onMouseClickleft();
                    }
                }

                Thread.Sleep(50);
            }
        }
        public async void onMouseClickleft()
        {
            if (ss == 0)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    System.Windows.Input.FocusManager.SetFocusedElement(this, null);
                });
                auto = true;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    leftclick.IsEnabled = false;
                    leftclick_false.IsEnabled = true;
                });
               
                ss++;
                await Task.Run(() => StartLeftautoclicker());
            }
            if (ss >= 1)
            {

                auto = false;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    leftclick.IsEnabled = true;
                    leftclick_false.IsEnabled = false;
                });
                    await Task.Run(() => StartLeftautoclicker());
                ss = 0;

            }
           

        }
        public async void onMouseClick()
        {
            if (ss == 0)
            {
                auto = true;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    clicker.IsEnabled = false;
                    clicker_false.IsEnabled = true;
                });
                ss++;
                await Task.Run(() => Startautoclicker());
            }
            if (ss >= 1)
            {

                auto = false;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    clicker.IsEnabled = true;
                    clicker_false.IsEnabled = false;
                });
               
                await Task.Run(() => Startautoclicker());
                ss = 0;

            }

        }
       string soltıklamahızı;
        public async void Startautoclicker()
        {

            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    soltıklamahızı = solhız.Text;
                });
                while (auto == true)
                {

                    Thread.Sleep(Convert.ToInt32(soltıklamahızı));
                    if (auto == true)
                    {

                        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    }
                }
            }
            catch
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Console.WriteLine(solhız.Text);
                });
                auto = false;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    clicker.IsEnabled = true;
                    clicker_false.IsEnabled = false;
                });
                
                ss = 0;
                MessageBox.Show("Lütfen Sayısal Bir Değer Giriniz");
            }
          


        }
        string sagtıklamahızı;
        public async void StartLeftautoclicker()
        {
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    sagtıklamahızı = saghız.Text;
                });

                while (auto == true)
                {
                    Thread.Sleep(Convert.ToInt32(sagtıklamahızı));
                    if (auto == true)
                    {

                        mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                    }
                }
            }
            catch
            {
                
                auto = false;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    leftclick.IsEnabled = true;
                    leftclick_false.IsEnabled = false;
                });

                ss = 0;
                MessageBox.Show("Lütfen Sayısal Bir Değer Giriniz");
            }



        }
        int ss = 0;

        private async void OnHotKeyPressed(object sender, HotkeyEventArgs e)
        {


            if (ss == 0)
            {
                auto = true;
                clicker.IsEnabled = false;
                clicker_false.IsEnabled = true;
                ss++;
                await Task.Run(() => Startautoclicker());
            }
            if (ss >= 1)
            {

                auto = false;
                clicker.IsEnabled = true;
                clicker_false.IsEnabled = false;
                await Task.Run(() => Startautoclicker());
                ss = 0;

            }
            e.Handled = true;
        }
        private async void OnHotKeyPressedleft(object sender, HotkeyEventArgs e)
        {


            if (ss == 0)
            {
                auto = true;
                leftclick.IsEnabled = false;
                leftclick_false.IsEnabled = true;
                ss++;
                await Task.Run(() => StartLeftautoclicker());
            }
            if (ss >= 1)
            {

                auto = false;
                leftclick.IsEnabled = true;
                leftclick_false.IsEnabled = false;
                await Task.Run(() => StartLeftautoclicker());
                ss = 0;

            }
            e.Handled = true;
        }


        private async void clicker_Click(object sender, RoutedEventArgs e)
        {
            auto = true;
            clicker.IsEnabled = false;
            clicker_false.IsEnabled = true;
            await Task.Run(() => Startautoclicker());

        }

        private async void clicker_false_Click(object sender, RoutedEventArgs e)
        {
            auto = false;
            clicker.IsEnabled = true;
            clicker_false.IsEnabled = false;
            await Task.Run(() => Startautoclicker());

        }
     
    

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            grid.Focus();
           windows1.Focus();
        }
       
        private async void windows1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
           
          
         

        }

        private void kuc_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void kapat_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }

        private async void leftclick_Click(object sender, RoutedEventArgs e)
        {
            auto = true;
            leftclick.IsEnabled = false;
            leftclick_false.IsEnabled = true;
            await Task.Run(() => StartLeftautoclicker());
        }

        private async void leftclick_false_Click(object sender, RoutedEventArgs e)
        {
            auto = false;
            leftclick.IsEnabled = true;
            leftclick_false.IsEnabled = false;
            await Task.Run(() => StartLeftautoclicker());
        }
    }
}
