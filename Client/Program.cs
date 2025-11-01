using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== TCP CLIENT STARTED ===");
            Console.Write("Enter server IP (default 127.0.0.1): ");
            string ip = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(ip)) ip = "127.0.0.1";

            Console.Write("Enter server port (default 5000): ");
            string portInput = Console.ReadLine()!;
            int port = string.IsNullOrWhiteSpace(portInput) ? 5000 : int.Parse(portInput);

            Console.Write("Enter message (e.g. SetA-Two): ");
            string msg = Console.ReadLine()!;
            string encrypted = EncryptionHelper.Encrypt(msg);

            try
            {
                using var client = new TcpClient(ip, port);
                var stream = client.GetStream();

                byte[] msgBytes = Encoding.UTF8.GetBytes(encrypted);
                await stream.WriteAsync(msgBytes);

                byte[] buffer = new byte[1024];
                while (true)
                {
                    int bytesRead = await stream.ReadAsync(buffer);
                    if (bytesRead == 0) break;

                    string encryptedresponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    string decryptedMsg = EncryptionHelper.Decrypt(encryptedresponse);
                    Console.WriteLine($"Received: {decryptedMsg}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Client finished.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
