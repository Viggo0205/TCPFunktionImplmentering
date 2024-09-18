using System;
using System.Diagnostics.Metrics;
using System.IO;
using System.Net;
using System.Net.Sockets;
//using System.Threading.Tasks;

Console.WriteLine("TCP server 1");

TcpListener listener = new TcpListener(IPAddress.Any, 7);
listener.Start();
while (true)
{
    TcpClient socket = listener.AcceptTcpClient();
    Task.Run(() => HandleClient(socket));
}

void HandleClient(TcpClient socket)
{

    NetworkStream ns = socket.GetStream();
    StreamReader reader = new StreamReader(ns);
    StreamWriter writer = new StreamWriter(ns);
    int count = 0;
    while (socket.Connected)
    {
        
        string message = reader.ReadLine().ToLower();
        count++;
        Console.WriteLine(message);
        if (message == "stop")
        {
            writer.WriteLine("Goodbye world");
            writer.Flush();
            socket.Close();
        }
        else if (message == "roll the dice")
        {
            Random random = new Random();
            int n = random.Next(1, 6);
            string sn = n.ToString();
            writer.WriteLine(sn);
            writer.Flush();
        }
        else if (message == "time now")
        {
            writer.WriteLine(DateTime.Now.ToString("HH:mm"));
            writer.Flush();
        }
        else if (message.Contains("random number"))
        {
            string[] messageA = message.Split(" ");
            int x = Int32.Parse(messageA[2]);
            int y = Int32.Parse(messageA[3]);
            Console.WriteLine(x);
            Console.WriteLine(y);

            Random random = new Random();
            int n = random.Next(x, y);
            string sn = n.ToString();
            writer.WriteLine(sn);
            writer.Flush();

        }
        else if (message.Contains("add"))
        {
            string[] messageB = message.Split(" ");
            int x2 = Int32.Parse(messageB[1]);
            int y2 = Int32.Parse(messageB[2]);
            writer.WriteLine(x2 + y2);
            writer.Flush();
        }
        else if (message == "message count")
        {

            writer.WriteLine(count);
            writer.Flush();
        }
    }
}


//listener.Stop();
