using UnityEngine;

public class PickupFloatAndFade : MonoBehaviour
{
    public float duration = 1.5f;      // 몇 초 동안 연출할지
    public float floatHeight = 0.3f;   // 얼마나 위로 붕 뜨는지
    public float spinSpeed = 90f;      // 초당 회전 속도
    private float elapsed = 0f;

    private Vector3 startPos;
    private Renderer rend;
    private Color baseColor;
    private bool hasColorProperty = false;

    void Start()
    {
        startPos = transform.position;
        rend = GetComponentInChildren<Renderer>();

        if (rend != null && rend.material != null && rend.material.HasProperty("_Color"))
        {
            baseColor = rend.material.color;
            hasColorProperty = true;

            // 표준 셰이더일 경우 페이드 모드로 전환
            if (rend.material.shader.name.Contains("Standard"))
            {
                rend.material.SetFloat("_Mode", 2f); // Fade
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

        // 카메라 위쪽 방향으로 살짝 떠오르기
        transform.position = startPos + Camera.main.transform.up * (t * floatHeight);

        // 회전
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime, Space.World);

        // 점점 투명해지기
        if (hasColorProperty)
        {
            Color c = baseColor;
            c.a = Mathf.Lerp(1f, 0f, t);
            rend.material.color = c;
        }

        // 끝나면 부드럽게 파괴
        if (elapsed >= duration)
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
