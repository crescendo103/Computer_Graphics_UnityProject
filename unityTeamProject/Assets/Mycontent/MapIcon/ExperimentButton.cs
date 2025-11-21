using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExperimentButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
   
    public Image baseImg;
    public Sprite nonchoiceImage;
    public Sprite choiceExperiment;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"{gameObject.name} : 버튼 클릭");
        LoadScene("stage1");        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"{gameObject.name} : 마우스 들어옴");
        baseImg.sprite = choiceExperiment;
    }

    // 마우스가 나갔을 때
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"{gameObject.name} : 마우스 나감");
        baseImg.sprite = nonchoiceImage;
    }
    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
   
}
