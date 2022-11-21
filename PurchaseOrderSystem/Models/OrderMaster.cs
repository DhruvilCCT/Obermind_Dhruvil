using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Models
{
    public class OrderMaster
    {
        [Key]
        public int ID { get; set; }
        public string OrderNumber { get; set; }
        public int Status { get; set; }
        public double? Amount { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }

    }

    public class OrderList
    {
        public int ProductID { get; set; }
        public double ProductPrice { get; set; }
    }
    public class childdetailsList
    {
        public string ProductName{ get; set; }
        public double ProductPrice { get; set; }
    }
    public class Editorderlist
    {
        public int OID { get; set; }
        public int ID { get; set; }
        public string OrderNumber { get; set; }
        public int Status { get; set; }
        public string  ProductName { get; set; }
        public int ProductID { get; set; }
        public double ProductPrice { get; set; }
        public double? Amount { get; set; }
    }
    public class PG_ModifyCRMOrder
    {
        public OrderMaster OrderMaster { get; set; }
        public OrderDetail _OrderDetail { get; set; }
        public List<ProductMaster> ProductMaster { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }

        public List<OrderList> _orderlist { get; set; }
        public childdetailsList Childdetails { get; set; }
        public List<childdetailsList> ChilddetailsList { get; set; }
        public Editorderlist Editorder { get; set; }
        public List<Editorderlist> Editorderlist { get; set; }
        public PG_ModifyCRMOrder()
        {
            OrderMaster = new OrderMaster();
            _OrderDetail = new OrderDetail();
            ProductMaster = new List<ProductMaster>();
            OrderDetail = new List<OrderDetail>();
            _orderlist = new List<OrderList>();
        }
    }
    //public class OrderDetails
    //{
    //    public int ID { get; set; }
    //    public string OrderNumber { get; set; }
    //    public int Status { get; set; }
    //    public int ProductID { get; set; }
    //    public double ProductPrice{ get; set; }
    //    public string ProductName{ get; set; }
    //}
}
