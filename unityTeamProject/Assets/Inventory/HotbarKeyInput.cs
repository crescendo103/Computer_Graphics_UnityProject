using UnityEngine;
using UnityEngine.InputSystem;   // »õ Input System

public class HotbarKeyInput : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    private void Awake()
    {
        if (inventory == null)
            inventory = FindObjectOfType<Inventory>();
    }

    private void Update()
    {
        if (inventory == null) return;

        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        if (keyboard.digit1Key.wasPressedThisFrame) inventory.SelectSlot(0);
        if (keyboard.digit2Key.wasPressedThisFrame) inventory.SelectSlot(1);
        if (keyboard.digit3Key.wasPressedThisFrame) inventory.SelectSlot(2);
        if (keyboard.digit4Key.wasPressedThisFrame) inventory.SelectSlot(3);
        if (keyboard.digit5Key.wasPressedThisFrame) inventory.SelectSlot(4);
        if (keyboard.digit6Key.wasPressedThisFrame) inventory.SelectSlot(5);
        if (keyboard.digit7Key.wasPressedThisFrame) inventory.SelectSlot(6);
        if (keyboard.digit8Key.wasPressedThisFrame) inventory.SelectSlot(7);
    }
}
