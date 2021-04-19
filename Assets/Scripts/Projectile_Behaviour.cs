using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile_Behaviour : MonoBehaviour {

    public GameObject player;

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
            Destroy(gameObject);
            col.gameObject.GetComponent<Player_Movement>().GiveDamage(1);

        } else if (col.gameObject.tag == "Defense") {
            Destroy(gameObject);
            player.GetComponent<Player_Movement>().PlayPing();
        } else {
            GameObject player = GameObject.Find("Player");
            Vector3 direction = player.transform.position - transform.position;
            Direction = direction / direction.magnitude;
        }
    }
}
