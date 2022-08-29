using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEnemy : MonoBehaviour {
    [SerializeField] private float HP;
    [SerializeField] private int movementSpeed;
    [SerializeField] private int turnSpeed;
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);

        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }
    void OnTriggerStay(Collider collision) {
        if (collision.gameObject.tag == "MurderImplement") {
            HP -= 1 * Time.deltaTime;
            Vector3 direction = transform.position - player.transform.position;
            direction.Normalize();
            GetComponent<Rigidbody>().velocity += direction*2;
            if (HP <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
