using UnityEngine;

public class SwordManager : MonoBehaviour
{
    [Header("Hidden Swords")]
    public GameObject hiddenSwordRed;
    public GameObject hiddenSwordGreen;
    public GameObject hiddenSwordBlue;

    [Header("Final Reward")]
    public GameObject flashBox; // 3개가 다 모이면 나타날 박스

    // 퍼즐 상태를 저장할 변수들
    private bool isRedActive = false;
    private bool isGreenActive = false;
    private bool isBlueActive = false;

    public void ShowRedSword()
    {
        if (hiddenSwordRed != null)
        {
            hiddenSwordRed.SetActive(true);
            isRedActive = true; // 빨강 켜짐 체크
            CheckAllSwordsActive(); // 다 켜졌는지 확인
        }
    }

    public void ShowGreenSword()
    {
        if (hiddenSwordGreen != null)
        {
            hiddenSwordGreen.SetActive(true);
            isGreenActive = true; // 초록 켜짐 체크
            CheckAllSwordsActive(); // 다 켜졌는지 확인
        }
    }

    public void ShowBlueSword()
    {
        if (hiddenSwordBlue != null)
        {
            hiddenSwordBlue.SetActive(true);
            isBlueActive = true; // 파랑 켜짐 체크
            CheckAllSwordsActive(); // 다 켜졌는지 확인
        }
    }

    // 3개가 모두 켜졌는지 검사하는 함수
    private void CheckAllSwordsActive()
    {
        if (isRedActive && isGreenActive && isBlueActive)
        {
            Debug.Log("Puzzle Solved! Showing FlashBox.");

            if (flashBox != null)
            {
                flashBox.SetActive(true);
            }
        }
    }
}