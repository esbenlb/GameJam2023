using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private Master master;
    GameObject camera;

    private Vector3 initCamPos;
    private Transform lastPlayerPos;
    bool fullScreen = true;

    bool trackPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        vcam = GameObject.FindGameObjectWithTag("CinemachineVCam").GetComponent<CinemachineVirtualCamera>();
        master = GameObject.FindGameObjectsWithTag("Master")[0].GetComponent<Master>();
        camera = transform.GetChild(0).gameObject;
        initCamPos = camera.transform.position;
        lastPlayerPos = GameObject.FindGameObjectWithTag("TreeCam").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(trackPlayer == true){
            if(GameObject.FindGameObjectWithTag("Player") != null)
                vcam.Follow = GameObject.FindGameObjectWithTag("Player").transform;
            else
                vcam.Follow = lastPlayerPos;
        }
        if(trackPlayer == false && GameObject.FindGameObjectWithTag("Player") == null)
            vcam.Follow = GameObject.FindGameObjectWithTag("TreeCam").transform;
        else if(GameObject.FindGameObjectWithTag("Player") != null)
            vcam.Follow = GameObject.FindGameObjectWithTag("Player").transform;
        if(GameObject.FindGameObjectWithTag("Player") != null)
            lastPlayerPos = GameObject.FindGameObjectWithTag("PlayerVCam").transform;
        MouseClick();
        if(Input.GetKeyDown(KeyCode.Space))
            trackPlayer = !trackPlayer;
        if (fullScreen == false && Input.GetKeyDown(KeyCode.Space))
        {
            fullScreen = true;
            camera.GetComponent<Camera>().orthographicSize = 5;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            fullScreen = false;
            camera.GetComponent<Camera>().orthographicSize = 2.5f;
        }
        
    }
    void MouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                
                Resources resource;
                if (hit.collider.GetComponent<Resources>())
                {
                    
                    resource = hit.collider.GetComponent<Resources>();
                    if (resource.CurrentResourceType == Resources.ResourseType.SpawingPoint)
                    {
                        Instantiate(master.snake, hit.transform.position, Quaternion.identity);
                    }
                }
            }
        }
    }
}
