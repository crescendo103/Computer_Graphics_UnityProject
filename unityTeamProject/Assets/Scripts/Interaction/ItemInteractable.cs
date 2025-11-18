using UnityEngine;
using UnityEngine.Events;

public class ItemInteractable : MonoBehaviour
{
    [SerializeField] private ItemData requiredItem;   // 필요 아이템 (null이면 아무거나)
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
            onInteract?.Invoke();
        }
    }
}
