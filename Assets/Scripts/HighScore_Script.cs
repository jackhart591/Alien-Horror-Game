using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore_Script : MonoBehaviour {
    void Awake() {
        GetComponent<Text>().text = ConvertToTime(PlayerPrefs.GetFloat("HighScore"));
    }

    public string ConvertToTime(float timer) {

        int hours = Mathf.FloorToInt(timer/3600);
        int minutes = Mathf.FloorToInt(timer/60);
        int seconds = Mathf.FloorToInt(timer % 60);
        int milliseconds = Mathf.FloorToInt((timer % 1) * 1000);

        return "Time Survived: " + hours + ":" + minutes + ":" + seconds + ":" + milliseconds;
    }
}
