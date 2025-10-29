using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class CameraViewManager : MonoBehaviour
{
    [Header("Dependencies")]
    public CameraOrbitController orbitController;
    public CameraInputActions inputActions;

    [Header("View Angles (deg)")]
    public float northAngle = 0f;
    public float eastAngle = 90f;
    public float southAngle = 180f;
    public float westAngle = 270f;

    [Header("Active Views")]
    public bool useNorth = true;
    public bool useEast = true;
    public bool useSouth = true;
    public bool useWest = true;

    private List<float> activeAngles = new List<float>();
    private int currentIndex = 0;

    void OnEnable()
    {
        if (inputActions == null)
            inputActions = new CameraInputActions();

        inputActions.Enable();
        inputActions.Camera.RotateLeft.performed += OnLeft;
        inputActions.Camera.RotateRight.performed += OnRight;
    }

    void OnDisable()
    {
        if (inputActions != null)
        {
            inputActions.Camera.RotateLeft.performed -= OnLeft;
            inputActions.Camera.RotateRight.performed -= OnRight;
            inputActions.Disable();
        }
    }

    void Start()
    {
        RebuildActiveList();
        SnapToIndex(0);
    }

    void Update()
    {
        if (activeAngles.Count > 0)
            orbitController.targetAngleDeg = activeAngles[currentIndex];
    }

    // --- 외부(UI)에서 토글 바뀔 때 호출 ---
    public void SetUseNorth(bool v) { useNorth = v; RebuildActiveListKeepClosest(); }
    public void SetUseEast(bool v) { useEast = v; RebuildActiveListKeepClosest(); }
    public void SetUseSouth(bool v) { useSouth = v; RebuildActiveListKeepClosest(); }
    public void SetUseWest(bool v) { useWest = v; RebuildActiveListKeepClosest(); }

    // --- 입력 처리 ---
    private void OnLeft(InputAction.CallbackContext ctx)
    {
        if (activeAngles.Count == 0) return;
        currentIndex++;
        if (currentIndex >= activeAngles.Count) currentIndex = 0;
    }

    private void OnRight(InputAction.CallbackContext ctx)
    {
        if (activeAngles.Count == 0) return;
        currentIndex--;
        if (currentIndex < 0) currentIndex = activeAngles.Count - 1;
    }

    // --- 내부 유틸 ---
    void RebuildActiveList()
    {
        activeAngles.Clear();
        if (useNorth) activeAngles.Add(northAngle);
        if (useEast) activeAngles.Add(eastAngle);
        if (useSouth) activeAngles.Add(southAngle);
        if (useWest) activeAngles.Add(westAngle);

        if (activeAngles.Count == 0)
            activeAngles.Add(orbitController.targetAngleDeg);

        currentIndex = Mathf.Clamp(currentIndex, 0, activeAngles.Count - 1);
    }

    void RebuildActiveListKeepClosest()
    {
        float currentAngle = (activeAngles.Count > 0) ? activeAngles[currentIndex] : orbitController.targetAngleDeg;
        RebuildActiveList();

        float bestDiff = 9999f;
        int bestIdx = 0;
        for (int i = 0; i < activeAngles.Count; i++)
        {
            float diff = Mathf.Abs(Mathf.DeltaAngle(currentAngle, activeAngles[i]));
            if (diff < bestDiff)
            {
                bestDiff = diff;
                bestIdx = i;
            }
        }
        currentIndex = bestIdx;
    }

    void SnapToIndex(int idx)
    {
        if (activeAngles.Count == 0) return;
        currentIndex = Mathf.Clamp(idx, 0, activeAngles.Count - 1);
        orbitController.ForceSnapAngle(activeAngles[currentIndex]);
    }
}
