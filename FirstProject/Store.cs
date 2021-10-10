using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FirstProject
{
    public class Store : IStore
    {

        const string path = @"products.json";
        public Store()
        {
         if(!File.Exists(path))
            {
                File.Create(path);
            }

        }
        public Product Add(string name, decimal price, Category category)
        {
            var products = GetProducts();
            int latestId = 0;
            // int lattestId = products.Length > 0 ? products.Select(product => product.Id).Max() : 0;
            if (products.Length > 0)
            {
                latestId = products.Select(product => product.Id).Max(); //praca domowa stworzyc funkcje ktora zrobi dokladnie to samo ale bez system.linq
            }
            var id = latestId + 1;
            Product product = new Product
            {
                Name = name,
                Price = price,
                Category = category,
                Id = id
            };
            File.WriteAllText(path, string.Empty);
            var newCollection = new List<Product>(products);
            newCollection.Add(product);

            var serialized = JsonSerializer.Serialize(newCollection);
            File.WriteAllText(path, serialized);
            return product;
        }
        public Product[] GetProducts()
        {
            var serialized = File.ReadAllText(path);
            if (string.IsNullOrEmpty(serialized))
                return Array.Empty<Product>();
            var result = JsonSerializer.Deserialize<Product[]>(serialized);
            return result;

        }

        public bool Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(int id, string name, decimal price, Category category)
        {
            throw new System.NotImplementedException();
        }
    }
}
