/*
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float rotateSpeed = 5f;
    private float yaw = 0f;
    private float pitch = 0f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            yaw += rotateSpeed * Input.GetAxis("Mouse X");
            pitch -= rotateSpeed * Input.GetAxis("Mouse Y");
            pitch = Mathf.Clamp(pitch, -40f, 40f); // 위아래 제한

            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }
    }
}
*/
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float rotateSpeed = 5f;   // 회전 속도
    private float targetAngle = 0f;  // 목표 각도
    private float currentAngle = 0f; // 실제 적용되고 있는 각도

    void Update()
    {
        // ← 키
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetAngle -= 90f;
        }

        // → 키
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetAngle += 90f;
        }

        // 부드럽게 회전
        currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * rotateSpeed);
        transform.rotation = Quaternion.Euler(0f, currentAngle, 0f);
    }
}
