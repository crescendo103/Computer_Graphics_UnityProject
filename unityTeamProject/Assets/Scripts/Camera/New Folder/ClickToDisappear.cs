using UnityEngine;

public class ClickToDisappear : MonoBehaviour
{
    // 마우스로 오브젝트를 클릭했을 때 자동으로 호출됨
    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
