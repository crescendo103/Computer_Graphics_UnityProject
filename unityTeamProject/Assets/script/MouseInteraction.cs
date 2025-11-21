using UnityEngine;
using UnityEngine.UI; // UI 버튼 사용을 위해 추가

public class MouseInteraction : MonoBehaviour
{
    // 1. 인스펙터에서 연결할 변수들
    [Tooltip("확대용 상세 카메라")]
    public Camera detailCamera;

    [Tooltip("뒤로가기 버튼이 있는 UI 캔버스")]
    public GameObject zoomUICanvas;

    [Tooltip("뒤로가기 버튼")]
    public Button backButton;

    // 2. 내부에서 사용할 변수들
    private Camera _mainCamera; // 이 스크립트가 붙어있는 메인 카메라
    private RoomViewRotation _roomRotationScript; // 90도 회전 스크립트

    void Start()
    {
        // 3. 컴포넌트 자동 참조
        _mainCamera = GetComponent<Camera>();
        _roomRotationScript = GetComponent<RoomViewRotation>();

        // 4. 시작 시 비활성화 확인
        if (detailCamera != null) detailCamera.enabled = false;
        if (zoomUICanvas != null) zoomUICanvas.SetActive(false);

        // 5. 뒤로가기 버튼에 ZoomOut 함수 연결
        if (backButton != null)
        {
            backButton.onClick.AddListener(ZoomOut);
        }
    }

    void Update()
    {
        // 6. 줌인 상태(상세 카메라 활성화)가 아닐 때만 마우스 클릭 감지
        if (detailCamera != null && detailCamera.enabled)
        {
            return; // 이미 줌인 상태이므로 아무것도 안함
        }

        // 7. 마우스 왼쪽 버튼 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            // 8. Raycast 발사
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 9. Raycast가 무언가에 맞았다면
            if (Physics.Raycast(ray, out hit))
            {
                // 10. 맞은 물체의 태그가 "Interactable"인지 확인
                if (hit.collider.CompareTag("Interactable"))
                {
                    // 11. "ZoomTarget" 자식 오브젝트를 찾음
                    Transform zoomTarget = hit.transform.Find("ZoomTarget");

                    if (zoomTarget != null)
                    {
                        // 12. 줌인 함수 호출
                        ZoomIn(zoomTarget);
                    }
                    else
                    {
                        Debug.LogWarning(hit.collider.name + "에는 ZoomTarget이 없습니다.");
                    }
                }
            }
        }
    }

    // 13. 줌인 기능
    private void ZoomIn(Transform target)
    {
        // DetailCamera를 ZoomTarget의 위치와 회전으로 즉시 이동
        detailCamera.transform.position = target.position;
        detailCamera.transform.rotation = target.rotation;

        // 카메라 및 UI 활성화/비활성화
        detailCamera.enabled = true;
        zoomUICanvas.SetActive(true);

        _mainCamera.enabled = false;
        _roomRotationScript.enabled = false; // 회전 스크립트도 중지
    }

    // 14. 줌아웃 기능 (버튼이 호출할 수 있도록 public이어야 함)
    public void ZoomOut()
    {
        // 카메라 및 UI 활성화/비활성화 (ZoomIn의 반대)
        detailCamera.enabled = false;
        zoomUICanvas.SetActive(false);

        _mainCamera.enabled = true;
        _roomRotationScript.enabled = true; // 회전 스크립트 다시 활성화
    }
}