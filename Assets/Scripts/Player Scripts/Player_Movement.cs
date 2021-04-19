using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]

public class Player_Movement : MonoBehaviour {
    [Header("Movement Values")]
    public float playerSpeed;
    public float sprintMult; /*do I want this?*/
    public float gravity;
    public float jumpHeight;
    public bool isGrounded;
    public bool isSprinting;

    [Header("Components")]
    public Transform groundCheck;
    public LayerMask groundMask;
    
    [Header("Other")]
    public GameObject master;
    public AudioClip backHit;
    public AudioClip hitHit;

    private int health = 3;

    private float groundDistance = 0.4f;

    private CharacterController controller;
    private Vector3 moveDirec = Vector3.zero;
    private Vector3 velocity;

    void Start() {
        controller = GetComponent<CharacterController>();
    }


    void Update() {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isSprinting = Input.GetButton("Sprint");

        float horizontal = Input.GetAxis("Horizontal");  
        float vertical = Input.GetAxis("Vertical");

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        moveDirec = transform.right * horizontal + transform.forward * vertical;

        velocity.y += gravity * Time.deltaTime;

        if (moveDirec != Vector3.zero && isSprinting) {
            controller.Move(moveDirec * Time.deltaTime * playerSpeed * sprintMult);
        } else if (moveDirec != Vector3.zero) {
            controller.Move(moveDirec * Time.deltaTime * playerSpeed);
        }
        
        // Done this way to replicate actual real world physics
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        controller.Move(velocity * Time.deltaTime);
    }

    public int GetHealth() {
        return health;
    }

    public void GiveDamage(int damage) {
        health -= damage;
        GetComponent<AudioSource>().PlayOneShot(hitHit);

        CheckDead();
    }

    public void CheckDead() {
        if (health <= 0) {
            if (PlayerPrefs.HasKey("HighScore")) {
                if (PlayerPrefs.GetFloat("HighScore") < master.GetComponent<Master_Script>().timer) {
                    PlayerPrefs.SetFloat("HighScore", master.GetComponent<Master_Script>().timer);
                    print("Saved");
                }
            } else {
                PlayerPrefs.SetFloat("HighScore", master.GetComponent<Master_Script>().timer);
                print("Saved");
            }
            SceneManager.LoadScene(2);
        }
    }

    public void PlayPing() {
        GetComponent<AudioSource>().PlayOneShot(backHit);
    }
}
