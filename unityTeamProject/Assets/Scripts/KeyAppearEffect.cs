using UnityEngine;

public class KeyAppearEffect : MonoBehaviour
{
    public float duration = 1.5f;      // 등장 연출 시간
    public float floatHeight = 0.5f;   // 얼마나 위로 떠오를지
    private float elapsed = 0f;

    private Vector3 startPos;
    private Renderer rend;
    private Color baseColor;
    private bool hasColorProperty = false;
    private bool flashTriggered = false;

    void Start()
    {
        startPos = transform.position;
        rend = GetComponentInChildren<Renderer>();

        if (rend != null && rend.material != null && rend.material.HasProperty("_Color"))
        {
            baseColor = rend.material.color;
            baseColor.a = 0f; // 완전 투명 시작
            rend.material.color = baseColor;
            hasColorProperty = true;

            // Standard Shader 페이드 모드 전환
            if (rend.material.shader.name.Contains("Standard"))
            {
                rend.material.SetFloat("_Mode", 2f);
                rend.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                rend.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                rend.material.SetInt("_ZWrite", 0);
                rend.material.DisableKeyword("_ALPHATEST_ON");
                rend.material.EnableKeyword("_ALPHABLEND_ON");
                rend.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                rend.material.renderQueue = 3000;
            }
        }
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / duration);

        // 1) 위로 서서히 상승
        transform.position = startPos + Camera.main.transform.up * (t * floatHeight);

        // 2) 점점 선명해지기 (페이드 인)
        if (hasColorProperty)
        {
            Color c = baseColor;
            c.a = Mathf.Lerp(0f, 1f, t);
            rend.material.color = c;
        }

        // 3) 살짝 회전
        transform.Rotate(Vector3.up * 30f * Time.deltaTime, Space.World);

        // 4) 마지막 순간에 반짝 효과
        if (t > 0.9f && !flashTriggered)
        {
            flashTriggered = true;
            TriggerFlash();
        }
    }

    void TriggerFlash()
    {
        // 번쩍 효과를 위한 임시 파티클 추가
        GameObject flash = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Destroy(flash.GetComponent<Collider>());
        flash.transform.position = transform.position;
        flash.transform.localScale = Vector3.one * 0.3f;
        flash.GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Color"));
        flash.GetComponent<Renderer>().material.color = new Color(1f, 1f, 0.8f, 0.8f);
        flash.AddComponent<FlashFadeOut>();
    }
}
