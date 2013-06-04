using UnityEngine;
using System.Collections;

public class OrderConfirm : MonoBehaviour {
	
	private static int TOPS_PACK_NUMBER = 1;
	private static int PANTS_PACK_NUMBER = 2;
	private static int SHOES_PACK_NUMBER = 3;
	private static int ALL_PACK_NUMBER = 4;
	
	private static string CATEGORY_TOPS = "tops";
	private static string CATEGORY_PANTS = "pants";
	private static string CATEGORY_SHOES = "shoes";
	
	private static string priceOfTop = "$1.00";
	private static string priceOfPants = "$1.00";
	private static string priceOfShoes = "$1.00";
	private static string priceOfAll = "$3.00";
	
	private static int packNumber = 0;
	
	// Use this for initialization
	void Start () {
	}
	
	void OnGUI() {
		int confirmButtonX = Screen.width/2-80;
		int confirmButtonY = Screen.height/2;
		
		if (packNumber != 0)
		{
			if (packNumber == TOPS_PACK_NUMBER) {
				GUI.Box(new Rect(confirmButtonX-25,confirmButtonY-20,250,100), "Buy Top ("+priceOfTop+")?");
			} else if (packNumber == PANTS_PACK_NUMBER) {
				GUI.Box(new Rect(confirmButtonX-25,confirmButtonY-20,250,100), "Buy Pants("+priceOfPants+")?");
			} else if (packNumber == SHOES_PACK_NUMBER) {
				GUI.Box(new Rect(confirmButtonX-25,confirmButtonY-20,250,100), "Buy Shoes("+priceOfShoes+")?");
			} else if (packNumber == ALL_PACK_NUMBER) {
				GUI.Box(new Rect(confirmButtonX-25,confirmButtonY-20,250,100), "Buy All("+priceOfAll +")?");
			}
			if (GUI.Button(new Rect(confirmButtonX,confirmButtonY+20,80,40), "Confirm")) {
				PurchaseResponse response = checkout();
				setPackNumber(0);
				Main back = GameObject.Find("GameObject").GetComponent<Main>();
				back.response = response;
				back.stage = 5;
			}
			if(GUI.Button(new Rect(confirmButtonX+120,confirmButtonY+20,80,40), "Cancel")) {
				setPackNumber(0);
				StoreFront sf = GameObject.Find("StoreFront").GetComponent<StoreFront>();
				sf.StoreFrontEnabled = true;
			}
		}
	}
	
	public void setPackNumber(int packNum) {
		packNumber = packNum;
	}
	
	private static PurchaseResponse checkout() {
		PurchaseResponse response;
		if (packNumber.Equals(TOPS_PACK_NUMBER)) {
			searchForItemAndAddItemToCart(CATEGORY_TOPS);
			response = DoPurchase();
		} else if (packNumber.Equals(PANTS_PACK_NUMBER)) {
			searchForItemAndAddItemToCart(CATEGORY_PANTS);	
			response = DoPurchase();
		} else if (packNumber.Equals(SHOES_PACK_NUMBER)) {
			searchForItemAndAddItemToCart(CATEGORY_SHOES);	
			response = DoPurchase();
		//else packNumber.Equals(4) 
		} else {
			searchForItemAndAddItemToCart(CATEGORY_TOPS);
			searchForItemAndAddItemToCart(CATEGORY_PANTS);
			searchForItemAndAddItemToCart(CATEGORY_SHOES);
			response = DoPurchase();
		}
		return response;
	}
	
	private static void searchForItemAndAddItemToCart(string itemCategory) {
		string itemUrl = PurchaseCurrentOutfit.getItemUrl(itemCategory);
		//Debug.Log(itemUrl);
		AddToCart.AddItemToCart(itemUrl);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	private static PurchaseResponse DoPurchase() {
		AddToCart.setShippingOptionInfoIfNeeded();
		// ***** NOTE *******
		// THIS WILL FAIL IF ITEM IS SHIPPABLE, WILL ENCOUNTER A NEEDINFO 
		return AddToCart.Purchase();
	}
}
