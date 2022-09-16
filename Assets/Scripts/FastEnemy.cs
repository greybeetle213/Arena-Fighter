using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : MonoBehaviour {
    [SerializeField] private float HP;
    [SerializeField] private int movementSpeed;
    [SerializeField] private int turnSpeed;
    private GameObject player;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject deathParticle;
    private bool invicible = false;
    private bool charging=false;
    private void Die() {
        Instantiate(deathParticle, transform.position, transform.rotation);
        player.GetComponent<Player>().addSouls(0.4f);
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("/Player");
    }

    // Update is called once per frame
    void Update() {
        var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);

        // Smoothly rotate towards the target point.
        if (!charging) {
            if (Quaternion.Angle(transform.rotation, targetRotation) > 1f) {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            } else {
                charging = true;
                anim.SetBool("Charging", true);
                StartCoroutine(move());
            }
        }
    }
    void OnTriggerStay(Collider collision) {
        if (collision.gameObject.tag == "Lazer") {
            HP -= 6 * Time.deltaTime;
            Vector3 direction = transform.position - player.transform.position;
            direction.Normalize();
            GetComponent<Rigidbody>().velocity += direction*2;
            if (HP <= 0) {
                Die();
            }
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PlayerBullet")&&!invicible) {
            StartCoroutine(InvinicibilityFrames());
            HP -= 1;
            Vector3 direction = transform.position - player.transform.position;
            direction.Normalize();
            GetComponent<Rigidbody>().velocity += direction * 10f;
            if (HP <= 0) {
                Die();
            }
        }
    }

    IEnumerator move() {
        yield return new WaitForSeconds(1f);
        GetComponent<Rigidbody>().velocity += transform.forward * movementSpeed;
        anim.SetBool("Charging", false);
        charging = false;
    }
    IEnumerator InvinicibilityFrames() {
        invicible = true;
        yield return new WaitForSeconds(0.1f);
        invicible = false;
    }
}
