using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public static Note Instance { get; private set; }   // 싱글톤 인스턴스
    public Camera mainCamera;
    public GameObject targetObject;
    public Image targetImage; // 표시할 Image
    public Image nexttargetImage;
    private bool flipflop;

    public Vector2 normalSize = new Vector2(600, 400); // 이미지 크기
   
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

        if (targetImage != null)
            targetImage.gameObject.SetActive(false);

        if (nexttargetImage != null)
            nexttargetImage.gameObject.SetActive(false);
        flipflop = false;
        SetImageSize(targetImage, normalSize);
        SetImageSize(nexttargetImage, normalSize);
    }

    void Update()
    {
        // 마우스 왼쪽 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //  targetObject 클릭한 경우
                if (hit.collider.gameObject == targetObject)
                {
                    Debug.Log($"Clicked target object: {hit.collider.name}");

                    // 토글 방식으로 이미지 표시/숨김
                    flipflop = !flipflop;
                    targetImage.gameObject.SetActive(flipflop);

                    

                }
                else
                {
                    // 다른 오브젝트 클릭 시 이미지 숨기기
                    if (flipflop)
                    {
                        flipflop = false;
                        targetImage.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                //빈 공간 클릭 시 이미지 숨기기
                if (flipflop)
                {
                    flipflop = false;
                    targetImage.gameObject.SetActive(false);
                }
            }
        }
    }

    public void changeImg()
    {
        targetImage = nexttargetImage;
    }
    private void SetImageSize(Image img, Vector2 size)
    {
        if (img != null)
        {
            RectTransform rect = img.GetComponent<RectTransform>();
            rect.sizeDelta = size;
        }
    }
}
