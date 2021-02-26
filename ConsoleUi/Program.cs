using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUi {
    class Program {
        static void Main(string[] args) {
            ProductManager productManager = new ProductManager(new EfProductDal());
            foreach (var product in productManager.GetAllByUnitPriceRange(10, 20)) {
                Console.WriteLine("{0} - ${1:F2}", product.ProductName, product.UnitPrice);
            }
        }
    }
}
