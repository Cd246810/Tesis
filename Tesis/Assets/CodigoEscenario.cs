using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Generic;
using Facebook.Unity;

public class CodigoEscenario : MonoBehaviour {

	public Text Nombre;
	
	public GameObject DialogProfilePic;
	// Include Facebook namespace


// Awake function from Unity's MonoBehavior
	void Awake ()
	{
  	  if (!FB.IsInitialized) {
        // Initialize the Facebook SDK
     	   FB.Init(InitCallback, OnHideUnity);
  	  	} else {
        // Already initialized, signal an app activation App Event
      	  FB.ActivateApp();
    	}
	}

	private void InitCallback ()
	{
    	if (FB.IsInitialized) {
        	// Signal an app activation App Event
        	FB.ActivateApp();
        	// Continue with Facebook SDK
        	// ...
    	} else {
        	Debug.Log("Failed to Initialize the Facebook SDK");
    	}
	}

	private void OnHideUnity (bool isGameShown)
	{
    	if (!isGameShown) {
        // Pause the game - we will need to hide
        	Time.timeScale = 0;
    	} else {
        // Resume the game - we're getting focus again
        	Time.timeScale = 1;
    	}
	}


	// Use this for initialization
	void Start () {
	//	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Boton(){
		var perms = new List<string>(){"public_profile", "email", "user_friends"};
		FB.LogInWithReadPermissions(perms, AuthCallback);
	}
	//var perms = new List<string>(){"public_profile", "email", "user_friends"};
	//FB.LogInWithReadPermissions(perms, AuthCallback);

	private void AuthCallback (ILoginResult result) {
    	if (FB.IsLoggedIn) {
        	// AccessToken class will have session details
        	var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
        	// Print current access token's User ID
        	Debug.Log(aToken.UserId);
        	// Print current access token's granted permissions
        	foreach (string perm in aToken.Permissions) {
            	Debug.Log(perm);
        	}
    	} else {
        	Debug.Log("User cancelled login");
    	}
		DealWithFBMenus(FB.IsLoggedIn);
	}

	void DealWithFBMenus(bool IsLoggedIn){
		if(FB.IsLoggedIn){
			FB.API("/me?fields=first_name",HttpMethod.GET,MostrarNombre);
			FB.API ("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
		}
	}

	void MostrarNombre(IResult result){
		if(result.Error==null){
			//Nombre.text=result.ResultDictionary["first_name"];
			Nombre.text=""+result.ResultDictionary["first_name"];
		}else{
			Debug.Log(result.Error);
		}
	}
	void DisplayProfilePic(IGraphResult result)
    {

        if (result.Texture != null) {

            Image ProfilePic = DialogProfilePic.GetComponent<Image> ();

            ProfilePic.sprite = Sprite.Create (result.Texture, new Rect (0, 0, 128, 128), new Vector2 ());

        }

    }
}
