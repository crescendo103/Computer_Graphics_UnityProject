using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public const int MaxSlots = 8;

    [Tooltip("인벤토리 슬롯 (null이면 빈 슬롯)")]
    [SerializeField] private ItemData[] slots = new ItemData[MaxSlots];

    public event Action<int> OnSlotChanged;       // 슬롯 하나 변경
    public event Action<int> OnSelectionChanged;  // 선택 슬롯 변경

    public int SelectedIndex { get; private set; } = -1;

    private void Reset()
    {
        slots = new ItemData[MaxSlots];
        SelectedIndex = -1;
    }

    public ItemData GetItem(int index)
    {
        if (!IsValidIndex(index)) return null;
        return slots[index];
    }

    public bool HasItemAt(int index)
    {
        return GetItem(index) != null;
    }

    public ItemData GetActiveItem()
    {
        if (!IsValidIndex(SelectedIndex)) return null;
        return slots[SelectedIndex];
    }

    /// <summary>
    /// 1번 슬롯부터 순서대로 빈곳에 추가. 성공 시 true, 실패(가득) 시 false.
    /// </summary>
    public bool AddItem(ItemData item)
    {
        if (item == null) return false;

        for (int i = 0; i < MaxSlots; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = item;
                OnSlotChanged?.Invoke(i);

                // 인벤토리에 아무 것도 없던 상태라면, 첫 아이템을 자동 선택
                if (SelectedIndex == -1)
                {
                    SelectedIndex = i;
                    OnSelectionChanged?.Invoke(SelectedIndex);
                }

                return true;
            }
        }

        return false; // 가득 찼음 (로그 출력 없음)
    }

    /// <summary>
    /// 슬롯 선택(하이라이트용)
    /// </summary>
    public void SelectSlot(int index)
    {
        if (!IsValidIndex(index)) return;
        //if (slots[index] == null) return; // 빈 슬롯은 선택 안 함

        if (SelectedIndex == index) return;

        SelectedIndex = index;
        OnSelectionChanged?.Invoke(SelectedIndex);
    }

    /// <summary>
    /// 아이템 제거 (필요하면 나중에 사용)
    /// </summary>
    public void RemoveAt(int index)
    {
        if (!IsValidIndex(index)) return;
        if (slots[index] == null) return;

        slots[index] = null;
        OnSlotChanged?.Invoke(index);

        if (SelectedIndex == index)
        {
            SelectedIndex = -1;
            OnSelectionChanged?.Invoke(SelectedIndex);
        }
    }

    private bool IsValidIndex(int index)
    {
        return index >= 0 && index < MaxSlots;
    }

    // 2) 현재 선택된 슬롯의 아이템 가져오기
    public ItemData GetSelectedItem()
    {
        if (!IsValidIndex(SelectedIndex)) return null;
        return slots[SelectedIndex];
    }

    // 3) 현재 선택된 슬롯 비우기(아이템 사용 후)
    public void ClearSelectedSlot()
    {
        if (!IsValidIndex(SelectedIndex)) return;

        slots[SelectedIndex] = null;

        // UI 갱신 함수가 있으면 호출
        // RefreshSlot(SelectedIndex); // 없으면 삭제
    }
}
