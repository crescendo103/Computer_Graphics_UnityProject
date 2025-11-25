using UnityEngine;

public class WorldItemPickup : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    // 클릭되었을 때 Inventory를 넘겨받아서 처리
    public void OnClicked(Inventory inventory)
    {
        // [추가됨] 이 컴포넌트(스크립트)가 꺼져있거나(!enabled), 
        // 게임 오브젝트가 비활성화(!activeInHierarchy) 상태라면 실행 중단
        if (!this.enabled || !gameObject.activeInHierarchy) return;

        if (inventory == null || itemData == null) return;

        // 네가 만든 Inventory의 AddItem 형식에 맞춰서 사용
        bool added = inventory.AddItem(itemData); // 이미 있을 거라고 가정

        if (added)
        {
            // 인벤토리에 성공적으로 들어갔으면 이 오브젝트 제거
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("인벤토리가 가득 차서 아이템을 넣을 수 없음");
        }
    }
}