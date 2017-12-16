using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Facebook.Unity;


public class CodigoPersonalizarBakcUp : MonoBehaviour {
	//Diferentes ejercicios
	public GameObject PanelObjeto;
	List<GameObject> Objetos;
	public GameObject canvas;
	public ScrollRect Scroll;
	int CantidadOpciones;
	//Numero de objeto seleccionado.
	public int NumeroEscogido=0;
	//Punto de referencia para centrar objetos
	public RectTransform PuntoCentro;
	//Distancia de cada panel al punto central
	float[] Distancia;
	//Variable donde se indica si el usuario esta interactuando con el scroll panel
	private bool Dragging=false;
	//Distancia entre los diferentes paneles
	float DistanciaBotones;
	//Escenario que tiene seleccionado el usuario
	int EscenarioSeleccionado;
	//nivel actual del usuario
	int nivel;

	void Start () {
		//Debug
		Variables.Var.nivel=PlayerPrefs.GetInt("nivel");
		nivel=Variables.Var.nivel;

		RectTransform rt = (RectTransform)PanelObjeto.transform;
		DistanciaBotones=rt.rect.width/1.095f;
		Debug.Log("Tamaño actual: "+DistanciaBotones);
		Objetos = new List<GameObject> ();
		Objetos.Add (PanelObjeto);
		CantidadOpciones=Variables.Var.objetos.Count;
		Distancia=new float[CantidadOpciones];
		Distancia[0] = Mathf.Abs(PuntoCentro.transform.position.x - PanelObjeto.transform.position.x);
		//---------------<<
		
		Image[] Imagenes0=PanelObjeto.GetComponentsInChildren<Image>();
		if(Variables.Var.objetos[0].nivelNecesario>nivel){
			Imagenes0[2].enabled=true;
			Imagenes0[4].enabled=false;
			Imagenes0[5].enabled=false;
		}else{
			Imagenes0[2].enabled=false;
			Imagenes0[4].enabled=true;
			Imagenes0[5].enabled=false;
		}
		Imagenes0[3].sprite=Resources.Load<Sprite>(Variables.Var.objetos[0].pathBloqueado);
		

		for(int x=1;x<CantidadOpciones;x++){
			GameObject Objeto = Instantiate(PanelObjeto) as GameObject;
			Objeto.transform.SetParent(canvas.transform, false);
			Objeto.name = "Objeto"+x;
			Objeto.transform.position = new Vector3(PanelObjeto.transform.position.x+(DistanciaBotones*x), PanelObjeto.transform.position.y, PanelObjeto.transform.position.z);
			Image[] Imagenes=Objeto.GetComponentsInChildren<Image>();
			Imagenes[3].sprite=Resources.Load<Sprite>(Variables.Var.objetos[x].pathBloqueado);
			if(Variables.Var.objetos[x].nivelNecesario>nivel){
				Imagenes[2].enabled=true;
				Imagenes[4].enabled=false;
				Imagenes[5].enabled=false;
				Debug.Log("Objeto "+x+" Esta bloqueado.");
			}else{
				Imagenes[2].enabled=false;
				Imagenes[4].enabled=true;
				Imagenes[5].enabled=false;
			}



			//if(Variables.Var.objetos.[x].){
//
//			}
			//Text[] Textos=Objeto.GetComponentsInChildren<Text>();
			//Textos[0].text=Variables.Var.ejercicios[x].getTipo();
			//Textos[1].text=Variables.Var.ejercicios[x].getNombre();
			//Textos[2].text=Variables.Var.ejercicios[x].getDescripcion();
			Distancia[x]=Mathf.Abs(PuntoCentro.transform.position.x - Objeto.transform.position.x);
			Objetos.Add (Objeto);
		}
		//Practica=false;
	}
	void FixedUpdate ()
	{  
		for(int x=0;x<CantidadOpciones;x++){
			Distancia [x] = Mathf.Abs(PuntoCentro.transform.position.x - Objetos[x].transform.position.x);
		}
		float DistanciaMinima = Mathf.Min (Distancia);
		for(int x=0;x<CantidadOpciones;x++){
			if(DistanciaMinima==Distancia[x]){
				NumeroEscogido = x;
			}
		}
		if(!Dragging){
			if (Mathf.Abs (Scroll.velocity.x) < 600f) {
				Scroll.velocity = new Vector2 (0f, 0f);
				MoverDistancia (-(DistanciaBotones) * NumeroEscogido);
			}
		}
	}

	public void StartDrag(){
		Dragging = true;
	}
	public void StopDrag(){
		Dragging = false;
	}

	void MoverDistancia(float Movimiento){
		float PosicionX = Mathf.Lerp (canvas.GetComponent<RectTransform>().anchoredPosition.x,Movimiento,Time.deltaTime*20f);
		canvas.GetComponent<RectTransform>().anchoredPosition=new Vector2(PosicionX,canvas.GetComponent<RectTransform>().anchoredPosition.y);
	}

	public void TecnicaClick(){
		Debug.Log("Click");
		for(int x=0;x<CantidadOpciones;x++){
			Image[] Imagenes=Objetos[x].GetComponentsInChildren<Image>();
			if(x==NumeroEscogido && Variables.Var.objetos[x].nivelNecesario<=nivel){
				Imagenes[5].enabled=true;
			}else{
				Imagenes[5].enabled=false;
			}
		}
		//Variables.Var.EjercicioActual=NumeroEscogido;
		//SceneManager.LoadScene("Instrucciones");
	}
}