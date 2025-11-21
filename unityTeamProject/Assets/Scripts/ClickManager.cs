using UnityEngine;
using TMPro;
using System.Collections;

public class ClickManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI bottomText;
    public TextMeshProUGUI topText;
    public GameObject menuPanel;
    public GameObject potCloseButton;
    public GameObject menuCloseButton;

    [Header("3D Prefabs")]
    public GameObject leafPrefab;
    public GameObject meatPrefab;
    public GameObject powderPrefab;
    public GameObject potPrefab;
    public GameObject keyPrefab;

    private bool gotLeaf = false;
    private bool gotMeat = false;
    private bool gotPowder = false;
    private GameObject currentObj;

    public GameObject DoorGate;
    private bool doorgateflipflop = false;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                string name = hit.collider.name;
                Debug.Log("Clicked: " + name);

                switch (name)
                {
                    case "VentA":
                        SpawnPickup(leafPrefab, "You obtained a glowing leaf.");
                        gotLeaf = true;
                        break;

                    case "Beam":
                        SpawnPickup(meatPrefab, "You obtained a shining chunk.");
                        gotMeat = true;
                        break;

                    case "Cabinet":
                        SpawnPickup(powderPrefab, "You obtained unknown white powder.");
                        gotPowder = true;
                        break;

                    case "Pot":
                        ShowPot();
                        break;

                    case "MenuBoard":
                        ShowMenu();
                        break;

                    case "VentB":
                        ShowText("", "A normal ventilation duct.");
                        break;

                    case "Table":
                        ShowText("", "A sturdy-looking table.");
                        break;

                    case "Chair":
                        ShowText("", "A chair that doesn't look very comfortable.");
                        break;

                    case "Food_Empty_Cup":
                        ShowText("", "An unknown liquid, left half-drunk by someone.");
                        break;

                    case "TableFood":
                        ShowText("", "Something that looks like a carrot.");
                        break;
                    case "DoorGate":
                        //ShowText("", "Something that looks like a carrot.");
                        DoorGate gateScript = hit.collider.gameObject.GetComponent<DoorGate>();//스크립트 가져오기
                        if (!doorgateflipflop)
                        {
                            gateScript.Open();
                            doorgateflipflop = true;
                        }
                        else
                        {
                            gateScript.Close();
                            doorgateflipflop = false;
                        }
                        break;

                    default:
                        Debug.Log("Clicked object is nothing special");
                        break;
                }
            }
        }
    }

    void SpawnPickup(GameObject prefab, string message)
    {
        if (currentObj != null) Destroy(currentObj);

        // Slightly below screen center
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2 - Screen.height * 0.1f, 0));
        Vector3 spawnPos = ray.GetPoint(2f);
        Quaternion spawnRot = Quaternion.LookRotation(Camera.main.transform.forward);

        currentObj = Instantiate(prefab, spawnPos, spawnRot);
        currentObj.transform.localScale = Vector3.one * 5f;

        ShowText("", message);

        if (prefab != potPrefab)
            currentObj.AddComponent<PickupFloatAndFade>();
    }

    void ShowPot()
    {
        if (currentObj != null) Destroy(currentObj);

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2 - Screen.height * 0.1f, 0));
        Vector3 spawnPos = ray.GetPoint(2f);
        Quaternion spawnRot = Quaternion.LookRotation(Camera.main.transform.forward);

        currentObj = Instantiate(potPrefab, spawnPos, spawnRot);
        currentObj.transform.localScale = Vector3.one * 5f;
        ShowText("Unknown liquid is boiling...", "");

        potCloseButton.SetActive(true);

        if (gotLeaf && gotMeat && gotPowder)
            StartCoroutine(ShowKeyAfterDelay());
    }

    void ShowMenu()
    {
        menuPanel.SetActive(true);
        menuCloseButton.SetActive(true);
    }

    IEnumerator ShowKeyAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        if (currentObj != null) Destroy(currentObj);

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2 - Screen.height * 0.1f, 0));
        Vector3 spawnPos = ray.GetPoint(4.5f);
        Quaternion spawnRot = Quaternion.LookRotation(Camera.main.transform.forward);

        currentObj = Instantiate(keyPrefab, spawnPos, spawnRot);
        currentObj.transform.localScale = Vector3.one * 5f;

        // ✨ Key special appearance effect
        currentObj.AddComponent<KeyAppearEffect>();

        ShowText("A key slowly rises...", "");
    }

    // -----------------------------
    // Text fade control
    // -----------------------------
    void ShowText(string topMsg, string bottomMsg)
    {
        topText.text = topMsg;
        bottomText.text = bottomMsg;
        SetTextAlpha(1f);
        StopAllCoroutines();
        StartCoroutine(HideTextAfterDelay(1f)); // shorter duration!
    }

    IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        for (float t = 0; t < 1f; t += Time.deltaTime * 2f)
        {
            float a = Mathf.Lerp(1f, 0f, t);
            SetTextAlpha(a);
            yield return null;
        }
        SetTextAlpha(0f);
    }

    void SetTextAlpha(float a)
    {
        Color topC = topText.color;
        Color botC = bottomText.color;
        topC.a = a;
        botC.a = a;
        topText.color = topC;
        bottomText.color = botC;
    }

    public void ClosePot()
    {
        if (currentObj != null) Destroy(currentObj);
        potCloseButton.SetActive(false);
        ShowText("", "");
    }

    public void CloseMenu()
    {
        menuPanel.SetActive(false);
        menuCloseButton.SetActive(false);
        ShowText("", "");
    }
}
