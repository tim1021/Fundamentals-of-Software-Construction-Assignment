//写一个订单管理的控制台程序，能够实现添加订单、删除订单、修改订单、查询订单
//（按照订单号、商品名称、客户、订单金额等进行查询）功能。
//并对各个Public方法编写测试用例。// 单元测试

//提示：主要的类有
//Order（订单）、
//OrderDetails（订单明细），
//OrderService（订单服务），
//订单数据可以保存在OrderService中一个List中。
//在Program里面可以调用OrderService的方法完成各种订单操作。

//要求：
//（1）使用LINQ语言实现各种查询功能，查询结果按照订单总金额排序返回。
//（2）在订单删除、修改失败时，能够产生异常并显示给客户错误信息。
//（3）作业的订单和订单明细类需要重写Equals方法，确保添加的订单不重复，每个订单的订单明细不重复。
//（4）订单、订单明细、客户、货物等类添加ToString方法，用来显示订单信息。
//（5） OrderService提供排序方法对保存的订单进行排序。默认按照订单号排序，也可以使用Lambda表达式进行自定义排序。

using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Runtime.InteropServices;

class Order {
    public string OrderId { get; set; }
    public string CustomName { get; set; }
    public List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();

    public decimal TotalAmount => OrderDetails.Sum(od => od.TotalAmount);

    //重写Equals方法，确保添加的订单不重复，每个订单的订单明细不重复
    public override bool Equals(object obj)
    {
        if (obj is Order other)
        {
            return this.OrderId== other.OrderId;
        }
        return false;
    }
    //加ToString方法，用来显示订单信息。
    public override string ToString()
    {
        return $"{OrderId} - {CustomName} - total: {TotalAmount:C}";
    }
}
class OrderDetails {
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount => Quantity * UnitPrice;

    //重写Equals方法，确保添加的订单不重复，每个订单的订单明细不重复
    public override bool Equals(object obj) {
        if (obj is OrderDetails other)
        {
            return this.ProductName == other.ProductName && Quantity == other.Quantity;
        }
        return false;
    }
    //加ToString方法，用来显示订单信息。
    public override string ToString() {
        return $"{ProductName}x{Quantity}@{UnitPrice:C}={TotalAmount:C}";
    }
}
class OrderService {
    private List<Order> orders = new List<Order>();

    //增
    public void AddOrder(Order order) 
    {
        if (orders.Contains(order))
        {
            throw new Exception("order already exist");
        }
        orders.Add(order);
    }
    //删
    public void DeleteOrder(string orderId) {
        var order = orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order == null)
        {
            //订单不存在删除失败
            throw new Exception("order not exist");
        }
        orders.Remove(order);
    }
    //改
    public void UpdateOrder(string orderId,Order updateOrder) {
        var order = orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order == null)
        {
            //订单不存在删除失败
            throw new Exception("order not exist");
        }
        order.CustomName = updateOrder.CustomName;
        order.OrderDetails = updateOrder.OrderDetails;
    }
    //查
    public List<Order> QueryOrder(Func<Order,bool> filter = null) {
        var query = orders.AsQueryable();
        if (filter != null)
        {
            query = (IQueryable<Order>)query.Where(filter);
        }
        return query.OrderBy(o => o.TotalAmount).ToList();
    }
    //提供排序方法对保存的订单进行排序
    public void SortOrders(Order order) { }
}
class Program
{
    static void Main()
    {
        //测试 调用orderservice的增删改查函数
        OrderService os = new OrderService();
        //增
        try
        {
            var order1 = new Order
            {
                OrderId = "001",
                CustomName = "Alice",
                OrderDetails = new List<OrderDetails>
                {
                    new OrderDetails{ProductName = "laptop",Quantity = 1,UnitPrice=6998},
                    new OrderDetails{ProductName = "mouse",Quantity = 2,UnitPrice=98},
                }
            };
            os.AddOrder(order1);
            var order2 = new Order
            {
                OrderId = "002",
                CustomName = "Bob",
                OrderDetails = new List<OrderDetails>
                {
                    new OrderDetails{ProductName = "desk",Quantity = 1,UnitPrice=1598},
                    new OrderDetails{ProductName = "chair",Quantity = 2,UnitPrice=258},
                }
            };
            os.AddOrder(order2);
            var order3 = new Order
            {
                OrderId = "002",
                CustomName = "Bob",
                OrderDetails = new List<OrderDetails>
                {
                    new OrderDetails{ProductName = "desk",Quantity = 1,UnitPrice=1598},
                    new OrderDetails{ProductName = "chair",Quantity = 2,UnitPrice=258},
                }
            };
            os.AddOrder(order3);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"execution fail:{ex.Message}");
        }


        //查
        Console.WriteLine("orders:");
        foreach (var order in os.QueryOrder())
        {
            Console.WriteLine(order);
        }

        //删
        try
        {
            os.DeleteOrder("002");
            Console.WriteLine("orders after delete:");
            foreach (var order in os.QueryOrder())
            {
                Console.WriteLine(order);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"delete fail:{ex.Message}");
        }

        //改
        try
        {
            var updatedOrder = new Order
            {
                OrderId = "001",
                CustomName = "Alice Updated",
                OrderDetails = new List<OrderDetails>
                    {
                        new OrderDetails { ProductName = "Laptop", Quantity = 1, UnitPrice = 1200 },
                        new OrderDetails { ProductName = "Mouse", Quantity = 1, UnitPrice = 60 }
                    }
            };
            os.UpdateOrder("001", updatedOrder);
            Console.WriteLine(updatedOrder);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"delete fail:{ex.Message}");
        }
    }
}
