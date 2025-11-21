using UnityEngine;

public class ClickToAcquire : MonoBehaviour
{
    // 이 스크립트가 붙어있는 오브젝트가 'Collider'를 가지고 있고,
    // 해당 Collider가 마우스로 클릭되면 이 함수가 자동으로 호출됩니다.
    void OnMouseDown()
    {
        // 1. 콘솔 창에 획득 메시지를 출력합니다. (확인용)
        // gameObject.name 은 이 스크립트가 붙어있는 오브젝트의 이름입니다. (예: "Knife")
        Debug.Log(gameObject.name + " 을(를) 획득했습니다!");

        // 2. (미래 확장) 여기에 인벤토리 시스템에 아이템을 추가하는 코드를 넣습니다.
        // 예: Inventory.instance.AddItem(this.gameObject);

        // 3. 오브젝트를 씬에서 파괴(제거)하여 획득한 것처럼 보이게 합니다.
        Destroy(gameObject);
    }
}