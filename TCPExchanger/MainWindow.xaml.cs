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
using Microsoft.Win32;

namespace TCPExchanger
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
      //  private Client client;
        public MainWindow()
        {
            InitializeComponent();
          
        }

        private void TextBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            // ファイルの種類を設定
            dialog.Filter = "全てのファイル (*.*)|*.*";
            // 説明文を設定
          //  dialog.Description = "ファイルを選択してやんよ";
          if (dialog.ShowDialog() == true)
            {
                var filename = dialog.FileName;
                File.Text = filename;
             //   MessageBox.Show(dialog.FileName);
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
    
            string ip = ipText.Text;
            int port = 30000;
            string filename = File.Text;
            Client client = new Client();
            //fe.OnSendEvent += (data) =>
            //{
            //    client.Send(data);
            //};
            client.Connect(ip, port,filename);
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var receiveWindow = new ReceiveWindow();
            receiveWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ipText.Text = "localhost";
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
