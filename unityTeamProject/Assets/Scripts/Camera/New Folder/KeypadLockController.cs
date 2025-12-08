using UnityEngine;
using UnityEngine.InputSystem;

public class KeypadLockController : MonoBehaviour
{
    [Header("Keypad UI")]
    public KeypadUIController keypadUI;

    [Header("Camera ViewPoints")]
    public Transform keypadViewPoint;
    public Transform playerOriginalView;
    public Transform clueViewPoint;

    [Header("Monitor Screen (Screen_A)")]
    public MeshRenderer monitorRenderer;
    public Texture clueTexture;

    private bool isUnlocked = false;
    private bool isViewingClue = false;

    //----------------------------
    //   INTERACT (클릭 시)
    //----------------------------
    public void OnInteract()
    {
        if (!isUnlocked)
        {
            // 키패드 확대
            CameraFocusController.Instance.SnapTo(keypadViewPoint);
            DisableCaco();

            // 키패드 열기 + 정답 콜백 등록
            keypadUI.OpenUI("4797", OnCorrectCode);
        }
        else
        {
            // 단서2 확대
            isViewingClue = true;
            CameraFocusController.Instance.SnapTo(clueViewPoint);
            DisableCaco();
        }
    }

    //----------------------------
    //  키패드 정답 시 실행되는 콜백
    //----------------------------
    private void OnCorrectCode()
    {
        isUnlocked = true;

        // 단서2 이미지 변경
        if (monitorRenderer != null && clueTexture != null)
            monitorRenderer.material.SetTexture("_BaseMap", clueTexture);

        // 원래 위치로 복귀
        CameraFocusController.Instance.SnapTo(playerOriginalView);
        EnableCaco();
    }

    //----------------------------
    //   단서 확대 모드에서 클릭 시 복귀
    //----------------------------
    private void Update()
    {
        if (isViewingClue)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                isViewingClue = false;
                CameraFocusController.Instance.SnapTo(playerOriginalView);
                EnableCaco();
            }
        }
    }

    //----------------------------
    //   배경 클릭 → 키패드 복귀 (UI Button이 호출)
    //----------------------------
    public void ExitKeypad()
    {
        keypadUI.CloseUI();
        CameraFocusController.Instance.SnapTo(playerOriginalView);
        EnableCaco();
    }

    //----------------------------
    //   caco ON/OFF
    //----------------------------
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
