using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreraMenuPrincipal : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		//Debug.Log ("Aqui vamos con el objeto: "+other.gameObject.name);
		CodigoMenuPrincipal.MenuEscogido = other.gameObject.name;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
