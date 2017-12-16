using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectarPulso {
	float MAX_PERIOD=1.5f;
	float MIN_PERIOD=0.1f;
	float INVALID_ENTRY=-100f;
	int MAX_PERIODS_TO_STORE=20;
	int AVERAGE_SIZE=20;
	int INVALID_PULSE_PERIOD=-1;

	float[] upVals;
	float[] downVals;
	int upValIndex;
	int downValIndex;
	
	float lastVal;
	float periodStart;
	float[] periods;
	float[] periodTimes;
	
	int periodIndex;
	bool started;
	float freq;
	float average;
	
	bool wasDown;
	
	public detectarPulso(){
		upVals= new float[AVERAGE_SIZE];
		downVals=new float[AVERAGE_SIZE];
		periods=new float[MAX_PERIODS_TO_STORE];
		periodTimes=new float[MAX_PERIODS_TO_STORE];
		reiniciar();
	}

	public void reiniciar() {
		for(int i=0; i<MAX_PERIODS_TO_STORE; i++) {
			periods[i]=INVALID_ENTRY;
		}
		for(int i=0; i<AVERAGE_SIZE; i++) {
			upVals[i]=INVALID_ENTRY;
			downVals[i]=INVALID_ENTRY;
		}
    	freq=0.5f;
		periodIndex=0;
		downValIndex=0;
		upValIndex=0;    
	}

	public float addNewValue(float newVal, float time) {	
  // we keep track of the number of values above and below zero
    
    if(newVal>0) {
		upVals[upValIndex]=newVal;
		upValIndex++;
		if(upValIndex>=AVERAGE_SIZE) {
			upValIndex=0;
		}
	}
    
    
	if(newVal<0) {
		downVals[downValIndex]=-newVal;
		downValIndex++;
		if(downValIndex>=AVERAGE_SIZE) {
			downValIndex=0;
		}		
	}
  // work out the average value above zero
	float count=0;
	float total=0;
	for(int i=0; i<AVERAGE_SIZE; i++) {
		if(upVals[i]!=INVALID_ENTRY) {
			count++;
			total+=upVals[i];
		}
	}
    
	float averageUp=total/count;
  // and the average value below zero
	count=0;
	total=0;
	for(int i=0; i<AVERAGE_SIZE; i++) {
		if(downVals[i]!=INVALID_ENTRY) {
			count++;
			total+=downVals[i];
		}
	}
	float averageDown=total/count;

  // is the new value a down value?
	if(newVal<-0.5*averageDown) {
		wasDown=true;
	}
	
  // is the new value an up value and were we previously in the down state?
	if(newVal>=0.5*averageUp && wasDown) {
		wasDown=false;
    // work out the difference between now and the last time this happenned
		if(time-periodStart<MAX_PERIOD && time-periodStart>MIN_PERIOD) {
			
            periods[periodIndex]=time-periodStart;
            periodTimes[periodIndex]=time;
			periodIndex++;
			if(periodIndex>=MAX_PERIODS_TO_STORE) {
				periodIndex=0;
			}
            
		}
    // track when the transition happened
		periodStart=time;
	} 
	// return up or down
	if(newVal<-0.5*averageDown) {
		return -1;
	} else if(newVal>0.5*averageUp) {
		return 1;
	}
	return 0;
}

public float getAverage(float time) {
    
	//double time=CACurrentMediaTime();
	float total=0;
	float count=0;
	for(int i=0; i<MAX_PERIODS_TO_STORE; i++) {
    // only use upto 10 seconds worth of data
		if(periods[i]!=INVALID_ENTRY  && time-periodTimes[i]<10) {
			count++;
			total+=periods[i];
 		}
	}
  // do we have enough values?
	if(count>2) {
		return total/count;
	}
	return INVALID_PULSE_PERIOD;
}


}
