using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour{
	public static Variables Var;
	public int EjercicioActual;
	void Awake(){
		if(Var==null){
			DontDestroyOnLoad(gameObject);
			Var=this;
		}else if(Var!=this){
			Destroy(gameObject);
		}
		
	}
}
