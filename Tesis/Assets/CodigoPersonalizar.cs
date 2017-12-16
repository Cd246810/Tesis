using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Facebook.Unity;


public class CodigoPersonalizar : MonoBehaviour {
	//Panel objetos personalizacion
	public GameObject PanelEscenarios;
	public GameObject PanelObjetosA;
	public GameObject PanelObjetosB;

	//Botones diferentes categorias de personalizacion
	public GameObject BotonEscenarios;
	public GameObject BotonObjetosA;
	public GameObject BotonObjetosB;

	//Colores a utilizar
	Color activado=new Color(0.49f,0.651f,1f,1f);
	Color desactivado=new Color(0.631f,0.631f,0.631f,1f);

	void Start () {
		botonEscenariosClick();
	}
	public void botonEscenariosClick(){
		PanelEscenarios.SetActive(true);
		Image[] ImagenesEscenarios=BotonEscenarios.GetComponentsInChildren<Image>();
		ImagenesEscenarios[1].color=activado;
		Text[] TextosPractica=BotonEscenarios.GetComponentsInChildren<Text>();
		TextosPractica[0].color=activado;
		PanelObjetosA.SetActive(false);
		Image[] ImagenesObjetosA=BotonObjetosA.GetComponentsInChildren<Image>();
		ImagenesObjetosA[1].color=desactivado;
		Text[] TextosObjetosA=BotonObjetosA.GetComponentsInChildren<Text>();
		TextosObjetosA[0].color=desactivado;
		PanelObjetosB.SetActive(false);
		Image[] ImagenesObjetosB=BotonObjetosB.GetComponentsInChildren<Image>();
		ImagenesObjetosB[1].color=desactivado;
		Text[] TextosObjetosB=BotonObjetosB.GetComponentsInChildren<Text>();
		TextosObjetosB[0].color=desactivado;
	}
	public void botonObjetosAClick(){
		PanelEscenarios.SetActive(false);
		PanelObjetosA.SetActive(true);
		PanelObjetosB.SetActive(false);
	}
	public void botonObjetosBClick(){
		PanelEscenarios.SetActive(false);
		PanelObjetosA.SetActive(false);
		PanelObjetosB.SetActive(true);
	}
}