
// HTTP Listener 사용
HttpListener listener = new HttpListener();
listener.Prefixes.Add("http://127.0.0.1:8080/");
listener.Start();

while (listener != null)
{
    HttpListenerContext context = listener.GetContext();
    string rawUrl = context.Request.RawUrl;
    string httpMethod = context.Request.HttpMethod;
    string requestBody = new StreamReader(context.Request.InputStream).ReadToEnd();
    JObject jsonBody = new JObject();
    if (requestBody != null && !requestBody.Equals("") && !requestBody.Equals("null"))
    {
        jsonBody = JObject.Parse(requestBody);
    }
    
    //...Response 만들기
    JObject responseBody = new JObject();
    responseBody["jsonKey"] = "jsonValue";

    string responseString = responseBody.ToString();
    byte[] byteArray = Encoding.UTF8.GetBytes(responseString);
    context.Response.OutputStream.Write(byteArray, 0, byteArray.Length);
    context.Response.StatusCode = 200;
    context.Response.Close();
}

// HTTP Client
HttpClient client = new HttpClient();
var res = client.GetAsync("http://127.0.0.1:8080/helloworld").Result;
Console.WriteLine("Response: " + res.StatusCode + " - " + res.Content.ReadAsStringAsync().Result);


// JSON Sample
JObject json = new JObject();
json["name"] = "John Doe";
json["salary"] = 300100;
string jsonStr = json.ToString();
Console.WriteLine("Json : " + jsonStr);
JObject json2 = JObject.Parse(jsonStr);
Console.WriteLine($"Name : {json2["name"]}, Salary : {json2["salary"]}");
