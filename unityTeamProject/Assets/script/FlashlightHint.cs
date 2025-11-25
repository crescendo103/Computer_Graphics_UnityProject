using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlashlightHint : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private ItemData flashlightItem; // 손전등 아이템 데이터
    [SerializeField] private string hintMessage;      // 보여줄 숫자 (예: 5)
    [SerializeField] private float duration = 3.0f;   // 텍스트가 떠있는 시간

    [Header("UI Reference")]
    [SerializeField] private Text uiText; // 화면 중앙에 있는 UI 텍스트 오브젝트

    private Coroutine currentCoroutine;

    // 컨트롤러가 호출할 함수
    public void TryShowHint(Inventory inventory)
    {
        if (inventory == null) return;

        // 1. 현재 들고 있는 아이템 확인
        ItemData currentItem = inventory.GetSelectedItem();

        // 2. 손전등인지 확인
        if (currentItem == flashlightItem)
        {
            ShowText();
        }
    }

    private void ShowText()
    {
        if (uiText == null) return;

        // 실행 중인 코루틴이 있다면 취소 (연타 방지)
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);

        // 텍스트 내용 설정 및 활성화
        uiText.text = hintMessage;
        uiText.gameObject.SetActive(true);

        // n초 뒤에 끄기 시작
        currentCoroutine = StartCoroutine(HideTextRoutine());

        Debug.Log("Hint Show: " + hintMessage);
    }

    IEnumerator HideTextRoutine()
    {
        yield return new WaitForSeconds(duration);

        if (uiText != null)
        {
            uiText.gameObject.SetActive(false);
        }
        currentCoroutine = null;
    }
}