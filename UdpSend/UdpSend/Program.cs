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

namespace UdpClientApp
{
    class Pack
    {
        public byte[] BitArrayToByteArray(BitArray bits)
        {
            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(ret, 0);
            return ret;
        }

        public byte[] SendByte(byte u8, UInt16 u16, UInt32 u32, String message)
        {

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);


            byte[] str = Encoding.Unicode.GetBytes(message);

            writer.Write(u8);
            writer.Write(u16);
            writer.Write(u32);
            writer.Write(str);

            byte[] final = stream.ToArray();
            return final;
        }
    };


    class Program
    {
        static string remoteAddress; // хост для отправки данных
        static int remotePort; // порт для отправки данных
        static int localPort; // локальный порт для прослушивания входящих подключений

        static void Main(string[] args)
        {
            try
             {
           // Console.Write("Введите порт для прослушивания: "); // локальный порт
            localPort = Int32.Parse("8001");
           // Console.Write("Введите удаленный адрес для подключения: ");
            remoteAddress = "127.0.0.1"; // адрес, к которому мы подключаемся
           // Console.Write("Введите порт для подключения: ");
            remotePort = Int32.Parse("8002"); // порт, к которому мы подключаемся

            SendMessage(); // отправляем сообщение
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void SendMessage()
        {
            UdpClient sender = new UdpClient(); // создаем UdpClient для отправки сообщений
            try
            {
                while (true)
                {

                    byte s8 = 1;
                    UInt16 u16 = 2;
                    UInt32 u32 = 3;
                    string str = "Hello Zaslon";

                    Pack m = new Pack();

                    for (int i = 0; i < 100; i++)
                    {
                        s8++;
                        byte[] data = m.SendByte(s8, u16, u32, str);
                        sender.Send(data, data.Length, remoteAddress, remotePort); // отправка
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sender.Close();
            }
          }


        }
}