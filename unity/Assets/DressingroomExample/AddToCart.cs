using UnityEngine;
using System.Net;
using System.Collections;
using JsonExSerializer;

public class AddToCart {
	
	private static string cortexServerUrl = "http://10.10.121.110:8080/cortex";
	private static string storeScope = "/unity";
	private static string quantityJsonForm = "{\"quantity\":1}";
	private static string purchaseJsonForm = "{}";
	
	public static void AddItemToCart (string itemId, string AuthToken) {
    	string url = cortexServerUrl + "/carts/" + storeScope + "/default/lineitems/items/" + storeScope + "/" + itemId;
		
		HttpWebResponse httpResponse = SendHttpRequestToCortex.SendRequest(url, "POST", quantityJsonForm, AuthToken);
		
		Debug.Log(httpResponse.StatusCode);
	}
	
	public static Response GetCartResponse (string AuthToken) {
		string url = cortexServerUrl + "/carts/" + storeScope + "/default";
		HttpWebResponse httpResponse = SendHttpRequestToCortex.SendRequest(url, "GET", quantityJsonForm, AuthToken);
		
		string responseJSON = SendHttpRequestToCortex.GetResponseBody(httpResponse);
		
	   	Serializer responseSerializer = new Serializer(typeof(Response));
		Response response = (Response) responseSerializer.Deserialize(responseJSON);
		
		Debug.Log("Self.type: " + response.self.type);
		Debug.Log("Order.URL: " + response.links[2].href);
		
		return response;
	}
	
	public static string GetOrderId (string AuthToken){
		Response cartResponse = GetCartResponse(AuthToken);
		
		string orderUrl = cartResponse.links[2].uri;
		char[] seperators = new char[] {'/'};
		string[] orderUrlTokens = orderUrl.Split(seperators);
		
		Debug.Log("Order ID: " + orderUrlTokens[3]);
		return orderUrlTokens[3];
	}
	
	public static void Purchase(string AuthToken) {
		string orderId = GetOrderId (AuthToken);
		string url = cortexServerUrl + "/purchases/orders/" + storeScope + "/" + orderId;
		
		HttpWebResponse httpResponse = SendHttpRequestToCortex.SendRequest(url, "POST", purchaseJsonForm, AuthToken);
		
		Debug.Log(httpResponse.StatusCode);
	}
}

