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
    waitTimer = WAIT_TIME;
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
    } else if (waitTimer <= 0) {
      isTimerRunning = true;
      waitTimer = WAIT_TIME;
    } else {
      waitTimer -= Time.deltaTime;
    }
  }

  private void OnGUI() {
    float waitBoxX = (Screen.width / 2) - 100;
    float waitBoxY = (Screen.height / 3) - 10;
    GUI.Box(new Rect(10,10,50,20),"" + playTimer.ToString("0"));

    if (!isTimerRunning) {
      GUI.Box(new Rect(waitBoxX, waitBoxY, 200, 20),"Time to continue: " + waitTimer.ToString("0"));
    }
  }

  void KillPlayer() {
    player.GameOver();
  }
}
