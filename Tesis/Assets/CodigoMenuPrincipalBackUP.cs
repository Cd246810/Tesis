using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Facebook.Unity;


public class CodigoMenuPrincipalBackUP : MonoBehaviour {
	//Diferentes ejercicios
	public GameObject PanelActividad;
	List<GameObject> Actividades;
	public GameObject canvas;
	public ScrollRect Scroll;
	int CantidadOpciones=4;
	public GameObject PanelPractica;

	//Panel de personalizacion
	public GameObject PanelPersonalizacion;

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

	// Objetos necesarios para representar la racha de dias que el usuario ha usado la aplicacion
	public Text[] textoRacha;
	public GameObject[] rachaPositva;
	public GameObject[] rachaNegativa;

	//Objetos necesarios para representar las estadisticas del usuario
	public RectTransform barraExperiencia;
	public Text nivel;
	public Text ejercicioFavorito;

	public Text PruebaRacha;
	string StringPruebaRacha="";

	void Start () {
		//Obtengo las fechas almacenadas de los dias en los cuales el usuario a ingresado.
		for (int x=0;x<Variables.Var.racha.Length;x++){
			//Si el dia no existe aparece como una cadena vacia.
			Variables.Var.racha[x]=PlayerPrefs.GetString("dia"+x);
		}

		//Fecha Actual en la cual el usuario inicio la sesion.
		System.DateTime ahora=System.DateTime.Now;

		//Ultima fecha registrada del usuario en formato String
		string stringUltimaSesion= Variables.Var.racha[Variables.Var.racha.Length-1];
		Debug.Log("Ultima sesion almacenada del usuario: "+ stringUltimaSesion);
		if(stringUltimaSesion==""){
			Debug.Log("No hay fecha almacenada.");
			//Como no se ha almacenado ninguna fecha previamente, se guarda el dia actual como el ultimo valor de la lista
			Variables.Var.racha[Variables.Var.racha.Length-1]=""+ahora;
		}else{
			System.DateTime ultimaSesion=System.Convert.ToDateTime(stringUltimaSesion);
			int DiasSinJugar=ahora.Subtract(ultimaSesion).Days;	
			if(DiasSinJugar!=0){
				//Si la ultima sesion fue el mismo dia que la sesion actual no hacer nada, en cambio si es un dia diferente se debe cambiar el registro de rachas
				for (int x=0;x<Variables.Var.racha.Length;x++){
					if(x+DiasSinJugar<Variables.Var.racha.Length){
						Variables.Var.racha[x]=Variables.Var.racha[x+DiasSinJugar];
					}else{
						Variables.Var.racha[x]="";
					}
				}
				Variables.Var.racha[Variables.Var.racha.Length-1]=""+ahora;
			}
		}
		
		int diasRacha=0;
		for (int x=0;x<Variables.Var.racha.Length;x++){
			//Se despliega la informacion
			if(Variables.Var.racha[x].Equals("")){
				diasRacha=0;
				rachaPositva[x].SetActive(false);
				rachaNegativa[x].SetActive(true);
			}else{
				diasRacha++;
				rachaPositva[x].SetActive(true);
				rachaNegativa[x].SetActive(false);
			}
			System.DateTime diaRacha=ahora.AddDays(-1*(Variables.Var.racha.Length-x-1));
			textoRacha[x].text=diaRacha.Day+"/"+diaRacha.Month;
			//Se guarda el arreglo en el Prefab
			StringPruebaRacha=StringPruebaRacha+" - "+Variables.Var.racha[x];
			PlayerPrefs.SetString("dia"+x,Variables.Var.racha[x]);
		}
		PruebaRacha.text=StringPruebaRacha;
		//Se obtiene el nivel y la cantidad de experiencia actual del usuario
		Variables.Var.diasRacha=diasRacha;
		//Variables.Var.nivel=PlayerPrefs.GetInt("nivel");
		//Variables.Var.experiencia=PlayerPrefs.GetInt("experiencia");


		//Obtener elementos para el panel de estadisticas
		Variables.Var.nivel=PlayerPrefs.GetInt("nivel");
		Variables.Var.experiencia=PlayerPrefs.GetInt("experiencia");
		if(Variables.Var.nivel>9){
			nivel.text="Maximo";
			barraExperiencia.localScale=new Vector3(1F,1F,1F);
		}else{
			nivel.text=""+Variables.Var.nivel;
			barraExperiencia.localScale=new Vector3(Variables.Var.calcularEscalaExperienciaActual((float)Variables.Var.experiencia, (float)Variables.Var.nivel),1F,1F);
		}
		//Debugger
		Debug.Log("Fecha de hoy: " + ahora);
		Debug.Log("Posicion de Ultima sesion almacenada del usuario: "+ (Variables.Var.racha.Length-1));
		
		System.DateTime pruebaFecha= new System.DateTime(2017,12,7,23,10,11);
		Debug.Log("Prueba de sesion: "+ pruebaFecha);
		System.TimeSpan pruebaDias=ahora.Subtract(pruebaFecha);
		Debug.Log("Dias desde prueba a ahora: "+ pruebaDias.Days);

		RectTransform rt = (RectTransform)PanelActividad.transform;

		DistanciaBotones=rt.rect.width/1.095f;

		Debug.Log("Tamanño actual: "+DistanciaBotones);

		//Debug.Log ("x= "+canvas.transform.position.x+"\ty= "+canvas.transform.position.y);
		Actividades = new List<GameObject> ();
		Actividades.Add (PanelActividad);
		CantidadOpciones=Variables.Var.ejercicios.Count;
		Distancia=new float[CantidadOpciones];
		Distancia[0] = Mathf.Abs(PuntoCentro.transform.position.x - PanelActividad.transform.position.x);
		DistanciaCentro = PuntoCentro.transform.position.x - canvas.transform.position.x;

		//---------------<<
		Text[] Textos0=PanelActividad.GetComponentsInChildren<Text>();
		Textos0[0].text=Variables.Var.ejercicios[0].getTipo();
		Textos0[1].text=Variables.Var.ejercicios[0].getNombre();
		Textos0[2].text=Variables.Var.ejercicios[0].getDescripcion();


		for(int x=1;x<CantidadOpciones;x++){
			GameObject Actividad = Instantiate(PanelActividad) as GameObject;
			Actividad.transform.SetParent(canvas.transform, false);
			Actividad.name = "Actividad"+x;
			//Actividad.transform.position = new Vector3(PanelActividad.transform.position.x+(684.9f*x), PanelActividad.transform.position.y, PanelActividad.transform.position.z);
			Actividad.transform.position = new Vector3(PanelActividad.transform.position.x+(DistanciaBotones*x), PanelActividad.transform.position.y, PanelActividad.transform.position.z);
			Text[] Textos=Actividad.GetComponentsInChildren<Text>();
			//Textos[0].text = "Intermedio";
			//Textos[0].text = Variables.Var.TipoListaEjercicios[x];
			//Textos[1].text = Variables.Var.NombreListaEjercicios[x];
			Textos[0].text=Variables.Var.ejercicios[x].getTipo();
			Textos[1].text=Variables.Var.ejercicios[x].getNombre();
			Textos[2].text=Variables.Var.ejercicios[x].getDescripcion();
			Distancia[x]=Mathf.Abs(PuntoCentro.transform.position.x - Actividad.transform.position.x);
			Actividades.Add (Actividad);
		}
		Practica=false;
		Practica_OnClick();
		if (!FB.IsInitialized) {
     	   		FB.Init(InitCallback, OnHideUnity);
  	  		} else {
    			FB.ActivateApp();
    		}
		if(FB.IsLoggedIn){
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
        	// Print current access token's User ID
        	Debug.Log("!!!!!!!!!!!!!!!"+aToken.UserId);
        	// Print current access token's granted permissions
        	foreach (string perm in aToken.Permissions) {
            	Debug.Log("!!!!!!!!!!"+perm);
        	}
			FB.API("/me?fields=first_name",HttpMethod.GET,MostrarNombre);
			FB.API ("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
			TextoBoton.text="Cerrar sesión";
			//ColorBlock cb = BotonFB.colors;
        	//cb.normalColor = new Color(218/255,27/255,87/255,1);
			//BotonFB.colors=cb;
			//BotonFB.GetComponent<Image>().color = new Color(218/255,27/255,87/255,1);
			//BotonFB.GetComponent<Image>().color = Color.blue;
			BotonFB.GetComponent<Image>().color = new Color(0.85f,0.11f,0.34f,1f);
			Iniciado=true;
		}
		OneSignal.StartInit("66f0dd94-acad-4252-a0e8-f708de54525a").HandleNotificationOpened(HandleNotificationOpened).EndInit();
  		OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;


	}

	// Update is called once per frame
	void Update () {
		

	}

	private static void HandleNotificationOpened(OSNotificationOpenedResult result) {
}


	private void InitCallback ()
	{
    	if (FB.IsInitialized) {
        	FB.ActivateApp();
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
		//if (Usuario == false) {
			PanelUsuario.SetActive(true);
			BotonUsuario.image.overrideSprite = SpriteUsuarioActivo;
			PanelPractica.SetActive(false);
			BotonPractica.image.overrideSprite = SpritePracticaInactivo;
			PanelPersonalizacion.SetActive(false);
			Usuario = true;
			Practica = false;
			if(FB.IsLoggedIn){
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
        	// Print current access token's User ID
        	Debug.Log("!!!!!!!!!!!!!!!"+aToken.UserId);
        	// Print current access token's granted permissions
        	foreach (string perm in aToken.Permissions) {
            	Debug.Log("!!!!!!!!!!"+perm);
        	}
			FB.API("/me?fields=first_name",HttpMethod.GET,MostrarNombre);
			FB.API ("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
			TextoBoton.text="Cerrar sesión";
			//ColorBlock cb = BotonFB.colors;
        	//cb.normalColor = new Color(218/255,27/255,87/255,1);
			//BotonFB.colors=cb;
			//BotonFB.GetComponent<Image>().color = new Color(218/255,27/255,87/255,1);
			//BotonFB.GetComponent<Image>().color = Color.blue;
			BotonFB.GetComponent<Image>().color = new Color(0.85f,0.11f,0.34f,1f);
			Iniciado=true;
			}
		//}
	}

	public void Practica_OnClick(){
		//if (Practica == false) {
			PanelPractica.SetActive(true);
			BotonPractica.image.overrideSprite = SpritePracticaActivo;
			PanelUsuario.SetActive(false);
			BotonUsuario.image.overrideSprite = SpriteUsuarioInactivo;
			PanelPersonalizacion.SetActive(false);
			//Practica = true;
			//Usuario = false;
		//}
	}

	public void Personalizacion_OnClick(){
		//if (Practica == false) {
			PanelPractica.SetActive(false);
			BotonPractica.image.overrideSprite = SpritePracticaInactivo;
			PanelUsuario.SetActive(false);
			BotonUsuario.image.overrideSprite = SpriteUsuarioInactivo;
			PanelPersonalizacion.SetActive(true);
			//Practica = true;
			//Usuario = false;
		//}
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
			OneSignal.SendTag("name", " "+result.ResultDictionary["first_name"]);
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
		BotonFB.GetComponent<Image>().color = new Color(0.2588f,0.4039f,0.698f,1f);
		Nombre.text="Usuario";
		//BotonFB.GetComponent<Image>().color = Color.red;
		//BotonFB.colors=cb;
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