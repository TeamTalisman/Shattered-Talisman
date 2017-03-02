using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.CharacterController;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
  public PlayerController player;
  protected vThirdPersonController cc; // access the ThirdPersonController component                



    public Transform LoadingBar;
    [SerializeField] private float currrentAmount;
    [SerializeField] private float speed;

  void Awake() {
    cc = GetComponent<vThirdPersonController>();
  }
  void Start() {
  
  }

  // Update is called once per frame
  void Update () {

        if (currrentAmount < 100 && currrentAmount >= 0)
        {
            currrentAmount += speed * Time.deltaTime;


        }

        else
        {
            Invoke("KillPlayer", 1f);
        }
        LoadingBar.GetComponent<Image>().fillAmount = currrentAmount / 100;








   
  }



    private void KillPlayer()
    {
        player.GameOver();
    }
}
