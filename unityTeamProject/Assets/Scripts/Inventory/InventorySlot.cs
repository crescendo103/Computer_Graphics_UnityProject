using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject selectedHighlight;

    private Inventory inventory;
    private int slotIndex = -1;
    private ItemData currentItem;

    public bool IsEmpty
    {
        get { return currentItem == null; }
    }

    // ─────────────────────────────────────────────────────
    // 에디터에서 아이콘/하이라이트 자동 연결
    // ─────────────────────────────────────────────────────
    private void OnValidate()
    {
        if (iconImage == null)
        {
            Transform t = transform.Find("Icon");
            if (t != null)
                iconImage = t.GetComponent<Image>();
        }

        if (selectedHighlight == null)
        {
            Transform t = transform.Find("SelectedHighlight");
            if (t != null)
                selectedHighlight = t.gameObject;
        }

        if (!Application.isPlaying && selectedHighlight != null)
        {
            selectedHighlight.SetActive(false);
        }
    }

    // ─────────────────────────────────────────────────────
    // 런타임에서 자기 자신 세팅 (inventory, slotIndex)
    // ─────────────────────────────────────────────────────
    private void Awake()
    {
        // Inventory 자동 찾기
        if (inventory == null)
        {
            inventory = FindObjectOfType<Inventory>();
        }

        // slotIndex 가 지정되지 않았다면, 이름에서 숫자 파싱 (Slot_1 → 0)
        if (slotIndex < 0)
        {
            Match m = Regex.Match(name, @"\d+");
            if (m.Success)
            {
                int num;
                if (int.TryParse(m.Value, out num))
                {
                    slotIndex = num - 1; // Slot_1 이면 0
                }
            }
        }

        // 처음에는 선택 해제 상태
        SetSelected(false);
    }

    // InventoryHotbarUI 에서 명시적으로 초기화할 때 사용
    public void Initialize(Inventory inv, int index)
    {
        if (inv != null)
            inventory = inv;

        if (index >= 0)
            slotIndex = index;
    }

    public void Refresh(ItemData item)
    {
        currentItem = item;

        if (iconImage == null)
            return;

        if (currentItem != null && currentItem.icon != null)
        {
            iconImage.sprite = currentItem.icon;
            iconImage.enabled = true;
        }
        else
        {
            iconImage.sprite = null;
            iconImage.enabled = false;
        }
    }

    public void SetSelected(bool selected)
    {
        if (selectedHighlight != null)
            selectedHighlight.SetActive(selected);

        // 디버그용 (하이라이트 토글 확인)
        Debug.Log($"Slot {name} SetSelected({selected})");
    }

    // ─────────────────────────────────────────────────────
    // 클릭 처리
    // ─────────────────────────────────────────────────────
    public void OnPointerClick(PointerEventData eventData)
    {
        // 디버그용: 클릭 들어오는지 확인
        Debug.Log($"Slot {name} OnPointerClick  inv={(inventory != null)}  index={slotIndex}");

        if (inventory == null)
            return;

        if (slotIndex < 0)
            return;

        inventory.SelectSlot(slotIndex);
    }
}
