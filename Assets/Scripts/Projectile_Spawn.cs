using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Spawn : MonoBehaviour {

    float timer = 0f; 

    public GameObject Object;

    void Update() {

        if (timer <= 3) { 
            timer += Time.deltaTime;
        } else {
            timer = 0f;

            float x = Random.Range(-8, 8);
            float z = Mathf.Sqrt(64 - Mathf.Pow(x, 2)); // Edited equation of a circle
            int y = Random.Range(1, 10);

            Vector3 position = new Vector3 (x, 0, z);
            position.y = y;

            Instantiate(Object, position, Quaternion.LookRotation(new Vector3(position.x, 0, position.z), Vector3.up));
        }
    }
}
