using Lab8.Models;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab8
{
    public static class Extensions
    {
        public static string ReadMessage(this NetworkStream stream)
        {
            StringBuilder messageData = new StringBuilder();
            try
            {
                byte[] buffer = new byte[2048];
                int bytes = -1;

                do
                {
                    bytes = stream.Read(buffer, 0, buffer.Length);
                    Decoder decoder = Encoding.UTF8.GetDecoder();
                    char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                    decoder.GetChars(buffer, 0, bytes, chars, 0);
                    messageData.Append(chars);
                }
                while (bytes != 0);
            }
            catch { }

            return messageData.ToString();
        }

        public static Cafedra ParseCafedra(this string request)
        {
            Regex regex = new Regex("\r\n\r\n");
            string[] requestItems = regex.Split(request, 2);

            var body = requestItems.Last();

            return JsonConvert.DeserializeObject<Cafedra>(body.Trim());
        }

        public static int ParseInt(this string request)
        {
            Regex regex = new Regex("\r\n\r\n");
            string[] requestItems = regex.Split(request, 2);

            var body = requestItems.Last();

            var idItem = JsonConvert.DeserializeObject<IdItem>(body.Trim());
            return idItem.Id;
        }

        class IdItem
        {
            public int Id { get; set; }
        }
    }
}
