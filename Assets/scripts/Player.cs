using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField] private GameObject bloodParticle;
    [SerializeField] private GameObject lazer;
    [SerializeField] private Rigidbody rb;
    Vector3 newSpeed;
    public float PlayerHealth;
    private bool up = false;
    private bool down = false;
    private bool left = false;
    private bool right = false;
    private bool dashing = false;
    private bool shooting = false;
    private bool invicible = false;
    private Vector2 lookDir = new Vector2();
    private Vector3 dashvector;
    void Start() {

    }

    // Update is called once per frame

    void Update() {
        if (Input.GetKeyDown("z") && !dashing && !shooting) {
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
        if (Input.GetKeyDown("x") && !dashing) {
            shooting = true;
            lazer.SetActive(true);
        }
        if (Input.GetKeyUp("x")) {
            shooting = false;
            lazer.SetActive(false);
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
        if (!dashing && !shooting && (up || down || left || right)) {
            lookDir = new Vector2();
            if (left) {
                lookDir.x = -1;
                if (rb.velocity.x > -10) {
                    rb.velocity += new Vector3(-0.3f, 0, 0);
                }
            }
            if (right) {
                lookDir.x = 1;
                if (rb.velocity.x < 10) {
                    rb.velocity += new Vector3(0.3f, 0, 0);
                }
            }
            if (down) {
                lookDir.y = -1;
                if (rb.velocity.z > -10) {
                    rb.velocity += new Vector3(0, 0, -0.3f);
                }
            }
            if (up) {
                lookDir.y = 1;
                if (rb.velocity.z < 10) {
                    rb.velocity += new Vector3(0, 0, 0.3f);
                }
            }
        } else if (dashing) {
            rb.velocity = dashvector;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, Angle(lookDir), 0f), 10f * Time.deltaTime);
    }
    public static float Angle(Vector2 vector2) {
        if (vector2.x < 0) {
            return 360 - (Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg * -1);
        } else {
            return Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg;
        }
    }
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy") && !invicible) {
            PlayerHealth -= 1;
            StartCoroutine(InvinicibilityFrames());
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(bloodParticle, pos, rot);
            if (PlayerHealth <= 0) {
                SceneManager.LoadScene("Arena");
            }
        }
    }
    IEnumerator InvinicibilityFrames() {
        invicible = true;
        yield return new WaitForSeconds(0.5f);
        invicible = false;
    }
}
