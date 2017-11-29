using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Facebook.Unity;

public class CodigoMenuPrincipal : MonoBehaviour {
	//Diferentes ejercicios
	public GameObject PanelActividad;
	List<GameObject> Actividades;
	public GameObject canvas;
	public ScrollRect Scroll;
	int CantidadOpciones=4;
	public GameObject PanelPractica;


	//Panel De usaurio
	public GameObject PanelUsuario;
	public Button BotonPractica;
	public Button BotonUsuario;

	bool Practica=true;
	bool Usuario=false;

	public static int NumeroEscogido=0;
	public static string NombreMenuEscogido="Actividad0";

	public Sprite SpriteUsuarioActivo;
	public Sprite SpriteUsuarioInactivo;
	public Sprite SpritePracticaActivo;
	public Sprite SpritePracticaInactivo;

	public RectTransform PuntoCentro;
	float[] Distancia;
	private bool Dragging=false;
	float DistanciaCentro;
	float DistanciaBotones;

	float DistanciaAnterior=0f;

	float DistanciaActual=0f;

	bool Iniciado=false;

	public Sprite blank;


	public Text Nombre;
	public GameObject DialogProfilePic;
	public Text TextoBoton;
	public Button BotonFB;

	// Use this for initialization
	void Start () {
		RectTransform rt = (RectTransform)PanelActividad.transform;

		DistanciaBotones=rt.rect.width/1.095f;

		Debug.Log("Tamanño actual: "+DistanciaBotones);

		//Debug.Log ("x= "+canvas.transform.position.x+"\ty= "+canvas.transform.position.y);
		Actividades = new List<GameObject> ();
		Actividades.Add (PanelActividad);
		CantidadOpciones=Variables.Var.NombreListaEjercicios.Count;
		Distancia=new float[CantidadOpciones];
		Distancia[0] = Mathf.Abs(PuntoCentro.transform.position.x - PanelActividad.transform.position.x);
		DistanciaCentro = PuntoCentro.transform.position.x - canvas.transform.position.x;
		for(int x=1;x<CantidadOpciones;x++){
			GameObject Actividad = Instantiate(PanelActividad) as GameObject;
			Actividad.transform.SetParent(canvas.transform, false);
			Actividad.name = "Actividad"+x;
			//Actividad.transform.position = new Vector3(PanelActividad.transform.position.x+(684.9f*x), PanelActividad.transform.position.y, PanelActividad.transform.position.z);
			Actividad.transform.position = new Vector3(PanelActividad.transform.position.x+(DistanciaBotones*x), PanelActividad.transform.position.y, PanelActividad.transform.position.z);
			Text[] Textos=Actividad.GetComponentsInChildren<Text>();
			//Textos[0].text = "Intermedio";
			Textos[0].text = Variables.Var.TipoListaEjercicios[x];
			Textos[1].text = Variables.Var.NombreListaEjercicios[x];
			Distancia[x]=Mathf.Abs(PuntoCentro.transform.position.x - Actividad.transform.position.x);
			Actividades.Add (Actividad);
		}
		Practica=false;
		Practica_OnClick();
	}

	// Update is called once per frame
	void Update () {
		

	}

	void FixedUpdate ()
	{  
		for(int x=0;x<CantidadOpciones;x++){
			Distancia [x] = Mathf.Abs(PuntoCentro.transform.position.x - Actividades[x].transform.position.x);
		}
		float DistanciaMinima = Mathf.Min (Distancia);
		for(int x=0;x<CantidadOpciones;x++){
			if(DistanciaMinima==Distancia[x]){
				NumeroEscogido = x;
			}
		}
		if(!Dragging){
			//Debug.Log (Scroll.velocity);
			if (Mathf.Abs (Scroll.velocity.x) < 600f) {
				//Debug.Log ("Velocidad minima");
				Scroll.velocity = new Vector2 (0f, 0f);
				MoverDistancia (-(DistanciaBotones) * NumeroEscogido);
				//MoverDistancia (-(684.9f) * NumeroEscogido);
			}
		}
	}

	public void StartDrag(){
		Dragging = true;
		//dragging = 1;
		//DistanciaAnterior = 0f;
		//DistanciaActual =0f;
	}
	public void StopDrag(){
		Debug.Log ("Se detuvo");
		//Debug.Log (Scroll.velocity);
		//Debug.Log ("x= "+canvas.transform.position.x+"\ty= "+canvas.transform.position.y);
		Dragging = false;
		//dragging = 2;
		//Debug.Log (NumeroEscogido);
	}

