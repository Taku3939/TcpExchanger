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
using System.Windows.Shapes;


namespace TCPExchanger
{
    /// <summary>
    /// ReceiveWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ReceiveWindow : Window
    {
        Server server;
        public ReceiveWindow()
        {
            InitializeComponent();
        }
        private void ReceiveButton_Click(object sender, RoutedEventArgs e)
        {
            string path = pathText.Text;
            server = new Server();
            var fe = new FileExchange();
            server.OnConnectEvent += (tcpClient) =>
            {
                Console.WriteLine(tcpClient.Client.RemoteEndPoint);
            };
            server.receiveCallBack += (bytes) =>
            {
                fe.WriteFile(path, bytes);
            };
            server.Accept();

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            pathText.Text = @"E:\";
        }

        private void AppExit(object sender, EventArgs e)
        {
            server.CloseListen();
        }
    }
}
