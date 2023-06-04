using System;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace application_calling_test
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var apiKey = "Alguma key";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var jsonContent = new
            {
                prompt = "Olá ChatGPT. O que você pode fazer?",
                model = "text-davinci-003",
                max_tokens = 1000
            };

            var responseContent = await client.PostAsync("https://api.openai.com/v1/completions",
                new StringContent(JsonConvert.SerializeObject(jsonContent), Encoding.UTF8,
                "application/json"));

            var resContext = await responseContent.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<dynamic>(resContext);
            string response = data.choices[0].text;

            Console.WriteLine(response);
        }
    }
}