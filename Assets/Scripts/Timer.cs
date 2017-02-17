using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    public float timer = 600.0f;
    public PlayerController player;
    
    void Awake() {
    
    }

	// Update is called once per frame
	void Update () {

        timer -= Time.deltaTime;

        if (timer <= 0) {
            timer = 0;
            Invoke("KillPlayer", 1f);
        }

	}

    private void OnGUI() {
        GUI.Box(new Rect(10,10,50,20),"" + timer.ToString("0"));
    }

    void KillPlayer() {
        player.GameOver();
    }
}
