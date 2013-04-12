using UnityEngine;
using System.Collections;

public class OrderConfirm : MonoBehaviour {
	
	public int packNumber=0;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI()
	{
		if (packNumber != 0)
		{
			string itemId = "";
			
			if (packNumber ==1)
			{
				GUI.Box(new Rect(0,0,Screen.width,Screen.height), "You have Ordered: Pack 1");
				
				//get itemId
			}
			if (packNumber ==2)
			{
				GUI.Box(new Rect(0,0,Screen.width,Screen.height), "You have Ordered: Pack 2");
				
				//get itemId
			}
			if (packNumber==3)
			{
				GUI.Box(new Rect(0,0,Screen.width,Screen.height), "You have Ordered: Pack 3");
				
				//get itemId
			}
			
			if(GUI.Button(new Rect(Screen.width/2-80,Screen.height/2,80,80), "Confirm"))
				{
					DoPurchase(itemId);
					
					packNumber = 0;
					Main back = GameObject.Find("GameObject").GetComponent<Main>();
					back.stage = 1;
				}
			if(GUI.Button(new Rect(Screen.width/2+80,Screen.height/2,80,80), "Cancel"))
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
		string AuthToken = main.auth.access_token;
		//string itemID = "mu4wczjwheztkmdghaydgndemjsdmmruha4tsy3bmyywiytggzsdiojvgztgimld";
		AddToCart.AddItemToCart(itemID, auth.access_token);
		AddToCart.Purchase(auth.access_token);
	}
}
