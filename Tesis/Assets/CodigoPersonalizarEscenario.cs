using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Facebook.Unity;


public class CodigoPersonalizarEscenario : MonoBehaviour {
	//Diferentes ejercicios
	public GameObject PanelObjeto;
	List<GameObject> Objetos;
	public GameObject canvas;
	public ScrollRect Scroll;
	int CantidadOpciones=0;
	//Numero de objeto seleccionado.
	public int NumeroEscogido=0;
	//Punto de referencia para centrar objetos
	public RectTransform PuntoCentro;
	//Distancia de cada panel al punto central
	float[] Distancia;
	//tipo de objetos que se seleccionaran
	int tipo=0;
	//indice de los objetos que tengan el tipo seleccionado
	int[] indiceObjetos;
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
		int escenarioSeleccionado=PlayerPrefs.GetInt("escenario");

		//variables necesarias para crear paneles de forma dinamica
		RectTransform rt = (RectTransform)PanelObjeto.transform;
		DistanciaBotones=rt.rect.width/1.095f;
		Objetos = new List<GameObject> ();

		//Se obtienen la cantidad de objetos que cumplen con el tipo de categoria seleccionada
		for(int x=0;x<Variables.Var.objetos.Count;x++){
			if(Variables.Var.objetos[x].tipo==tipo){
				CantidadOpciones++;
			}
		}
		//Se crea el arreglo de los indices donde se encuentran los objetos
		indiceObjetos=new int[CantidadOpciones];
		//Se guardan los indices de los objetos que cumplen con el tipo de categoria seleccionada
		int temporalCantidadOpciones=0;
		for(int x=0;x<Variables.Var.objetos.Count;x++){
			if(Variables.Var.objetos[x].tipo==tipo){
				indiceObjetos[temporalCantidadOpciones]=x;
				temporalCantidadOpciones++;
			}
		}
		Distancia=new float[CantidadOpciones];

		for(int x=0;x<CantidadOpciones;x++){
			GameObject Objeto;
			if(x==0){
				Objeto=PanelObjeto;
			}else{
				Objeto = Instantiate(PanelObjeto) as GameObject;
				Objeto.transform.SetParent(canvas.transform, false);
				Objeto.name = "Objeto"+x;
				Objeto.transform.position = new Vector3(PanelObjeto.transform.position.x+(DistanciaBotones*x), PanelObjeto.transform.position.y, PanelObjeto.transform.position.z);
			}
			Image[] Imagenes=Objeto.GetComponentsInChildren<Image>();
			if(Variables.Var.objetos[indiceObjetos[x]].nivelNecesario>nivel){
				Imagenes[2].enabled=true;
				Imagenes[4].enabled=false;
				Imagenes[5].enabled=false;
				Imagenes[3].sprite=Resources.Load<Sprite>(Variables.Var.objetos[indiceObjetos[x]].pathBloqueado);
			}else{
				Imagenes[2].enabled=false;
				Imagenes[4].enabled=true;
				Imagenes[5].enabled=false;
				Imagenes[3].sprite=Resources.Load<Sprite>(Variables.Var.objetos[indiceObjetos[x]].pathDesBloqueado);
			}
			if(x==escenarioSeleccionado){
				Imagenes[5].enabled=true;
			}
			Distancia[x]=Mathf.Abs(PuntoCentro.transform.position.x - Objeto.transform.position.x);
			Objetos.Add (Objeto);
		}
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

	public void ObjetoClick(){
		Debug.Log("Click Escenarios");
		if(Variables.Var.objetos[indiceObjetos[NumeroEscogido]].nivelNecesario<=nivel){
			for(int x=0;x<CantidadOpciones;x++){
				Image[] Imagenes=Objetos[x].GetComponentsInChildren<Image>();
				if(x==NumeroEscogido && Variables.Var.objetos[indiceObjetos[x]].nivelNecesario<=nivel){
					Imagenes[5].enabled=true;
					PlayerPrefs.SetInt("escenario",NumeroEscogido);
				}else{
					Imagenes[5].enabled=false;
				}
			}
		}
	}
}