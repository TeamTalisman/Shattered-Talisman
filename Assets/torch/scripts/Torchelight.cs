using UnityEngine;
using System.Collections;

public class Torchelight : MonoBehaviour {
	
	public GameObject TorchLight;
	public GameObject MainFlame;
	public GameObject BaseFlame;
	public GameObject Etincelles;
	public GameObject Fumee;
	public float MaxLightIntensity;
	public float IntensityLight;
	

	void Start () {
		MainFlame.GetComponent<ParticleSystem>().emissionRate=IntensityLight*20f;
		BaseFlame.GetComponent<ParticleSystem>().emissionRate=IntensityLight*15f;	
		Etincelles.GetComponent<ParticleSystem>().emissionRate=IntensityLight*7f;
		Fumee.GetComponent<ParticleSystem>().emissionRate=IntensityLight*12f;
	}
	

	void Update () {
		if (IntensityLight<0) IntensityLight=0;
		if (IntensityLight>MaxLightIntensity) IntensityLight=MaxLightIntensity;		

		
		MainFlame.GetComponent<ParticleSystem>().emissionRate=IntensityLight*20f;
		BaseFlame.GetComponent<ParticleSystem>().emissionRate=IntensityLight*15f;
		Etincelles.GetComponent<ParticleSystem>().emissionRate=IntensityLight*7f;
		Fumee.GetComponent<ParticleSystem>().emissionRate=IntensityLight*12f;		

	}
}
