using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OutDoor : MonoBehaviour
{
    public static OutDoor Instance { get; private set; }   // 싱글톤 인스턴스

    public GameObject num1;
    public GameObject num2;
    public GameObject num3;
    public GameObject num4;
    public GameObject num5;
    public GameObject num6;
    public GameObject num7;
    public GameObject num8;
    public GameObject num9;

    private List<int> Passward;
    private int index;
    private bool IsPassed;
    //-----------  
    private GameObject focusTarget;     // 이동시킬 물체
    public Vector3 focusWorldPosition = new Vector3(4.80999994f, -0.512000024f, -0.61499977f); // 월드 좌표로 이동할 위치
    public Vector3 focusScale = new Vector3(1.5f, 1.5f, 1.5f);
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private bool isFocused = false;
    //--------------
    public Image p1;
    public Image p2;
    public Image p3;
    public Image p4;
    private List<Image> PasswordImage;
    public List<Sprite> PasswordSprite;

    public AudioSource Sound; 
    public AudioClip lockdoor;
    public AudioClip falselockdoor;


    public GameObject doorgate;
    bool lastonetwo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 싱글톤 초기화
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지하고 싶으면 사용
        }
        else
        {
            Destroy(gameObject); // 중복 방지
            return;
        }

        Passward = new List<int> { 3, 4, 8, 2 };//비밀번호 리스트
        index = 0;
        IsPassed = false;
        focusTarget = this.gameObject;
        //originalPosition = new Vector3(6.80999994f, -0.512000024f, -4.79799986f);
        PasswordImage = new List<Image> { p1, p2, p3, p4 };
        lastonetwo = true;
    }

    public void pressPassword(GameObject gameObject)
    {
        ChangeImage(gameObject,index);

        if(gameObject == num3 && index == 0)
        {
            
        }
        else if(gameObject == num4 && index == 1)
        {
            
        }
        else if (gameObject == num8 && index == 2)
        {
            lastonetwo = false;
        }
        else if (gameObject == num2 && index == 3 && !lastonetwo)
        {
            IsPassed = true;
            DelLock();
        }
        else
        {
            IsPassed = false;
            lastonetwo = true;
        }

            index++;
        if (IsPassed)
        {
            index = 0;
        }
        else
        {
            if (index >= 4)
            {
                StartCoroutine(DoAfterDelay(1.0f));
                index = 0;
                Sound.PlayOneShot(falselockdoor);

            }
        }

        
    }

    private void DelLock()
    {
        Sound.PlayOneShot(lockdoor);
        focusTarget.transform.position = new Vector3(10, 0, 0); ;
        //Destroy(this);
        Stage1Manager.Instance.SetDoorPass(true);
        DoorGate script = doorgate.GetComponent<DoorGate>();
        script.ColliderOn();   // 대문의 충돌체 활성화
    }

    public void FocusLock()
    {
        
        if (focusTarget == null) return;

        // 원래 상태 저장
        originalPosition = focusTarget.transform.position;
        originalScale = focusTarget.transform.localScale;

        // 월드좌표로 즉시 이동 (회전은 그대로 유지)
        focusTarget.transform.position = focusWorldPosition;
        focusTarget.transform.localScale = focusScale;

        Debug.Log($"[FocusLock] {focusTarget.name} 이동됨 → {focusWorldPosition}, scale={focusScale}");
        
    }

    public void UnFocusLock()
    {

        if (focusTarget == null) return;

        focusTarget.transform.position = originalPosition;
        focusTarget.transform.localScale = originalScale;

        Debug.Log($"[UnFocusLock] {focusTarget.name} 원위치 복귀 → {originalPosition}, scale={originalScale}");

    }

    public void ChangeImage(GameObject gameObject,int index)
    {
        if (gameObject == num1)
        {
            PasswordImage[index].sprite = PasswordSprite[0];
        }
        else if (gameObject == num2)
        {
            PasswordImage[index].sprite = PasswordSprite[1];
        }
        else if (gameObject == num3)
        {
            PasswordImage[index].sprite = PasswordSprite[2];
        }
        else if (gameObject == num4)
        {
            PasswordImage[index].sprite = PasswordSprite[3];
        }
        else if (gameObject == num5)
        {
            PasswordImage[index].sprite = PasswordSprite[4];
        }
        else if (gameObject == num6)
        {
            PasswordImage[index].sprite = PasswordSprite[5];
        }
        else if (gameObject == num7)
        {
            PasswordImage[index].sprite = PasswordSprite[6];
        }
        else if (gameObject == num8)
        {
            PasswordImage[index].sprite = PasswordSprite[7];
        }
        else if (gameObject == num9)
        {
            PasswordImage[index].sprite = PasswordSprite[8];
        }
    }

    IEnumerator DoAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log($"{delay}초 후 실행됨!");
        PasswordImage[0].sprite = PasswordSprite[0];
        PasswordImage[1].sprite = PasswordSprite[0];
        PasswordImage[2].sprite = PasswordSprite[0];
        PasswordImage[3].sprite = PasswordSprite[0];
    }

}
