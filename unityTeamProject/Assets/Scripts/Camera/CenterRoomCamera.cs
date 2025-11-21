using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CenterRoomCamera : MonoBehaviour
{
    [Header("Pivot")]
    [Tooltip("회전의 기준이 되는 피벗 (보통 카메라의 부모). 비어 있으면 이 오브젝트 사용.")]
    public Transform pivot;

    [Header("Directions (동서남북 사용 여부)")]
    public bool enableNorth = true;  // 0도
    public bool enableEast = true;  // 90도
    public bool enableSouth = true;  // 180도
    public bool enableWest = true;  // 270도

    [Header("Rotation")]
    public float rotationSpeed = 5f;

    private struct DirSlot
    {
        public string name;
        public float yaw;
        public DirSlot(string name, float yaw) { this.name = name; this.yaw = yaw; }
    }

    private readonly List<DirSlot> _dirs = new List<DirSlot>();
    private int _currentIndex;
    private Quaternion _targetRot;

    private void Awake()
    {
        if (pivot == null) pivot = transform;
        BuildDirList();

        if (_dirs.Count > 0)
        {
            _currentIndex = 0;
            var d = _dirs[_currentIndex];
            _targetRot = Quaternion.Euler(0f, d.yaw, 0f);
            pivot.rotation = _targetRot;
        }
        else
        {
            Debug.LogWarning("[CenterRoomCamera] 활성화된 방향이 없습니다.");
        }
    }

    private void Update()
    {
        var kb = Keyboard.current;
        if (kb != null)
        {
            if (kb.dKey.wasPressedThisFrame)
                NextDirection();
            else if (kb.aKey.wasPressedThisFrame)
                PreviousDirection();
        }

        if (pivot == null || _dirs.Count == 0) return;

        pivot.rotation = Quaternion.Slerp(
            pivot.rotation,
            _targetRot,
            Time.deltaTime * rotationSpeed
        );
    }

    public void NextDirection()
    {
        if (_dirs.Count == 0) return;
        _currentIndex = (_currentIndex + 1) % _dirs.Count;
        ApplyCurrentDir();
    }

    public void PreviousDirection()
    {
        if (_dirs.Count == 0) return;
        _currentIndex--;
        if (_currentIndex < 0) _currentIndex = _dirs.Count - 1;
        ApplyCurrentDir();
    }

    private void ApplyCurrentDir()
    {
        var d = _dirs[_currentIndex];
        _targetRot = Quaternion.Euler(0f, d.yaw, 0f);
        Debug.Log($"[CenterRoomCamera] 현재 방향: {d.name} (yaw={d.yaw})");
    }

    private void BuildDirList()
    {
        _dirs.Clear();
        if (enableNorth) _dirs.Add(new DirSlot("North", 0f));
        if (enableEast) _dirs.Add(new DirSlot("East", 90f));
        if (enableSouth) _dirs.Add(new DirSlot("South", 180f));
        if (enableWest) _dirs.Add(new DirSlot("West", 270f));

        if (_dirs.Count == 0)
        {
            _dirs.Add(new DirSlot("North", 0f));
            enableNorth = true;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (pivot == null) pivot = transform;
        BuildDirList();
        if (_dirs.Count > 0)
        {
            _currentIndex = Mathf.Clamp(_currentIndex, 0, _dirs.Count - 1);
            var d = _dirs[_currentIndex];
            _targetRot = Quaternion.Euler(0f, d.yaw, 0f);
            pivot.rotation = _targetRot;
        }
    }
#endif
}
