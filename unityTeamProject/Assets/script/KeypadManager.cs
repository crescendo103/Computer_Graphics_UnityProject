using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeypadManager : MonoBehaviour
{
    [Tooltip("비밀번호가 맞았을 때 열릴 문 오브젝트")]
    public GameObject doorToOpen;

    [Tooltip("사라지게 할 최상위 부모 KeyPad 오브젝트")]
    public GameObject keyPadParent; // 'KeyPad' 부모를 연결할 변수

    private string currentInput = "";
    private string correctPassword = "5197";

    // 1. 숫자 버튼을 눌렀을 때
    public void AddDigit(string digit)
    {
        if (currentInput.Length >= 10) return;
        currentInput += digit;
    }

    // 2. Back 버튼을 눌렀을 때
    public void Backspace()
    {
        if (currentInput.Length > 0)
        {
            currentInput = currentInput.Substring(0, currentInput.Length - 1);
        }
    }

    // 3. Enter 버튼을 눌렀을 때
    public void CheckPassword()
    {
        if (currentInput == correctPassword)
        {
            // 정답일 때
            Debug.Log("비밀번호 정답! 문을 엽니다.");
            if (doorToOpen != null)
            {
                EndingSceneLoad.Instance.LoadEnding();
                doorToOpen.SetActive(false); // 1. 문 비활성화
                
            }

            // 2. 'KeyPad' 부모 오브젝트를 비활성화
            if (keyPadParent != null)
            {
                keyPadParent.SetActive(false);
                
            }
        }
        else
        {
            // 오답일 때
            Debug.Log("비밀번호 오류! 초기화합니다.");
            ClearInput();
        }
    }

    // 4. 입력 초기화
    private void ClearInput()
    {
        currentInput = "";
    }

    

}