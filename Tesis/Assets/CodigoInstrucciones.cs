using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CodigoInstrucciones : MonoBehaviour {

	public GameObject PanelPaso;
	List<GameObject> Pasos;
	public GameObject canvas;
	public ScrollRect Scroll;
	int CantidadOpciones=4;
	bool Practica=true;
	bool Usuario=false;

	public static int NumeroEscogido=0;
	public static string NombreMenuEscogido;

	public RectTransform PuntoCentro;
	float[] Distancia;
	private bool Dragging=false;
	float DistanciaCentro;
	int DistanciaBotones=460;

	float DistanciaAnterior=0f;

	float DistanciaActual=0f;

	// Use this for initialization
	void Start () {
		Pasos = new List<GameObject> ();
		Pasos.Add (PanelPaso);
		Distancia=new float[CantidadOpciones];
		Distancia[0] = Mathf.Abs(PuntoCentro.transform.position.x - PanelPaso.transform.position.x);
		DistanciaCentro = PuntoCentro.transform.position.x - canvas.transform.position.x;
		for(int x=1;x<CantidadOpciones;x++){
			GameObject Paso = Instantiate(PanelPaso) as GameObject;
			Paso.transform.SetParent(canvas.transform, false);
			Paso.name = "Paso"+x;
			Paso.transform.position = new Vector3(PanelPaso.transform.position.x+(460f*x), PanelPaso.transform.position.y, PanelPaso.transform.position.z);
			Text[] Textos=Paso.GetComponentsInChildren<Text>();
			Textos[0].text = "Intermedio";
			Distancia[x]=Mathf.Abs(PuntoCentro.transform.position.x - Paso.transform.position.x);
			Pasos.Add (Paso);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Regresar(){
		SceneManager.LoadScene("MenuPrincipal");
	}
}
