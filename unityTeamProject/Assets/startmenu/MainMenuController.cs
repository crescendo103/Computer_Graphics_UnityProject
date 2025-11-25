using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // UI 관련 기능

public class MainMenuController : MonoBehaviour
{
    [Header("씬 설정")]
    public string gameSceneName = "MainGame"; // 이동할 게임 씬 이름

    [Header("포인터 설정")]
    public RectTransform pointerIcon; // 포인터(삼각형) 이미지
    public float pointerXOffset = -20f; // 버튼 글자로부터 얼마나 왼쪽에 떨어질지

    // ================= 기능 버튼 클릭 =================
    public void OnClickPlay()
    {
        // 씬 이동
        if (!string.IsNullOrEmpty(gameSceneName))
        {
            SceneManager.LoadScene(gameSceneName);
        }
        else
        {
            Debug.LogError("이동할 씬 이름이 설정되지 않았습니다.");
        }
    }

    public void OnClickExit()
    {
        Debug.Log("게임 종료");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    // ================= 마우스 호버 처리 =================

    // 버튼에 마우스가 올라왔을 때 호출될 함수
    public void OnButtonHoverEnter(RectTransform buttonRect)
    {
        if (pointerIcon == null) return;

        // 포인터 켜기
        pointerIcon.gameObject.SetActive(true);

        // 포인터의 위치를 버튼의 Y축 높이와 맞춤
        // X축은 버튼의 왼쪽 끝(anchoredPosition.x)에서 설정한 오프셋만큼 더 왼쪽으로 이동
        // (버튼들의 중심축(Pivot)이 가운데(0.5, 0.5)라고 가정했을 때의 계산입니다)
        float targetX = buttonRect.anchoredPosition.x - (buttonRect.rect.width / 2) + pointerXOffset;
        float targetY = buttonRect.anchoredPosition.y;

        pointerIcon.anchoredPosition = new Vector2(targetX, targetY);
    }

    // 버튼에서 마우스가 나갔을 때 호출될 함수
    public void OnButtonHoverExit()
    {
        if (pointerIcon == null) return;

        // 포인터 끄기
        pointerIcon.gameObject.SetActive(false);
    }
}