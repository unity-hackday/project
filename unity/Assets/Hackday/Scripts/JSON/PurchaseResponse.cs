using System;
using System.Collections;
using System.Collections.Generic;

public class PurchaseResponse : Response {
	
	private List<Total> MonetaryTotal;
    private PurchaseDate DateOfPurchase;
    private string PurchaseNumber;
    private string Status;
    private Total TaxTotal;

    public List<Total> monetarytotal
    {
        get { return this.MonetaryTotal; }
        set { this.MonetaryTotal = value; }
    }

    public Total taxtotal
    {
        get { return this.TaxTotal; }
        set { this.TaxTotal = value; }
    }
	
	public string status
    {
        get { return this.Status; }
        set { this.Status = value; }
    }

    public string purchasenumber
    {
        get { return this.PurchaseNumber; }
        set { this.PurchaseNumber = value; }
    }
	
    public PurchaseDate purchasedate
    {
        get { return this.DateOfPurchase; }
        set { this.DateOfPurchase = value; }
    }
	
	public class Total {
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
	
	public class PurchaseDate {
		private string DisplayValue;
		private double Value;
			
        public double value
        {
            get { return this.Value; }
            set { this.Value = value; }
        }
        public string displayvalue
        {
            get { return this.DisplayValue; }
            set { this.DisplayValue = value; }
        }
	}
}

