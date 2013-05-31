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
	
	public static void AddItemToCart (string itemUri, string AuthToken) {
    	//string url = cortexServerUrl + "/carts/" + storeScope + "/default/lineitems/items/" + storeScope + "/" + itemId;		
		string url = itemUri;
		
		HttpWebResponse itemResponse = SendHttpRequestToCortex.SendRequest(url, "GET", emptyJsonForm, AuthToken);
		string itemResponseJSON = SendHttpRequestToCortex.GetResponseBody(itemResponse);
		
		Serializer responseSerializer = new Serializer(typeof(Response));
		Response itemResponseObj = (Response) responseSerializer.Deserialize(itemResponseJSON);
		// -- Get add to cart form uri
		string addToCartFormUrl = "";
		foreach (Response.Links linkObj in itemResponseObj.links) {
			if(linkObj.rel.Equals("addtocartform")) {
				addToCartFormUrl = linkObj.href;
			}
		}
		
		Debug.Log(addToCartFormUrl);
		
		// -- get add to cart form object to get addtodefaultcart action url
		HttpWebResponse addToCartFormResponse = SendHttpRequestToCortex.SendRequest(addToCartFormUrl, "GET", emptyJsonForm, AuthToken);
		string addToCartFormResponseJSON = SendHttpRequestToCortex.GetResponseBody(addToCartFormResponse);
		
		Response addToCartFormResponseObj = (Response) responseSerializer.Deserialize(addToCartFormResponseJSON);
		
		Debug.Log(addToCartFormResponseJSON);
		
		// -- Get add to cart form uri
		string addToDefaultCartActionUrl = "";
		
		foreach (Response.Links linkObj in addToCartFormResponseObj.links) {
			if(linkObj.rel.Equals("addtodefaultcartaction")) {
				addToDefaultCartActionUrl = linkObj.href;
			}
		}
		
		Debug.Log(addToDefaultCartActionUrl);
		// add item to cart
		HttpWebResponse httpResponse = SendHttpRequestToCortex.SendRequest(addToDefaultCartActionUrl, "POST", quantityJsonForm, AuthToken);
		
		Debug.Log(httpResponse.StatusCode);
	}
	
	public static Response GetCartResponse (string AuthToken) {
		string url = SendHttpRequestToCortex.cartsUrl;
		HttpWebResponse httpResponse = SendHttpRequestToCortex.SendRequest(url, "GET", quantityJsonForm, AuthToken);
		
		string responseJSON = SendHttpRequestToCortex.GetResponseBody(httpResponse);
	   	Debug.Log(responseJSON);
		
		Serializer responseSerializer = new Serializer(typeof(Response));
		Response response = (Response) responseSerializer.Deserialize(responseJSON);
		
		//Debug.Log("Self.type: " + response.self.type);
		//Debug.Log("Order.URL: " + response.links[2].href);
		
		return response;
	}
	
	public static string GetOrderUrl (string AuthToken){
		Response cartResponse = GetCartResponse(AuthToken);
		
		string orderUrl = null;
		foreach (Response.Links linkObj in cartResponse.links) {
			if(linkObj.rel.Equals("order")) {
				orderUrl = linkObj.href;
			}
		}
		return orderUrl;
	}
	
	public static PurchaseResponse Purchase(string AuthToken) {
		string orderUrl = GetOrderUrl (AuthToken);
		Debug.Log(orderUrl);
		//Debug.Log("URL: " + url + "\nAuth token: " + AuthToken + "\nJson: " + emptyJsonForm);
		
		//Get purchase form URL:
		HttpWebResponse orderResponse = SendHttpRequestToCortex.SendGetRequest(orderUrl);
		string orderResponseJSON = SendHttpRequestToCortex.GetResponseBody(orderResponse);
		
		Serializer responseSerializer = new Serializer(typeof(Response));
		Response orderResponseObject = (Response) responseSerializer.Deserialize(orderResponseJSON);
		string purchaseFormUrl = "";
		foreach (Response.Links linkObj in orderResponseObject.links) {
			if (linkObj.rel.Equals ("purchaseform")) {
				purchaseFormUrl = linkObj.href;
			}
		}
		
		//Get submit order action URL:
		HttpWebResponse purchaseFormResponse = SendHttpRequestToCortex.SendGetRequest(purchaseFormUrl);
		string purchaseFormResponseJSON = SendHttpRequestToCortex.GetResponseBody(purchaseFormResponse);
		
		Response purchaseFormResponseObject = (Response) responseSerializer.Deserialize(purchaseFormResponseJSON);
		string submitOrderActionUrl = "";
		foreach (Response.Links linkObj in purchaseFormResponseObject.links) {
			if (linkObj.rel.Equals ("submitorderaction")) {
				submitOrderActionUrl = linkObj.href;
			}
		}
		Debug.Log(purchaseFormResponseJSON);
		Debug.Log(submitOrderActionUrl);
		
		//Submit order to make purchase
		submitOrderActionUrl = submitOrderActionUrl+ "?followLocation";
		HttpWebResponse submitOrderActionResponse = SendHttpRequestToCortex.SendRequest(submitOrderActionUrl, "POST", emptyJsonForm, AuthToken);
		
		Debug.Log(submitOrderActionResponse.StatusCode);
		
		string submitOrderActionResponseJSON = SendHttpRequestToCortex.GetResponseBody(submitOrderActionResponse);
		Debug.Log(submitOrderActionResponseJSON);
		
		//remove all dashes from json response since C# hates them
		submitOrderActionResponseJSON = submitOrderActionResponseJSON.Replace("-","");
		Debug.Log(submitOrderActionResponseJSON);
		
		Serializer purchaseResponseSerializer = new Serializer(typeof(PurchaseResponse));
		PurchaseResponse response = (PurchaseResponse) purchaseResponseSerializer.Deserialize(submitOrderActionResponseJSON);
		
		Debug.Log(response.monetarytotal[0].amount);
		Debug.Log(response.status);
		
		return response;
	}
	
	
	public static void setShippingOptionInfoIfNeeded(string AuthToken) {
		//Get OrderUri
		string orderUrl = AddToCart.GetOrderUrl (AuthToken);
		
		Debug.Log("BeforeGetOrderResponse");
		
		//Get Order
		Response orderResponse = GetOrderResponse(orderUrl, AuthToken);
		
		Debug.Log("AfterGetOrderResponse");
		
		//check if order has needInfoLink
		string needInfoUrl = GetRelUri("needinfo", orderResponse);
		
		//if needInfo is blank, no needInfo found
		if(needInfoUrl.Equals("")) {
			Debug.Log("No NeedInfo Found");
			return;
		}
		else{
			//get shipment details id
			string[] needInfoUrlTokens = needInfoUrl.Split(seperators);
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
		string url = "";
		foreach (Response.Links linkObj in response.links) {
			if(linkObj.rel.Equals(rel)) {
				url = linkObj.uri;
			}
		}
		return url;
	}
	
	public static Response GetOrderResponse (string orderUri, string AuthToken) {
		string url = orderUri;
		
		Debug.Log(url);
		
		HttpWebResponse httpResponse = SendHttpRequestToCortex.SendRequest(url, "GET", emptyJsonForm, AuthToken);
		
		string responseJSON = SendHttpRequestToCortex.GetResponseBody(httpResponse);
	   	Debug.Log(responseJSON);
		
		Serializer responseSerializer = new Serializer(typeof(Response));
		Response response = (Response) responseSerializer.Deserialize(responseJSON);
		
		return response;
	}
}

