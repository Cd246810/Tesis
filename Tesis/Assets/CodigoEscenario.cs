using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class CodigoEscenario : MonoBehaviour {

	public Text Tiempo;
	int Minuto=0;
	int Segundo=0;
	float timer = 0.0f;
	bool Pausa=false;
	// Use this for initialization
	void Start () {
	//	
		Minuto=Variables.Var.Minutos;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Segundo>9){
			Tiempo.text=Minuto+":"+Segundo;
		}else{
			Tiempo.text=Minuto+":0"+Segundo;
		}
		timer += Time.deltaTime;
		bool CambioMin=false;
		if(Segundo==0){
			if(CambioMin==false){
				Minuto=Minuto-1;
				CambioMin=true;
			}
		}else if(Segundo<0){
			CambioMin=false;
			timer=0.0f;
		}
		Segundo = 59-System.Convert.ToInt32(timer % 60);
	}

	public void botonPausa(){
		if(Pausa==true){
			Pausa=false;
			Time.timeScale = 1;
		}else{
			Time.timeScale = 0;
			Pausa=true;
		}
	}

	public void Boton(){
		/* 
		FB.ShareLink(
			new System.Uri("http://google.com"),
			
			callback: ShareCallback);
			*/

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
	/* 
	private void ShareCallback (IShareResult result) {
    if (result.Cancelled || !string.IsNullOrEmpty(result.Error)) {
        Debug.Log("ShareLink Error: "+result.Error);
    } else if (!string.IsNullOrEmpty(result.PostId)) {
        // Print post identifier of the shared content
        Debug.Log(result.PostId);
    } else {
        // Share succeeded without postID
        Debug.Log("ShareLink success!");
    }
	}
	*/

private void Apicallback(IGraphResult result){
     //Parse Graph response into a specific class created for this result. 
     //parsedData = JsonUtility.FromJson<FBData>(result.RawResult);

     //Pass each field into UserInfo class. 
    // UserInfo.EMAIL = parsedData.email;
     //UserInfo.FRIENDS = parsedData.friends;
    // UserInfo.NAME = parsedData.first_name;
    // UserInfo.FACEBOOKID = parsedData.id;

             /*problem area, if I comment line below, then previous information is apparently not stored. If left as is then testTxt displays correct information but code gets stuck there.  */
    // testTxt.text = "This is the info from USerInfoInside the APICallback: " + UserInfo.EMAIL + UserInfo.FRIENDS + UserInfo.FACEBOOKID;
 }



}
