using UnityEngine;

public class DoorRotator : MonoBehaviour
{
    [SerializeField] private float openAngle = -90f;  // 목표 Z각도
    [SerializeField] private float speed = 2f;        // 회전 속도
    [SerializeField] private bool isOpen = false;

    private bool isAnimating = false;

    public void ToggleDoor()
    {
        if (!isAnimating)
            StartCoroutine(AnimateDoor());
    }

    private System.Collections.IEnumerator AnimateDoor()
    {
        isAnimating = true;

        float start = transform.localEulerAngles.z;
        if (start > 180f) start -= 360f; // 360도 보정

        float end = isOpen ? 0f : openAngle;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            float z = Mathf.Lerp(start, end, t);

            transform.localEulerAngles = new Vector3(
                transform.localEulerAngles.x,
                transform.localEulerAngles.y,
                z
            );

            yield return null;
        }

        isOpen = !isOpen;
        isAnimating = false;
    }
}
