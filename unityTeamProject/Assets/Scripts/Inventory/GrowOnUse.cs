using UnityEngine;

public class GrowOnUse : MonoBehaviour
{
    [SerializeField] private float scaleMultiplier = 2f;

    // 특정 아이템만 허용하고 싶으면 넣고, 아무 아이템이나 상관없으면 비워둬도 됨
    [SerializeField] private ItemData requiredItem;

    public void OnClicked(Inventory inventory)
    {
        if (inventory == null) return;

        ItemData cur = inventory.GetSelectedItem();

        if (cur == null)
        {
            Debug.Log("선택된 슬롯에 아이템이 없음");
            return;
        }

        // 특정 아이템만 허용할 경우 체크
        if (requiredItem != null && cur != requiredItem)
        {
            Debug.Log("이 오브젝트에는 이 아이템을 사용할 수 없음");
            return;
        }

        // 큐브 크기 2배
        transform.localScale *= scaleMultiplier;

        // 아이템 사용 후 슬롯 비우기
        inventory.ClearSelectedSlot();
    }
}
