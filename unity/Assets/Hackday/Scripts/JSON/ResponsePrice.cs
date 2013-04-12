using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResponsePrice : Response {
	
	 private List<PurchasePrice> Purchase_Price;
     public List<PurchasePrice> purchaseprice
     {
        get { return this.Purchase_Price; }
        set { this.Purchase_Price = value; }
     }
	
	public class PurchasePrice {
		private decimal Amount;	
		private string Currency;
		private string Display;
			
        public decimal amount
        {
            get { return this.Amount; }
            set { this.Amount = value; }
        }
        public string currency
        {
            get { return this.Currency; }
            set { this.Currency = value; }
        }
        public string display
        {
            get { return this.Display; }
            set { this.Display = value; }
        }
			
	}
}