using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
//using Area730.Notifications.IOS;

public class Variables : MonoBehaviour{
	public static Variables Var;
	public int EjercicioActual;
	//public List<string> NombreListaEjercicios;
	//public List<string> TipoListaEjercicios;
	public int Minutos=0;
	public List<ejercicio> ejercicios;
	string notificationName = "Tesis";
	//Variable donde se almacena las rachas en tiempo de ejecucion
	public string[] racha;
	//Variables donde se guarda el nivel y la experiencia en tiempo de ejecucion
	public int nivel;
	public int experiencia;

	AccessToken token;
	void Awake(){
		if(Var==null){
			DontDestroyOnLoad(gameObject);
			Var=this;
			//NombreListaEjercicios=new List<string>();
			//TipoListaEjercicios=new List<string>();
			/*
			NombreListaEjercicios.Add("Respiración Natural");
			TipoListaEjercicios.Add("Tutorial");
			NombreListaEjercicios.Add("Respiración Torácica");
			TipoListaEjercicios.Add("Basica");
			NombreListaEjercicios.Add("Respiración Abdominal");
			TipoListaEjercicios.Add("Basica");
			*/
			ejercicios=new List<ejercicio>();
			ejercicio RespiraciónNatural=new ejercicio("Respiración Natural","Tutorial","Es una técnica simple que introduce a los nuevos practicantes a la respiración Pranayama.");
			ejercicios.Add(RespiraciónNatural);
			ejercicio RespiraciónTorácica=new ejercicio("Respiración Torácica","Basica","");
			ejercicios.Add(RespiraciónTorácica);
			ejercicio RespiraciónAbdominal=new ejercicio("Respiración Abdominal","Basica","");
			ejercicios.Add(RespiraciónAbdominal);
			racha=new string[7];
			/* 
			IOSNotificationBuilder builder = IOSNotifications.GetNotificationBuilderByName(notificationName);
			if (builder != null)
			{
				IOSNotification notif = builder.build();
				IOSNotifications.scheduleNotification(notif);
		}
		*/
		}else if(Var!=this){
			Destroy(gameObject);
		}
		//if(FB.IsLoggedIn){
		//	token = AccessToken.CurrentAccessToken;
		//}
	}

	

}
