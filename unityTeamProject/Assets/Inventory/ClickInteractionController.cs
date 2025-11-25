using UnityEngine;
using UnityEngine.InputSystem;

public class ClickInteractionController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera detailCamera;

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

        // 어떤 카메라를 쓸지 결정하는 로직 
        // 기본은 메인 카메라를 쓰되...
        Camera activeCamera = mainCamera;

        // 만약 디테일 카메라가 연결되어 있고 + 현재 켜져 있다면(Active) -> 디테일 카메라로 교체!
        if (detailCamera != null && detailCamera.gameObject.activeInHierarchy)
        {
            activeCamera = detailCamera;
        }

        // 3. activeCamera를 사용하여 Ray 발사
        Ray ray = activeCamera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            // 1. [줍기] WorldItemPickup
            var pickup = hit.collider.GetComponent<WorldItemPickup>();
            // pickup이 존재하고(&&) 컴포넌트가 켜져있을 때(.enabled)만 실행
            if (pickup != null && pickup.enabled)
            {
                pickup.OnClicked(inventory);
                return;
            }

            // 2. [이벤트] ItemTrigger
            var trigger = hit.collider.GetComponent<ItemTrigger>();
            // trigger가 존재하고(&&) 컴포넌트가 켜져있을 때(.enabled)만 실행
            if (trigger != null && trigger.enabled)
            {
                trigger.TryTrigger(inventory);
                return;
            }

            // 3. [손전등 힌트] FlashlightHint (추가된 기능)
            var hint = hit.collider.GetComponent<FlashlightHint>();
            // hint가 존재하고(&&) 컴포넌트가 켜져있을 때(.enabled)만 실행
            if (hint != null && hint.enabled)
            {
                hint.TryShowHint(inventory);
                return;
            }

            // 4. [범용] ItemInteractable
            var interactable = hit.collider.GetComponent<ItemInteractable>();
            // interactable이 존재하고(&&) 컴포넌트가 켜져있을 때(.enabled)만 실행
            if (interactable != null && interactable.enabled)
            {
                interactable.TryInteract(inventory);
                return;
            }
        }
    }
}