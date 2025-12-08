using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using TMPro;

public class KeypadUIController : MonoBehaviour
{
    [Header("UI References")]
    public GameObject root;                  // Keypad UI 전체
    public TextMeshProUGUI displayText;      // ---- , 숫자표시

    private string currentInput = "";
    private const int MaxDigits = 4;

    private string targetCode = "";

    private Action onCorrectCallback;

    private void Start()
    {
        if (root != null) root.SetActive(false);
        if (displayText != null) displayText.text = "----";
    }

    public void OpenUI(string correctCode, Action correctCallback)
    {
        targetCode = correctCode;
        onCorrectCallback = correctCallback;

        currentInput = "";
        displayText.text = "----";

        root.SetActive(true);
    }

    public void CloseUI()
    {
        root.SetActive(false);
    }

    // 숫자버튼에서 호출
    public void PressNumber(string num)
    {
        if (currentInput.Length >= MaxDigits)
            return;

        currentInput += num;

        string shown = currentInput.PadRight(MaxDigits, '-');
        displayText.text = shown;

        if (currentInput.Length == MaxDigits)
            StartCoroutine(AutoEnter());
    }

    private IEnumerator AutoEnter()
    {
        yield return new WaitForSeconds(0.1f);
        Enter();
    }

    public void Enter()
    {
        if (currentInput == targetCode)
        {
            StartCoroutine(ShowOpen());
        }
        else
        {
            StartCoroutine(ShowError());
        }
    }

    private IEnumerator ShowOpen()
    {
        displayText.text = "OPEN";

        yield return new WaitForSeconds(0.5f);

        CloseUI();

        onCorrectCallback?.Invoke();
    }

    private IEnumerator ShowError()
    {
        displayText.text = "ERROR";

        yield return new WaitForSeconds(0.5f);

        currentInput = "";
        displayText.text = "----";
    }
}
