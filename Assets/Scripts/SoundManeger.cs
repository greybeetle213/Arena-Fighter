using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManeger : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioClip bulletHitSound;
    public void PlayBulletSound() {
        audioSource.PlayOneShot(bulletHitSound, 1f);
    }
}
