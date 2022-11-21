using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PurchaseOrderSystem.Data;
using PurchaseOrderSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PurchaseOrderSystem.Controllers
{
    public class OrderListController : Controller
    {
        private readonly ILogger<OrderListController> _logger;
        private readonly Context _context;

        public OrderListController(ILogger<OrderListController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult CreateOrder()
        {
            PG_ModifyCRMOrder _createorder = new PG_ModifyCRMOrder();
            return View(_createorder);
        }

        [HttpPost]
        public IActionResult CreateOrder(string orderNumber, int status, int id, List<OrderList> orderlist)
        {
            double ordertotal = 0;
            try
            {
                PG_ModifyCRMOrder oid = new PG_ModifyCRMOrder();
                var data=_context.OrderMaster.Where(x => x.ID == id).FirstOrDefault();
                var existdata = _context.OrderDetail.Where(x => x.PurchaseOrderID == id).ToList();
                if (data!=null) {

                    foreach (var item in orderlist)
                    {
                        var _checkdata = _context.OrderDetail.Where(x => x.PurchaseOrderID == id && x.ProductID == item.ProductID).FirstOrDefault();
                        if (_checkdata != null)
                        {
                            //oid._OrderDetail.ID = item.ID;
                            //oid._OrderDetail.PurchaseOrderID = id;
                            //oid._OrderDetail.ProductID = item.ProductID;
                            _checkdata.ProductPrice = item.ProductPrice;
                            //oid._OrderDetail.UpdatedBy = "Admin";
                            _checkdata.UpdatedBy = this.HttpContext.Session.GetString("uname");
                            _checkdata.UpdatedDate = DateTime.Now;
                            _context.OrderDetail.Update(_checkdata);
                            _context.SaveChanges();
                            ordertotal += item.ProductPrice;
                        }
                        else
                        {
                            oid._OrderDetail.ID = 0;
                            oid._OrderDetail.PurchaseOrderID = id;
                            oid._OrderDetail.ProductID = item.ProductID;
                            oid._OrderDetail.ProductPrice = item.ProductPrice;
                            //oid._OrderDetail.CreatedBy = "Admin";
                            oid._OrderDetail.CreatedBy = this.HttpContext.Session.GetString("uname");
                            oid._OrderDetail.CreatedDate = DateTime.Now;
                            _context.OrderDetail.Add(oid._OrderDetail);
                            _context.SaveChanges();
                            ordertotal += item.ProductPrice;
                        }
                    }
                    data.Status = status;
                    data.Amount = ordertotal;
                    data.UpdatedOn = DateTime.Now;
                    data.UpdatedBy = this.HttpContext.Session.GetString("uname");
                    _context.OrderMaster.Update(data);
                    _context.SaveChanges();
                }
                else
                {
                    oid.OrderMaster.OrderNumber = orderNumber;
                    oid.OrderMaster.Status = status;
                    oid.OrderMaster.CreatedOn = DateTime.Now;
                    //oid.OrderMaster.CreatedBy = "Admin";
                    oid.OrderMaster.CreatedBy = this.HttpContext.Session.GetString("uname");
                    _context.OrderMaster.Add(oid.OrderMaster);
                    _context.SaveChanges();
                    OrderMaster _objid = _context.OrderMaster.Where(x => x.OrderNumber == orderNumber).FirstOrDefault();
                   
                    foreach (var item in orderlist)
                    {
                        oid._OrderDetail.ID = 0;
                        oid._OrderDetail.PurchaseOrderID = _objid.ID;
                        oid._OrderDetail.ProductID = item.ProductID;
                        oid._OrderDetail.ProductPrice = item.ProductPrice;
                        //oid._OrderDetail.CreatedBy = "Admin";
                        oid._OrderDetail.CreatedBy = this.HttpContext.Session.GetString("uname");
                        oid._OrderDetail.CreatedDate = DateTime.Now;
                        _context.OrderDetail.Add(oid._OrderDetail);
                        _context.SaveChanges();
                        ordertotal += item.ProductPrice;
                        oid.OrderMaster.Amount = ordertotal;
                        _context.OrderMaster.Update(oid.OrderMaster);
                        _context.SaveChanges();
                    }
                }
                return Redirect("~/Home/Index");

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<SelectListItem>> GetStatusList()
        {
            try
            {
                var list = new List<SelectListItem>
                  {
                      new SelectListItem { Text = "DRAFT", Value = "1" },
                      new SelectListItem { Text = "SUBMITTED",    Value = "2" }
                   };
                return list;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }

        [HttpGet]
        public ActionResult EditOrder(int? ID)
        {
            try
            {
                var _abc = new PG_ModifyCRMOrder();
                var checkstatus = _context.OrderMaster.Where(x => x.ID == ID).FirstOrDefault();
                //if (checkstatus.Status == 1)
                //{
                    var Order = (from o in _context.OrderMaster
                                 join a in _context.OrderDetail on o.ID equals a.PurchaseOrderID
                                 join p in _context.ProductMaster on a.ProductID equals p.ProductID
                                 let OID= a.ID
                                 where o.ID == ID
                                 select new { o.ID, o.OrderNumber,o.Amount, o.Status, a.ProductID, a.ProductPrice, p.ProductName,
                                     OID
                                 }).ToList();
                    //var Order = (from c in _context.OrderMaster
                    //                    where c.ID == ID
                    //                    select c).FirstOrDefault();
                    //var Orderdetail = (from d in _context.OrderDetail
                    //             where d.PurchaseOrderID == ID
                    //             select d).FirstOrDefault();
                    //var productmaster = (from e in _context.ProductMaster
                    //                   where e. == ID
                    //                   select d).FirstOrDefault();
                    List<Editorderlist> editorderlists = new List<Editorderlist>();
                    foreach (var item in Order)
                    {
                        Editorderlist editor = new Editorderlist();
                        editor.OID = item.OID;
                        editor.ID = item.ID;
                        editor.OrderNumber = item.OrderNumber;
                        editor.Status = item.Status;
                        editor.ProductID = item.ProductID;
                        editor.ProductPrice = item.ProductPrice;
                        editor.ProductName = item.ProductName;
                        editor.Amount = item.Amount;
                        editorderlists.Add(editor);
                    }

                    _abc.Editorderlist = editorderlists;
                    return View(_abc);
                //}
                //else
                //{
                //    return Redirect("~/Home/Index");
                //}
            }
            
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult DeleteOrder(int? ID)
        {
            
                var customer = _context.OrderMaster.Find(ID);
                if (ID == null)
                    return Json(data: "Not Deleted");
                _context.OrderMaster.Remove(customer);
                _context.SaveChanges();

                return Json(data: "Deleted");
            
        }
        [HttpPost]
        public JsonResult Deleteproduct(int? ID)
        {

            var customer = _context.OrderDetail.Find(ID);
            if (ID == null)
                return Json(data: "Not Deleted");
            _context.OrderDetail.Remove(customer);
            _context.SaveChanges();

            return Json(data: "Deleted");

        }
        [HttpGet]
        public PartialViewResult ProductList()
        {
            var model = new PG_ModifyCRMOrder();
            var customerData = (from c in _context.ProductMaster
                                select c).ToList();

            model.ProductMaster = customerData;
            return PartialView(@"~/Views/Shared/_AddItemList.cshtml", model);

        }

       
        [HttpGet]
        public string AddItemTotal(int Id, int productid, double price)
        {
            string JSONresult = string.Empty;
            OrderDetail oid = new OrderDetail();
            List<OrderDetail> _oid = new List<OrderDetail>();
            //var data = _context.OrderMaster.Where(x => x.ID == id).FirstOrDefault();
            oid.ProductPrice = Convert.ToDouble(price);
            oid.PurchaseOrderID = Id;
            oid.ProductID = productid;
            oid.CreatedDate = DateTime.Now;
            //oid.CreatedBy = "Admin";
            oid.CreatedBy = this.HttpContext.Session.GetString("uname");
            _context.OrderDetail.Add(oid);
            _context.SaveChanges();
            JSONresult = JsonConvert.SerializeObject(oid);
            return JSONresult;

        }

        [HttpPost]
        public JsonResult DeleteexitItem(int oid, int? ID)
        {

            var _existitemid = _context.OrderDetail.Find(ID);
            var _existcount = _context.OrderDetail.Where(x => x.PurchaseOrderID == oid).ToList();
            if (_existcount.Count > 1)
            {
                if (ID == null)
                    return Json(data: "Not Deleted");
                _context.OrderDetail.Remove(_existitemid);
                _context.SaveChanges();
                return Json(data: "Deleted");

            }
            else
            {
                return Json(data: "At least one line item should be in PO.");
            }

        }
    }
}
