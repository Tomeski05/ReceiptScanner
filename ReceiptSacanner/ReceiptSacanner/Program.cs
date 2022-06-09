using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

            List<Product> receipt = JsonConvert.DeserializeObject<List<Product>>(response);

            receipt.Sort((x, y) => string.Compare(x.Name, y.Name));

            int count = receipt.Count(x => x.Domestic);
            int countImported = receipt.Count(x => x.Domestic == false);

            double sumDomestic = receipt.Where(c => c.Domestic).Sum(c => c.Price);
            double sumImported = receipt.Where(x => x.Domestic == false).Sum(x => x.Price);

            Console.WriteLine("Domestic: ");
            foreach (var item in receipt)
            {
                if (item.Domestic == true)
                {
                    if (item.Description.Length > 10)
                    {
                        item.Description = item.Description.Substring(0, 10);
                    }
                    if (item.Weight == null)
                    {
                        Console.WriteLine(item.Name + "\nPrice: $ " + item.Price + "\n" + item.Description + "\nWeight: N/A");
                    }
                    else if (item.Weight != null)
                    {
                        Console.WriteLine(item.Name + "\nPrice: $ " + item.Price + "\n" + item.Description + "\nWeight: " + item.Weight);
                    }
                }
            }
            Console.WriteLine("---------------------------");


            Console.WriteLine("Imported: ");
            foreach (var item in receipt)
            {
                if (item.Domestic != true)
                {
                    if (item.Description.Length > 10)
                    {
                        item.Description = item.Description.Substring(0, 10);
                    }
                    if (item.Weight == null)
                    {
                        Console.WriteLine(item.Name + "\nPrice: $ " + item.Price + "\n" + item.Description + "\nWeight: N/A");
                    }
                    else if (item.Weight != null)
                    {
                        Console.WriteLine(item.Name + "\nPrice: $ " + item.Price + "\n" + item.Description + "\nWeight: " + item.Weight);
                    }
                }
            }

            Console.WriteLine("---------------------------");
            Console.WriteLine("Domestic count: " + count);
            Console.WriteLine("Imported count: " + countImported);
            Console.WriteLine("---------------------------");
            Console.WriteLine("Imported cost: {0:C}", sumDomestic);
            Console.WriteLine("Imported cost: {0:C}", sumImported);

        }

        public class Product
        {
            public string Name { get; set; }
            public bool Domestic { get; set; }
            public double Price { get; set; }
            public int? Weight { get; set; }
            public string Description { get; set; }
        }
    }
}
