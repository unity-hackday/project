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
	private static string searchURL = "http://10.10.121.110:8080/cortex/searches/unity/keywords/items?followLocation";
	
	private static int PANTS_INDEX = 3;
	private static int SHOES_INDEX = 4;
	private static int TOPS_INDEX = 5;
	
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
	 * returning itemurl 
	 */
	public static string getItemUrl(string itemCategory) {
		string itemName = getItemName(itemCategory);
		return SearchForItem(itemName);	
	}
	
	private static string getItemName(string itemCategory){
		int itemMaterialIndex;
		if (itemCategory.Equals("tops")) {
			itemMaterialIndex = TOPS_INDEX;
		}
		else if (itemCategory.Equals("pants")) {
			itemMaterialIndex = PANTS_INDEX;
		}
		else if (itemCategory.Equals("shoes")) {
			itemMaterialIndex = SHOES_INDEX;
		} else {
			//item category is invalid. Therefore, no item id exists
			return "";
		}
		FindCharacter ();
		string itemName = characterSkin.materials[itemMaterialIndex].name;
		return itemName.Replace(" (Instance)","");
	}
	
	/*
	 * returns item url
	 */
	static string SearchForItem (string itemName) {
		//Create search request json object
		RequestItemSearch searchRequestObj = new RequestItemSearch ();
		searchRequestObj.keywords = itemName;

	    string requestJSON = RequestUtils.serialize(searchRequestObj, typeof(RequestItemSearch));
		HttpWebResponse httpResponse = SendHttpRequestToCortex.SendPostRequest(searchURL, requestJSON);
		
		string responseJSON = SendHttpRequestToCortex.GetResponseBody(httpResponse);
		ResponseSearch responseSearchObj = (ResponseSearch) RequestUtils.deserialize(responseJSON, typeof(ResponseSearch));
		
		string itemUrl = responseSearchObj.links[0].href; //We know there is only one link in the search results
		Debug.Log("Item URL: " + itemUrl);
		
		return itemUrl;
	}
	
	/*
	 * returns price
	 */
	public static string FindItemPrice(string itemUrl) {
		Response itemResponse = getItem(itemUrl);
		
		//get link to prices from item
		string priceHref = "";
		foreach (Response.Links linkObj in itemResponse.links) {
			if(linkObj.rel.Equals("price")) {
				priceHref = linkObj.href;
			}
		}
		
		HttpWebResponse priceResponse = SendHttpRequestToCortex.SendGetRequest(priceHref);
		string priceJSONResponse = SendHttpRequestToCortex.GetResponseBody(priceResponse);
		
		ResponsePrice priceObject = (ResponsePrice) RequestUtils.deserialize(priceJSONResponse, typeof(ResponsePrice));
		return priceObject.purchaseprice[0].display;
	}
	
	private static Response getItem(string itemUrl) {
		HttpWebResponse response = SendHttpRequestToCortex.SendGetRequest(itemUrl);
		string responseJSON = SendHttpRequestToCortex.GetResponseBody(response);
		
		return (Response) RequestUtils.deserialize(responseJSON, typeof(Response));
	}
}
