using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class CodigoEscenario : MonoBehaviour {


	float TiempoInicio;

	public Text Tiempo;
	int Minuto=0;
	int Segundo=0;
	float timer = 0.0f;
	bool Pausa=false;
	bool CambioMin=false;
	bool finalizado=false;


	//Temporales

	public GameObject TextoPausa;
	public Text TextoBotonPausa;
	public GameObject TextoFinalizar;
	public GameObject PasuarFinalizar;

	//Luces
	public Light LuzInternaL1;
	public Light Luz1;
	public Light Luz2;




	// Use this for initialization
	void Start () {
	//	
		TiempoInicio=Time.time;
		Minuto=Variables.Var.Minutos;
		CambioMin=false;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Time.time=Tiempo inicio del frame, segundos desde que inicio el juego.
		//Debug.Log(Time.time);
		float phi = Time.time / 10f * 2 * Mathf.PI;
        float amplitude = Mathf.Cos(phi) * 100.0F + 50.0F; //Jugar con estos valores para marcar el ritmo
		//Debug.Log(amplitude);
        LuzInternaL1.intensity = amplitude;

		//Funcion luz1

		float Luz1x=(Time.time-TiempoInicio)*15f;
		float Red1= 0.3630909f - 0.001261212f*Luz1x + 0.000001824916f*Luz1x*Luz1x;
		//float Red1= 0.38f - 0.002225926f*Luz1x + 0.000004407407f*Luz1x*Luz1x - 1.655235f*Mathf.Pow(10,(-9*Luz1x*Luz1x*Luz1x));
		//float Red1=(11f*Luz1x*Luz1x/5062500f)-(7f*Luz1x/4500f)+19f/50f;
		float Green1=0.819999f - (-0.001149022f/0.006373684f)*(1 - Mathf.Exp(-0.006373684f*Luz1x));
		//float Blue1=1f - 0.001031481f*Luz1x + 0.000001117284f*Luz1x*Luz1x - 3.520805f*Mathf.Exp(-10)*Luz1x*Luz1x*Luz1x;
		//float Blue1=0.9677943f*Mathf.Pow(Luz1x,-0.2641697f);
		float Blue1=1f - 0.00102963f*Luz1x + 0.000001111111f*Luz1x*Luz1x - 3.47508f*Mathf.Pow(10f,-10)*Luz1x*Luz1x*Luz1x;
		//Debug.Log(Blue1);
		Luz1.color= new Color(Red1,Green1,Blue1,1f);

		float Red2 = 0.8850909f + 0.0001854545f*Luz1x - 7.631874f*Mathf.Pow(10,-8)*Luz1x*Luz1x;

		float phi2 = (Time.time-TiempoInicio) / 900 * Mathf.PI *15;
		float Green2 = (255f-(Mathf.Cos(phi2) * 23.5f)-124.5f)/255;
		float Blue2 = (255f+(Mathf.Cos(phi2) * 127.5f)-127.5f)/255;
		//Debug.Log(Blue2);

		Luz2.color= new Color(Red2,Green2,Blue2,1f);

		//float Green2=107f+Mathf.Cos(phi) * 100.0F;
	//	float Blue2=255f

		if(Segundo<0 && Minuto<1){
			if(finalizado==false){
				PasuarFinalizar.SetActive(false);
				TextoFinalizar.SetActive(true);
				finalizado=true;
			}
		}else{
			timer += Time.deltaTime;
			if(Segundo<0){
			//if(CambioMin==false){
				Minuto=Minuto-1;
				CambioMin=true;
			//}
				timer=0.0f;
			}
			if(Segundo>9){
				Tiempo.text=Minuto+":"+Segundo;
			}else{
				Tiempo.text=Minuto+":0"+Segundo;
			}
			Segundo = 59-System.Convert.ToInt32(timer % 60);
		}
	}

	public void botonPausa(){
		if(Pausa==true){
			Pausa=false;
			Time.timeScale = 1;
			TextoBotonPausa.text="Pausar";
			TextoPausa.SetActive(false);
		}else{
			Time.timeScale = 0;
			TextoBotonPausa.text="Reanudar";
			TextoPausa.SetActive(true);
			Pausa=true;
		}
	}

	public void Boton(){
		if (FB.IsInitialized) {
			FB.ShareLink(
				new System.Uri("http://18.216.107.179/FB/D00.html"),			
				callback: ShareCallback);
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

	public void BotonCompartir2(){
		if (FB.IsInitialized) {
			FB.ShareLink(
				new System.Uri("http://18.216.107.179/FB/D02.html"),			
				callback: ShareCallback);
		}
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
