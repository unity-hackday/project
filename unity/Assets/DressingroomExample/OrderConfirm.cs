using UnityEngine;
using System.Collections;

public class OrderConfirm : MonoBehaviour {
	
	public int packNumber=0;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI()
	{
		int confirmButtonX = Screen.width/2-80;
		int confirmButtonY = Screen.height/2;
		string priceOfTop = "$1.00";
		string priceOfPants = "$1.00";
		string priceOfShoes = "$1.00";
		string priceOfAll = "$3.00";
		PurchaseResponse response;
		if (packNumber != 0)
		{
			string itemUrl = "";
			if (packNumber ==1)
			{
				GUI.Box(new Rect(confirmButtonX-25,confirmButtonY-20,250,100), "Buy Top ("+priceOfTop+")?");
			}
			else if (packNumber ==2)
			{
				GUI.Box(new Rect(confirmButtonX-25,confirmButtonY-20,250,100), "Buy Pants("+priceOfPants+")?");
			}
			else if (packNumber==3)
			{
				GUI.Box(new Rect(confirmButtonX-25,confirmButtonY-20,250,100), "Buy Shoes("+priceOfShoes+")?");
			}
			else if (packNumber==4)
			{
				GUI.Box(new Rect(confirmButtonX-25,confirmButtonY-20,250,100), "Buy All("+priceOfAll +")?");
			}
			
			if(GUI.Button(new Rect(confirmButtonX,confirmButtonY+20,80,40), "Confirm"))
				{
					if (packNumber.Equals(1)) {
						itemUrl = PurchaseCurrentOutfit.PurchaseItem("tops");
						response = DoPurchase(itemUrl);
					} else if (packNumber.Equals (2)) {
						itemUrl = PurchaseCurrentOutfit.PurchaseItem("pants");	
						response = DoPurchase(itemUrl);
					} else if (packNumber.Equals (3)) {
						itemUrl = PurchaseCurrentOutfit.PurchaseItem("shoes");	
						response = DoPurchase(itemUrl);
					//} else if(packNumber.Equals (4)) {
					} else {
						Main main = GameObject.Find("GameObject").GetComponent<Main>();
						string access_token = main.auth.access_token;
						
						string topsUrl = PurchaseCurrentOutfit.PurchaseItem("tops");
						string pantsUrl = PurchaseCurrentOutfit.PurchaseItem("pants");
						string shoesUrl = PurchaseCurrentOutfit.PurchaseItem("shoes");
					
						Debug.Log(topsUrl);
						Debug.Log(pantsUrl);
						Debug.Log(shoesUrl);
					
						AddToCart.AddItemToCart(topsUrl, access_token);
						AddToCart.AddItemToCart(pantsUrl, access_token);
						AddToCart.AddItemToCart(shoesUrl, access_token);
						AddToCart.setShippingOptionInfoIfNeeded(access_token);
					
						response = AddToCart.Purchase(access_token);
					}
					packNumber = 0;
					Main back = GameObject.Find("GameObject").GetComponent<Main>();
					back.response = response;
					back.stage = 5;
			}
			if(GUI.Button(new Rect(confirmButtonX+120,confirmButtonY+20,80,40), "Cancel"))
			{
				packNumber = 0;
				StoreFront sf = GameObject.Find("StoreFront").GetComponent<StoreFront>();
				sf.StoreFrontEnabled = true;
			}
		}
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
	
	PurchaseResponse DoPurchase(string itemUrl) {
		Main main = GameObject.Find("GameObject").GetComponent<Main>();
		string access_token = main.auth.access_token;
		Debug.Log(access_token);
		//string itemID = "mu4wczjwheztkmdghaydgndemjsdmmruha4tsy3bmyywiytggzsdiojvgztgimld";
		
		//Add Item to Cart
		AddToCart.AddItemToCart(itemUrl, access_token);
		
		AddToCart.setShippingOptionInfoIfNeeded(access_token);
		
		//Debug.Log(AddToCart.GetOrderUrl (access_token));
		
		// ***** NOTE *******
		// THIS WILL FAIL IF ITEM IS SHIPPABLE, WILL ENCOUNTER A NEEDINFO 
		return AddToCart.Purchase(access_token);
	}
}
