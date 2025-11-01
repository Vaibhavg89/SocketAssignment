using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        static readonly Dictionary<string, Dictionary<string, int>> sets = new()
        {
            {"SetA", new(){{"One",1},{"Two",2}}},
            {"SetB", new(){{"Three",3},{"Four",4}}},
            {"SetC", new(){{"Five",5},{"Six",6}}},
            {"SetD", new(){{"Seven",7},{"Eight",8}}},
            {"SetE", new(){{"Nine",9},{"Ten",10}}}
        };

        static async Task Main(string[] args)
        {
            Console.WriteLine("=== TCP SERVER STARTED ===");
            Console.Write("Enter port to listen on (default 5000): ");
            string portInput = Console.ReadLine()!;
            int port = string.IsNullOrWhiteSpace(portInput) ? 5000 : int.Parse(portInput);

            var listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            Console.WriteLine($"Server listening on port {port}...");

            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();
                Console.WriteLine($"New client connected: {client.Client.RemoteEndPoint}");
                _ = Task.Run(() => HandleClient(client));
            }
        }

        static async Task HandleClient(TcpClient client)
        {
            try
            {
                using var stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer);
                string encryptedMsg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                string received = EncryptionHelper.Decrypt(encryptedMsg);
                Console.WriteLine($"Received: {received}");

                string response = ProcessMessage(received);
                if (response == "EMPTY")
                {
                    string encrypted = EncryptionHelper.Encrypt("EMPTY");
                    byte[] emptyBytes = Encoding.UTF8.GetBytes(encrypted);
                    await stream.WriteAsync(emptyBytes);
                }
                else
                {
                    int n = int.Parse(response);
                    for (int i = 0; i < n; i++)
                    {
                        string time = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                        string encrypted = EncryptionHelper.Encrypt(time);

                        byte[] msgBytes = Encoding.UTF8.GetBytes(encrypted);
                        await stream.WriteAsync(msgBytes);
                        Console.WriteLine($"Sent: {time}");
                        await Task.Delay(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }

        static string ProcessMessage(string message)
        {
            var parts = message.Split('-');
            if (parts.Length != 2) return "EMPTY";

            string setKey = parts[0];
            string subKey = parts[1];

            if (sets.ContainsKey(setKey) && sets[setKey].ContainsKey(subKey))
            {
                return sets[setKey][subKey].ToString();
            }

            return "EMPTY";
        }
    }
}
