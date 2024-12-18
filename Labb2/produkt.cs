using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    public class Produkt
    {



        public List<Produkt> Productlist;
        public string produktName { get; set; }
        public double price { get; set; }
        public Produkt(string produktName, double price)
        {
            this.produktName = produktName;
            this.price = price;

        }
        public static void BuyProducts(ShoppingCart cart)
        {
            var productsCollection = MongoDbConnection.GetCollection<BsonDocument>("products");
            var productsdoc = productsCollection.Find(Builders<BsonDocument>.Filter.Empty).ToList();
            var products = productsdoc.Select(doc => new Produkt(
                 doc["name"].AsString,
                 doc["price"].ToDouble()
             )).ToArray();

            for (int i = 0; i < products.Length; i++)
            {
                Console.WriteLine($"{i + 1}) {products[i].produktName} - {products[i].price:C}");
            }
            Console.WriteLine("choose a product");
            ConsoleKeyInfo userchoice = Console.ReadKey(true);
            int productIndex;
            
            if (userchoice.KeyChar == '1')
            {
                productIndex = 0;
                Produkt selectedProduct = products[productIndex];
                cart.AddProduct(selectedProduct);
                Console.Clear();
                Console.WriteLine($"{selectedProduct.produktName} added to cart");
                Thread.Sleep(1000);
                Console.Clear();
            }
            else if (userchoice.KeyChar == '2')
            {
                productIndex = 1;
                Produkt selectedProduct = products[productIndex];
                cart.AddProduct(selectedProduct);
                Console.Clear();
                Console.WriteLine($"{selectedProduct.produktName} added to cart");
                Thread.Sleep(1000);
                Console.Clear();
            }
            else if (userchoice.KeyChar == '3')
            {
                productIndex = 2;
                Produkt selectedProduct = products[productIndex];
                cart.AddProduct(selectedProduct);
                Console.Clear();
                Console.WriteLine($"{selectedProduct.produktName} added to cart");
                Thread.Sleep(1000);
                Console.Clear();
            }

        }
        
    }    

    
}










