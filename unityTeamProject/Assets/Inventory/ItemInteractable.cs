using UnityEngine;
using UnityEngine.Events;

public class ItemInteractable : MonoBehaviour
{
    [SerializeField] private ItemData requiredItem;   // 필요 아이템 (null이면 아무거나)

    // [추가됨] 아이템을 사용한 뒤 인벤토리에서 지울지 여부 (기본값 true)
    [SerializeField] private bool consumeItem = true;

    [SerializeField] private UnityEvent onInteract;   // 조건 만족 시 호출되는 이벤트

    /// <summary>
    /// 현재 인벤토리의 활성 아이템이 requiredItem 이면 이벤트 실행.
    /// </summary>
    public void TryInteract(Inventory inventory)
    {
        if (inventory == null) return;

        ItemData active = inventory.GetActiveItem();

        // 필요 아이템이 없으면 그냥 항상 실행, 있으면 동일한지 비교
        if (requiredItem == null || active == requiredItem)
        {
            // 1. 이벤트 실행
            onInteract?.Invoke();

            // 2. [추가됨] 아이템 삭제 로직
            // (필요 아이템이 설정되어 있고 + 소모 옵션이 켜져 있다면 삭제)
            if (requiredItem != null && consumeItem)
            {
                inventory.RemoveAt(inventory.SelectedIndex);
            }
        }
    }
}