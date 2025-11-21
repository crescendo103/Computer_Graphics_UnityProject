using UnityEngine;

public class FlashFadeOut : MonoBehaviour
{
    private float duration = 0.5f;
    private float elapsed = 0f;
    private Renderer rend;
    private Color c;

    void Start()
    {
        rend = GetComponent<Renderer>();
        c = rend.material.color;
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        float t = elapsed / duration;
        c.a = Mathf.Lerp(0.8f, 0f, t);
        rend.material.color = c;
        transform.localScale = Vector3.Lerp(Vector3.one * 0.3f, Vector3.one * 1.2f, t);

        if (elapsed >= duration)
            Destroy(gameObject);
    }
}
