using System.Collections.Generic;
using UnityEngine;
using System; // <-- 중요: Action 기능을 사용하기 위해 추가됨

public class InventoryManager : MonoBehaviour
{
    // 'Singleton' 패턴: 이 매니저를 게임 내 어디서든 쉽게 참조할 수 있도록 함
    public static InventoryManager Instance { get; private set; }

    [Tooltip("플레이어가 현재 소지한 아이템 리스트")]
    public List<ItemData> items = new List<ItemData>();

    [Tooltip("인벤토리 슬롯의 총 개수 (핫바 + 전체 인벤토리)")]
    public int inventorySlotCount = 30;

    // ★ 중요: 인벤토리 내용이 변경될 때마다 울리는 '신호(이벤트)' 정의
    public static event Action OnInventoryChanged;

    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // 아이템을 인벤토리에 추가하는 함수
    public bool AddItem(ItemData itemToAdd)
    {
        // 1. 인벤토리가 꽉 찼는지 확인
        if (items.Count >= inventorySlotCount)
        {
            Debug.Log("인벤토리가 꽉 찼습니다.");
            return false; // 추가 실패
        }

        // 2. 인벤토리에 아이템 추가
        items.Add(itemToAdd);
        Debug.Log(itemToAdd.itemName + "을(를) 획득했습니다.");

        // ★ 중요: 아이템이 추가되었으니 UI에게 "화면 갱신해!"라고 신호를 보냄
        OnInventoryChanged?.Invoke();

        return true; // 추가 성공
    }

    // 아이템을 인벤토리에서 제거하는 함수
    public void RemoveItem(ItemData itemToRemove)
    {
        if (items.Contains(itemToRemove))
        {
            items.Remove(itemToRemove);
            Debug.Log(itemToRemove.itemName + "을(를) 사용했습니다.");

            // ★ 중요: 아이템이 삭제되었으니 UI에게 "화면 갱신해!"라고 신호를 보냄
            OnInventoryChanged?.Invoke();
        }
    }
}