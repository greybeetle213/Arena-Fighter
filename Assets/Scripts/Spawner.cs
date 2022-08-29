using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField] private GameObject SpawnObject;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("c")) {
            GameObject NewEnemy;
            NewEnemy = Instantiate(SpawnObject, transform.position, transform.rotation);
            NewEnemy.SetActive(true);
        }
    }
}
