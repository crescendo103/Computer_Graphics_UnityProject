using UnityEngine;
using System.Collections.Generic;

public class BottleCrash : MonoBehaviour
{

    [SerializeField]
    private Transform debrisRoot;  // Bottle_root 오브젝트
    [SerializeField]
    private List<GameObject> debrisList = new List<GameObject>();

    public GameObject bts;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Bottle_root의 모든 자식 오브젝트를 리스트에 추가
        debrisList.Clear();
        foreach (Transform child in debrisRoot)
        {
            debrisList.Add(child.gameObject);
        }
    }

   public void Crash()
    {
        BottleSound script = bts.GetComponent<BottleSound>();
        script.CrashSound();   // 함수 실행


        foreach (GameObject ob in debrisList)
        {
            Rigidbody rb = ob.GetComponent<Rigidbody>();
            rb.isKinematic = false;

            // 부모 관계 해제
            ob.transform.parent = null;

        }

        Destroy(this.gameObject);
    }

    
}
