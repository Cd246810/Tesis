using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour{
	public static Variables Var;
	public int EjercicioActual;
	public List<string> NombreListaEjercicios;
	public List<string> TipoListaEjercicios;
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
		}else if(Var!=this){
			Destroy(gameObject);
		}
		
	}
}
