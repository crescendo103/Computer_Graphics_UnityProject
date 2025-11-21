using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSceneLoad : MonoBehaviour
{
    public static EndingSceneLoad Instance { get; private set; }   // 싱글톤 인스턴스

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지하고 싶으면 사용
        }
        else
        {
            Destroy(gameObject); // 중복 방지
            return;
        }
    }

    public void LoadEnding()
    {
        StartCoroutine(OpenDoorCoroutine());
    }

    //1초 있다가 endingScene으로 전환합니다.
    //우주선 있는거 1초동안 보여줄려고 만들었습니다.
    private IEnumerator OpenDoorCoroutine()
    {

        // 1초 대기
        yield return new WaitForSeconds(1f);

        // 2. 씬 전환
        SceneManager.LoadScene("EndingScene");
    }
}
