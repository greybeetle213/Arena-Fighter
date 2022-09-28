using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField] private GameObject bulletStartPos;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bloodParticle;
    [SerializeField] private GameObject lazer;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider soulBar;
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    Vector3 newSpeed;
    public float PlayerHealth;
    private float speed = 1;
    private bool up = false;
    private bool down = false;
    private bool left = false;
    private bool right = false;
    private bool shooting = false;
    private bool invicible = false;
    private int score = 0;
    public float souls = 0;
    private Vector2 lookDir = new Vector2();
    
    public void addSouls(float ammount) {
        souls += ammount;
        if (souls > 2) {
            souls = 2;
        }
        soulBar.value = souls;
    }
    public void addScore(int ammount) {
        score += ammount;
        scoreDisplay.text = score.ToString();
    }
    void Start() {

    }

    // Update is called once per frame

    void Update() {
        if (Input.GetKeyDown("z")) {
            GameObject bulletClone;
            bulletClone = Instantiate(bullet,bulletStartPos.transform.position,bulletStartPos.transform.rotation);
            Vector3 direction = bulletClone.transform.position - transform.position;
            direction.Normalize();
            bulletClone.SetActive(true);
            bulletClone.GetComponent<Rigidbody>().velocity += (direction * 20)+rb.velocity;
            rb.velocity *= 0.5f;
            StartCoroutine(BulletSlowDown());
        }
        if (Input.GetKeyDown("c")&&souls==2&&PlayerHealth!=3) {
            souls = 0;
            soulBar.value = souls;
            PlayerHealth++;
            healthBar.value = PlayerHealth;
        }
        if (shooting) {
            souls -= Time.deltaTime;
            if (souls < 0) {
                souls = 0;
                shooting = false;
                lazer.SetActive(false);
            }
            soulBar.value = souls;
        }
        if (Input.GetKeyDown("x") && souls > 0.1) {
            shooting = true;
            lazer.SetActive(true);
        }
        if (Input.GetKeyUp("x")) {
            shooting = false;
            lazer.SetActive(false);
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
        if ((up || down || left || right)) {
            lookDir = new Vector2();
            if (left) {
                lookDir.x = -1;
                if (rb.velocity.x > -10) {
                    rb.velocity += new Vector3(-0.3f, 0, 0)*speed;
                }
            }
            if (right) {
                lookDir.x = 1;
                if (rb.velocity.x < 10) {
                    rb.velocity += new Vector3(0.3f, 0, 0)*speed;
                }
            }
            if (down) {
                lookDir.y = -1;
                if (rb.velocity.z > -10) {
                    rb.velocity += new Vector3(0, 0, -0.3f)*speed;
                }
            }
            if (up) {
                lookDir.y = 1;
                if (rb.velocity.z < 10) {
                    rb.velocity += new Vector3(0, 0, 0.3f)*speed;
                }
            }
        }
        if (!shooting) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, Angle(lookDir), 0f), 10f * Time.deltaTime);
        }
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
            TakeDamage(1, collision);
        }
    }
    IEnumerator InvinicibilityFrames() {
        invicible = true;
        yield return new WaitForSeconds(0.5f);
        invicible = false;
    }
    IEnumerator BulletSlowDown() {
        speed = 0.2f;
        yield return new WaitForSeconds(0.1f);
        speed = 1;
    }

    private void TakeDamage(float damage, Collision source) {
        PlayerHealth -= damage;
        StartCoroutine(InvinicibilityFrames());
        ContactPoint contact = source.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(bloodParticle, pos, rot);
        healthBar.value -= 1;
        if (PlayerHealth <= 0) {
            SceneManager.LoadScene("StartScreen");
        }
    }
}
