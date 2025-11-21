using UnityEngine;

// 货 Input System / 备 Input System 笛 促 瘤盔
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
using UnityEngine.InputSystem;
#endif

public class DebugUIVisibility : MonoBehaviour
{
    [Header("This whole object will be shown/hidden")]
    public GameObject targetRoot; // CameraSetupPanel 持阑 磊府

    public KeyCode legacyToggleKey = KeyCode.F1;
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
    public Key newInputKey = Key.F1;
#endif

    void Update()
    {
        bool pressed = false;

        // 备 Input System
#if ENABLE_LEGACY_INPUT_MANAGER
        if (Input.GetKeyDown(legacyToggleKey))
            pressed = true;
#endif

        // 货 Input System
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
        if (Keyboard.current != null && Keyboard.current[newInputKey].wasPressedThisFrame)
            pressed = true;
#endif

        if (pressed && targetRoot != null)
        {
            bool next = !targetRoot.activeSelf;
            targetRoot.SetActive(next);
        }
    }
}
