using UnityEngine;
using UnityEngine.InputSystem;

public class caco : MonoBehaviour
{
    [Header("Camera Reference (Main Camera)")]
    public Transform cameraTransform;

    [Header("Orbit Settings")]
    public Transform center;
    public float height = 3f;
    public float radius = 6f;

    [Header("Direction Enable")]
    public bool enableNorth = true;
    public bool enableEast = true;
    public bool enableSouth = true;
    public bool enableWest = true;

    [Header("Rotation Smooth")]
    public float rotateSpeed = 3f;

    private int currentDir = 0;
    private float currentAngle = 0f;
    private float targetAngle = 0f;

    void Start()
    {
        if (center == null)
        {
            GameObject temp = new GameObject("OrbitCenter");
            temp.transform.position = Vector3.zero;
            center = temp.transform;
        }

        currentAngle = currentDir * 90f;
        targetAngle = currentAngle;

        ApplyCameraPosition();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.aKey.wasPressedThisFrame)
            RotateLeft();    // A → 반시계

        if (Keyboard.current.dKey.wasPressedThisFrame)
            RotateRight();   // D → 시계

        currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * rotateSpeed);
        ApplyCameraPosition();
    }

    void RotateLeft()
    {
        ChangeDirection(-1);
    }

    void RotateRight()
    {
        ChangeDirection(+1);
    }

    void ChangeDirection(int delta)
    {
        int newDir = currentDir;

        for (int i = 0; i < 4; i++)
        {
            newDir = (newDir + delta + 4) % 4;

            if (DirectionEnabled(newDir))
            {
                currentDir = newDir;
                targetAngle = currentDir * 90f;
                return;
            }
        }
    }

    bool DirectionEnabled(int dir)
    {
        return dir switch
        {
            0 => enableNorth,
            1 => enableEast,
            2 => enableSouth,
            3 => enableWest,
            _ => false
        };
    }

    void ApplyCameraPosition()
    {
        if (cameraTransform == null) return;

        float rad = currentAngle * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(
            Mathf.Sin(rad) * radius,
            height,
            Mathf.Cos(rad) * radius
        );

        cameraTransform.position = center.position + offset;
        cameraTransform.LookAt(center.position);
    }
}
