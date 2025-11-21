using UnityEngine;
using System.Collections;

public class RewardKeySequence : MonoBehaviour
{
    [Header("보상으로 줄 아이템")]
    public ItemData rewardItem;

    [Header("나타나는 속도 (초)")]
    public float fadeDuration = 2.0f;

    private Inventory inventory;
    private Renderer objRenderer;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        objRenderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        // 오브젝트가 켜지면 연출 시작
        StartCoroutine(ProcessSequence());
    }

    IEnumerator ProcessSequence()
    {
        // 1. 처음엔 투명하게 설정 (Material이 Transparent여야 함)
        Color startColor = objRenderer.material.color;
        startColor.a = 0f;
        objRenderer.material.color = startColor;

        // 2. 서서히 나타나기 (Fade In)
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);

            Color newColor = startColor;
            newColor.a = alpha;
            objRenderer.material.color = newColor;

            yield return null;
        }

        // 확실하게 불투명하게 마무리
        startColor.a = 1f;
        objRenderer.material.color = startColor;

        // 3. 잠시 대기 (유저가 "오 열쇠다!" 하고 볼 시간 줌)
        yield return new WaitForSeconds(0.5f);

        // 4. 인벤토리에 넣기
        if (inventory != null && rewardItem != null)
        {
            inventory.AddItem(rewardItem);
            Debug.Log(" 열쇠 획득 완료!");
        }

        // 5. 화면에서 사라지기
        Destroy(gameObject);
    }
}