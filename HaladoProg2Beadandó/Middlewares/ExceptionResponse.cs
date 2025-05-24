using System.Net;

namespace HaladoProg2Beadandó.Middlewares
{
    internal class ExceptionResponse
    {
        private HttpStatusCode badRequest;
        private string v;

        public ExceptionResponse(HttpStatusCode badRequest, string v)
        {
            this.badRequest = badRequest;
            this.v = v;
        }

        public int StatusCode { get; internal set; }
    }
}