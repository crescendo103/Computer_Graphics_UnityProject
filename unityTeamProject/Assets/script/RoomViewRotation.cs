using System.Collections;
using UnityEngine;

public class RoomViewRotation : MonoBehaviour
{
    // public을 사용하면 유니티 인스펙터 창에서 값을 쉽게 조절할 수 있습니다.
    [Tooltip("카메라가 90도 회전하는 데 걸리는 시간(초)")]
    public float rotationDuration = 0.5f;

    private bool _isRotating = false; // 현재 회전 중인지 확인하는 변수
    private float _targetYRotation = 0f; // 목표 Y축 회전값

    void Update()
    {
        // 1. 만약 현재 카메라가 회전 중이라면, 새로운 입력을 받지 않습니다.
        if (_isRotating)
        {
            return;
        }

        // 2. 오른쪽 방향키 입력 감지
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // 목표 회전값에 90도를 더합니다.
            _targetYRotation += 90f;
            StartCoroutine(RotateCamera()); // 회전 시작
        }
        // 3. 왼쪽 방향키 입력 감지
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // 목표 회전값에서 90도를 뺍니다.
            _targetYRotation -= 90f;
            StartCoroutine(RotateCamera()); // 회전 시작
        }
    }

    // 4. 카메라를 부드럽게 회전시키는 코루틴(Coroutine)
    private IEnumerator RotateCamera()
    {
        _isRotating = true; // 회전 시작 (입력 잠금)

        Quaternion startRotation = transform.rotation; // 현재 카메라의 회전값
        Quaternion targetRotation = Quaternion.Euler(0, _targetYRotation, 0); // 목표 회전값

        float elapsedTime = 0f;

        // rotationDuration에 설정된 시간 동안 부드럽게 회전
        while (elapsedTime < rotationDuration)
        {
            // Slerp: 두 회전값 사이를 부드럽게 보간합니다.
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationDuration);

            elapsedTime += Time.deltaTime; // 경과 시간 추가
            yield return null; // 다음 프레임까지 대기
        }

        // 5. 회전이 끝난 후 정확한 목표값으로 설정 (오차 보정)
        transform.rotation = targetRotation;
        _isRotating = false; // 회전 끝 (입력 잠금 해제)
    }
}