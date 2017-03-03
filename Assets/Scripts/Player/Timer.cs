using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.CharacterController;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
  GameObject player;
  public PlayerController playerController;
  protected vThirdPersonController cc; // access the ThirdPersonController component                

  public Transform LoadingBar;
  [SerializeField] private float currrentAmount;
  [SerializeField] private float speed;

  public float waitTimer = 0f;
  float playTimer = 60.0f;
  public float TIMER = 60.0f;
  public bool isTimerRunning;
  public static float WAIT_TIME = 2f;

  void Awake() {
    player = GameObject.FindGameObjectWithTag("Player");
    cc = player.GetComponent<vThirdPersonController>();
    playerController = player.GetComponent<PlayerController>();
  }
  
  void Start() {
    isTimerRunning = true;
    waitTimer = WAIT_TIME;
    playTimer = TIMER;
  }

  // Update is called once per frame
  void Update () {
    if (isTimerRunning) {
      playTimer -= Time.deltaTime;
      LoadingBar.GetComponent<Image>().fillAmount = playTimer / TIMER;
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

  private void KillPlayer() {
    playerController.GameOver();
  }
}
