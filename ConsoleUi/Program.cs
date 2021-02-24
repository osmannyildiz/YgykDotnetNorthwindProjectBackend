using Business.Concrete;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUi {
    class Program {
        static void Main(string[] args) {
            ProductManager productManager = new ProductManager(new InMemoryProductDal());
            foreach (var p in productManager.GetAll()) {
                Console.WriteLine(p.ProductName);
            }
        }
    }
}
