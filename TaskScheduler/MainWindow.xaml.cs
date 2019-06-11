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
using System.IO;
using System.Threading;

namespace TaskScheduler
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ActionTask newTask = new ActionTask();
        public MainWindow()
        {
            InitializeComponent();
            periodicityComboBox.ItemsSource = Enum.GetValues(typeof(Periodicity)).Cast<Periodicity>();
        }


        private void directoryChooseClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            newTask.From = fileDialog.FileName;
            actionTextBox.Text = fileDialog.FileName;
            newTask.From = fileDialog.FileName;
        }

        private void directoryChoose1Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fileDialog = new FolderBrowserDialog();
            fileDialog.ShowDialog();
            newTask.To = fileDialog.SelectedPath;
            actionTextBoxReplace.Text = fileDialog.SelectedPath;
            FileInfo file = new FileInfo(newTask.From);
            string name = file.Name;
            string ext = file.Extension;
            newTask.To = fileDialog.SelectedPath + "\\" + name + "." + ext;

        }

        private void SendingEmailSelected()
        {
            actionDescription.Visibility = Visibility.Visible;
            actionDescription.Text = "Получатель:";
            actionTextBox.Visibility = Visibility.Visible;
            directoryChoose.Visibility = Visibility.Collapsed;
            directoryChoose1.Visibility = Visibility.Collapsed;
            actionTextBoxReplace.Visibility = Visibility.Visible;
            actionDescriptionReplace.Text = "Message:";
            actionDescriptionReplace.Visibility = Visibility.Visible;

            //newTask.EmailAddress = actionTextBox.Text;
        }

        private void DownloadSelected()
        {
            actionDescription.Visibility = Visibility.Visible;
            actionDescription.Text = "Ссылка:";
            actionTextBox.Visibility = Visibility.Visible;
            directoryChoose.Visibility = Visibility.Collapsed;
            directoryChoose1.Visibility = Visibility.Collapsed;
            actionTextBoxReplace.Visibility = Visibility.Collapsed;
            actionDescriptionReplace.Visibility = Visibility.Collapsed;

            //  newTask.DownloadDirectory = actionTextBox.Text;
        }

        private void ReplaceSelected()
        {
            actionDescription.Visibility = Visibility.Visible;
            actionDescription.Text = "Путь к файлу:";
            actionTextBox.Visibility = Visibility.Visible;
            actionTextBoxReplace.Visibility = Visibility.Visible;
            actionDescriptionReplace.Visibility = Visibility.Visible;
            actionDescriptionReplace.Text = "Переместить в";
            directoryChoose.Visibility = Visibility.Visible;
            directoryChoose1.Visibility = Visibility.Visible;

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

        private void periodicityChanged(object sender, SelectionChangedEventArgs e)
        {
            if (periodicityComboBox.SelectedIndex == 0)
            {
                onceAMonth.Visibility = Visibility.Collapsed;
                onceAWeek.Visibility = Visibility.Visible;
                dayOfWeek.ItemsSource = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();
            }
            else if (periodicityComboBox.SelectedIndex == 1)
            {
                onceAWeek.Visibility = Visibility.Collapsed;
                onceAMonth.Visibility = Visibility.Visible;

            }
        }

        private void datePickerSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePicker.SelectedDate < DateTime.Now)
            {
                System.Windows.MessageBox.Show("Дата не может быть меньше нынешней!", "Error!");
                datePicker.SelectedDate = null;
            }
        }

        private void typeComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (typeComboBox.SelectedIndex == 0)
            {
                SendingEmailSelected();
            }
            else if (typeComboBox.SelectedIndex == 1)
            {
                ReplaceSelected();
            }
            else if (typeComboBox.SelectedIndex == 2)
            {
                DownloadSelected();
            }
        }

        private void SaveToDB(object obj)
            {
                using (var context = new TaskContext())
                {
                    context.Tasks.Add(newTask);
                    context.SaveChanges();
                }
            }

        private void CreateNewTask(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text) || !datePicker.SelectedDate.HasValue || typeComboBox.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("Fill all blanks!", "Error!");
                return;
            }
            else
            {
                FileDownloader downloader = new FileDownloader();
                MessageSender mSender = new MessageSender();
                newTask.TaskName = textBox.Text;
                TimeSpan? needDate = newTask.Date - DateTime.Now;
                var needDate1 = TimeSpan.Parse(needDate.ToString());
                //newTask.Periodicity = periodicityComboBox.SelectedItem as Periodicity;
                if (typeComboBox.SelectedIndex == 0)
                {
                    newTask.EmailAddress = actionTextBox.Text;
                    newTask.Message = actionTextBoxReplace.Text;

                    if (periodicityComboBox.SelectedIndex == 0)
                    {
                        System.Threading.Timer timer = new System.Threading.Timer(mSender.SendMessage, new MessageAttributes
                        {
                            Subject = textBox.Text,
                            Message = actionTextBox.Text,
                            To = actionTextBoxReplace.Text
                        }, TimeSpan.FromDays(1), TimeSpan.FromDays(7));
                    }
                    else if (periodicityComboBox.SelectedIndex == 1)
                    {
                        TimeSpan start = datePicker.SelectedDate.GetValueOrDefault() - DateTime.Now;
                        System.Threading.Timer timer = new System.Threading.Timer(mSender.SendMessage, new MessageAttributes
                        {
                            Subject = textBox.Text,
                            Message = actionTextBox.Text,
                            To = actionTextBoxReplace.Text
                        }, start, TimeSpan.FromDays(30));
                    }

                }
                else if (typeComboBox.SelectedIndex == 2)
                {
                    newTask.DownloadDirectory = actionTextBox.Text;
                    if (periodicityComboBox.SelectedIndex == 0)
                    {
                        System.Threading.Timer timer = new System.Threading.Timer(downloader.Download, actionTextBox.Text, TimeSpan.FromDays(1), TimeSpan.FromDays(7));
                    }
                    else if(periodicityComboBox.SelectedIndex==1)
                    {
                        TimeSpan start = datePicker.SelectedDate.GetValueOrDefault() - DateTime.Now;
                        System.Threading.Timer timer = new System.Threading.Timer(downloader.Download, actionTextBox.Text,start , TimeSpan.FromDays(30));
                    }

                }

                var thread = ThreadPool.QueueUserWorkItem(SaveToDB);
            }



        }
    }
}

