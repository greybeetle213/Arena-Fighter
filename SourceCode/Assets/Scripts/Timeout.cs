using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeout : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float timeoutDelay;
    void Start()
    {
        Destroy(gameObject, timeoutDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
