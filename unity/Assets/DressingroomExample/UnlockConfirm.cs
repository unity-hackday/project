using UnityEngine;
using System.Collections;

public class UnlockConfirm : MonoBehaviour {
	
	public int packNumber=0;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI()
	{
		int confirmButtonX = Screen.width/2-80;
		int confirmButtonY = Screen.height/2;
		
		if (packNumber != 0)
		{
			string itemId = "";
			if (packNumber ==1)
			{
				GUI.Box(new Rect(confirmButtonX-25,confirmButtonY-20,250,100), "Unlock Body for $1?");
			}
			else if (packNumber ==2)
			{
				GUI.Box(new Rect(confirmButtonX-25,confirmButtonY-20,250,100), "Unlock Legs for $1?");
			}
			else if (packNumber==3)
			{
				GUI.Box(new Rect(confirmButtonX-25,confirmButtonY-20,250,100), "Unlock Shoes for $1?");
			}
			
			if(GUI.Button(new Rect(confirmButtonX,confirmButtonY+20,80,40), "Confirm"))
				{		
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
