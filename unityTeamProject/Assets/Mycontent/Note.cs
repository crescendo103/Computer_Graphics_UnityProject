using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject targetObject;
    public Image targetImage; // 표시할 Image
    private bool flipflop;

    void Start()
    {
        if (targetImage != null)
            targetImage.gameObject.SetActive(false);
        flipflop = false;
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
}
