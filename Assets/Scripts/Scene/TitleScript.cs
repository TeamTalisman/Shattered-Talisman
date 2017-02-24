using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleScript : MonoBehaviour {

	GUISkin playButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPlayButtonClicked() {
		float fadeTime = gameObject.GetComponent<Fading>().BeginFade(1);
		Invoke("Play", fadeTime);
	}

	void Play() {
		SceneManager.LoadScene("Level");
	}

	void Quit() {
		Application.Quit();
	}
}
