using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.SceneManagement;

public class CodigoDesbloqueable : MonoBehaviour {
	public GameObject PanelIniciar;
	// Use this for initialization
	void Start () {
		PanelIniciar.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Compartir(){
		if (FB.IsInitialized) {
			if(FB.IsLoggedIn){
				//FB.ShareLink(
				//	new System.Uri("http://18.216.107.179/FB/D00.html"),			
				//	callback: ShareCallback);
				FB.FeedShare(
  					link: new System.Uri("http://18.216.107.179/FB/D00.html"),
  				//	linkName: "The Larch",
  					callback: ShareCallback
				);
			}else{
				PanelIniciar.SetActive(true);
			}
		}else{
			PanelIniciar.SetActive(true);
		}
		/*
		FB.FeedShare(
			link: new System.Uri("http://usacseats.tk"),
			linkName: "He desbloqueado un nuevo item en ZenBreath",
			mediaSource: "https://upload.wikimedia.org/wikipedia/commons/4/4a/Usac_logo.png",
			callback: ShareCallback
		);
		*/
		/* 
		//quote: "He desbloqueado un nuevo item en ZenBreath",
		var width = Screen.width;
    	var height = Screen.height;
    	var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
    // Read screen contents into the texture
    	tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
    	tex.Apply();
    	byte[] screenshot = tex.EncodeToPNG();

    	var wwwForm = new WWWForm();
    	wwwForm.AddBinaryData("image", screenshot, "Screenshot.png");

    	FB.API("me/photos", HttpMethod.POST, Apicallback, wwwForm);
		*/
		/*
		FB.FeedShare (
            string.Empty,
            new System.Uri("http://linktoga.me"),
            "Hello this is the title",
            "This is the caption",
            "Check out this game",
            new System.Uri("https://i.ytimg.com/vi/NtgtMQwr3Ko/maxresdefault.jpg"),
            string.Empty,
            ShareCallback
        );
		*/
	}

 	void ShareCallback(IResult result)
    {
        if (result.Cancelled) {
            Debug.Log ("Share Cancelled");
        } else if (!string.IsNullOrEmpty (result.Error)) {
            Debug.Log ("Error on share!");
        } else if (!string.IsNullOrEmpty (result.RawResult)) {
            Debug.Log ("Success on share");
        }
    }

	public void Finalizar(){
		SceneManager.LoadScene("MenuPrincipal");
	}

	public void Cancelar(){
		PanelIniciar.SetActive(false);
	}

	public void IniciarSesion(){
		var perms = new List<string>(){"public_profile", "email", "user_friends"};
		FB.LogInWithReadPermissions(perms, AuthCallback);
		PanelIniciar.SetActive(false);
	}
	private void AuthCallback (ILoginResult result) {
    	if (FB.IsLoggedIn) {
			FB.API("/me?fields=first_name",HttpMethod.GET,MostrarNombre);
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
	}
	void MostrarNombre(IResult result){
		if(result.Error==null){
			//Nombre.text=result.ResultDictionary["first_name"];
			OneSignal.SendTag("name", " "+result.ResultDictionary["first_name"]);
		}else{
			Debug.Log(result.Error);
		}
	}
}
