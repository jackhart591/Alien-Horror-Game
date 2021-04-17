using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player_Clap : MonoBehaviour {

    public Image tintImg;
    public AnimationCurve m_tintCurve;
    public float m_curveDuration;

    void Update() {
        if (Input.GetButtonDown("Clap")) {
            StartCoroutine(EvalCurve());
        }
    }



    IEnumerator EvalCurve() {
        float time = 0;
        while (time <= m_curveDuration) {
            float value = m_tintCurve.Evaluate(time/m_curveDuration);
            time += Time.deltaTime;

            Color tint = new Color(0, 0, 0, value);
            tintImg.color = tint;
            yield return null;
        }
    }
}
