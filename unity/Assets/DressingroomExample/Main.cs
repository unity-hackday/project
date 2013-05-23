using System.Collections;
using System.Net;
using JsonExSerializer;
using UnityEngine;

// This MonoBehaviour is responsible for controlling the CharacterGenerator,
// animating the character, and the user interface. When the user requests a 
// different character configuration the CharacterGenerator is asked to prepare
// the required assets. When all assets are downloaded and loaded a new
// character is created.
class Main : MonoBehaviour
{
	
	//set in login screen
	private string UserName = "billy.hon@elasticpath.com";
	private string Password = "password";
	public ResponseAuthentication auth = new ResponseAuthentication();
	
	private string cortexServerUrl = "http://10.10.121.110:8080/cortex";
	private string storeScope = "/unity";
	public int stage = 0;
	
	//User names and passwords (even numbers = user name; odd numbers = password)
	ArrayList users = new ArrayList();
	
    CharacterGenerator generator;
    GameObject character;
    bool usingLatestConfig;
    bool newCharacterRequested = true;
    bool firstCharacter = true;
    string nonLoopingAnimationToPlay;

    const float fadeLength = .6f;
    const int typeWidth = 80;
    const int buttonWidth = 20;
    const string prefName = "Character Generator Demo Pref";
	
	public bool bodyUnlock = true;
	public bool legUnlock = true;
	public bool shoeUnlock = true;
	
	public PurchaseResponse response =null;
	
    // Initializes the CharacterGenerator and load a saved config if any.
    IEnumerator Start()
    {
		auth.access_token = "";
		auth.expires_in = "";
		auth.token_type = "";
		
		//auth.access_token = "490499ab-d27d-4fa9-ab75-70b8ab7ecf0e";
		Debug.Log(stage);
		
        while (!CharacterGenerator.ReadyToUse) yield return 0;
        if (PlayerPrefs.HasKey(prefName))
            generator = CharacterGenerator.CreateWithConfig(PlayerPrefs.GetString(prefName));
        else
            generator = CharacterGenerator.CreateWithRandomConfig("Female");

    }

    // Requests a new character when the required assets are loaded, starts
    // a non looping animation when changing certain pieces of clothing.
    void Update()
    {
        if (generator == null) return;
        if (usingLatestConfig) return;
        if (!generator.ConfigReady) return;

        usingLatestConfig = true;

        if (newCharacterRequested)
        {
            Destroy(character);
            character = generator.Generate();
            character.animation.Play("idle1");
            character.animation["idle1"].wrapMode = WrapMode.Loop;
            newCharacterRequested = false;

            // Start the walkin animation for the first character.
            if (!firstCharacter) return;
            firstCharacter = false;
            if (character.animation["walkin"] == null) return;
            
            // Set the layer to 1 so this animation takes precedence
            // while it's blended in.
            character.animation["walkin"].layer = 1;
            
            // Use crossfade, because it will also fade the animation
            // nicely out again, using the same fade length.
            character.animation.CrossFade("walkin", fadeLength);
            
            // We want the walkin animation to have full weight instantly,
            // so we overwrite the weight manually:
            character.animation["walkin"].weight = 1;
            
            // As the walkin animation starts outside the camera frustrum,
            // and moves the mesh outside its original bounding box,
            // updateWhenOffscreen has to be set to true for the
            // SkinnedMeshRenderer to update. This should be fixed
            // in a future version of Unity.
            character.GetComponent<SkinnedMeshRenderer>().updateWhenOffscreen = true;
        }
        else
        {
            character = generator.Generate(character);
            
            if (nonLoopingAnimationToPlay == null) return;
            
            character.animation[nonLoopingAnimationToPlay].layer = 1;
            character.animation.CrossFade(nonLoopingAnimationToPlay, fadeLength);
            nonLoopingAnimationToPlay = null;
        }
 
    }

    void OnGUI()
    {
		//TestGUI();
		
		if (stage == 0) //Stage 0 is the login screen
		{
			LoginScreen();
		} //end of stage 0
		
		
		if(stage == 1) //stage 1 is the character selection screen
		{
	        if (generator == null) return;
	        GUI.enabled = usingLatestConfig && !character.animation.IsPlaying("walkin");
	        GUILayout.BeginArea(new Rect(10, 10, typeWidth + 2 * buttonWidth + 8, 500));
	
	        // Buttons for changing the active character.
	        GUILayout.BeginHorizontal();
	
	        if (GUILayout.Button("<", GUILayout.Width(buttonWidth)))
	            ChangeCharacter(false);
	
	        GUILayout.Box("Character", GUILayout.Width(typeWidth));
	
	        if (GUILayout.Button(">", GUILayout.Width(buttonWidth)))
	            ChangeCharacter(true);
	
	        GUILayout.EndHorizontal();
	
	        // Buttons for changing character elements.
	        AddCategory("face", "Head", null);
	        AddCategory("eyes", "Eyes", null);
	        AddCategory("hair", "Hair", null);
			AddCategory("top", "Body", "item_shirt");
	        AddCategory("pants", "Legs", "item_pants");
	        AddCategory("shoes", "Feet", "item_boots");

	
	        // Buttons for saving and deleting configurations.
	        // In a real world application you probably want store these
	        // preferences on a server, but for this demo configurations 
	        // are saved locally using PlayerPrefs.
	        if (GUILayout.Button("Save Configuration"))
	            PlayerPrefs.SetString(prefName, generator.GetConfig());
	
	        if (GUILayout.Button("Delete Configuration"))
	            PlayerPrefs.DeleteKey(prefName);
	
	        // Show download progress or indicate assets are being loaded.
	        GUI.enabled = true;
	        if (!usingLatestConfig)
	        {
	            float progress = generator.CurrentConfigProgress;
	            string status = "Loading";
	            if (progress != 1) status = "Downloading " + (int)(progress * 100) + "%";
	            GUILayout.Box(status);
	        }
	
	        GUILayout.EndArea();
	        /* 
			if(GUI.Button(new Rect(Screen.width-190,Screen.height-140,180,60), "Unlock Customizations"))

			{
				Unlock uk = GameObject.Find("Unlock").GetComponent<Unlock>();
				uk.UnlockEnabled = true;
				stage = 2;
			}
			*/
			
			if(GUI.Button(new Rect(Screen.width-190,Screen.height-70,180,60), "Purchase Current Selections"))

			{
				StoreFront sf = GameObject.Find("StoreFront").GetComponent<StoreFront>();
				sf.SetStoreFrontProductPrice();
				sf.StoreFrontEnabled = true;
				stage = 2;
			}
			
	    }//end of stage 1
		
		if(stage==5){
			int confirmButtonX = Screen.width/2-65;
			int confirmButtonY = Screen.height/2;
			GUI.Box(new Rect(confirmButtonX-25,confirmButtonY-20,280,100), "Your purchase has been proccessed. \n Thank you for shopping with Co-Op and Co!");
			
			if(GUI.Button(new Rect(confirmButtonX+75,confirmButtonY+20,80,40), "Contiune")){
				stage =1 ;
			}
		}
	}
	
