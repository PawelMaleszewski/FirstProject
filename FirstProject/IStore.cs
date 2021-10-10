namespace FirstProject
{
    public enum Category
    {
        Category1,
        Category2,
        Category3
    }
    public class Product
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Store : IStore
    {
        public Product Add(string name, decimal price, Category category)
        {
            return new Product {Id = 2, Name = name, Price = price, Category = category};
        }

        public bool Remove(int id)
        {
            return true;
        }

        public bool Update(int id, string name, decimal price, Category category)
        {
            throw new System.NotImplementedException();
        }
    }
    public interface IStore
    {
        Product Add(string name, decimal price, Category category);
        bool Remove(int id);
        bool Update(int id, string name, decimal price, Category category);
        Product[] GetProducts() => new []{new Product {Id = 1, Name = "piwo", Price = 21.37m, Category = Category.Category1}};
    }
}