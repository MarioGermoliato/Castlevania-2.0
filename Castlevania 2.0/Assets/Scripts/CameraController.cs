using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    public Transform playerTransform;

    public Transform limitCamRight;
    public Transform limitCamLeft;
    public float speedCam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        CamFollow();
    }

    private void CamFollow()
    {
        float posCamX = playerTransform.position.x;

        if (cam.transform.position.x < limitCamLeft.position.x && playerTransform.position.x < limitCamLeft.position.x)
        {
            posCamX = limitCamLeft.position.x;
        }
        else if(cam.transform.position.x > limitCamRight.position.x && playerTransform.position.x > limitCamRight.position.x)
        {
            posCamX = limitCamRight.position.x;
        }

        Vector3 posCam = new Vector3(posCamX, cam.transform.position.y, cam.transform.position.z);

        cam.transform.position = Vector3.Lerp(cam.transform.position, posCam, speedCam * Time.deltaTime);
    }
}
