using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Master_Script : MonoBehaviour {

    public Text timerText;
    public Text healthText;
    public GameObject player;
    public float timer;

    void Start() {
        timer = 0;
    }

    void Update() {
        timer += Time.deltaTime;
        timerText.text = ConvertToTime(timer);
        healthText.text = "Health Remaining: " + player.GetComponent<Player_Movement>().GetHealth();
    }

    public string ConvertToTime(float timer) {

        int hours = Mathf.FloorToInt(timer/3600);
        int minutes = Mathf.FloorToInt(timer/60);
        int seconds = Mathf.FloorToInt(timer % 60);
        int milliseconds = Mathf.FloorToInt((timer % 1) * 1000);

        return "Time Survived: " + hours + ":" + minutes + ":" + seconds + ":" + milliseconds;
    }
}
