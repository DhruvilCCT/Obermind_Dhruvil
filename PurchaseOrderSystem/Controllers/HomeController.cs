using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PurchaseOrderSystem.Data;
using PurchaseOrderSystem.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static PurchaseOrderSystem.Data.Context;

namespace PurchaseOrderSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context _context;

        public HomeController(ILogger<HomeController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OrderList(int length, int start)
        {
            var tid = this.HttpContext.Session.GetString("uname");
            var pid = this.HttpContext.Session.GetString("pwd");
            if (tid != null &&  pid != null)
            {
                try
                {
                    string searchVal = "";

                    if (HttpContext.Request.ContentType != null)
                    {
                        searchVal = HttpContext.Request.Form["search[value]"];
                    }
                    // getting all Customer data
                    List<OrderMaster> cRMOrders = PG_GetOrders(start, length, searchVal);
                    var customerData = (from c in _context.OrderMaster
                                        select c);

                    var TotalRecord = !string.IsNullOrEmpty(searchVal) ? cRMOrders.Count() : customerData.Count();
                    var response = new { data = cRMOrders, recordsFiltered = TotalRecord, recordsTotal = TotalRecord };

                    return Json(response);

                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return Redirect("~/Login/Login");
            }

        }

        public IActionResult CreateOrder()
        {
            return Redirect("~/Home/OrderList");
        }

        internal List<OrderMaster> PG_GetOrders(int skip, int take, string strSearchValue)
        {
            //connect to the DB and read order values
           
                if (!string.IsNullOrEmpty(strSearchValue))
                {
                    strSearchValue = strSearchValue.ToUpper().Trim();
                }

                IEnumerable<OrderMaster> second_data = new List<OrderMaster>();
                if (strSearchValue.Trim() == "" && skip == 0 && take == 0)
                {
                    second_data = (from c in _context.OrderMaster
                                   orderby c.ID ascending
                                   select c).ToList().Skip(0).Take(10);
                }
                else
                {
                    second_data = (from c in _context.OrderMaster
                                   orderby c.ID ascending
                                   select c).ToList().Skip(skip).Take(take);
                }


                var crmDBOrders = second_data.ToList();

                return crmDBOrders;
            
        }

        public List<childdetailsList> getlistitem(int Id)
        {
            //connect to the DB and read order values

           var second_data = (from c in _context.OrderDetail
                           join d in _context.ProductMaster on c.ProductID equals d.ProductID
                           where c.PurchaseOrderID == Id
                              select new { d.ProductName,d.ProductPrice });
            List<childdetailsList> ChilddetailsList = new List<childdetailsList>();
            foreach (var item in second_data)
            {
                childdetailsList childdetailsList = new childdetailsList();
                childdetailsList.ProductName = item.ProductName;
                childdetailsList.ProductPrice = item.ProductPrice;
                ChilddetailsList.Add(childdetailsList);
            }

            var crmDBOrders = ChilddetailsList;

            return crmDBOrders;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
