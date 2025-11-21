using UnityEngine;
using System;

public class InventoryHotbarUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private InventorySlot[] slots;

    // 에디터에서 프리팹/씬 수정할 때 자동으로 연결
    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            AutoWire();
        }
    }

    private void Reset()
    {
        AutoWire();
    }

    private void Awake()
    {
        if (inventory == null)
            inventory = FindObjectOfType<Inventory>();

        if (slots == null || slots.Length == 0)
            AutoWireSlotsOnly();

        if (inventory != null)
        {
            inventory.OnSlotChanged += HandleSlotChanged;
            inventory.OnSelectionChanged += HandleSelectionChanged;
        }

        // 슬롯 초기화
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null) continue;

            slots[i].Initialize(inventory, i);
            ItemData item = inventory != null ? inventory.GetItem(i) : null;
            slots[i].Refresh(item);
        }

        int selected = inventory != null ? inventory.SelectedIndex : -1;
        HandleSelectionChanged(selected);
    }

    private void OnDestroy()
    {
        if (inventory != null)
        {
            inventory.OnSlotChanged -= HandleSlotChanged;
            inventory.OnSelectionChanged -= HandleSelectionChanged;
        }
    }

    // ─────────────────────────────────────────────
    // 자동 연결
    // ─────────────────────────────────────────────
    private void AutoWire()
    {
        // 씬/프리팹 안에서 Inventory 찾기
        if (inventory == null)
            inventory = FindObjectOfType<Inventory>();

        AutoWireSlotsOnly();
    }

    private void AutoWireSlotsOnly()
    {
        // 1. 기존 리스트 초기화
        // (GetComponentsInChildren 대신 수동으로 찾습니다)
        System.Collections.Generic.List<InventorySlot> foundSlots = new System.Collections.Generic.List<InventorySlot>();

        // 2. 직계 자식만(transform.GetChild) 하나씩 검사
        foreach (Transform child in transform)
        {
            InventorySlot slot = child.GetComponent<InventorySlot>();
            if (slot != null)
            {
                foundSlots.Add(slot);
            }
        }

        // 3. 리스트를 배열로 변환
        slots = foundSlots.ToArray();

        // 4. 이름 기준 정렬 (Slot_1, Slot_2 ... 순서로)
        Array.Sort(slots, (a, b) =>
            string.Compare(a.name, b.name, StringComparison.Ordinal));
    }

    // ─────────────────────────────────────────────
    // 이벤트 처리
    // ─────────────────────────────────────────────
    private void HandleSlotChanged(int index)
    {
        if (slots == null) return;
        if (index < 0 || index >= slots.Length) return;
        if (slots[index] == null) return;

        ItemData item = inventory.GetItem(index);
        slots[index].Refresh(item);
    }

    private void HandleSelectionChanged(int selectedIndex)
    {
        if (slots == null) return;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null) continue;
            slots[i].SetSelected(i == selectedIndex && selectedIndex >= 0);
        }
    }
}