	void MoverDistancia(float Movimiento){
		float PosicionX = Mathf.Lerp (canvas.GetComponent<RectTransform>().anchoredPosition.x,Movimiento,Time.deltaTime*20f);
		canvas.GetComponent<RectTransform>().anchoredPosition=new Vector2(PosicionX,canvas.GetComponent<RectTransform>().anchoredPosition.y);
	}

	public void Usuario_OnClick(){
		if (Usuario == false) {
			PanelUsuario.SetActive(true);
			BotonUsuario.image.overrideSprite = SpriteUsuarioActivo;
			PanelPractica.SetActive(false);
			BotonPractica.image.overrideSprite = SpritePracticaInactivo;
			Usuario = true;
			Practica = false;
		}
	}

	public void Practica_OnClick(){
		if (Practica == false) {
			PanelPractica.SetActive(true);
			BotonPractica.image.overrideSprite = SpritePracticaActivo;
			PanelUsuario.SetActive(false);
			BotonUsuario.image.overrideSprite = SpriteUsuarioInactivo;
			Practica = true;
			Usuario = false;
		}
	}

	public void TecnicaClick(){
		Variables.Var.EjercicioActual=NumeroEscogido;
		SceneManager.LoadScene("Instrucciones");
	}
		

	//FB
	public void Boton(){
		if(Iniciado==false){
			var perms = new List<string>(){"public_profile", "email", "user_friends"};
			FB.LogInWithReadPermissions(perms, AuthCallback);
			//FB.LogInWithPublishPermissions(new List<string>() { "publish_actions" }, this.AuthCallback);
			Iniciado=true;
		}else{
			Iniciado=false;
			CerrarSesion();
		}
		
	}
/* 
	private void CallFBLoginForPublish()
	{

	}
	*/
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
		IniciarSesion(FB.IsLoggedIn);
	}

	void IniciarSesion(bool IsLoggedIn){
		if(FB.IsLoggedIn){
			FB.API("/me?fields=first_name",HttpMethod.GET,MostrarNombre);
			FB.API ("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
			TextoBoton.text="Cerrar sesión";
			//ColorBlock cb = BotonFB.colors;
        	//cb.normalColor = new Color(218/255,27/255,87/255,1);
			//BotonFB.colors=cb;
			//BotonFB.GetComponent<Image>().color = new Color(218/255,27/255,87/255,1);
			//BotonFB.GetComponent<Image>().color = Color.blue;
			BotonFB.GetComponent<Image>().color = new Color(0.85f,0.11f,0.34f,1f);
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

            ProfilePic.sprite = Sprite.Create (result.Texture, new Rect (0, 0, 100, 100), new Vector2 ());

        }

    }

	void CerrarSesion(){
		FB.LogOut();
		Image ProfilePic = DialogProfilePic.GetComponent<Image> ();
		ProfilePic.sprite=blank;
		TextoBoton.text="Iniciar sesión";
		//ColorBlock cb = BotonFB.colors;
        //cb.normalColor = new Color(82/255,139/255,219/255,1);
		BotonFB.GetComponent<Image>().color = new Color(0.32f,0.55f,0.86f,1f);
		Nombre.text="Usuario";
		//BotonFB.GetComponent<Image>().color = Color.red;
		//BotonFB.colors=cb;
	}

	public void BotonShare(){
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
	private void Apicallback(IGraphResult result){
     //Parse Graph response into a specific class created for this result. 
     //parsedData = JsonUtility.FromJson<FBData>(result.RawResult);

     //Pass each field into UserInfo class. 
    // UserInfo.EMAIL = parsedData.email;
     //UserInfo.FRIENDS = parsedData.friends;
    // UserInfo.NAME = parsedData.first_name;
    // UserInfo.FACEBOOKID = parsedData.id;

    /*
	problem area, if I comment line below, then previous information is apparently not stored. If left as is then testTxt displays correct information but code gets stuck there. 
	*/
    // testTxt.text = "This is the info from USerInfoInside the APICallback: " + UserInfo.EMAIL + UserInfo.FRIENDS + UserInfo.FACEBOOKID;
 }
}