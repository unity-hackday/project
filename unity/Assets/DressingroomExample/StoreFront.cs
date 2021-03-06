using UnityEngine;
using System.Collections;

public class StoreFront : MonoBehaviour {
	
	public bool StoreFrontEnabled = false;
	
	string priceOfTop = null;
	string priceOfPants = null;
	string priceOfShoes = null;
	
	private static int TOPS_PACK_NUMBER = 1;
	private static int PANTS_PACK_NUMBER = 2;
	private static int SHOES_PACK_NUMBER = 3;
	private static int ALL_PACK_NUMBER = 4;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {	
	
	}
	
	public void SetStoreFrontProductPrice () {
		string topsUrl = PurchaseCurrentOutfit.getItemUrl("tops");
		string pantsUrl = PurchaseCurrentOutfit.getItemUrl("pants");
		string shoesUrl = PurchaseCurrentOutfit.getItemUrl("shoes");
		
		priceOfTop = PurchaseCurrentOutfit.FindItemPrice(topsUrl);
		priceOfPants = PurchaseCurrentOutfit.FindItemPrice(pantsUrl);
		priceOfShoes = PurchaseCurrentOutfit.FindItemPrice(shoesUrl);
	}
		
	void OnGUI()
	{	
		if (StoreFrontEnabled)
		{
			int spacing = 150;
			int anchorX = Screen.width/2-((20*4+spacing*3)/2);
			int anchorY = 30;
			
			//KarenGetsBillyPriceListthanks
			decimal x = decimal.Parse(priceOfTop.Replace("$",""));
			decimal y = decimal.Parse(priceOfPants.Replace("$",""));
			decimal z = decimal.Parse(priceOfShoes.Replace("$",""));
			decimal sum = x+y+z;
			string priceOfAll = "$" + sum.ToString();
			
			GUI.Box(new Rect(0,0,Screen.width,90), "Select product(s) for purchasing:");
			
			if(GUI.Button(new Rect(anchorX,anchorY,120,40), "Top ("+priceOfTop+")"))
			{
				StoreFrontEnabled = false;
				OrderConfirm checkout = GameObject.Find("OrderConfirm").GetComponent<OrderConfirm>();
				checkout.setPackNumber(TOPS_PACK_NUMBER);
			}
			if(GUI.Button(new Rect(anchorX+spacing,anchorY,120,40), "Pants ("+priceOfPants+")"))
			{
				StoreFrontEnabled = false;
				OrderConfirm checkout = GameObject.Find("OrderConfirm").GetComponent<OrderConfirm>();
				checkout.setPackNumber(PANTS_PACK_NUMBER);
			}
			if(GUI.Button(new Rect(anchorX+spacing*2,anchorY,120,40), "Shoes ("+priceOfShoes+")"))
			{
				StoreFrontEnabled = false;
				OrderConfirm checkout = GameObject.Find("OrderConfirm").GetComponent<OrderConfirm>();
				checkout.setPackNumber(SHOES_PACK_NUMBER);
			}
			if(GUI.Button(new Rect(anchorX+spacing*3,anchorY,120,40), "All ("+priceOfAll+")"))
			{
				StoreFrontEnabled = false;
				OrderConfirm checkout = GameObject.Find("OrderConfirm").GetComponent<OrderConfirm>();
				checkout.setPackNumber(ALL_PACK_NUMBER);
			}
			
			if(GUI.Button(new Rect(Screen.width-90,Screen.height-70,80,60), "Cancel"))
			{
				StoreFrontEnabled = false;
				Main back = GameObject.Find("GameObject").GetComponent<Main>();
				back.stage = 1;
			}
		}
		
	}
}
