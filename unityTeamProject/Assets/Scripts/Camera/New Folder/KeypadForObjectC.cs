using UnityEngine;

public class KeypadForObjectC : MonoBehaviour
{
    [Header("Keypad UI")]
    public KeypadUIController keypadUI;

    [Header("Camera ViewPoints")]
    public Transform keypadViewPoint;
    public Transform playerOriginalView;

    [Header("문 회전 스크립트")]
    public LeftDoorRotate leftDoorRotate;   // ★ 변경됨

    private bool isUnlocked = false;

    public void OnInteract()
    {
        if (!isUnlocked)
        {
            CameraFocusController.Instance.SnapTo(keypadViewPoint);
            DisableCaco();

            keypadUI.OpenUI("6334", OnCorrectCode);
        }
        else
        {
            Debug.Log("이미 열림");
        }
    }

    private void OnCorrectCode()
    {
        isUnlocked = true;

        // 문 열기
        leftDoorRotate.OpenDoor();

        CameraFocusController.Instance.SnapTo(playerOriginalView);
        EnableCaco();
    }

    private void DisableCaco()
    {
        var c = FindObjectOfType<caco>();
        if (c != null) c.enabled = false;
    }

    private void EnableCaco()
    {
        var c = FindObjectOfType<caco>();
        if (c != null) c.enabled = true;
    }
}
