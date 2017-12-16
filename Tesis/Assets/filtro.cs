using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class filtro {

	float GAIN=1.894427025e+01f;
	int NZEROS=10, NPOLES=10;
	float[] xv, yv;
	public filtro(){
		xv=new float[NZEROS+1];
		yv=new float[NPOLES+1];
	}

	public float procesarValor(float value) {
		xv[0] = xv[1]; xv[1] = xv[2]; xv[2] = xv[3]; xv[3] = xv[4]; xv[4] = xv[5]; xv[5] = xv[6]; xv[6] = xv[7]; xv[7] = xv[8]; xv[8] = xv[9]; xv[9] = xv[10];
    
		xv[10] = value / GAIN;
    
		yv[0] = yv[1]; yv[1] = yv[2]; yv[2] = yv[3]; yv[3] = yv[4]; yv[4] = yv[5]; yv[5] = yv[6]; yv[6] = yv[7]; yv[7] = yv[8]; yv[8] = yv[9]; yv[9] = yv[10];
    
		yv[10] =   (xv[10] - xv[0]) + 5 * (xv[2] - xv[8]) + 10 * (xv[6] - xv[4])+ ( -0.0000000000f * yv[0]) + (  0.0357796363f * yv[1]) + ( -0.1476158522f * yv[2]) + (  0.3992561394f * yv[3]) + ( -1.1743136181f * yv[4]) + (  2.4692165842f * yv[5]) + ( -3.3820859632f * yv[6]) + (  3.9628972812f * yv[7])	+ ( -4.3832594900f * yv[8]) + (  3.2101976096f * yv[9]);
		return yv[10];
	} 

}
