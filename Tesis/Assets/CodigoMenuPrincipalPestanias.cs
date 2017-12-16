using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CodigoMenuPrincipalPestanias : MonoBehaviour {

	//Panel de practica
	public GameObject PanelPractica;

	//Panel de personalizacion
	public GameObject PanelPersonalizacion;

	//Panel De usuario
	public GameObject PanelUsuario;

	//Botones para cambiar de paneles
	public GameObject BotonPractica;
	public GameObject BotonPersonalizar;
	public GameObject BotonUsuario;

	//Colores a utilizar
	Color activado=new Color(0.49f,0.651f,1f,1f);
	Color desactivado=new Color(0.631f,0.631f,0.631f,1f);

	// Use this for initialization
	void Start () {
		practicaOnClick();
	}

	public void practicaOnClick(){
		//Panel Practica
		PanelPractica.SetActive(true);
		Image[] ImagenesPractica=BotonPractica.GetComponentsInChildren<Image>();
		ImagenesPractica[1].color=activado;
		Text[] TextosPractica=BotonPractica.GetComponentsInChildren<Text>();
		TextosPractica[0].color=activado;
		//Panel Personalizacion
		PanelPersonalizacion.SetActive(false);
		Image[] ImagenesPersonalizar=BotonPersonalizar.GetComponentsInChildren<Image>();
		ImagenesPersonalizar[1].color=desactivado;
		Text[] TextosPersonalizar=BotonPersonalizar.GetComponentsInChildren<Text>();
		TextosPersonalizar[0].color=desactivado;
		//Panel Usuario
		PanelUsuario.SetActive(false);
		Image[] ImagenesUsuario=BotonUsuario.GetComponentsInChildren<Image>();
		ImagenesUsuario[1].color=desactivado;
		Text[] TextosUsuario=BotonUsuario.GetComponentsInChildren<Text>();
		TextosUsuario[0].color=desactivado;
	}
	public void personalizarOnClick(){
		//Panel Practica
		PanelPractica.SetActive(false);
		Image[] ImagenesPractica=BotonPractica.GetComponentsInChildren<Image>();
		ImagenesPractica[1].color=desactivado;
		Text[] TextosPractica=BotonPractica.GetComponentsInChildren<Text>();
		TextosPractica[0].color=desactivado;
		//Panel Personalizacion
		PanelPersonalizacion.SetActive(true);
		Image[] ImagenesPersonalizar=BotonPersonalizar.GetComponentsInChildren<Image>();
		ImagenesPersonalizar[1].color=activado;
		Text[] TextosPersonalizar=BotonPersonalizar.GetComponentsInChildren<Text>();
		TextosPersonalizar[0].color=activado;
		//Panel Usuario
		PanelUsuario.SetActive(false);
		Image[] ImagenesUsuario=BotonUsuario.GetComponentsInChildren<Image>();
		ImagenesUsuario[1].color=desactivado;
		Text[] TextosUsuario=BotonUsuario.GetComponentsInChildren<Text>();
		TextosUsuario[0].color=desactivado;
	}
	public void usuarioOnClick(){
		//Panel Practica
		PanelPractica.SetActive(false);
		Image[] ImagenesPractica=BotonPractica.GetComponentsInChildren<Image>();
		ImagenesPractica[1].color=desactivado;
		Text[] TextosPractica=BotonPractica.GetComponentsInChildren<Text>();
		TextosPractica[0].color=desactivado;
		//Panel Personalizacion
		PanelPersonalizacion.SetActive(false);
		Image[] ImagenesPersonalizar=BotonPersonalizar.GetComponentsInChildren<Image>();
		ImagenesPersonalizar[1].color=desactivado;
		Text[] TextosPersonalizar=BotonPersonalizar.GetComponentsInChildren<Text>();
		TextosPersonalizar[0].color=desactivado;
		//Panel Usuario
		PanelUsuario.SetActive(true);
		Image[] ImagenesUsuario=BotonUsuario.GetComponentsInChildren<Image>();
		ImagenesUsuario[1].color=activado;
		Text[] TextosUsuario=BotonUsuario.GetComponentsInChildren<Text>();
		TextosUsuario[0].color=activado;
	}
}
