using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class Variables : MonoBehaviour{
	public static Variables Var;
	public int EjercicioActual;
	public List<string> NombreListaEjercicios;
	public List<string> TipoListaEjercicios;
	public int Minutos=0;
	void Awake(){
		if(Var==null){
			DontDestroyOnLoad(gameObject);
			Var=this;
			NombreListaEjercicios=new List<string>();
			TipoListaEjercicios=new List<string>();
			NombreListaEjercicios.Add("Respiración Natural");
			TipoListaEjercicios.Add("Tutorial");
			NombreListaEjercicios.Add("Respiración Torácica");
			TipoListaEjercicios.Add("Basica");
			NombreListaEjercicios.Add("Respiración Abdominal");
			TipoListaEjercicios.Add("Basica");
			if (!FB.IsInitialized) {
     	   		FB.Init(InitCallback, OnHideUnity);
  	  		} else {
    			FB.ActivateApp();
    		}
		}else if(Var!=this){
			Destroy(gameObject);
		}
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

}
