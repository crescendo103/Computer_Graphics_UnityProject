using UnityEngine;

public class CameraOrbitController : MonoBehaviour
{
    [Header("References")]
    public Transform orbitCenter;     // 방의 중앙 (카메라가 바라볼 대상)
    public Transform cam;             // 실제 Main Camera

    [Header("Orbit Shape")]
    public float radius = 5f;         // 카메라가 도는 반지름
    public float height = 1.7f;       // 카메라 눈높이 (y)

    [Header("Movement")]
    public float moveLerpSpeed = 5f;  // 시점 전환 시 부드럽게 이동

    [HideInInspector] public float targetAngleDeg = 0f; // 목표 각도
    private float currentAngleDeg = 0f;                 // 현재 각도 (보간용)

    void LateUpdate()
    {
        if (orbitCenter == null || cam == null)
            return;

        // 부드럽게 각도 보간
        currentAngleDeg = Mathf.LerpAngle(currentAngleDeg, targetAngleDeg, Time.deltaTime * moveLerpSpeed);

        float rad = currentAngleDeg * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(
            Mathf.Sin(rad) * radius,
            0f,
            Mathf.Cos(rad) * radius
        );

        Vector3 worldPos = orbitCenter.position + offset;
        worldPos.y = orbitCenter.position.y + height;

        cam.position = worldPos;
        cam.LookAt(orbitCenter.position + Vector3.up * height * 0.8f);
    }

    // 시점 전환 시작 시 위치를 바로 맞추는 함수
    public void ForceSnapAngle(float angle)
    {
        targetAngleDeg = angle;
        currentAngleDeg = angle;
    }
}
