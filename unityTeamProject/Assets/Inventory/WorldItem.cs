using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    /// <summary>
    /// 인벤토리에 아이템 추가 시도. 성공하면 이 오브젝트는 제거된다.
    /// </summary>
    public void TryPickup(Inventory inventory)
    {
        if (inventory == null) return;
        if (itemData == null) return;

        bool added = inventory.AddItem(itemData);
        if (added)
        {
            Destroy(gameObject);
        }
    }
}
