﻿using Newtonsoft.Json;
using System.Text;

namespace Lab8.Models
{
    public class Response
    {
        private object _model;

        public Response(object model)
        {
            _model = model;
        }

        public Response()
        {

        }

        public string GetResponse()
        {
            if (_model == null)
            {
                return SuccessHeaders(0);
            }

            var body = JsonConvert.SerializeObject(_model);
            return string.Concat(SuccessHeaders(body.Length), body);
        }

        private string SuccessHeaders(int contentLength)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("HTTP/1.1 200 OK").Append("\r\n");
            builder.Append("Date: ").Append(DateTime.Now).Append("\r\n");
            builder.Append("Content-Type: application/json; charset=UTF-8").Append("\r\n");
            builder.Append("Content-Length: ").Append(contentLength).Append("\r\n");
            builder.Append("Connection: close").Append("\r\n");
            builder.Append("\r\n");

            return builder.ToString();
        }
    }
}
