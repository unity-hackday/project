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
	
	public static void AddItemToCart (string itemUri) {
		HttpWebResponse itemResponse = SendHttpRequestToCortex.SendGetRequest(itemUri);
		string itemResponseJSON = SendHttpRequestToCortex.GetResponseBody(itemResponse);
		
		Response itemResponseObj = (Response) RequestUtils.deserialize(itemResponseJSON, typeof(Response));
		
		// -- Get add to cart form uri
		string addToCartFormUrl = "";
		foreach (Response.Links linkObj in itemResponseObj.links) {
			if(linkObj.rel.Equals("addtocartform")) {
				addToCartFormUrl = linkObj.href;
			}
		}
		Debug.Log(addToCartFormUrl);
		
		// -- get add to cart form object to get addtodefaultcart action url
		HttpWebResponse addToCartFormResponse = SendHttpRequestToCortex.SendGetRequest(addToCartFormUrl);
		string addToCartFormResponseJSON = SendHttpRequestToCortex.GetResponseBody(addToCartFormResponse);
		
		Response addToCartFormResponseObj = (Response) RequestUtils.deserialize(addToCartFormResponseJSON, typeof(Response));
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
		HttpWebResponse httpResponse = SendHttpRequestToCortex.SendPostRequest(addToDefaultCartActionUrl, quantityJsonForm);
		Debug.Log(httpResponse.StatusCode);
	}
	
	public static Response GetCartResponse () {
		string url = SendHttpRequestToCortex.cartsUrl;
		HttpWebResponse httpResponse = SendHttpRequestToCortex.SendGetRequest(url);
		
		string responseJSON = SendHttpRequestToCortex.GetResponseBody(httpResponse);
	   	Debug.Log(responseJSON);
		
		Response response = (Response) RequestUtils.deserialize(responseJSON, typeof(Response));
		
		return response;
	}
	
	public static string GetOrderUrl (){
		Response cartResponse = GetCartResponse();
		
		string orderUrl = null;
		foreach (Response.Links linkObj in cartResponse.links) {
			if(linkObj.rel.Equals("order")) {
				orderUrl = linkObj.href;
			}
		}
		return orderUrl;
	}
	
	public static PurchaseResponse Purchase() {
		string orderUrl = GetOrderUrl ();
		Debug.Log(orderUrl);
		
		//Get purchase form URL:
		HttpWebResponse orderResponse = SendHttpRequestToCortex.SendGetRequest(orderUrl);
		string orderResponseJSON = SendHttpRequestToCortex.GetResponseBody(orderResponse);
		
		Response orderResponseObject = (Response) RequestUtils.deserialize(orderResponseJSON, typeof(Response));
		string purchaseFormUrl = "";
		foreach (Response.Links linkObj in orderResponseObject.links) {
			if (linkObj.rel.Equals ("purchaseform")) {
				purchaseFormUrl = linkObj.href;
			}
		}
		
		//Get submit order action URL:
		HttpWebResponse purchaseFormResponse = SendHttpRequestToCortex.SendGetRequest(purchaseFormUrl);
		string purchaseFormResponseJSON = SendHttpRequestToCortex.GetResponseBody(purchaseFormResponse);
		
		Response purchaseFormResponseObject = (Response) RequestUtils.deserialize(purchaseFormResponseJSON, typeof(Response));
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
		HttpWebResponse submitOrderActionResponse = SendHttpRequestToCortex.SendPostRequest(submitOrderActionUrl, emptyJsonForm);
		Debug.Log(submitOrderActionResponse.StatusCode);
		
		string submitOrderActionResponseJSON = SendHttpRequestToCortex.GetResponseBody(submitOrderActionResponse);
		Debug.Log(submitOrderActionResponseJSON);
		
		//remove all dashes from json response since C# hates them
		submitOrderActionResponseJSON = submitOrderActionResponseJSON.Replace("-","");
		Debug.Log(submitOrderActionResponseJSON);
		
		PurchaseResponse response = (PurchaseResponse) RequestUtils.deserialize(submitOrderActionResponseJSON, typeof(PurchaseResponse));
		
		Debug.Log(response.monetarytotal[0].amount);
		Debug.Log(response.status);
		
		return response;
	}
	
	public static void setShippingOptionInfoIfNeeded() {
		//Get OrderUri
		string orderUrl = GetOrderUrl();
		
		//Get Order
		Response orderResponse = GetOrderResponse(orderUrl);
		
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
			
			HttpWebResponse getHttpResponse = SendHttpRequestToCortex.SendGetRequest(shippingOptionUrl);
			Debug.Log("GetResponse: " + SendHttpRequestToCortex.GetResponseBody(getHttpResponse));
			
			//send POST request to select shipping option
			HttpWebResponse httpResponse = SendHttpRequestToCortex.SendPostRequest(shippingOptionUrl, emptyJsonForm);
			
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
	
	public static Response GetOrderResponse (string orderUri) {
		string url = orderUri;
		Debug.Log(url);
		
		HttpWebResponse httpResponse = SendHttpRequestToCortex.SendGetRequest(url);
		
		string responseJSON = SendHttpRequestToCortex.GetResponseBody(httpResponse);
	   	Debug.Log(responseJSON);
		
		Response response = (Response) RequestUtils.deserialize(responseJSON, typeof(Response));
		return response;
	}
}

