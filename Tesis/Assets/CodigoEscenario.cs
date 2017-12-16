using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using UnityEngine.SceneManagement;


// We need this one for importing our IOS functions
using System.Runtime.InteropServices;


public class CodigoEscenario : MonoBehaviour {


	float TiempoInicio;

	public Text Tiempo;
	int Minuto=0;
	int Segundo=0;
	float timer = 0.0f;
	bool Pausa=false;
	bool CambioMin=false;
	bool finalizado=false;
	float TiempoFrecuencia=0f;
	string CadenaTiempo="";
	int Pixeles=0;
	public GameObject Escenario;


	//Temporales

	public Text TextoBotonPausa;
	public Text Debugger;
	public GameObject TextoFinalizar;
	public GameObject PasuarFinalizar;
	public GameObject PanelPausar;

	//Luces de la escena
	public Light LuzInternaL1;
	public Light Luz1;
	public Light Luz2;

	//Camara del dispositivo
	private bool camaraDisponible;
	private WebCamTexture backCam;

	//HeartBeat
	public Text advertencia;
	public Text beatMin;
	int frameValido=0;
	filtro filtro;
	detectarPulso detectarPulso;
	int INVALID_PULSE_PERIOD=-1;
	int MIN_FRAMES_FOR_FILTER_TO_SETTLE=10;

	//HSV
	public float h;
	public float s;
	public float v;
	
	//Manejo de tiempo
	private float segundosTranscurrido=0;

