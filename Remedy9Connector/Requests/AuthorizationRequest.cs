using System;
using System.Net.Http;
using System.Text;

namespace Remedy9Connector.Requests
{
    public class AuthorizationRequest : HttpRequestMessage
    {
        private const string _loginApi = "/api/jwt/login";

        public AuthorizationRequest (string baseAddress, string username, string password)
        {
            var loginAndPass = string.Format("username={0}&password={1}", username, password);
            var loginRequestContent = new StringContent(loginAndPass, Encoding.UTF8, "application/x-www-form-urlencoded");

            this.RequestUri = new Uri(baseAddress.TrimEnd('/') + _loginApi);
            this.Content = loginRequestContent;
            this.Method = HttpMethod.Post;
        }
    }
}