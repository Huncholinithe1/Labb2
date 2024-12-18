using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;


namespace Labb2
{
    public static class MongoDbConnection
    {
       private static readonly string connectionUri = "mongodb+srv://dilanraza:123@cluster0.pij1r.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
       private static readonly MongoClient Client;
       private static readonly IMongoDatabase Database;
        
        static MongoDbConnection()
        {
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            Client = new MongoClient(settings);
            Database = Client.GetDatabase("butikDb");
        }

        public static IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return Database.GetCollection<T>(collectionName);
        }

        public static void DisplayProducts()
        {
            
            var productsCollection = Database.GetCollection<BsonDocument>("products");

            var filter = Builders<BsonDocument>.Filter.Empty;
            var documents = productsCollection.Find(filter).ToList();

            foreach (var document in documents)
            {
                Console.WriteLine();
                Console.WriteLine($"\nProduct: {document["name"]}");
                Console.WriteLine($"Price: {document["price"].ToDouble():F2}");
                Console.WriteLine();


            }

        }
        public static void AddProductToDb()
        {
            var productsCollection = GetCollection<BsonDocument>("products");
            Console.Write("Type in the product name: ");
            string inputName = Console.ReadLine();
            Console.WriteLine();
            Console.Write("enter the price: ");
            string inputprice = Console.ReadLine();
            Console.WriteLine();
            var newproduct = new BsonDocument
            {
                {"name",inputName },
                {"price",inputprice},

                };
                productsCollection.InsertOneAsync(newproduct);
                Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"succesfully added {newproduct}");
            Console.ResetColor();
        }

        public static void DeleteProduct()
        {
            var productsCollection = GetCollection<BsonDocument>("products");
            var filter = Builders<BsonDocument>.Filter.Empty;
            var products = productsCollection.Find(filter).ToList();
            foreach (var doc in products)
            {
                Console.WriteLine();
                Console.WriteLine($"\nProduct: {doc["name"]}");
                Console.WriteLine();
                Thread.Sleep(250);
            }

            Console.Write("type the product you would like to delete: ");
            string input = Console.ReadLine();
            var deleteFilter = Builders<BsonDocument>.Filter.Eq("name", input);
            var result =  productsCollection.DeleteOne(deleteFilter);
            if (result.DeletedCount > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Successfully deleted product with name {input}.");
                Console.ResetColor();

               
            }
            else
            {
                Console.WriteLine($"No product found with the name: {input}.");
            }
        }
    }
}