	// Use this for initialization
	void Start () {
	//	
		int Temporal=0;
		//Cargar escenario
		int escenarioSeleccionado=PlayerPrefs.GetInt("escenario");
		Debug.Log("Escenario seleccionado: "+escenarioSeleccionado);
		for(int x=0;x<Variables.Var.objetos.Count;x++){
			if(Variables.Var.objetos[x].tipo==0){
				if(Temporal==escenarioSeleccionado){
					Debug.Log("Posicion donde estan: "+x);
					Debug.Log(Variables.Var.objetos[x].pathPrefab);
					GameObject objetoEscenario=Instantiate(Resources.Load(Variables.Var.objetos[x].pathPrefab) as GameObject);
					objetoEscenario.transform.SetParent(Escenario.transform, false);
				}
				Temporal++;
			}
		}


		TiempoInicio=Time.time;
		if(Variables.Var.configuracionMinutoEjercicio>1){
			Minuto=Variables.Var.configuracionMinutoEjercicio-1;
		}else{
			Minuto=Variables.Var.configuracionMinutoEjercicio-1;
		}
		
		CambioMin=false;
		TiempoFrecuencia=Minuto+1;
		PanelPausar.SetActive(false);

		//Camara
		//defaultBackground=background.texture;
		WebCamDevice[] devices=WebCamTexture.devices;
		if(devices.Length==0){
			Debug.Log("No camera found");
			camaraDisponible=false;
		}else{
			for (int i=0; i < devices.Length; i++)
			{
				if(!devices[i].isFrontFacing){
					backCam=new WebCamTexture(devices[i].name, Screen.width, Screen.height);
				}
			}
			if(backCam==null){
				Debug.Log("No camera found");
				camaraDisponible=false;
			}else{
				HelloWorldString();
				filtro=new filtro();
				detectarPulso=new detectarPulso();
				backCam.Play();
				//background.texture=backCam;
				camaraDisponible=true;
			}
			
		}
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Segundo<0 && Minuto<1){
			if(finalizado==false){
				PasuarFinalizar.SetActive(false);
				TextoFinalizar.SetActive(true);
				finalizado=true;
				/*
				FlashLightOff();
				Variables.Var.duracionSegundoEjercicio=(int)segundosTranscurrido;;
				Debug.Log("Tiempo Transcurrido: "+Variables.Var.duracionSegundoEjercicio);
				SceneManager.LoadScene("Experiencia");
				*/
				Terminar();
			}
		}else{
			if(camaraDisponible){
				//float ratio=(float)backCam.width/(float)backCam.height;
				//fit.aspectRatio=ratio;
				//float scaleY=backCam.videoVerticallyMirrored ? -1f:1f;
				//background.rectTransform.localScale=new Vector3(1f,scaleY,1f);
				//int orient=-backCam.videoRotationAngle;
				//background.rectTransform.localEulerAngles=new Vector3(0,0, orient);
				float Rojos=0f;
				float Verdes=0f;
				float Azules=0f;
				Pixeles=0;
				for (int Px=0; Px<backCam.width; Px=Px+50)
				{
					for(int Py=0; Py<backCam.height;Py=Py+50){
						Pixeles++;
						Rojos+=backCam.GetPixel(Px,Py).r;
						Verdes+=backCam.GetPixel(Px,Py).g;
						Azules+=backCam.GetPixel(Px,Py).b;
					}
				}
				Rojos/=Pixeles;
				Verdes/=Pixeles;
				Azules/=Pixeles;
				RGBaHSV(Rojos,Verdes,Azules);
				if(s>0.5 && v>0.5) {
					advertencia.text="";
					frameValido++;
					float filtered=filtro.procesarValor(h);
					if(frameValido>MIN_FRAMES_FOR_FILTER_TO_SETTLE){
						detectarPulso.addNewValue(filtered,segundosTranscurrido);
					}
				}else{
					frameValido=0;
					detectarPulso.reiniciar();
					advertencia.text="Coloque el dedo en la camara";
				}
				Debugger.text=Rojos+"-"+Verdes+"-"+Azules;
				float avePeriod=detectarPulso.getAverage(segundosTranscurrido);
    			if(avePeriod==INVALID_PULSE_PERIOD) {
				} else {
					float pulse=60.0f/avePeriod;
       				beatMin.text=""+pulse;
				}
			}
			timer += Time.deltaTime;


			
			segundosTranscurrido += Time.deltaTime;
			//Debug.Log(segundosTranscurrido);
			if(Segundo<0){
				Minuto=Minuto-1;
				CambioMin=true;
				timer=0.0f;
			}
			if(Segundo>9){
				Tiempo.text=Minuto+":"+Segundo;
				//CadenaTiempo=Minuto+":"+Segundo;
			}else{
				Tiempo.text=Minuto+":0"+Segundo;
				//CadenaTiempo=Minuto+":0"+Segundo;
			}
			Segundo = 59-System.Convert.ToInt32(timer % 60);

			//Time.time=Tiempo inicio del frame, segundos desde que inicio el juego.
			//Debug.Log(Time.time);
			float phi = Time.time / 5f * Mathf.PI; //5 segundos
        	//float amplitude = Mathf.Cos(phi) * 100.0F + 50.0F; //Jugar con estos valores para marcar el ritmo
			float amplitude = Mathf.Cos(phi) * 65.0F + 75.0F;
			//Debug.Log(amplitude);
	   	     LuzInternaL1.intensity = amplitude;

			//Funcion luz1

			float Luz1x=(Time.time-TiempoInicio)*(15f/TiempoFrecuencia);
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

			float phi2 = (Time.time-TiempoInicio) / 900 * Mathf.PI *(15f/TiempoFrecuencia);
			float Green2 = (255f-(Mathf.Cos(phi2) * 23.5f)-124.5f)/255;
			float Blue2 = (255f+(Mathf.Cos(phi2) * 127.5f)-127.5f)/255;
			//Debug.Log(Blue2);

			Luz2.color= new Color(Red2,Green2,Blue2,1f);

			//float Green2=107f+Mathf.Cos(phi) * 100.0F;
			//	float Blue2=255f

		}
	}

	public void botonPausa(){
		//if(Pausa==true){
			//Pausa=false;
			Time.timeScale = 0;
			//TextoBotonPausa.text="Pausar";
			PanelPausar.SetActive(true);
	//	}else{
	//		Time.timeScale = 0;
	//		TextoBotonPausa.text="Reanudar";
			//TextoPausa.SetActive(true);
			//Pausa=true;
	//	}
	}

	public void botonReanudar(){
		Time.timeScale = 1;
		PanelPausar.SetActive(false);
	//		TextoBotonPausa.text="Reanudar";
			//TextoPausa.SetActive(true);
			//Pausa=true;
	//	}
	}

	public void Terminar()
	{
		Time.timeScale = 1;
		//SceneManager.LoadScene("MenuPrincipal");
		FlashLightOff();
		Variables.Var.duracionSegundoEjercicio=(int)segundosTranscurrido;
		//Debug.Log("Tiempo Transcurrido: "+Variables.Var.duracionSegundoEjercicio);
		if(camaraDisponible){
			backCam.Stop();
		}
		SceneManager.LoadScene("Experiencia");
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


public void RGBaHSV(float r, float g, float b){
	float min, max, delta;
	min = System.Math.Min( r, System.Math.Min(g, b ));
    max = System.Math.Max( r, System.Math.Max(g, b ));
	v=max;
	delta = max - min;
	if( max != 0 )
        s = delta / max;
    else {
        // r = g = b = 0
        s = 0;
        h = -1;
        return;
    }
    if( r == max )
        h = ( g - b ) / delta;
    else if( g == max )
        h=2+(b-r)/delta;
    else
        h=4+(r-g)/delta;
    h = 60;
    if( h < 0 )
        h += 360;
}


#if UNITY_IPHONE
[DllImport ("__Internal")]
private static extern int _add(int x, int y);
 
// For the most part, your imports match the function defined in the iOS code, except char* is replaced with string here so you get a C# string.  
  
[DllImport ("__Internal")]
private static extern string _helloWorldString();

[DllImport ("__Internal")]
private static extern string _flashLightOff();
 
#endif
// Now make methods that you can provide the iOS functionality
 
static int Add(int x, int y)
{
    int result = 0;
    // We check for UNITY_IPHONE again so we don't try this if it isn't iOS platform.
#if UNITY_IPHONE
    // Now we check that it's actually an iOS device/simulator, not the Unity Player. You only get plugins on the actual device or iOS Simulator.
    if (Application.platform == RuntimePlatform.IPhonePlayer)
    {
        result = _add(x, y);
    }
#endif
 
    return result;
}
static string HelloWorldString()
{
    string helloWorld = "";
 
#if UNITY_IPHONE
    if (Application.platform == RuntimePlatform.IPhonePlayer)
    {
        _helloWorldString();
    }
#endif
	return helloWorld;
}

static string FlashLightOff()
{
    string helloWorld = "";
 
#if UNITY_IPHONE
    if (Application.platform == RuntimePlatform.IPhonePlayer)
    {
        _flashLightOff();
    }
#endif
	return helloWorld;
}




}
