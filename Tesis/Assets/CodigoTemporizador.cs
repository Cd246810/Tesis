using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CodigoTemporizador : MonoBehaviour {

	public static int NumeroEscogido=0;
	public static string NombreMenuEscogido;

	public Text NombreTecnica;
	public Button Boton5M;
	public Button Boton10M;
	public Button Boton15M;

	Color Seleccionado=new Color(0.49f,0.65f,1f,1f);
	//Color NoSeleccionado=new Color(1f,0.895f,0.6314f,1f);
	Color NoSeleccionado=new Color(1f,0.895f,0.7412f,1f);

	// Use this for initialization
	void Start () {
		NombreTecnica.text=Variables.Var.ejercicios[Variables.Var.EjercicioActual].getNombre();
		//NombreTecnica.text=Variables.Var.NombreListaEjercicios[Variables.Var.EjercicioActual];	
		Boton5M.GetComponent<Image>().color = Seleccionado;
		Variables.Var.configuracionMinutoEjercicio=1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Regresar(){
		SceneManager.LoadScene("Instrucciones");
	}
	public void Escenario5(){
		Boton5M.GetComponent<Image>().color = Seleccionado;
		Boton10M.GetComponent<Image>().color = NoSeleccionado;
		Boton15M.GetComponent<Image>().color = NoSeleccionado;
		Variables.Var.configuracionMinutoEjercicio=1;
		//SceneManager.LoadScene("Escenario1");
	}
	public void Escenario10(){
		Boton5M.GetComponent<Image>().color = NoSeleccionado;
		Boton10M.GetComponent<Image>().color = Seleccionado;
		Boton15M.GetComponent<Image>().color = NoSeleccionado;
		Variables.Var.configuracionMinutoEjercicio=3;
		//SceneManager.LoadScene("Escenario1");
	}
	public void Escenario15(){
		Boton5M.GetComponent<Image>().color = NoSeleccionado;
		Boton10M.GetComponent<Image>().color = NoSeleccionado;
		Boton15M.GetComponent<Image>().color = Seleccionado;
		Variables.Var.configuracionMinutoEjercicio=5;
		//SceneManager.LoadScene("Escenario1");
	}

	public void Iniciar(){
		SceneManager.LoadScene("Escenario1");
	}
}
