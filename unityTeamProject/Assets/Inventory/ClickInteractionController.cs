using UnityEngine;
using UnityEngine.InputSystem;

public class ClickInteractionController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera detailCamera; // 1. 디테일 카메라 변수 추가

    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private Inventory inventory;

    private void Awake()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        if (inventory == null) inventory = FindObjectOfType<Inventory>();
    }

    private void Update()
    {
        if (Mouse.current == null) return;

        // 마우스 왼쪽 클릭 감지
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        //  2. 어떤 카메라를 쓸지 결정하는 로직 (수정된 부분)
        // 기본은 메인 카메라를 쓰되...
        Camera activeCamera = mainCamera;

        // 만약 디테일 카메라가 연결되어 있고 + 현재 켜져 있다면(Active) -> 디테일 카메라로 교체!
        if (detailCamera != null && detailCamera.gameObject.activeInHierarchy)
        {
            activeCamera = detailCamera;
        }

        //  3. mainCamera 대신 activeCamera 사용
        Ray ray = activeCamera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            // 1. [줍기] WorldItemPickup
            var pickup = hit.collider.GetComponent<WorldItemPickup>();
            if (pickup != null)
            {
                pickup.OnClicked(inventory);
                return;
            }

            // 2. [이벤트] ItemTrigger
            var trigger = hit.collider.GetComponent<ItemTrigger>();
            if (trigger != null)
            {
                trigger.TryTrigger(inventory);
                return;
            }

            // 3. [범용] ItemInteractable
            var interactable = hit.collider.GetComponent<ItemInteractable>();
            if (interactable != null)
            {
                interactable.TryInteract(inventory);
                return;
            }
        }
    }
}