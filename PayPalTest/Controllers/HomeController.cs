using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PayPalTest.Models;
using System.Configuration;

namespace PayPalTest.Controllers
{

    //http://ravi-kumar-singh.blogspot.com.br/2014/07/how-to-integrate-paypal-in-aspnet-mvc-5.html

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PostToPayPal(string item, string amount)
        {
            Paypal paypal = new Paypal();
            paypal.cmd = "_xclick";
            paypal.business = ConfigurationManager.AppSettings["BusinessAccountKey"];

            bool useSandbox = Convert.ToBoolean(ConfigurationManager.AppSettings["UseSandBox"]);
            if (useSandbox)
            {
                ViewBag.ActionUrl = "https://www.sandbox.paypal.com/cgi-bin/webscr";
            }
            else
            {
                ViewBag.ActionUrl = "https://www.paypal.com/cgi-bin/webscr";
            }

            paypal.cancel_return = ConfigurationManager.AppSettings["CancelURL"];
            paypal.@return = ConfigurationManager.AppSettings["ReturnURL"]; // + "&PaymentId=1"; can append order Id here
            paypal.notify_url = ConfigurationManager.AppSettings["NotifyURL"]; // +"?PaymentId=1"; to maintain database logic

            paypal.currency_code = ConfigurationManager.AppSettings["CurrencyCode"];

            paypal.item_name = item;
            paypal.amount = amount;

            return View(paypal);
        }

        public ActionResult RedirectFromPayPal(FormCollection collection)
        {
            Payment payment = new Payment();

            payment.Address_City = Request.Form["Address_City"].ToString();
            payment.Address_Country = Request.Form["Address_Country"].ToString();
            payment.Address_Country_Code = Request.Form["Address_Country_Code"].ToString();
            
            if (!string.IsNullOrEmpty(Request.Form["Contact_Phone"]))
            {
                payment.Contact_Phone = Request.Form["Contact_Phone"].ToString();
            }
            else
            {
                payment.Contact_Phone = string.Empty;
            }

            if (!string.IsNullOrEmpty(Request.Form["First_Name"]))
            {
                payment.First_Name = Request.Form["First_Name"].ToString();    
            }
            else
            {
                payment.First_Name = string.Empty;
            }

            if (!string.IsNullOrEmpty(Request.Form["Last_Name"]))
            {
                payment.Last_Name = Request.Form["Last_Name"].ToString();
            }
            else
            {
                payment.Last_Name = string.Empty;
            }

            if (!string.IsNullOrEmpty(Request.Form["Payer_Business_Name"]))
            {
                payment.Payer_Business_Name = Request.Form["Payer_Business_Name"].ToString();
            }
            else
            {
                payment.Payer_Business_Name = string.Empty;
            }

            payment.Payer_Email = Request.Form["Payer_Email"].ToString();
            payment.Payer_Id = Request.Form["Payer_Id"].ToString();
            payment.Payment_Status = Request.Form["Payment_Status"].ToString();
            payment.Tax = Convert.ToDecimal(Request.Form["Tax"].ToString());
            payment.Mc_Gross = Convert.ToDecimal(Request.Form["Mc_Gross"]);
            payment.Txn_Id = Request.Form["Txn_Id"].ToString();

            return View(payment);
        }

        public ActionResult CancelFromPayPal()
        {
            return RedirectToAction("Index");
        }

        public ActionResult NotifyFromPayPal()
        {
            return View();
        }

    }
}