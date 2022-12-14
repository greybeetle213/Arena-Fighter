using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEnemy : MonoBehaviour {
    [SerializeField] private float HP;
    [SerializeField] private int movementSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float knockBack;
    [SerializeField] private int score;
    private GameObject player;
    [SerializeField] private GameObject deathParticle;
    private bool invicible = false;
    private void Die() {
        Instantiate(deathParticle, transform.position, transform.rotation);
        player.GetComponent<Player>().addSouls(0.2f);
        player.GetComponent<Player>().addScore(score);
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
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }
    void OnTriggerStay(Collider collision) {
        if (collision.gameObject.tag == "Lazer") {
            HP -= 6 * Time.deltaTime;
            Vector3 direction = transform.position - player.transform.position;
            direction.Normalize();
            GetComponent<Rigidbody>().velocity += direction*4*knockBack;
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
            GetComponent<Rigidbody>().velocity += direction * 10f * knockBack;
            if (HP <= 0) {
                Die();
            }
        }
    }

    IEnumerator InvinicibilityFrames() {
        invicible = true;
        yield return new WaitForSeconds(0.1f);
        invicible = false;
    }
}
