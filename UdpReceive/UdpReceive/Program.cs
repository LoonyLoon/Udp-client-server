using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UdpReceive
{
    class Program
    {

       // static string remoteAddress; // хост для отправки данных
      //  static int remotePort; // порт для отправки данных
        static int localPort; // локальный порт для прослушивания входящих подключений

        static void Main(string[] args)
        {
            //Console.Write("Введите порт для прослушивания: "); // локальный порт
            localPort = Int32.Parse("8002");

            Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
            receiveThread.Start();
        }

               private static void ReceiveMessage()
               {
                   UdpClient receiver = new UdpClient(localPort); // UdpClient для получения данных
                   IPEndPoint remoteIp = null; // адрес входящего подключения
                   try
                   {
                       while (true)
                       {
                           byte[] Receivedata = receiver.Receive(ref remoteIp); // получаем данные
                           
                           MemoryStream ms = new MemoryStream(Receivedata);
                           BinaryReader reader = new BinaryReader(ms);

                           byte g = reader.ReadByte();
                           UInt16 u16 = reader.ReadUInt16();
                           UInt32 u32 = reader.ReadUInt32();
                           byte[] str = reader.ReadBytes(Receivedata.Length);
                           String message = Encoding.Unicode.GetString(str);  

                           Console.WriteLine("g" + g);
                           Console.WriteLine("u16" + u16);
                           Console.WriteLine("u32" + u32);
                           Console.WriteLine("str " + message);

                       }
                   }
                   catch (Exception ex)
                   {
                       Console.WriteLine(ex.Message);
                   }
                   finally
                   {
                       receiver.Close();
                   }
               }
    }
}
