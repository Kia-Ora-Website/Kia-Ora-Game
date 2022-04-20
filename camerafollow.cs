/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class camerafollow : MonoBehaviour
{

    public CinemachineFreeLook cameraLook;

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            cameraLook.m_YAxis.m_InputAxisName = "Mouse Y";
            cameraLook.m_XAxis.m_InputAxisName = "Mouse X";
        }
        else
        {
            cameraLook.m_YAxis.m_InputAxisName = "";
            cameraLook.m_XAxis.m_InputAxisName = "";
        }
    }
}
