﻿using System.Net;

namespace ECommerce.Core.Common.Response
{
    public class ResponseApp<T>
    {
        public ResponseApp()
        {

        }
        public ResponseApp(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public ResponseApp(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public ResponseApp(string message, bool succeeded)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public HttpStatusCode StatusCode { get; set; }
        public object Meta { get; set; }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
