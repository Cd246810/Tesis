using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ejercicio {
	private string nombre;
	private string tipo;
	private List<string> instrucciones;
	private string descripcion;
	public ejercicio(string paraNombre, string paraTipo, string paraDescripcion){
		nombre=paraNombre;
		tipo=paraTipo;
		descripcion=paraDescripcion;
		instrucciones=new List<string>();
	}
	public string getNombre(){
		return nombre;
	}
	public string getTipo(){
		return tipo;
	}
	public string getDescripcion(){
		return descripcion;
	}
	public void agregarInstrucciones(string instruccion){
		instrucciones.Add(instruccion);
	}
	public List<string> getInstrucciones(){
		return instrucciones;
	}
}