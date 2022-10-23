using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject bulletParticle;
    private SoundManeger audioManeger;

    void Start()
    {
        audioManeger = GameObject.Find("SoundManeger").GetComponent<SoundManeger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        Instantiate(bulletParticle, transform.position,transform.rotation);
        audioManeger.PlayBulletSound();
        Destroy(gameObject);
    }
}
