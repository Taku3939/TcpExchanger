using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace TCPExchanger
{
    class Server
    {
        private TcpListener _listener;
        public event Action<TcpClient> OnConnectEvent;
        public event Action<Byte[]> receiveCallBack;
        //接続準備、接続待機
        public Server()
        {
            _listener = new TcpListener(IPAddress.Any, 30000);
            _listener.Start();
            Console.WriteLine("Listen");
        }
        //接続要求待ち
        public void Accept()
        {
            _listener.AcceptTcpClientAsync().ContinueWith(task =>
            {
                TcpClient client = task.Result;
                OnConnectEvent(client);
                var rec = Receive(client);
                Console.WriteLine("Accept");
                Accept();
            });
        }
        //データ受信
        public async Task Receive(TcpClient client)
        {
            bool isError = false;
            NetworkStream nStream = client.GetStream();
            MemoryStream mStream = new MemoryStream();
            byte[] gdata = new byte[256];
            do
            {
                int dataSize = await nStream.ReadAsync(gdata, 0, gdata.Length);
                if (dataSize == 0) isError = true;
                await mStream.WriteAsync(gdata, 0, dataSize);
            }
            while (nStream.DataAvailable);
            byte[] receiveBytes = mStream.GetBuffer();

            byte[] data = new byte[mStream.Length];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = receiveBytes[i];
            }
            if (isError) return;
            Console.WriteLine("Recived : " + data.Length + " bytes ");

            receiveCallBack?.Invoke(data);
            mStream.Close();
            client.Close();
        }
    }
}
