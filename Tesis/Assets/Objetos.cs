using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetos {
	public string url;// URL para compartir el objeto desbloqueable en FB
	public int nivelNecesario;//Nivel necesario para utilizar el objeto
	public string pathBloqueado;//Path donde se encuentra la imagen que representa el objeto bloqueado
	public string pathDesBloqueado;//Path donde se encuentra la imagen que representa el objeto desbloqueado
	public string pathPrefab;//Path donde se almacenan los prefabs
	public int tipo; //0 Escenarios, 1 Objeto A, 2 Objeto B y 3 Puentes
	public Objetos(string paraNombreObjeto, int paraNivel, int paraTipo){
		url="http://18.216.107.179/FB/"+paraNombreObjeto+".html";
		pathBloqueado="ImagenesBloqueado/Bloqueado"+paraNombreObjeto;
		pathDesBloqueado="ImagenesDesBloqueado/DesBloqueado"+paraNombreObjeto;
		pathPrefab="PreFabs/"+paraNombreObjeto;
		nivelNecesario=paraNivel;
		tipo=paraTipo;
	}
}
