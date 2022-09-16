using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject FastEnemy;

    public int wave = 1;
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(SpawnEnemys());
    }
    IEnumerator SpawnEnemys() {
        while (true) {
            var SpawnQuantity = wave+4;
            for (int i = 0; i < Random.Range(0, SpawnQuantity / 2); i++) {
                GameObject NewFastEnemy;
                NewFastEnemy = Instantiate(FastEnemy, transform.position, transform.rotation);
                SpawnQuantity--;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < SpawnQuantity; i++) {
                GameObject NewEnemy;
                NewEnemy = Instantiate(Enemy, transform.position, transform.rotation);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(10);
            wave++;
        }
    }
}
