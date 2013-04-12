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
		
		if (packNumber != 0)
		{
			string itemId = "";
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
						itemId = PurchaseCurrentOutfit.PurchaseItem("tops");
					} else if (packNumber.Equals (2)) {
						itemId = PurchaseCurrentOutfit.PurchaseItem("pants");					
					} else if (packNumber.Equals (3)) {
						itemId = PurchaseCurrentOutfit.PurchaseItem("shoes");					
					} else if (packNumber.Equals (4)) {
					
						Main main = GameObject.Find("GameObject").GetComponent<Main>();
						string access_token = main.auth.access_token;
						
						string topsId = PurchaseCurrentOutfit.PurchaseItem("tops");
						string pantsId = PurchaseCurrentOutfit.PurchaseItem("pants");
						string shoesId = PurchaseCurrentOutfit.PurchaseItem("shoes");
					
						Debug.Log(topsId);
						Debug.Log(pantsId);
						Debug.Log(shoesId);
					
						AddToCart.AddItemToCart(topsId, access_token);
						AddToCart.AddItemToCart(pantsId, access_token);
						AddToCart.AddItemToCart(shoesId, access_token);
					
						AddToCart.Purchase(access_token);
					
					}
					if(!itemId.Equals("")) {
						DoPurchase(itemId);
					}
					packNumber = 0;
					Main back = GameObject.Find("GameObject").GetComponent<Main>();
					back.stage = 1;
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
	
	void DoPurchase(string itemId) {
		Main main = GameObject.Find("GameObject").GetComponent<Main>();
		string access_token = main.auth.access_token;
		Debug.Log(access_token);
		//string itemID = "mu4wczjwheztkmdghaydgndemjsdmmruha4tsy3bmyywiytggzsdiojvgztgimld";
		AddToCart.AddItemToCart(itemId, access_token);
		// ***** NOTE *******
		// THIS WILL FAIL IF ITEM IS SHIPPABLE, WILL ENCOUNTER A NEEDINFO 
		AddToCart.Purchase(access_token);
	}
}
