using System.Collections.Specialized;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using Newtonsoft.Json.Linq;

namespace HTTP_CLIENT
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
            
            string url = "http://127.0.0.1:8080/";
            string path = "api/test";
            string method = Console.ReadLine();

            if (method.Equals("GET"))
            {
                // query 만들기
                NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);
                query["id"] = "1";
                query["name"] = "jinseok";
                string queryString = query.ToString();

                TaskAwaiter<HttpResponseMessage> taskResponse = HttpGetAsync(url + path + "?" + queryString).GetAwaiter();
                HttpResponseMessage response = taskResponse.GetResult();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("body : " + responseBody);
            }
            else if (method.Equals("POST"))
            {
                // body 만들기
                JObject bodyJson = new JObject();
                bodyJson["id"] = 1;
                bodyJson["name"] = "jinseok";
                bodyJson["age"] = 40;

                HttpResponseMessage response = HttpPostAsync(url + path, bodyJson).GetAwaiter().GetResult();
            }

            Console.Write($"{Environment.NewLine}Press any key to exit...");
            Console.ReadLine();
        }

        static async Task<HttpResponseMessage> HttpGetAsync(string uri)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await client.GetAsync(uri);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string body = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("GET response body : " + body);
                }
                else
                {
                    Console.WriteLine($" -- response.ReasonPhrase ==> {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }

            return response;
        }

        static async Task<HttpResponseMessage> HttpPostAsync(string uri, JObject bodyJson)
        {
            HttpResponseMessage response = null;
            try
            {
                HttpContent httpContent = new StringContent(bodyJson.ToString(), Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                response = await client.PostAsync(uri, httpContent);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string body = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("POST response body : " + body);
                }
                else
                {
                    Console.WriteLine($" -- response.ReasonPhrase ==> {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            
            return response;
        }
    }
}
