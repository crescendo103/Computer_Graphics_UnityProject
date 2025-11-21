using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class DoorGate : MonoBehaviour
{
    public AudioSource Sound;
    public AudioClip opengateclip;
    public GameObject leftdoor;
    public GameObject rightdoor;
    public Vector3 closedPosR = new Vector3(-4.67444515f, -2.13074994f, -0.747322083f);
    public Vector3 closedPosL = new Vector3(5.27334976f, -2.1307497f, -0.76432991f);
    public Vector3 openPosR = new Vector3(-9.18000031f, -2.13074994f, -0.747322083f);
    public Vector3 openPosL = new Vector3(10.1099997f, -2.1307497f, -0.76432991f);

    public AnimationCurve ease = AnimationCurve.EaseInOut(0, 0, 1, 1);
    Coroutine runningL;//중복실행을 막기 위해
    Coroutine runningR;

    private bool isopen = false;


    
    public void Open()
    {
        if (runningL != null)
        {
            StopCoroutine(runningL);
        }

        if (runningR != null)
        {
            StopCoroutine(runningR);
        }

        runningL = StartCoroutine(MoveToL(openPosL));
        runningR = StartCoroutine(MoveToR(openPosR));

        
    }

    public void Close()
    {
        if (runningL != null)
        {
            StopCoroutine(runningL);
        }

        if (runningR != null)
        {
            StopCoroutine(runningR);
        }

        runningL = StartCoroutine(MoveToL(closedPosL));
        runningR = StartCoroutine(MoveToR(closedPosR));
    }

    IEnumerator MoveToR(Vector3 target)
    {
        Sound.PlayOneShot(opengateclip);
        Vector3 start = rightdoor.transform.localPosition;
        float t = 0f;
        float duration = 1.0f;
        float dur = Mathf.Max(0.0001f, duration);
        while (t < 1f)
        {
            t += Time.deltaTime / dur;
            float e = ease.Evaluate(Mathf.Clamp01(t));
            rightdoor.transform.localPosition = Vector3.Lerp(start, target, e);
            yield return null;
        }
        rightdoor.transform.localPosition = target;
        
        runningR = null;
    }

    IEnumerator MoveToL(Vector3 target)
    {
        Sound.PlayOneShot(opengateclip);
        Vector3 start = leftdoor.transform.localPosition;
        float t = 0f;
        float duration = 1.0f;
        float dur = Mathf.Max(0.0001f, duration);
        while (t < 1f)
        {
            t += Time.deltaTime / dur;
            float e = ease.Evaluate(Mathf.Clamp01(t));
            leftdoor.transform.localPosition = Vector3.Lerp(start, target, e);
            yield return null;
        }
        leftdoor.transform.localPosition = target;
        
        runningL = null;
        if (!isopen)
        {
            isopen = true;
            SceneManager.LoadScene("Map");
        }
        else
        {
            isopen = false;
        }
            
    }

    public void ColliderOn()//대문 충돌체가 도어락 충돌체보다 커서 도어락이 풀린후 활성화 되도록 함
    {
        Debug.Log("충돌체 시작");
        GetComponent<BoxCollider>().enabled = true;
    }
}
