using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReceiptSacanner
{
    internal class Program
    {
        HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            Program program = new Program();
            await program.GetTodoItems();
        }
        private async Task GetTodoItems()
        {
            string response = await client.GetStringAsync(
               "https://interview-task-api.mca.dev/qr-scanner-codes/alpha-qr-gFpwhsQ8fkY1");

            List<Todo> todo = JsonConvert.DeserializeObject<List<Todo>>(response);
        }

        public class Todo
        {
            public string Name { get; set; }
            public bool Domestic { get; set; }
            public double Price { get; set; }
            public int? Weight { get; set; }
            public string Description { get; set; }
        }

    }
}
