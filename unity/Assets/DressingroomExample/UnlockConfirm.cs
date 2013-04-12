using UnityEngine;
using System.Collections;

public class UnlockConfirm : MonoBehaviour {
	
	public int packNumber=0;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI()
	{
		Main back = GameObject.Find("GameObject").GetComponent<Main>();
		int confirmButtonX = Screen.width/2-80;
		int confirmButtonY = Screen.height/2;
		int toBeUnlocked = 0;
		
		if (packNumber != 0)
		{
			string itemId = "";
			if (packNumber ==1)
			{
				GUI.Box(new Rect(confirmButtonX-25,confirmButtonY-20,250,100), "Unlock Tops for $1?");
				toBeUnlocked = 1;
			}
			else if (packNumber ==2)
			{
				GUI.Box(new Rect(confirmButtonX-25,confirmButtonY-20,250,100), "Unlock Pants for $1?");
				toBeUnlocked = 2;
			}
			else if (packNumber==3)
			{
				GUI.Box(new Rect(confirmButtonX-25,confirmButtonY-20,250,100), "Unlock Shoes for $1?");
				toBeUnlocked = 3;
			}
			
			if(GUI.Button(new Rect(confirmButtonX,confirmButtonY+20,80,40), "Confirm"))
				{		
					packNumber = 0;
					if(toBeUnlocked == 1)
						back.bodyUnlock = true;
					if(toBeUnlocked == 2)
						back.legUnlock = true;
					if(toBeUnlocked == 3)
						back.shoeUnlock = true;
				
					back.stage = 1;
				}
			if(GUI.Button(new Rect(confirmButtonX+120,confirmButtonY+20,80,40), "Cancel"))
				{
					packNumber = 0;
					Unlock sf = GameObject.Find("Unlock").GetComponent<Unlock>();
					sf.UnlockEnabled = true;
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
