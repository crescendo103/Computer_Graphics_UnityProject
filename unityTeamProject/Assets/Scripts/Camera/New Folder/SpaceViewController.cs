using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceViewController : MonoBehaviour
{
    [Header("Camera ViewPoints")]
    public Transform spaceViewPoint;       // 확대할 위치
    public Transform playerOriginalView;   // 기본 위치

    private bool isViewing = false;

    // 오브젝트 클릭 시 실행 (ItemInteractable/ClickInteraction과 연결)
    public void OnInteract()
    {
        if (!isViewing)
        {
            // 확대 모드 ON
            isViewing = true;
            CameraFocusController.Instance.SnapTo(spaceViewPoint);
            DisableCaco();
        }
        else
        {
            // 다시 클릭하면 복귀
            ExitView();
        }
    }

    private void Update()
    {
        if (!isViewing) return;

        // 확대 상태에서 아무 클릭 → 복귀
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            ExitView();
        }
    }

    private void ExitView()
    {
        isViewing = false;
        CameraFocusController.Instance.SnapTo(playerOriginalView);
        EnableCaco();
    }

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
