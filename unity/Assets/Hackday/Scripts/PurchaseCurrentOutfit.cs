using UnityEngine;
using System.Net;
using System.Collections;
using JsonExSerializer;

public class PurchaseCurrentOutfit : MonoBehaviour {
	
	public static SkinnedMeshRenderer characterSkin;
	
	// Materials[0] = Eyes
	// Materials[1] = Face
	// Materials[2] = Hair
	// Materials[3] = Pants
	// Materials[4] = Shoes
	// Materials[5] = Tops
	
	
	private string cortexServerUrl = "http://10.10.121.110:8080/cortex";
	
	void PurchaseSelectedOutfit () {
//		string url = cortexServerUrl + "/carts/" + storeScope + "/default";
		this.FindCharacter();
		Debug.Log(characterSkin.materials.Length);
		Debug.Log(characterSkin.materials[0].name);
		Debug.Log(characterSkin.materials[1].name);
		Debug.Log(characterSkin.materials[2].name);
		Debug.Log(characterSkin.materials[3].name);
		Debug.Log(characterSkin.materials[4].name);
		Debug.Log(characterSkin.materials[5].name);
		
		string item = this.SearchForItem (characterSkin.materials[5].name);
	}
	
	static void FindCharacter () {
		GameObject characterObj = null;
		if (characterObj == null) {
			characterObj = GameObject.Find("female");
		} else {
			characterObj = GameObject.Find("male");
		}
		
		if (characterObj != null) {
			characterSkin = characterObj.GetComponent<SkinnedMeshRenderer>();
		}	
	}
	
	/* 
	 * itemCategory is either: tops, pants, shoes
	 * returning item id 
	 */
	public static string PurchaseItem(string itemCategory) {
		int itemMaterialIndex;
		if (itemCategory.Equals("tops")) {
			itemMaterialIndex = 5;
		}
		else if (itemCategory.Equals("pants")) {
			itemMaterialIndex = 3;
		}
		else if (itemCategory.Equals("shoes")) {
			itemMaterialIndex = 4;
		} else {
			return "";
		}
		
		FindCharacter ();
		string itemName = characterSkin.materials[itemMaterialIndex].name;
		Debug.Log(itemName);
		string itemId = SearchForItem (itemName);	
		return itemId;
	}
	
	/*
	 * returns item id
	 */
	static string SearchForItem (string itemName) {
		string url = "http://10.10.121.110:8080/cortex/searches/unity/keywords/items?followLocation";
	
		RequestItemSearch requestObj = new RequestItemSearch ();
		requestObj.keywords = itemName;
		requestObj.pagesize = 5;
		requestObj.keywords = requestObj.keywords.Replace(" (Instance)","");
		//Debug.Log(requestObj.username + requestObj.password);
	    Serializer serializer = new Serializer(typeof(RequestItemSearch));
		//**** JSON Text **** 
	    string jsonText = serializer.Serialize(requestObj);
		jsonText = jsonText.Replace("pagesize", "page-size");
		
		Main mainRef = GameObject.Find("GameObject").GetComponent<Main>();
		
		HttpWebResponse httpResponse = SendHttpRequestToCortex.SendRequest(url, "POST", jsonText, mainRef.auth.access_token);
		
		string responseJSON = SendHttpRequestToCortex.GetResponseBody(httpResponse);
		Debug.Log(httpResponse.StatusCode);
		Debug.Log(responseJSON);
		
		Serializer responseSerializer = new Serializer (typeof(ResponseSearch));
		ResponseSearch responseSearchObj = (ResponseSearch) responseSerializer.Deserialize(responseJSON);
		string itemUri = responseSearchObj.links[0].uri;
		
		
		char[] seperators = new char[] {'/'};
		string[] itemUriTokens = itemUri.Split(seperators);
		
		Debug.Log("Item ID: " + itemUriTokens[3]);
		return itemUriTokens[3];
	}
	
}