	void TestGUI() {
		
		GUI.Box(new Rect(10,10,100,90), "Loader Menu");
		
		if(GUI.Button(new Rect(20,40,80,20), "AddToCart")) {
			
			string itemID = "mu4wczjwheztkmdghaydgndemjsdmmruha4tsy3bmyywiytggzsdiojvgztgimld";
			AddToCart.AddItemToCart(itemID, auth.access_token);
			
			//AddToCart.GetCartResponse(auth.access_token);
			//AddToCart.GetOrderId(auth.access_token);
			AddToCart.Purchase(auth.access_token);
			
			//HttpWebResponse httpResponse = SendRequst(url,"POST",json);
			//Debug.Log(httpResponse.StatusCode);
			
			//Debug.Log(getResponseBody(httpResponse));
		}
	}
	
	void LoginScreen() {
		// Make a background box
		GUI.backgroundColor = Color.black;
		GUI.Box(new Rect(Screen.width/2-110,Screen.height/2-100,300,140), "Welcome, Please Log In");
		
		//make login text fields
		GUI.backgroundColor = Color.gray;
		UserName = GUI.TextField(new Rect(Screen.width/2-60, Screen.height/2-60, 200, 20),UserName, 50);
		//Password = GUI.TextField(new Rect(Screen.width/2-60, Screen.height/2-30, 200, 20),Password, 50);
		Password = GUI.PasswordField(new Rect(Screen.width/2-60, Screen.height/2-30, 200, 20),Password,"*"[0], 50);
		
		// Make the first button. 
		if (GUI.Button(new Rect(Screen.width/2,Screen.height/2,80,20), "Login"))
		{
			RequestUser user = new RequestUser();				
			user.username = UserName;
			user.password = Password;
			
			//Debug.Log(UserName);
			//Debug.Log("access_token: " + auth.access_token);
			
		    // serialize to a string
		    Serializer userSerializer = new Serializer(typeof(RequestUser));
		    string jsonText = userSerializer.Serialize(user);
			
			//Get params for Cortex reequest
			string url = cortexServerUrl + "/authentication" + storeScope;
			string authToken = auth.access_token;
			
			//Debug.Log(jsonText);
			HttpWebResponse httpResponse = SendHttpRequestToCortex.SendRequest(url,"POST",jsonText,authToken);
			
			Debug.Log(httpResponse.StatusCode);
			/*
			users.Add("user");
			users.Add("123");

			//if (UserName=="user" && Password == "123")
			*/
			if(httpResponse.StatusCode == HttpStatusCode.OK)
			{
				stage = 1; 
				//get AuthToken
				string responseJSON = SendHttpRequestToCortex.GetResponseBody(httpResponse);
				//Debug.Log(responseJSON);
				
	   			Serializer authSerializer = new Serializer(typeof(ResponseAuthentication));
				auth = (ResponseAuthentication) authSerializer.Deserialize(responseJSON);
				
				//Debug.Log("access_token: " + auth.access_token);
			}
			else
			{
				//stage = 0;
				GUI.Box(new Rect(Screen.width/2+30,Screen.height/2+30,80,20), "Invalid Password, Please try Again");
			}
		}
	}

    // Draws buttons for configuring a specific category of items, like pants or shoes.
    void AddCategory(string category, string displayName, string anim)
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("<", GUILayout.Width(buttonWidth)))
            ChangeElement(category, false, anim);

        GUILayout.Box(displayName, GUILayout.Width(typeWidth));

        if (GUILayout.Button(">", GUILayout.Width(buttonWidth)))
            ChangeElement(category, true, anim);

        GUILayout.EndHorizontal();
    }

    void ChangeCharacter(bool next)
    {
        generator.ChangeCharacter(next);
        usingLatestConfig = false;
        newCharacterRequested = true;
    }

    void ChangeElement(string catagory, bool next, string anim)
    {
        generator.ChangeElement(catagory, next);
        usingLatestConfig = false;
        
        if (!character.animation.IsPlaying(anim))
            nonLoopingAnimationToPlay = anim;
    }
}
