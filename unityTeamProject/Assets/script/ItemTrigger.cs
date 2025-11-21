using UnityEngine;
using UnityEngine.Events;

public class ItemTrigger : MonoBehaviour
{
    [Header("Required Item")]
    [SerializeField] private ItemData requiredItem; // 필요한 아이템 (예: Sword9_Red 데이터)

    [Header("On Success Event")]
    public UnityEvent onSuccess; // 성공 시 실행할 이벤트 (SwordManager 연결용)

    // 컨트롤러가 이 함수를 호출합니다.
    public void TryTrigger(Inventory inventory)
    {
        if (inventory == null) return;

        // 1. 현재 들고 있는 아이템 확인
        ItemData currentItem = inventory.GetSelectedItem();

        // 2. 아이템이 일치하는지 확인
        if (currentItem == requiredItem)
        {
            // 3. 인벤토리에서 아이템 삭제 (소모)
            inventory.ClearSelectedSlot();

            // 4. 성공 이벤트 실행 (매니저에게 신호 보냄)
            Debug.Log("Item Match Success");
            onSuccess?.Invoke();
        }
        else
        {
            Debug.Log("Item Mismatch");
        }
    }
}