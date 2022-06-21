using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PredictionWebClient {
    class Program {
        static async Task Main(string[] args) {
            Console.WriteLine("press any key to cont....");
            Console.ReadLine();

            using (HttpClient client = new HttpClient()) {
                var response = await client.GetAsync("https://localhost:44317/api/user/gethistory");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode) {
                    string msg = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(msg);
                }
                else {
                    Console.WriteLine($"response error code: {response.StatusCode}");
                }
            }
        }
    }
}
