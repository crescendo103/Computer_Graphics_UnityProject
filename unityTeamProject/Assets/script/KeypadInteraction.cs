using UnityEngine;

public class KeypadInteraction : MonoBehaviour
{
    private Camera _detailCamera;

    void Start()
    {
        _detailCamera = GetComponent<Camera>();
    }

    void Update()
    {
        // 줌인 상태에서 마우스 왼쪽 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _detailCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 레이저가 "KeypadButton" 태그를 가진 것에 맞았다면
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("KeypadButton"))
            {
                // 맞은 버튼의 부모에게서 KeypadManager 스크립트를 찾음
                KeypadManager keypad = hit.collider.GetComponentInParent<KeypadManager>();

                if (keypad != null)
                {
                    // 버튼 이름에 따라 KeypadManager의 함수를 호출
                    string buttonName = hit.collider.gameObject.name;

                    switch (buttonName)
                    {
                        case "Button_1": keypad.AddDigit("1"); break;
                        case "Button_2": keypad.AddDigit("2"); break;
                        case "Button_3": keypad.AddDigit("3"); break;
                        case "Button_4": keypad.AddDigit("4"); break;
                        case "Button_5": keypad.AddDigit("5"); break;
                        case "Button_6": keypad.AddDigit("6"); break;
                        case "Button_7": keypad.AddDigit("7"); break;
                        case "Button_8": keypad.AddDigit("8"); break;
                        case "Button_9": keypad.AddDigit("9"); break;
                        case "Button_0": keypad.AddDigit("0"); break;
                        case "Button_Enter": keypad.CheckPassword(); break;
                        case "Button_Back": keypad.Backspace(); break;
                    }
                }
            }
        }
    }
}