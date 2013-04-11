using UnityEngine;
using System.Collections;
using JsonExSerializer;

public class JSONExample : MonoBehaviour {
	
	void Start() {
		// customer
		RequestUser user = new RequestUser();
		user.Username = "oliver.harris@elasticpath.com";
		user.Password = "password";
	
	    // serialize to a string
	    Serializer serializer = new Serializer(typeof(RequestUser));
		//**** JSON Text **** 
	    string jsonText = serializer.Serialize(user);
	    Debug.Log(jsonText);
		
		// **** JSON to object 
		RequestUser deserializedUser = (RequestUser) serializer.Deserialize(jsonText);
		Debug.Log("Username: " + deserializedUser.Username + " Password: " + deserializedUser.Password);
	}
}
