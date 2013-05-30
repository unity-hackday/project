using System;
using UnityEngine;
using System.Collections;
using JsonExSerializer;

public class RequestUtils  : MonoBehaviour {
	public static string getAuthToken ()
	{
		Main mainRef = GameObject.Find("GameObject").GetComponent<Main>();
		return mainRef.auth.access_token;
	}
	
	public static string serialize(object request, Type requestClass) {
		Serializer serializer = new Serializer(requestClass);
	    String jsonText = serializer.Serialize(request);
		return addDashes(jsonText);
	}
	
	public static object deserialize(string response, Type responseClass){
		Serializer serializer = new Serializer(responseClass);
		String jsonText = removeDashes(response);
		return serializer.Deserialize(jsonText);
	}
	
	private static string addDashes(string jsonText) {
		return jsonText.Replace("pagesize", "page-size");
	}
	
	private static string removeDashes(string jsonText){
		return jsonText.Replace("purchase-price","purchaseprice");
	}
}
