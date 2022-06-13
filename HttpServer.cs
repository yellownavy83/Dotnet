using System.Collections.Specialized;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace HTTP_SERVER
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:8080/");
            listener.Start();
            Console.WriteLine("Server Started @ http://127.0.0.1:8080/");

            while (listener != null)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                JObject responseJson = new JObject();       // response의 body를 만들기 위한 json 객체

                // request parsing
                string httpMethod = request.HttpMethod;
                string path = request.Url.LocalPath;
                string contentType = request.ContentType;
                Encoding encoding = request.ContentEncoding;
                string[] acceptTypes = request.AcceptTypes;

                // test를 위해 request parsing 결과를 response body에 저장
                responseJson.Add("uri", request.Url);
                responseJson.Add("path", path);
                responseJson.Add("method", httpMethod);
                responseJson.Add("contentType", contentType);
                responseJson.Add("encoding", encoding.EncodingName);
                JArray acceptTypesArray = new JArray();
                if (acceptTypes != null)
                {
                    foreach (string acceptType in acceptTypes)
                    {
                        acceptTypesArray.Add(acceptType);
                    }
                    responseJson.Add("acceptTypes", acceptTypesArray);
                }

                Console.WriteLine($"{httpMethod} : {request.Url}");
                // REST API PATH 는 request.Url.LocalPath 참조
                // GET
                if (httpMethod.Equals("GET"))
                {
                    string query = request.Url.Query;
                    responseJson.Add("query", query);

                    if (query != null && !query.Equals(""))
                    {
                        // query의 key, value 가져오기
                        // string[] key = request.QueryString.AllKeys;
                        NameValueCollection queryString = request.QueryString;
                        JArray queryArray = new JArray();
                        foreach (string key in queryString)
                        {
                            queryArray.Add(new JObject { {key, queryString[key]} });
                        }
                        responseJson.Add("queryValue", queryArray);
                    }
                }
                else if (httpMethod.Equals("POST"))
                {
                    if (request.HasEntityBody)
                    {
                        responseJson["body"] = true;
                        Stream bodyStream = request.InputStream;
                        StreamReader bodyReader = new StreamReader(bodyStream, encoding);
                        string bodyString = bodyReader.ReadToEnd();

                        JObject bodyJson = JObject.Parse(bodyString);
                        responseJson["body"] = bodyJson;

                        bodyStream.Close();
                        bodyReader.Close();
                    }
                    else
                    {
                        responseJson["body"] = false;
                    }

                }

                // json bodyb를 byte array로 변환 후 outputstream에 전송
                byte[] byteArray = Encoding.UTF8.GetBytes(responseJson.ToString());
                response.OutputStream.Write(byteArray, 0, byteArray.Length);
                response.StatusCode = (int)HttpStatusCode.OK;
                response.ContentType = contentType;
                response.Close();
            }
        }
    }
}
