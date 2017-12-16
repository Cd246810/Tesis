using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CodigoExperiencia : MonoBehaviour {

	//Elementos de la barra de experiencia
	public RectTransform objetoExperienciaActual;
	public RectTransform objetoExperienciaGanada;


	//Eliminar despues prueba

	public Text Nivel;

	float escalaExperienciaGanada;
	float temporalEscalaExperienciaGanada;

	int inicioAnimacion=10;
	int nivelInicial=0;

	// Use this for initialization
	void Start () {
		//Debug
		/* */
		
		Variables.Var.nivel=PlayerPrefs.GetInt("nivel");
		Variables.Var.experiencia=PlayerPrefs.GetInt("experiencia");
		nivelInicial=Variables.Var.nivel;
		if(nivelInicial<10){
			Nivel.text=""+nivelInicial;
			objetoExperienciaActual.localScale=new Vector3(calcularEscalaExperienciaActual(),1F,1F);
			objetoExperienciaGanada.localScale=new Vector3(calcularEscalaExperienciaActual(),1F,1F);
			escalaExperienciaGanada=calcularEscalaExperienciaGanada();
			temporalEscalaExperienciaGanada=calcularEscalaExperienciaActual();
			//Debug.Log("Nivel: "+Variables.Var.nivel);
			//Debug.Log("Experiencia: "+Variables.Var.experiencia);
			guardarExperiencia();
		}else{
			objetoExperienciaActual.localScale=new Vector3(1F,1F,1F);
			objetoExperienciaGanada.localScale=new Vector3(1F,1F,1F);
			Nivel.text="Maximo";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if(inicioAnimacion<20){
			inicioAnimacion++;
		}else{
			if(temporalEscalaExperienciaGanada<escalaExperienciaGanada && nivelInicial<10){
				temporalEscalaExperienciaGanada+=0.01f;
				if(temporalEscalaExperienciaGanada>=1f){
					//Inidica que el jugador subio de nivel
					nivelInicial=nivelInicial+1;
					Nivel.text=""+nivelInicial;
					inicioAnimacion=0;
					temporalEscalaExperienciaGanada=temporalEscalaExperienciaGanada-1f;
					escalaExperienciaGanada=escalaExperienciaGanada-1f;
					objetoExperienciaActual.localScale=new Vector3(0F,1F,1F);
					objetoExperienciaGanada.localScale=new Vector3(0F,1F,1F);
				}else{
					objetoExperienciaGanada.localScale=new Vector3(temporalEscalaExperienciaGanada,1F,1F);
				}
			}
			//temporalEscalaExperienciaGanada+=0.01f;
		}
	}

	public void guardarExperiencia(){
		int experiencia=Variables.Var.experiencia;
		int totalexperienciaSiguienteNivel=60+180*Variables.Var.nivel;
		float aumentoRacha=1f+((Variables.Var.diasRacha-1)/10);
		float totalExperiencia=experiencia+Variables.Var.duracionSegundoEjercicio*aumentoRacha;
		while(totalexperienciaSiguienteNivel<=totalExperiencia){
			totalExperiencia=totalExperiencia-totalexperienciaSiguienteNivel;
			//Variables.Var.nivel=Variables.Var.nivel+1;
			subirNivel();
			totalexperienciaSiguienteNivel=60+180*Variables.Var.nivel;
		}
		Variables.Var.experiencia=(int)totalExperiencia;
		PlayerPrefs.SetInt("nivel",Variables.Var.nivel);
		PlayerPrefs.SetInt("experiencia",Variables.Var.experiencia);
	}

	public float calcularEscalaExperienciaActual(){
		float retorno=0.2f;
		float experiencia=Variables.Var.experiencia;
		Debug.Log("Experiencia: "+Variables.Var.experiencia);
		float totalexperienciaSiguienteNivel=60f+180f*Variables.Var.nivel;
		Debug.Log("Total de nivel: "+totalexperienciaSiguienteNivel);
		retorno=experiencia/totalexperienciaSiguienteNivel;
		Debug.Log("Escala de experiencia actual: "+Variables.Var.experiencia/totalexperienciaSiguienteNivel);
		Debug.Log("Escala experiencia actual: "+retorno);
		return retorno;
	}
	/*
	public float calcularEscalaExperienciaGanada(){
		float retorno=0.6f;
		float aumentoRacha=1f+(Variables.Var.diasRacha-1)/10;
		float segundosAumentado=Variables.Var.duracionSegundoEjercicio*aumentoRacha;
		float totalexperienciaSiguienteNivel=60f+180f*Variables.Var.nivel;
		retorno=segundosAumentado/totalexperienciaSiguienteNivel;
		//Debug
		Debug.Log("Escala experiencia ganada: "+retorno);
		return retorno;
	}
	*/
	public float calcularEscalaExperienciaGanada(){
		float retorno=0f;
		int experiencia=Variables.Var.experiencia;
		int nivelActual=Variables.Var.nivel;
		int totalexperienciaSiguienteNivel=60+180*nivelActual;
		float aumentoRacha=1f+((Variables.Var.diasRacha-1)/10);
		float totalExperiencia=experiencia+(Variables.Var.duracionSegundoEjercicio*aumentoRacha);
		while(totalexperienciaSiguienteNivel<=totalExperiencia){
			totalExperiencia=totalExperiencia-totalexperienciaSiguienteNivel;
			retorno=retorno+1f;
			nivelActual=nivelActual+1;
			totalexperienciaSiguienteNivel=60+180*nivelActual;
		}
		retorno=retorno+totalExperiencia/totalexperienciaSiguienteNivel;
		return retorno;
	}

	public void subirNivel(){
		if (Variables.Var.nivel<10){
			Variables.Var.nivel=Variables.Var.nivel+1;
			Debug.Log("Nivel: "+Variables.Var.nivel);
			Variables.Var.niveles.Add(Variables.Var.nivel);
			PlayerPrefs.SetInt("nivel",Variables.Var.nivel);
		}
	}
	public void nivel0(){
		Variables.Var.nivel=0;
		Debug.Log("Nivel: "+Variables.Var.nivel);
		PlayerPrefs.SetInt("nivel",Variables.Var.nivel);
		PlayerPrefs.SetInt("ObjetoA",0);
		PlayerPrefs.SetInt("ObjetoB",0);
		PlayerPrefs.SetInt("experiencia",0);
		PlayerPrefs.SetInt("escenario",0);
	}

	public void Continuar()
	{
		if(Variables.Var.niveles.Count>0){
			SceneManager.LoadScene("Desbloqueable");
		}else{
			SceneManager.LoadScene("MenuPrincipal");
		}
	}
}
