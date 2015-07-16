using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPalTest.Models
{
    public class Payment
    {
        public string Address_Country { get; set; }
        public string Address_City { get; set; }
        public string Address_Country_Code { get; set; }
        public string Contact_Phone { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Payer_Business_Name { get; set; }
        public string Payer_Email { get; set; }
        public string Payer_Id { get; set; }

        public string Txn_Id { get; set; }
        public string Payment_Status { get; set; }
        public decimal Tax { get; set; }
        public decimal Mc_Gross { get; set; }
    }
}