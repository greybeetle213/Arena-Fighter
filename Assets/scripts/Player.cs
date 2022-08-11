using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody rb;
    Vector3 newSpeed;
    private bool up = false;
    private bool down = false; 
    private bool left = false;
    private bool right = false;
    private bool dashing = false;
    private Vector3 dashvector;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z") && !dashing) {
            dashing = true;
            dashvector = new Vector3(0, 0, 0);
            if (left) {
                dashvector += new Vector3(-25, 0, 0);
            }
            if (right) {
                dashvector += new Vector3(25, 0, 0);
            }
            if (up) {
                dashvector += new Vector3(0, 0, 25);
            }
            if (down) {
                dashvector += new Vector3(0, 0, -25);
            }
        }
        if (Input.GetKeyUp("z")) {
            dashing = false;
        }
        if (Input.GetKeyUp("z")) {
            dashing = false;
        }
        if (Input.GetKeyDown("left")) {
            left = true;
        }
        if (Input.GetKeyUp("left")) {
            left = false;
        }
        if (Input.GetKeyDown("right")) {
            right = true;
        }
        if (Input.GetKeyUp("right")) {
            right = false;
        }
        if (Input.GetKeyDown("up")) {
            up = true;
        }
        if (Input.GetKeyUp("up")) {
            up = false;
        }
        if (Input.GetKeyDown("down")) {
            down = true;
        }
        if (Input.GetKeyUp("down")) {
            down = false;
        }
        if (!dashing) {
            if (left) {
                if (rb.velocity.x > -10) {
                    rb.velocity += new Vector3(-0.3f, 0, 0);
                }
            }
            if (right) {
                if (rb.velocity.x < 10) {
                    rb.velocity += new Vector3(0.3f, 0, 0);
                }
            }
            if (down) {
                if (rb.velocity.z > -10) {
                    rb.velocity += new Vector3(0, 0, -0.3f);
                }
            }
            if (up) {
                if (rb.velocity.z < 10) {
                    rb.velocity += new Vector3(0, 0, 0.3f);
                }
            }
        } else {
            rb.velocity = dashvector;
        }
    }
}
