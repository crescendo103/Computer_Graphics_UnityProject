using UnityEngine;

public class CameraFocusController : MonoBehaviour
{
    public static CameraFocusController Instance;
    [SerializeField] private Camera mainCamera;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        if (mainCamera == null) mainCamera = Camera.main;
    }

    public void SnapTo(Transform target)
    {
        if (mainCamera == null || target == null) return;

        mainCamera.transform.position = target.position;
        mainCamera.transform.rotation = target.rotation;
    }
}
