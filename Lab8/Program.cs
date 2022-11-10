using System.Net.Sockets;
using System.Net;
using System.Text;
using Lab8.Services;
using Lab8.Controllers;
using Lab8.Models;

namespace Lab8
{
    class Program
    {
        private const string ConnectionString = "Server=localhost;Database=PspLab8_My;Trusted_Connection=True;";
        private const string Address = "127.0.0.1";
        private const int Port = 8080;

        private static CafedraController _controller;
        private static string _lastResult;

        public static void Main(string[] args)
        {
            CafedraDataContext.ConnectionString = ConnectionString;

            var dataContext = new CafedraDataContext();
            var service = new CafedraService(dataContext);
            _controller = new CafedraController(service);

            var listener = new TcpListener(IPAddress.Parse(Address), Port);

            listener.Start();
            Console.WriteLine("Server started");

            while (true)
            {
                var client = listener.AcceptTcpClient();
                HandleRequest(client);
            }
        }

        private static void HandleRequest(TcpClient client)
        {
            var clientStream = client.GetStream();

            clientStream.ReadTimeout = 200;
            clientStream.WriteTimeout = 200;

            var request = clientStream.ReadMessage();

            if (request.Equals(string.Empty))
            {
                byte[] responseBytes = Encoding.UTF8.GetBytes(_lastResult);
                clientStream.Write(responseBytes, 0, _lastResult.Length);
                clientStream.Flush();
                clientStream.Close();
                client.Close();

                return;
            }

            Console.WriteLine(request);

            Response responseModel = null;

            if (request.StartsWith("GET"))
            {
                responseModel = _controller.GetAll();
            }
            
            else if (request.StartsWith("OPTIONS")) 
            {
                Cafedra model = request.ParseCafedra();
                responseModel = _controller.Create(model);
            }
            else if (request.StartsWith("PATCH"))
            {
                int model = request.ParseInt();
                responseModel = _controller.GetById(model);
            }
            else if (request.StartsWith("POST"))
            {
                Cafedra model = request.ParseCafedra();
                responseModel = _controller.Create(model);
            }
            else if (request.StartsWith("PUT"))
            {
                Cafedra model = request.ParseCafedra();
                responseModel = _controller.Update(model);
            }
            else if (request.StartsWith("DELETE"))
            {
                int model = request.ParseInt();
                responseModel = _controller.Delete(model);
            }

            try
            {
                var response = responseModel.GetResponse();
                _lastResult = response;

                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                clientStream.Write(responseBytes, 0, response.Length);
                clientStream.Flush();
            }
            catch { }


            clientStream.Close();
            client.Close();
        }
    }
}