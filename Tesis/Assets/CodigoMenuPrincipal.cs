using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Facebook.Unity;

public class CodigoMenuPrincipal : MonoBehaviour {

	public GameObject PanelActividad;
	List<GameObject> Actividades;
	public GameObject canvas;
	public ScrollRect Scroll;
	int CantidadOpciones=4;
	public GameObject PanelPractica;
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
	int DistanciaBotones=695;

	float DistanciaAnterior=0f;

	float DistanciaActual=0f;


	public Text Nombre;
	public GameObject DialogProfilePic;
	public Text TextoBoton;

	// Use this for initialization
	void Start () {
		
		Debug.Log ("x= "+canvas.transform.position.x+"\ty= "+canvas.transform.position.y);
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
			Actividad.transform.position = new Vector3(PanelActividad.transform.position.x+(684.9f*x), PanelActividad.transform.position.y, PanelActividad.transform.position.z);
			Text[] Textos=Actividad.GetComponentsInChildren<Text>();
			//Textos[0].text = "Intermedio";
			Textos[0].text = Variables.Var.TipoListaEjercicios[x];
			Textos[1].text = Variables.Var.NombreListaEjercicios[x];
			Distancia[x]=Mathf.Abs(PuntoCentro.transform.position.x - Actividad.transform.position.x);
			Actividades.Add (Actividad);
		}
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

				MoverDistancia (-(684.9f) * NumeroEscogido);
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
		float PosicionX = Mathf.Lerp (canvas.GetComponent<RectTransform>().anchoredPosition.x,Movimiento,Time.deltaTime*5f);
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
			TextoBoton.text="Cerrar sesión";
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
}