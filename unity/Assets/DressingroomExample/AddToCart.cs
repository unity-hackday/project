using UnityEngine;
using System.Net;
using System.Collections;

public class AddToCart {
	
	private static string cortexServerUrl = "http://10.10.121.110:8080/cortex";
	private static string storeScope = "/unity";
	private static string quantityJsonForm = "{\"quantity\":1}";
	
	public static void AddItemToCart (string itemId, string AuthToken) {
    	string url = cortexServerUrl + "/carts/" + storeScope + "/default/lineitems/items/" + storeScope + "/" + itemId;
		
		HttpWebResponse httpResponse = SendHttpRequestToCortex.SendRequest(url, "POST", quantityJsonForm, AuthToken);
	}
}

