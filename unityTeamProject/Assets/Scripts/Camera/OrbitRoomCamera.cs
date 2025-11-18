using UnityEngine;
using UnityEngine.InputSystem;

public class OrbitRoomCamera : MonoBehaviour
{
    [Header("Camera Points (방 외곽 카메라 위치들 순서대로 등록)")]
    [Tooltip("방을 둘러싸는 카메라 포인트들 (예: North, East, South, West 순)")]
    public Transform[] cameraPoints;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;

    [Header("Input (New Input System 사용)")]
    [Tooltip("다음 포인트로 이동 (예: D 키)")]
    public InputActionReference nextPointAction;

    [Tooltip("이전 포인트로 이동 (예: A 키)")]
    public InputActionReference prevPointAction;

    private int _currentIndex = 0;
    private Transform _targetPoint;

    private void Awake()
    {
        if (cameraPoints != null && cameraPoints.Length > 0)
        {
            _currentIndex = 0;
            _targetPoint = cameraPoints[_currentIndex];
            // 시작 위치/회전 맞추기
            transform.position = _targetPoint.position;
            transform.rotation = _targetPoint.rotation;
        }
        else
        {
            Debug.LogWarning("[OrbitRoomCamera] cameraPoints가 비어 있습니다.");
        }
    }

    private void OnEnable()
    {
        if (nextPointAction != null)
        {
            nextPointAction.action.performed += OnNextPoint;
            nextPointAction.action.Enable();
        }

        if (prevPointAction != null)
        {
            prevPointAction.action.performed += OnPrevPoint;
            prevPointAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (nextPointAction != null)
        {
            nextPointAction.action.performed -= OnNextPoint;
            nextPointAction.action.Disable();
        }

        if (prevPointAction != null)
        {
            prevPointAction.action.performed -= OnPrevPoint;
            prevPointAction.action.Disable();
        }
    }

    private void Update()
    {
        if (_targetPoint == null) return;

        // 위치 보간
        transform.position = Vector3.Lerp(
            transform.position,
            _targetPoint.position,
            Time.deltaTime * moveSpeed
        );

        // 회전 보간
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            _targetPoint.rotation,
            Time.deltaTime * rotateSpeed
        );
    }

    private void OnNextPoint(InputAction.CallbackContext ctx)
    {
        if (cameraPoints == null || cameraPoints.Length == 0) return;

        _currentIndex = (_currentIndex + 1) % cameraPoints.Length;
        _targetPoint = cameraPoints[_currentIndex];
    }

    private void OnPrevPoint(InputAction.CallbackContext ctx)
    {
        if (cameraPoints == null || cameraPoints.Length == 0) return;

        _currentIndex--;
        if (_currentIndex < 0) _currentIndex = cameraPoints.Length - 1;
        _targetPoint = cameraPoints[_currentIndex];
    }
}
