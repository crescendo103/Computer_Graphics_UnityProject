using System.Xml.Linq;
using UnityEngine;

public class Padlock : MonoBehaviour
{
    public GameObject Chain1;
    public GameObject Chain2;
    public GameObject Lock;
    public GameObject BoxCover;   
    
    public void Unlock()
    {
        Rigidbody rb1 = Chain1.GetComponent<Rigidbody>();
        if (rb1 != null)
            rb1.isKinematic = false; // Rigidbody È°¼ºÈ­

        Rigidbody rb2 = Chain2.GetComponent<Rigidbody>();
        if (rb2 != null)
            rb2.isKinematic = false;

        Rigidbody rbLock = Lock.GetComponent<Rigidbody>();
        if (rbLock != null)
            rbLock.isKinematic = false;

        Stage1Manager.Instance.SethasLock(true);
        Debug.Log("Padlock unlocked");
    }
}
