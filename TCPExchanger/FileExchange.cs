using System;
using static System.Console;
using System.IO;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

namespace TCPExchanger
{
    class FileExchange 
    {
        //ファイル（名前データを含む）をバイトでもどす
        public Byte[] LoadFile(string filename)
        {
            var readbyte = File.ReadAllBytes(filename);
            string file = Path.GetFileName(filename);
            byte[] namebyte = Encoding.UTF8.GetBytes(file);

            byte[] bytes = new byte[namebyte.Length+readbyte.Length+1] ;
          
            Array.Copy(namebyte,0,bytes,0,namebyte.Length);
            Array.Copy(readbyte,0,bytes,namebyte.Length + 1,readbyte.Length);
            bytes[namebyte.Length] = 255;
            Console.WriteLine("convert");
            //Client cl = new Client();
            return bytes;
        }
        
        //ファイルのpathとbyteを渡すと書き込む
        public void WriteFile(string path, byte[] res)
        {
            string filename;
            var filebyte = GetbyByte(res, out filename);
            FileStream fs = new FileStream(path + filename, FileMode.Create, FileAccess.Write);
            fs.WriteAsync(filebyte, 0, filebyte.Length).ContinueWith(task =>
            {
                Console.WriteLine("write");
                MessageBox.Show("send");
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        //CutByteをつかって名前のstringとファイルのバイトを取得する
        public static byte[] GetbyByte(byte[] bytes,out string name)
        {
            byte[] namebyte, filebyte;
            if (!CutByte(bytes, out namebyte, out filebyte)) throw new Exception("Error");
            name = Encoding.UTF8.GetString(namebyte);
            return filebyte;
        }

        //名前と本体のバイトデータにわける
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


    class JsonExchange
    {
        /// <summary>
        /// 任意のオブジェクトを JSON メッセージへシリアライズします。
        /// </summary>
        public static string Serialize(object graph)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(graph.GetType());
                serializer.WriteObject(stream, graph);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// Jsonメッセージをオブジェクトへデシリアライズします。
        /// </summary>
        public static T Deserialize<T>(string message)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(message)))
            {
                var deserialized = new DataContractJsonSerializer(typeof(T));
                Console.WriteLine(@"Name:{deserialized.Name}");
                return (T) deserialized.ReadObject(stream);
            }
        }
    }
}
