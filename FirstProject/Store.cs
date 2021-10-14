using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace FirstProject
{
    public class Store : IStore
    {

        const string path = @"products.json";
        public Store()
        {
            if (!File.Exists(path))
            {
                File.Create(path);
            }

        }
        private int GetLatestId(Product[] products)
        {
            int id = 0;
            for (int i = 0; i < products.Length; i++)
            {

                if (products[i].Id > id)
                {
                    id = products[i].Id;

                }
            }
            return id;
        }
        public Product Add(string name, decimal price, Category category)
        {
            var products = GetProducts();

            // int lattestId = products.Length > 0 ? products.Select(product => product.Id).Max() : 0;
            //if (products.Length > 0)
            //{
            //    latestId = products.Select(product => product.Id).Max(); //praca domowa stworzyc funkcje ktora zrobi dokladnie to samo ale bez system.linq
            //}
            var id = GetLatestId(products) + 1;


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
            int indexx = -1;
            var products = GetProducts();
            var newCollection = new List<Product>(products);

            for (int i = 0; i < newCollection.Count; i++)
            {
                if (newCollection[i].Id == id)
                {
                    indexx = i;
                    break;
                }
            }
            if (indexx != -1)
            {
                newCollection.RemoveAt(indexx);
            }

            var serialized = JsonSerializer.Serialize(newCollection);
            File.WriteAllText(path, serialized);

            return true;



        }


        public bool Update(int id, string name, decimal price, Category category)
        {
            int index = -1;
            var products = GetProducts();
            var newCollection = new List<Product>(products);

            for (int i = 0; i < newCollection.Count; i++)
            {
                if (newCollection[i].Id == id)
                {
                    index = i;
                    break;
                }
            }

            newCollection[index].Name = name;

            newCollection[index].Price = price;

            newCollection[index].Category = category;

            var serialized = JsonSerializer.Serialize(newCollection);
            File.WriteAllText(path, serialized);

            return true;
        }

        //Przeczytac o typie referencyjnym i przekazywaniu go w tablicy albo w metodzie. jaka jest roznica miedzy typem referencyjnym a int albo stringiem.
        // **** EXTENSION METHODS***
        //czy obiekt klasy store jest typem referencyjnym? Pouczyc sie o strukturze. Roznica miedzy struktura a klasa. podaj przyklady struktury i przyklady klas zaimplemenotwanych juz  w bibliotekach microsoftu.
    }
}
