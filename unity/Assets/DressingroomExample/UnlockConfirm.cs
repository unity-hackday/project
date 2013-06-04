using UnityEngine;
using System.Collections;

public class UnlockConfirm : MonoBehaviour {
	
	private static int TOPS_PACK_NUMBER = 1;
	private static int PANTS_PACK_NUMBER = 2;
	private static int SHOES_PACK_NUMBER = 3;
	
	public int packNumber=0;
	// Use this for initialization
	void Start () {
	}
	
	void OnGUI() {
		Main back = GameObject.Find("GameObject").GetComponent<Main>();
		int confirmButtonX = Screen.width/2-80;
		int confirmButtonY = Screen.height/2;
		int toBeUnlocked = 0;
		
		if (packNumber != 0) {
			if (packNumber == TOPS_PACK_NUMBER) {
				GUI.Box(new Rect(confirmButtonX-25,confirmButtonY-20,250,100), "Unlock Body for $1?");
				toBeUnlocked = TOPS_PACK_NUMBER;
			} else if (packNumber == PANTS_PACK_NUMBER) {
				GUI.Box(new Rect(confirmButtonX-25,confirmButtonY-20,250,100), "Unlock Legs for $1?");
				toBeUnlocked = PANTS_PACK_NUMBER;
			} else if (packNumber == SHOES_PACK_NUMBER) {
				GUI.Box(new Rect(confirmButtonX-25,confirmButtonY-20,250,100), "Unlock Shoes for $1?");
				toBeUnlocked = SHOES_PACK_NUMBER;
			}
			
			if(GUI.Button(new Rect(confirmButtonX,confirmButtonY+20,80,40), "Confirm")) {		
				packNumber = 0;
				if (toBeUnlocked == TOPS_PACK_NUMBER)
					back.bodyUnlock = true;
				if (toBeUnlocked == PANTS_PACK_NUMBER)
					back.legUnlock = true;
				if (toBeUnlocked == SHOES_PACK_NUMBER)
					back.shoeUnlock = true;
			
				back.stage = 1;
			}
			if(GUI.Button(new Rect(confirmButtonX+120,confirmButtonY+20,80,40), "Cancel")) {
				packNumber = 0;
				Unlock sf = GameObject.Find("Unlock").GetComponent<Unlock>();
				sf.UnlockEnabled = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
