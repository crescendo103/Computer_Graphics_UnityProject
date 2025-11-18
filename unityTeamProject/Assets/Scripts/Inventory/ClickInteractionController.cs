using UnityEngine;
using UnityEngine.InputSystem;  // ← 이 줄 추가 

public class ClickInteractionController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private Inventory inventory;   

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (inventory == null)
            inventory = FindObjectOfType<Inventory>(); // 경고 유지 OK
    }

    private void Update()
    {
        // 새 Input System 사용
        if (Mouse.current == null) return;

        // 마우스 왼쪽 버튼이 이번 프레임에 눌렸는지
        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        // 마우스 위치
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePos);

        if (!Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            return;

        // 오른쪽 큐브(월드 아이템)
        var pickup = hit.collider.GetComponent<WorldItemPickup>();
        if (pickup != null)
        {
            pickup.OnClicked(inventory);
            return;
        }

        // 왼쪽 큐브(아이템 사용해서 커지는 애)
        var grow = hit.collider.GetComponent<GrowOnUse>();
        if (grow != null)
        {
            grow.OnClicked(inventory);
            return;
        }
    }
}
