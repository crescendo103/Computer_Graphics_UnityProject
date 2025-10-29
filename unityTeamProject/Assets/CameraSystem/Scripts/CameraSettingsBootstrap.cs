using UnityEngine;

public class CameraSettingsBootstrap : MonoBehaviour
{
    [Header("References")]
    public CameraOrbitController orbitController;
    public CameraViewManager viewManager;

    // PlayerPrefs keys (UISetupPanel과 동일하게 유지)
    const string KEY_NORTH = "CameraView_UseNorth";
    const string KEY_EAST = "CameraView_UseEast";
    const string KEY_SOUTH = "CameraView_UseSouth";
    const string KEY_WEST = "CameraView_UseWest";
    const string KEY_RADIUS = "CameraView_Radius";
    const string KEY_HEIGHT = "CameraView_Height";

    void Awake()
    {
        // 1) 방향 활성 여부 복원
        if (PlayerPrefs.HasKey(KEY_NORTH))
            viewManager.useNorth = PlayerPrefs.GetInt(KEY_NORTH) == 1;
        if (PlayerPrefs.HasKey(KEY_EAST))
            viewManager.useEast = PlayerPrefs.GetInt(KEY_EAST) == 1;
        if (PlayerPrefs.HasKey(KEY_SOUTH))
            viewManager.useSouth = PlayerPrefs.GetInt(KEY_SOUTH) == 1;
        if (PlayerPrefs.HasKey(KEY_WEST))
            viewManager.useWest = PlayerPrefs.GetInt(KEY_WEST) == 1;

        // 2) 카메라 거리/높이 복원
        if (PlayerPrefs.HasKey(KEY_RADIUS))
            orbitController.radius = PlayerPrefs.GetFloat(KEY_RADIUS);

        if (PlayerPrefs.HasKey(KEY_HEIGHT))
            orbitController.height = PlayerPrefs.GetFloat(KEY_HEIGHT);

        // 3) viewManager 내부 리스트 재빌드 (현재 각도 목록 갱신)
        viewManager.SetUseNorth(viewManager.useNorth);
        viewManager.SetUseEast(viewManager.useEast);
        viewManager.SetUseSouth(viewManager.useSouth);
        viewManager.SetUseWest(viewManager.useWest);
    }
}
