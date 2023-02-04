using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Master master;
    GameObject camera;

    private Vector3 initCamPos;
    bool fullScreen = true;
    // Start is called before the first frame update
    void Start()
    {
        master = GameObject.FindGameObjectsWithTag("Master")[0].GetComponent<Master>();
        camera = transform.GetChild(0).gameObject;
        initCamPos = camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MouseClick();
        if (fullScreen == false && Input.GetKeyDown(KeyCode.Space))
        {
            camera.transform.position = new(10, 4.45f, -10);
            camera.GetComponent<Camera>().orthographicSize = 5;
            fullScreen = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            camera.transform.position = new(10.1f, 2.0f, -10);
            camera.GetComponent<Camera>().orthographicSize = 2.5f;
            fullScreen = false;
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
