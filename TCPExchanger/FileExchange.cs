using System;
using System.IO;
using System.Net.Sockets;
using System.Windows;
using System.Text;
using System.Threading.Tasks;

namespace TCPExchanger
{
    class FileExchange
    {
        //ファイルをバイナリ形式で読み込む
        public void LoadFile(string filename, TcpClient client)
        {
            var readbyte = File.ReadAllBytes(filename);
            string file = Path.GetFileName(filename);
            byte[] namebyte = Encoding.UTF8.GetBytes(file);

            //FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            //string file = Path.GetFileName(filename);
            //byte[] namebyte = Encoding.UTF8.GetBytes(file);
            //byte[] readbyte = new byte[fs.Length];
            //fs.Read(readbyte, 0, readbyte.Length);
            //fs.Close();

            byte[] bytes = new byte[namebyte.Length+readbyte.Length+1] ;
          
            Array.Copy(namebyte,0,bytes,0,namebyte.Length);
            Array.Copy(readbyte,0,bytes,namebyte.Length + 1,readbyte.Length);
            bytes[namebyte.Length] = 255;
            Client cl = new Client();
            cl.Send(client, bytes);
            Console.WriteLine("convert");
        }
        public void WriteFile(string path, byte[] res)
        {
            string filename;
            var filebyte = GetbyByte(res, out filename);
            FileStream fs = new FileStream(path + filename, FileMode.Create, FileAccess.Write);
            fs.WriteAsync(filebyte, 0, filebyte.Length).ContinueWith(task =>
            {
                fs.Close();
                Console.WriteLine("write");
                MessageBox.Show("send");
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        public static byte[] GetbyByte(byte[] bytes,out string name)
        {
            byte[] namebyte, filebyte;
            if (!CutByte(bytes, out namebyte, out filebyte)) throw new Exception("Error");
            name = Encoding.UTF8.GetString(namebyte);
            return filebyte;

        }

        public static bool CutByte(byte[] bytes, out byte[] head, out byte[] footer)
        {
            head = null; footer = null;
            for (int i = 0; i < bytes.Length; i++)
            {
                if(bytes[i]==255)
                {
                    head = new byte[i];
                    Array.Copy(bytes, 0, head, 0, i);
                    footer = new byte[bytes.Length-i-1];
                    Array.Copy(bytes, i + 1, footer, 0, bytes.Length - i - 1);
                    return true;
                }
            }
            return false;
        }
    }
}
