using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region VariablesPC
    [Header("PC Variables")]
    private bool isMoving = true;

    public float panSpeed = 35.0f;
    public float panBorderThick = 20.0f;

    public float scrollSpeed;
    //zoom in and out
    public float minY;
    public float maxY;
    #endregion
    #region VariablesANDROID
    Vector3 startTouch;
    //min and max zoom
    public float minZoom;
    public float maxZoom;
    //Orthographic zoom in and out
    public float zoomOutMin;
    public float zoomOutMax;
    #endregion

    //private void LateUpdate()
    //{
    //    if(Input.mousePosition.x >= Screen.width - panBorderThick)
    //    {
    //        transform.position.x = Mathf.Clamp(transform.position.x, -100, 100);
    //        transform.position += transform.right * Time.deltaTime * panSpeed; 
    //    }
    //    if(Input.mousePosition.x <= 0 + panBorderThick)
    //    {
    //        transform.position -= transform.right * Time.deltaTime;
    //    }
    //    if(Input.mousePosition.y >=Screen.height - panBorderThick)
    //    {
    //        transform.position += transform.right * Time.deltaTime * panSpeed;
    //    }
    //    if(Input.mousePosition.y <= 0 + panBorderThick)
    //    {
    //        transform.position -= transform.right * Time.deltaTime;
    //    }
    //
    //}


    void Update()
    {
       if(Input.GetMouseButtonDown(0))
       {
            startTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       }

       if(Input.touchCount == 2)
       {
            //setting the first and second touch
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);
            
            //setting the prevous first and second touch to their first and second touch position
            //& taking their deltaPosition away from their position
            Vector2 prevousTouchFirst = firstTouch.position - firstTouch.deltaPosition;
            Vector2 prevousTouchSecond = secondTouch.position - secondTouch.deltaPosition;
            
            //setting the magnitudes for the current and previous magnitudes
            float previouseMag = (firstTouch.position - secondTouch.position).magnitude;
            float currentMag = (prevousTouchFirst - prevousTouchSecond).magnitude;

            //setting the difference to currentMag - previousMag
            float difference = currentMag - previouseMag;
            //getting the zoom difference for the magnitudes of the first and second touch positions
            //and * by 0.01
            zoom(difference * 0.01f);
       }
       else if(Input.GetMouseButton(0))
       {
            Vector3 dir = startTouch - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += dir;
       }

        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }
    
    /// <summary>
    /// Zooming increment
    /// </summary>
    /// <param name="increment"></param>
    void zoom (float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
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
       
        // Debug.Log(scroll);
        #endregion

    }


}
