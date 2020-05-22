using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleTransition : MonoBehaviour
{    [SerializeField]
    private GameObject CharWalk;
    
    public  GameObject CharPlayer;
    
    public GameObject Door;
    

    private CameraController _CameraController;

    // Start is called before the first frame update
    void Start()
    {
        _CameraController = FindObjectOfType(typeof(CameraController)) as CameraController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Door.SetActive(true);
        _CameraController.limitCamRight = _CameraController.limitCamRight2;
        Destroy(collision.gameObject);
        CharWalk.SetActive(true);
        _CameraController.playerTransform = Door.transform;

        StartCoroutine("CamTran");
        StartCoroutine("DoorClose");

        
    }
    

    IEnumerator DoorClose()
    {
        yield return new WaitForSeconds(3.5f);

        Door.SetActive(false);
        CharWalk.SetActive(false);               
        CharPlayer.SetActive(true);        
        
    }
    IEnumerator CamTran()
    {
        yield return new WaitForSeconds(2.5f);
        _CameraController.limitCamLeft = _CameraController.limitCamLeft2;
    }
}
