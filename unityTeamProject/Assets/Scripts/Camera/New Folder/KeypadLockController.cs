using UnityEngine;

public class KeypadLockController : MonoBehaviour
{
    [Header("Keypad UI")]
    public KeypadUIController keypadUI;

    [Header("Camera ViewPoints")]
    public Transform keypadViewPoint;       // 키패드 확대 위치
    public Transform playerOriginalView;    // 기본 위치
    public Transform clueViewPoint;         // 단서 화면 위치

    [Header("Monitor Screen (Screen_A)")]
    public MeshRenderer monitorRenderer;
    public Texture clueTexture;

    private bool isUnlocked = false;   // 비밀번호 풀렸는가?
    private bool isViewingClue = false; // 단서 화면 보고 있는가?

    //--------------------------------------------------
    //   클릭 시 호출 (ItemInteractable에서 실행됨)
    //--------------------------------------------------
    public void OnInteract()
    {
        // 1) 비밀번호가 아직 안 풀렸을 때 → 키패드 확대
        if (!isUnlocked)
        {
            CameraFocusController.Instance.SnapTo(keypadViewPoint);
            DisableCaco();

            keypadUI.OpenUI("4797", OnCorrectCode);
            return;
        }

        // 2) 비밀번호 풀렸고, 지금 clue 안 보는 상태 → 단서 확대
        if (!isViewingClue)
        {
            isViewingClue = true;
            CameraFocusController.Instance.SnapTo(clueViewPoint);
            DisableCaco();
            return;
        }

        // 3) 비밀번호 풀렸고, clueViewPoint 보는 상태에서 클릭 → 복귀
        isViewingClue = false;
        CameraFocusController.Instance.SnapTo(playerOriginalView);
        EnableCaco();
    }

    //--------------------------------------------------
    //   키패드 정답 입력 시 실행되는 콜백
    //--------------------------------------------------
    private void OnCorrectCode()
    {
        isUnlocked = true;

        // 단서 이미지 변경
        if (monitorRenderer != null && clueTexture != null)
            monitorRenderer.material.SetTexture("_BaseMap", clueTexture);

        // 키패드 닫고 원래 위치로 복귀
        CameraFocusController.Instance.SnapTo(playerOriginalView);
        EnableCaco();
    }

    //--------------------------------------------------
    //   키패드 UI 닫기 버튼에서 호출됨
    //--------------------------------------------------
    public void ExitKeypad()
    {
        keypadUI.CloseUI();
        CameraFocusController.Instance.SnapTo(playerOriginalView);
        EnableCaco();
    }

    //--------------------------------------------------
    //   caco ON/OFF
    //--------------------------------------------------
    private void DisableCaco()
    {
        caco c = FindObjectOfType<caco>();
        if (c != null) c.enabled = false;
    }

    private void EnableCaco()
    {
        caco c = FindObjectOfType<caco>();
        if (c != null) c.enabled = true;
    }
}
