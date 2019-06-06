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
using System.Windows.Forms;

namespace TaskScheduler
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
 
        }




























        private void HideToTray(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                Hide();
        }

        private void HideToTrayInsteadClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            NotifyIcon nIcon = new NotifyIcon();
            nIcon.Icon = new System.Drawing.Icon(@"C:\Users\ТурмаханД\Downloads\clock_time_watch.ico");
            this.ShowInTaskbar = true;
            e.Cancel = true;
            Hide();
            nIcon.Visible = true;
            nIcon.ShowBalloonTip(5000, "Программа перешла в фоновый режим", "Вы можете открыть ее кликнув на иконку", ToolTipIcon.Info);
            nIcon.Click += nIcon_Click;
        }

        void nIcon_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = WindowState.Normal;
        }
    }
 }

