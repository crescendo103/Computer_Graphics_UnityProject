using System.Xml.Linq;
using UnityEngine;

//문 열고닫는 기능만 구현하려고 제가 임시로 구현했어요 수정 필요하시면 수정하시면 됩니다.
public class RoomManager : MonoBehaviour
{
    public GameObject Gate;
    public Camera mainCamera;
    private bool doorgateflipflop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorgateflipflop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (mainCamera == null)
            {
                return;
            }

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //  targetObject 클릭한 경우
                if (hit.collider.gameObject == Gate)
                {
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
                }
            }
        }
    }
}
