using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows;

namespace TCPExchanger
{
    class Client
    {
        private TcpClient _client;
    
        public void Connect(string ip, int port,string file)
        {
            try
            {
                //接続要求
                _client = new TcpClient(ip, port);
                Console.WriteLine("Connect");
            }
            catch
            {
                Console.WriteLine("Error");
                return ;
            }
 
            var fe = new FileExchange();
            fe.LoadFile(file,_client);
 
            }
        public void Send(TcpClient client ,Byte[] sendBytes){
            //ファイルの送信
            var nstream = client.GetStream();
            nstream.ReadTimeout = 15000;
            nstream.WriteTimeout = 15000;

            nstream.WriteAsync(sendBytes, 0, sendBytes.Length).ContinueWith(task =>
            {
                Console.WriteLine("send");
                MessageBox.Show("sendeBytes" + sendBytes.Length.ToString() + "Bytes"); 
                nstream.Close();
            },TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
