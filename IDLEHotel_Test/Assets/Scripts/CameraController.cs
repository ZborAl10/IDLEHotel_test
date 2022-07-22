using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Vector2 border1;
    public Vector2 border2;

    public float ZoomMin;
    public float ZoomMax;
    public float Sensitivity;

    private Vector3 startPos;
    private Camera cam;

    private Touch TouchA;
    private Touch TouchB;
    private Vector2 TouchADir;
    private Vector2 TouchBDir;
    private float dstBtwTouchesPosition;
    private float dstBtwTouchesDirection;
    private float zoom;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        Debug.Log(cam.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {
            TouchA = Input.GetTouch(0);
            TouchB = Input.GetTouch(1);
            TouchADir = TouchA.position - TouchA.deltaPosition;
            TouchBDir = TouchB.position - TouchB.deltaPosition;

            dstBtwTouchesPosition = Vector2.Distance(TouchA.position, TouchB.position);
            dstBtwTouchesDirection = Vector2.Distance(TouchADir, TouchBDir);

            zoom = dstBtwTouchesPosition - dstBtwTouchesDirection;

            var currentZoom = cam.orthographicSize - zoom * Sensitivity;

            cam.orthographicSize = Mathf.Clamp(currentZoom, ZoomMin, ZoomMax);

            //if (TouchBDir != TouchB.position)

        }

        else
        {
            if (Input.GetMouseButtonDown(0)) {
                startPos = cam.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 pos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x - startPos.x,
                    cam.ScreenToWorldPoint(Input.mousePosition).y - startPos.y,
                    cam.ScreenToWorldPoint(Input.mousePosition).x - startPos.x);
                //if (transform.position.x <= border1.x && transform.position.z >= border1.y)
                transform.position = new Vector3((transform.position.x - pos.x), transform.position.y, (transform.position.z - pos.z));
            }
        }
    }
}
