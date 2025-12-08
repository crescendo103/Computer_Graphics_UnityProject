using UnityEngine;

public class LeftDoorRotate : MonoBehaviour
{
    public float targetZ = -90f;  // 최종적으로 도달할 z 회전값
    public float speed = 120f;

    private bool isOpening = false;
    private float startZ;
    private float currentZ;
    private float elapsed = 0f;

    private void Start()
    {
        // 시작 각도 기록 (현재 localRotation의 Z 각도)
        startZ = transform.localEulerAngles.z;
        currentZ = startZ;
    }

    public void OpenDoor()
    {
        isOpening = true;
        elapsed = 0f;
    }

    private void Update()
    {
        if (!isOpening) return;

        // 경과 시간 증가
        elapsed += Time.deltaTime * speed;

        // Z만 부드럽게 보간
        currentZ = Mathf.LerpAngle(startZ, targetZ, elapsed / 100f);

        // 회전 적용 (X,Y는 유지)
        Vector3 rot = transform.localEulerAngles;
        rot.z = currentZ;
        transform.localEulerAngles = rot;

        // 목표에 가까워지면 멈춤
        if (Mathf.Abs(Mathf.DeltaAngle(currentZ, targetZ)) < 0.1f)
        {
            isOpening = false;

            // 최종 보정
            rot.z = targetZ;
            transform.localEulerAngles = rot;
        }
    }
}
