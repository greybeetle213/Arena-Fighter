using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject particleEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerStay(Collider collision) {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit)) {
            Instantiate(particleEffect, hit.point,new Quaternion(0,0,0,0));
        }
    }
}
