using Unity.VisualScripting;
using UnityEngine;

public class BottleSound : MonoBehaviour
{
    public AudioSource Sound;
    public AudioClip glassbreakclip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void CrashSound()
    {
        Sound.PlayOneShot(glassbreakclip);
    }


}
