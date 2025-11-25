using UnityEngine;
using UnityEngine.EventSystems; // 마우스 이벤트 감지용

// 마우스 Enter와 Exit 이벤트를 받겠다고 선언
public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private MainMenuController mainController;
    private RectTransform myRectTransform;

    void Start()
    {
        // 씬에서 MainMenuController를 자동으로 찾음
        mainController = FindObjectOfType<MainMenuController>();
        // 내 자신의 위치 정보를 가져옴
        myRectTransform = GetComponent<RectTransform>();
    }

    // 마우스가 버튼 영역에 들어왔을 때 자동 실행
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (mainController != null)
        {
            // 컨트롤러에게 "나(이 버튼)한테 마우스 왔어!"라고 알림
            mainController.OnButtonHoverEnter(myRectTransform);
        }
    }

    // 마우스가 버튼 영역에서 나갔을 때 자동 실행
    public void OnPointerExit(PointerEventData eventData)
    {
        if (mainController != null)
        {
            // 컨트롤러에게 "마우스 나갔어. 포인터 꺼줘."라고 알림
            mainController.OnButtonHoverExit();
        }
    }
}