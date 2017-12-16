using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
//using Area730.Notifications.IOS;

public class Variables : MonoBehaviour{
	//Objeto que permite acceder a las variables de la instancia de esta clase en ejecucion.
	public static Variables Var;

	//Variable utilizada para almacenar el ejercicio que selacciono el usuario
	public int EjercicioActual;
	
	//Ejercicios disponibles en la aplicacion
	public List<ejercicio> ejercicios;

	//Objetos tridimensionales disponibles en la aplicacion.
	public List<Objetos> objetos;

	//Variable donde se almacena la informacion de tiempo escogidos por el usaurio
	public int configuracionMinutoEjercicio=0;
	public int duracionSegundoEjercicio=0;
	//Variable donde se almacena las rachas en tiempo de ejecucion
	public string[] racha;
	public int diasRacha;
	//Variables donde se guarda el nivel y la experiencia en tiempo de ejecucion
	public int nivel=0;
	public int experiencia=0;
	//Niveles subidos
	public List<int> niveles;

	public int PersonalizacionOpcion=0;//Categoria en la cual se encuentra el panel de personalizacion

	AccessToken token;
	void Awake(){
		//Si la clase no tiene una instancia creada, la crea:
		if(Var==null){
			//Indica que la instancia de la clase perdurara durante el tiempo de ejecucion.
			DontDestroyOnLoad(gameObject);
			Var=this;
			//Creacion de los ejercicios existentes en la aplicacion
			ejercicios=new List<ejercicio>();
			ejercicio RespiraciónNatural=new ejercicio("Respiración Natural","Tutorial","Es una técnica simple que introduce a los nuevos practicantes a la respiración Pranayama.");
			ejercicios.Add(RespiraciónNatural);
			ejercicio RespiraciónTorácica=new ejercicio("Respiración Torácica","Basica","");
			ejercicios.Add(RespiraciónTorácica);
			ejercicio RespiraciónAbdominal=new ejercicio("Respiración Abdominal","Basica","");
			ejercicios.Add(RespiraciónAbdominal);
			//Objetos disponibles:
			objetos=new List<Objetos>();
			Objetos objetoTemporal=new Objetos("E000",0,0);
			objetos.Add(objetoTemporal);
			objetoTemporal=new Objetos("C000",0,1);
			objetos.Add(objetoTemporal);
			objetoTemporal=new Objetos("V000",0,2);
			objetos.Add(objetoTemporal);
			objetoTemporal=new Objetos("C001",1,1);
			objetos.Add(objetoTemporal);
			objetoTemporal=new Objetos("V001",2,2);
			objetos.Add(objetoTemporal);
			objetoTemporal=new Objetos("E001",3,0);
			objetos.Add(objetoTemporal);
			objetoTemporal=new Objetos("C002",4,1);
			objetos.Add(objetoTemporal);
			objetoTemporal=new Objetos("C003",5,1);
			objetos.Add(objetoTemporal);
			//Cantidad de dias que tendra la racha
			racha=new string[7];
			niveles=new List<int>();
		}else if(Var!=this){
			//Si la clase tiene una instancia creada, la destruye:
			Destroy(gameObject);
		}
	}
	//Metodo para calcular la experiencia actual que ha ganado el usuario en el nivel actual
	public float calcularEscalaExperienciaActual(float paraExperiencia, float paraNivel){
		float retorno=0f;
		float totalexperienciaSiguienteNivel=60+180*paraNivel;
		retorno=paraExperiencia/totalexperienciaSiguienteNivel;
		return retorno;
	}
}
