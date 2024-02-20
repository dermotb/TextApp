using SMSGateWay.Models;
using System.Net.Http.Headers;

namespace TextApp
{
    public class Program
    {
        static void Main()
        {
            Task result = SendMessageAsync();
            result.Wait();
        }

        static async Task SendMessageAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5292/");

                client.DefaultRequestHeaders.
                    Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                TextMessage payload = new TextMessage() { FromNumber = "088-12345667", ToNumber = "5558889", Content = "Message from the client" };

                HttpResponseMessage response = await client.PostAsJsonAsync("api/SMS", payload);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("All good, message sent");
                }
                else
                {
                    Console.WriteLine(response.StatusCode + " | " + response.ReasonPhrase);
                }
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}