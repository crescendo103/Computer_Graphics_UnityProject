using TMPro;
using UnityEngine;
using System.Collections;

public class Stage1Manager : MonoBehaviour
{
    public static Stage1Manager Instance { get; private set; }   // 싱글톤 인스턴스

    public Camera mainCamera;
    public GameObject AxeObject;
    public GameObject Padlock;
    public GameObject BoxCover;
    public GameObject Bottle;
    public GameObject Water;
    public GameObject DoorLock;
    public GameObject Component1;
    public GameObject Gate;

    public float moveDuration = 2f; // 이동에 걸리는 시간 (초)
    private bool haveAxe;
    private bool hasLock;
    private bool DoorPass;
    private bool flipflopLock;
    private bool havecompo1;
    private bool doorgateflipflop;
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

        haveAxe = false;
        hasLock = false;
        DoorPass = false;
        flipflopLock = false;
        havecompo1 = false;
        doorgateflipflop = false;

    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 왼쪽 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            if (mainCamera == null)
            {
                return;
            }
                
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //  targetObject 클릭한 경우
                if (hit.collider.gameObject == AxeObject)
                {
                    haveAxe=true;
                    Debug.Log($"Clicked target object: {hit.collider.name}");
                    Destroy(hit.collider.gameObject);
                    // 토글 방식으로 이미지 표시/숨김
                    //flipflop = !flipflop;
                    //targetImage.gameObject.SetActive(flipflop);
                }
                else if(hit.collider.gameObject == Padlock && haveAxe)
                {
                    Debug.Log($"Clicked target object: {hit.collider.name}");
                    // Padlock 스크립트 가져오기
                    Padlock padlockScript = hit.collider.gameObject.GetComponent<Padlock>();

                    if (padlockScript != null)
                    {
                        padlockScript.Unlock(); // 함수 호출
                    }
                    else
                    {
                        Debug.LogWarning("Padlock 스크립트를 찾을 수 없습니다");
                    }
                }
                else if (hit.collider.gameObject == BoxCover && hasLock)
                {
                    StartCoroutine(MoveBoxCover());
                }else if (hit.collider.gameObject == Bottle )//조건추가예정
                {
                    BottleCrash BottleScript = hit.collider.gameObject.GetComponent<BottleCrash>();
                    BottleScript.Crash();
                }else if(hit.collider.gameObject == Water)
                {
                    Debug.Log($"Clicked target object: {hit.collider.name}");
                    //Note noteScript = hit.collider.gameObject.GetComponent<Note>();
                    //noteScript.changeImg();
                    Note.Instance.changeImg();
                    Destroy(Water);
                }else if(hit.collider.CompareTag("password"))
                {
                    Debug.Log($"Clicked target object: compareTag password");
                    OutDoor.Instance.pressPassword(hit.collider.gameObject);
                }else if(hit.collider.gameObject == DoorLock)
                {
                    Debug.Log($"Clicked target object: doorlock");
                    if (!flipflopLock)
                    {
                        flipflopLock = true;
                        OutDoor.Instance.FocusLock();
                    }
                    else
                    {
                        flipflopLock = false;
                        OutDoor.Instance.UnFocusLock();
                    }
                }else if(hit.collider.gameObject == Component1)
                {
                    havecompo1 = true;
                    Destroy(Component1);
                }else if(hit.collider.gameObject == Gate)
                {
                    DoorGate gateScript = hit.collider.gameObject.GetComponent<DoorGate>();//스크립트 가져오기
                    if (!doorgateflipflop)
                    {
                        gateScript.Open();
                        doorgateflipflop = true;
                    }
                    else
                    {
                        gateScript.Close();
                        doorgateflipflop = false;
                    }
                }
            }
            
        }
    }
    

    public void SethasLock(bool state)
    {
        hasLock = state;
    }

    IEnumerator MoveBoxCover()
    {
        Vector3 startPos = BoxCover.transform.position;           // 시작 위치
        Vector3 endPos = startPos + new Vector3(0f, 0f, 1.8f);      // 목표 위치 (x축 +1)
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            // 0~1 사이 비율 계산
            float t = elapsed / moveDuration;

            // Lerp로 부드럽게 이동
            BoxCover.transform.position = Vector3.Lerp(startPos, endPos, t);

            elapsed += Time.deltaTime;
            yield return null; // 한 프레임 대기
        }

        // 마지막 위치를 정확히 맞추기
        BoxCover.transform.position = endPos;

        Rigidbody rb1 = BoxCover.GetComponent<Rigidbody>();
        if (rb1 != null)
            rb1.isKinematic = false; // Rigidbody 활성화
    }

    public void SetDoorPass(bool state)
    {
        DoorPass = state;
    }
}
