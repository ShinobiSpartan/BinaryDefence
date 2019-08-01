using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region VariablesPC
    private bool isMoving = true;

    public float panSpeed = 35.0f;
    public float panBorderThick = 20.0f;

    public float scrollSpeed;
    public float minY;
    public float maxY;
    #endregion

    #region VariablesANDRIOD
    public float zoomSpeed = 0.5f; //zoom out min
    public float orthoZoomSpeed = 0.5f;    //zoom out max
    public Camera cam;
    #endregion

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
       
        if (Input.touchCount == 2)
        {
            //Touch Zero
            Touch tZero = Input.GetTouch(0);
            //Touch 1
            Touch tOne = Input.GetTouch(1);

            //Touching previous positions
            Vector2 zPrevPos = tZero.position - tZero.deltaPosition;
            Vector2 oPrevPos = tOne.position - tOne.deltaPosition;

            //previous magitude 
            float prevMag = (zPrevPos - oPrevPos).magnitude;
            //current magitude
            float currentMag = (tZero.position - tOne.position).magnitude;

            float difference = currentMag - prevMag;

            if(cam.orthographic)
            {
                cam.orthographicSize += difference * orthoZoomSpeed;
            }
            else
            {
                cam.fieldOfView += difference * zoomSpeed;
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 0.1f, 179.9f);

            }

        }
    }


    void cameraMovement()
    {
        #region Camera Lock
        //locking the camera if escape is pressed :)
        if (Input.GetKeyDown(KeyCode.Escape))
            isMoving = !isMoving;

        if (!isMoving)
            return;
        #endregion

        #region Camera MovementPC
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThick)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <=  panBorderThick)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThick)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThick)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        #endregion
        
        #region ScrollWheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        
        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
       
        Debug.Log(scroll);
        #endregion

    }


}
