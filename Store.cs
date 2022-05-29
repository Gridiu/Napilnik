using System;
using System.Collections.Generic;
using System.Linq;

namespace Store
{
    internal class Store
    {
        private static void Main(string[] args)
        {
            Product iPhone12 = new Product("IPhone 12");
            Product iPhone11 = new Product("IPhone 11");

            Warehouse warehouse = new Warehouse();

            Shop shop = new Shop(warehouse);

            warehouse.Deliver(iPhone12, 10);
            warehouse.Deliver(iPhone11, 1);

            //Вывод всех товаров на складе с их остатком
            GoodsView goodsView = new GoodsView();
            goodsView.Display(warehouse.Products);

            Cart cart = shop.Cart();
            cart.Add(iPhone12, 4);

            cart.Add(iPhone11, 3); //при такой ситуации возникает ошибка так, как нет нужного количества товара на складе

            //Вывод всех товаров в корзине
            goodsView.Display(cart.Products);

            Console.WriteLine(cart.Order().Paylink);

            cart.Add(iPhone12, 9); //Ошибка, после заказа со склада убираются заказанные товары
        }
    }

    public class Product
    {
        public Product(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    public class ProductStack
    {
        public ProductStack(Product product, int count)
        {
            Product = product;
            Count = count;
        }

        public Product Product { get; private set; }

        public int Count { get; private set; }

        public void Merge(ProductStack newStack)
        {
            if (newStack.Product != Product)
                throw new NotImplementedException();

            Count += newStack.Count;
        }

        public void Spend(int count)
        {
            if (count > Count)
                throw new ArgumentOutOfRangeException(nameof(count));

            Count -= count;
        }
    }

    public class Warehouse
    {
        private List<ProductStack> _products = new List<ProductStack>();

        public IReadOnlyList<ProductStack> Products => _products;

        public void Deliver(Product product, int count)
        {
            var newStack = new ProductStack(product, count);

            int stackIndex = _products.FindIndex(stack => stack.Product == product);

            if (stackIndex == -1)
                _products.Add(newStack);
            else
                _products[stackIndex].Merge(newStack);
        }

        public void Ship(Product product, int count)
        {
            ProductStack productStack = _products.First(stack => stack.Product == product);

            if (productStack == null)
                throw new NotImplementedException();

            if (count < 1)
                throw new ArgumentOutOfRangeException(nameof(count));

            productStack.Spend(count);
        }
    }

    public class Shop
    {
        private Warehouse _warehouse;

        public Shop(Warehouse warehouse)
        {
            _warehouse = warehouse;
        }

        public Cart Cart() => new Cart(_warehouse);
    }

    public class Cart
    {
        private List<ProductStack> _products = new List<ProductStack>();
        private Warehouse _warehouse;

        public Cart(Warehouse warehouse)
        {
            _warehouse = warehouse;
        }

        public IReadOnlyList<ProductStack> Products => _products;

        public void Add(Product product, int count)
        {
            ProductStack productStack = _warehouse.Products.First(stack => stack.Product == product);

            if (productStack == null)
            {
                throw new NotImplementedException();
            }
            else if (productStack.Count < count)
            {
                throw new ArgumentOutOfRangeException();
            }

            var newStack = new ProductStack(product, count);

            int stackIndex = _products.FindIndex(stack => stack.Product == product);

            if (stackIndex == -1)
                _products.Add(newStack);
            else
                _products[stackIndex].Merge(newStack);

            _warehouse.Ship(product, count);
        }

        public ProductsOrder Order()
        {
            if (_products.Count < 0)
                throw new ArgumentOutOfRangeException(nameof(_products.Count));

            return new ProductsOrder();
        }
    }

    public class ProductsOrder
    {
        private readonly string _paylink = "http";

        public string Paylink => _paylink;
    }

    public class GoodsView
    {
        public void Display(IReadOnlyList<ProductStack> products)
        {
            Console.WriteLine("Goods");

            foreach (ProductStack stack in products)
            {
                Console.WriteLine($"{stack.Product} {stack.Count}");
            }
        }
    }
}