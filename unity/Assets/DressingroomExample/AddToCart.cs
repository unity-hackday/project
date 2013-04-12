using UnityEngine;
using System.Net;
using System.Collections;
using JsonExSerializer;

public class AddToCart {
	
	private static string cortexServerUrl = "http://10.10.121.110:8080/cortex";
	private static string storeScope = "/unity";
	private static string quantityJsonForm = "{\"quantity\":1}";
	private static string emptyJsonForm = "{}";
	private static char[] seperators = new char[] {'/'};
	
	private static string shippingOptionId = "iyzemqzsga4tmljrgzbdeljqimyucljzgnbdqlkcgfbdmnsdgu4tgmjwhe";
	
	public static void AddItemToCart (string itemId, string AuthToken) {
    	string url = cortexServerUrl + "/carts/" + storeScope + "/default/lineitems/items/" + storeScope + "/" + itemId;		
		HttpWebResponse httpResponse = SendHttpRequestToCortex.SendRequest(url, "POST", quantityJsonForm, AuthToken);
		
		Debug.Log(httpResponse.StatusCode);
	}
	
	public static Response GetCartResponse (string AuthToken) {
		string url = cortexServerUrl + "/carts/" + storeScope + "/default";
		HttpWebResponse httpResponse = SendHttpRequestToCortex.SendRequest(url, "GET", quantityJsonForm, AuthToken);
		
		string responseJSON = SendHttpRequestToCortex.GetResponseBody(httpResponse);
	   	Debug.Log(responseJSON);
		
		Serializer responseSerializer = new Serializer(typeof(Response));
		Response response = (Response) responseSerializer.Deserialize(responseJSON);
		
		//Debug.Log("Self.type: " + response.self.type);
		//Debug.Log("Order.URL: " + response.links[2].href);
		
		return response;
	}
	
	public static string GetOrderId (string AuthToken){
		Response cartResponse = GetCartResponse(AuthToken);
		
		string orderUri = null;
		foreach (Response.Links linkObj in cartResponse.links) {
			if(linkObj.rel.Equals("order")) {
				orderUri = linkObj.uri;
			}
		}
		
		//string orderUrl = cartResponse.links[2].uri;
		string orderUrl = orderUri;
		
		//Debug.Log(orderUrl);
		string[] orderUrlTokens = orderUrl.Split(seperators);
		
		//Debug.Log("Order ID: " + orderUrlTokens[3]);
		return orderUrlTokens[3];
	}
	
	public static void Purchase(string AuthToken) {
		string orderId = GetOrderId (AuthToken);
		string url = cortexServerUrl + "/purchases/orders" + storeScope + "/" + orderId;
		Debug.Log("URL: " + url + "\nAuth token: " + AuthToken + "\nJson: " + emptyJsonForm);
		HttpWebResponse httpResponse = SendHttpRequestToCortex.SendRequest(url, "POST", emptyJsonForm, AuthToken);
		
		Debug.Log(httpResponse.StatusCode);
	}
	
	
	public static void setShippingOptionInfoIfNeeded(string AuthToken) {
		//Get OrderId
		string orderId = AddToCart.GetOrderId (AuthToken);
		
		Debug.Log("BeforeGetOrderResponse");
		
		//Get Order
		Response orderResponse = GetOrderResponse(orderId, AuthToken);
		
		Debug.Log("AfterGetOrderResponse");
		
		//check if order has needInfoLink
		string needInfoUri = GetRelUri("needinfo", orderResponse);
		
		//if needInfo is blank, no needInfo found
		if(needInfoUri.Equals("")) {
			Debug.Log("No NeedInfo Found");
			return;
		}
		else{
			//get shipment details id
			string[] needInfoUrlTokens = needInfoUri.Split(seperators);
			string shipmentDetailsId = needInfoUrlTokens[3];
			
			//create shippingOptionUrl
			string shippingOptionUrl =  cortexServerUrl + "/shipmentdetails" + storeScope + "/" + 
					shipmentDetailsId + "/shippingoptioninfo/selector/shipmentdetails" + storeScope + "/" + 
					shipmentDetailsId + "/shippingoptions/" + shippingOptionId;
			
			Debug.Log("ShippingOptionUrl: " + shippingOptionUrl);
			
			HttpWebResponse getHttpResponse = SendHttpRequestToCortex.SendRequest(shippingOptionUrl, "GET", emptyJsonForm, AuthToken);
			Debug.Log("GetResponse: " + SendHttpRequestToCortex.GetResponseBody(getHttpResponse));
			
			//send POST request to select shipping option
			HttpWebResponse httpResponse = SendHttpRequestToCortex.SendRequest(shippingOptionUrl, "POST", emptyJsonForm, AuthToken);
			
			Debug.Log(httpResponse.StatusCode);
		}
	}
	
	public static string GetRelUri(string rel, Response response) {
		string uri = "";
		foreach (Response.Links linkObj in response.links) {
			if(linkObj.rel.Equals(rel)) {
				uri = linkObj.uri;
			}
		}
		return uri;
	}
	
	public static Response GetOrderResponse (string orderId, string AuthToken) {
		string url = cortexServerUrl + "/orders" + storeScope + "/" + orderId;
		
		Debug.Log(url);
		
		HttpWebResponse httpResponse = SendHttpRequestToCortex.SendRequest(url, "GET", emptyJsonForm, AuthToken);
		
		string responseJSON = SendHttpRequestToCortex.GetResponseBody(httpResponse);
	   	Debug.Log(responseJSON);
		
		Serializer responseSerializer = new Serializer(typeof(Response));
		Response response = (Response) responseSerializer.Deserialize(responseJSON);
		
		return response;
	}
}

