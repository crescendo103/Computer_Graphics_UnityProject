using UnityEngine;
using UnityEngine.UI;

public class UISetupPanel : MonoBehaviour
{
    [Header("Toggles")]
    public Toggle northToggle;
    public Toggle eastToggle;
    public Toggle southToggle;
    public Toggle westToggle;

    [Header("Sliders")]
    public Slider radiusSlider;
    public Slider heightSlider;
    public Text radiusValueText;
    public Text heightValueText;

    [Header("References")]
    public CameraOrbitController orbitController;
    public CameraViewManager viewManager;

    // PlayerPrefs keys
    const string KEY_NORTH = "CameraView_UseNorth";
    const string KEY_EAST = "CameraView_UseEast";
    const string KEY_SOUTH = "CameraView_UseSouth";
    const string KEY_WEST = "CameraView_UseWest";

    const string KEY_RADIUS = "CameraView_Radius";
    const string KEY_HEIGHT = "CameraView_Height";

    void Start()
    {
        // 1) PlayerPrefs에서 저장된 설정 불러오기 (있으면 반영)
        LoadPrefsIntoRuntime();

        // 2) UI 초기 상태를 Runtime 값과 동기화
        SyncRuntimeToUI();

        // 3) UI 이벤트 리스너 연결
        HookUIEvents();
    }

    void HookUIEvents()
    {
        if (northToggle != null)
            northToggle.onValueChanged.AddListener(OnNorthChanged);

        if (eastToggle != null)
            eastToggle.onValueChanged.AddListener(OnEastChanged);

        if (southToggle != null)
            southToggle.onValueChanged.AddListener(OnSouthChanged);

        if (westToggle != null)
            westToggle.onValueChanged.AddListener(OnWestChanged);

        if (radiusSlider != null)
            radiusSlider.onValueChanged.AddListener(OnRadiusChanged);

        if (heightSlider != null)
            heightSlider.onValueChanged.AddListener(OnHeightChanged);
    }

    // ========== 불러오기 / 저장하기 ==========

    void LoadPrefsIntoRuntime()
    {
        // 토글: 저장된 값이 있으면 그걸 사용, 없으면 지금 인스펙터 기본값 유지
        if (PlayerPrefs.HasKey(KEY_NORTH))
            viewManager.useNorth = PlayerPrefs.GetInt(KEY_NORTH) == 1;
        if (PlayerPrefs.HasKey(KEY_EAST))
            viewManager.useEast = PlayerPrefs.GetInt(KEY_EAST) == 1;
        if (PlayerPrefs.HasKey(KEY_SOUTH))
            viewManager.useSouth = PlayerPrefs.GetInt(KEY_SOUTH) == 1;
        if (PlayerPrefs.HasKey(KEY_WEST))
            viewManager.useWest = PlayerPrefs.GetInt(KEY_WEST) == 1;

        // radius/height
        if (PlayerPrefs.HasKey(KEY_RADIUS))
            orbitController.radius = PlayerPrefs.GetFloat(KEY_RADIUS);

        if (PlayerPrefs.HasKey(KEY_HEIGHT))
            orbitController.height = PlayerPrefs.GetFloat(KEY_HEIGHT);

        // viewManager 내부 activeAngles 갱신해줘야 함
        viewManager.SetUseNorth(viewManager.useNorth);
        viewManager.SetUseEast(viewManager.useEast);
        viewManager.SetUseSouth(viewManager.useSouth);
        viewManager.SetUseWest(viewManager.useWest);
    }

    void SavePrefsFromRuntime()
    {
        PlayerPrefs.SetInt(KEY_NORTH, viewManager.useNorth ? 1 : 0);
        PlayerPrefs.SetInt(KEY_EAST, viewManager.useEast ? 1 : 0);
        PlayerPrefs.SetInt(KEY_SOUTH, viewManager.useSouth ? 1 : 0);
        PlayerPrefs.SetInt(KEY_WEST, viewManager.useWest ? 1 : 0);

        PlayerPrefs.SetFloat(KEY_RADIUS, orbitController.radius);
        PlayerPrefs.SetFloat(KEY_HEIGHT, orbitController.height);

        PlayerPrefs.Save();
    }

    // ========== UI <-> Runtime 싱크 ==========

    void SyncRuntimeToUI()
    {
        // 토글 UI를 runtime 상태로 맞추기
        if (northToggle != null)
            northToggle.isOn = viewManager.useNorth;
        if (eastToggle != null)
            eastToggle.isOn = viewManager.useEast;
        if (southToggle != null)
            southToggle.isOn = viewManager.useSouth;
        if (westToggle != null)
            westToggle.isOn = viewManager.useWest;

        // 슬라이더 UI를 runtime 값에 맞추기
        if (radiusSlider != null)
            radiusSlider.value = orbitController.radius;
        if (heightSlider != null)
            heightSlider.value = orbitController.height;

        UpdateRadiusText();
        UpdateHeightText();
    }

    // ========== 이벤트 핸들러 (UI 조작 시 호출) ==========

    void OnNorthChanged(bool v)
    {
        viewManager.SetUseNorth(v);
        SavePrefsFromRuntime();
    }

    void OnEastChanged(bool v)
    {
        viewManager.SetUseEast(v);
        SavePrefsFromRuntime();
    }

    void OnSouthChanged(bool v)
    {
        viewManager.SetUseSouth(v);
        SavePrefsFromRuntime();
    }

    void OnWestChanged(bool v)
    {
        viewManager.SetUseWest(v);
        SavePrefsFromRuntime();
    }

    void OnRadiusChanged(float r)
    {
        orbitController.radius = r;
        UpdateRadiusText();
        SavePrefsFromRuntime();
    }

    void OnHeightChanged(float h)
    {
        orbitController.height = h;
        UpdateHeightText();
        SavePrefsFromRuntime();
    }

    void UpdateRadiusText()
    {
        if (radiusValueText != null)
            radiusValueText.text = orbitController.radius.ToString("0.0");
    }

    void UpdateHeightText()
    {
        if (heightValueText != null)
            heightValueText.text = orbitController.height.ToString("0.0");
    }
}
