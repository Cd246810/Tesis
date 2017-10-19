using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodigoMenuPrincipal : MonoBehaviour {

	public GameObject PanelActividad;
	List<GameObject> Actividades;
	public GameObject canvas;
	int Actual=0;
	int CantidadOpciones=4;

	public static string MenuEscogido="Actividad0";

	private Vector2 startPos;
	private Vector2 endPos;
	private Vector2 prevPos;
	private Vector2 touchPos;
	private Rigidbody rb;
	private Touch touch;
	private int Estado; //0 en posicion, 1 se esta moviendo, 2 regresar a posicion

	// Use this for initialization
	void Start () {

		Debug.Log ("x= "+PanelActividad.transform.position.x+"\ty= "+PanelActividad.transform.position.y);
		Actividades = new List<GameObject> ();
		Actividades.Add (PanelActividad);
		for(int x=1;x<CantidadOpciones;x++){
			GameObject Actividad = Instantiate(PanelActividad) as GameObject;
			Actividad.transform.SetParent(canvas.transform, false);
			Actividad.name = "Actividad"+x;
			Actividad.transform.position = new Vector3(PanelActividad.transform.position.x+(694.9f*x), PanelActividad.transform.position.y, PanelActividad.transform.position.z);
			Text[] Textos=Actividad.GetComponentsInChildren<Text>();
			Textos[0].text = "Intermedio";
			Actividades.Add (Actividad);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate ()
	{  
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Began)
			{
				startPos = touch.position;
				prevPos =touch.position;
				Estado = 1;
			}
			//Ver velocidad tambien :/
			if (touch.phase == TouchPhase.Moved && touch.phase != TouchPhase.Stationary)
			{
				endPos=touch.position;
				//if(Mathf.Abs(endPos.x)>(Mathf.Abs(startPos.x)+30) || Mathf.Abs(endPos.x)<(Mathf.Abs(startPos.x) - 30)){//Arreglar esto
					MoverDistancia (endPos.x-prevPos.x);
				//}
				//MoverDistancia (endPos.x-prevPos.x);
				prevPos = touch.position;
			}
			if (touch.phase == TouchPhase.Ended) {
				if (startPos == endPos) { //Darle +-20 pixeles
					Variables.EjercicioActual = MenuEscogido;
					Debug.Log ("Se selecciono la actividad: " + Variables.EjercicioActual);
					//Estado = 0;
				} else {
					//Estado = 2;
				}
				Estado = 2;
			}
		}
		if(Estado==2){
			float Mover=Offset (MenuEscogido);
			if (Mover < 0.2f) {
				MoverLocacion (-30);
			} else if (Mover > 81.2) {
				MoverLocacion (30);
			} else {
				MoverLocacion (Mover);
				Estado = 0;
			}

			//MoverLocacion (Mover);
			//Estado = 0;
		}
	}

	void MoverDistancia(float Movimiento){
		for(int x=0;x<CantidadOpciones;x++){
			Actividades[x].transform.position = new Vector3(Actividades[x].transform.position.x+Movimiento, Actividades[x].transform.position.y, Actividades[x].transform.position.z);
		}
	}

	float Offset(string Locacion){
		float Mover = 0f;
		for (int x = 0; x < CantidadOpciones; x++) {
			if(Actividades[x].name.Equals(Locacion)){
				Mover = Actividades [x].transform.position.x - 41.2f - Mover;
			}
		}
		return Mover;
	}

	void MoverLocacion(float Mover){
		
		for(int x=0;x<CantidadOpciones;x++){
			Actividades[x].transform.position = new Vector3(Actividades[x].transform.position.x-Mover, Actividades[x].transform.position.y, Actividades[x].transform.position.z);
		}
	}
		
}