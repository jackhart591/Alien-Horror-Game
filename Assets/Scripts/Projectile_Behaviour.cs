using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Behaviour : MonoBehaviour {

    Vector3 Direction;
    float speed;

    void Start() {
        Direction = Vector3.forward;
        speed = Random.Range(20f, 35f);
    }

    void Update() {
        transform.Translate(Direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Player") {
            Destroy(gameObject); // add health stuff here
        } else if (col.gameObject.tag == "Defense") {
            Destroy(gameObject);
        } else {
            GameObject player = GameObject.Find("Player");
            Vector3 direction = player.transform.position - transform.position;

            Direction = direction.normalized;
        }
    }
}
