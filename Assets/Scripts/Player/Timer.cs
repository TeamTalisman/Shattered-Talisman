using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.CharacterController;

public class Timer : MonoBehaviour {
  public PlayerController player;
  protected vThirdPersonController cc; // access the ThirdPersonController component                

  public float waitTimer = 0f;
  public float playTimer = 60.0f;
  public bool isTimerRunning;
  public static float WAIT_TIME = 2f;

  void Awake() {
    cc = GetComponent<vThirdPersonController>();
  }
  void Start() {
    isTimerRunning = true;
  }

  // Update is called once per frame
  void Update () {

    if (isTimerRunning) {
      playTimer -= Time.deltaTime;
      
      // If player movement if locked
      if (cc.lockMovement) {
        // unlock it
        cc.lockMovement = false;
      }
      if (playTimer <= 0) {
          playTimer = 0;
          Invoke("KillPlayer", 1f);
      }
    } else if (waitTimer >= WAIT_TIME ) {
      isTimerRunning = true;
      waitTimer = 0f;
    } else {
      waitTimer += Time.deltaTime;
    }
  }

  private void OnGUI() {
    GUI.Box(new Rect(10,10,50,20),"" + playTimer.ToString("0"));
  }

  void KillPlayer() {
    player.GameOver();
  }
}
