using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region VariablesPC
    [Header("PC Variables")]
    private bool isMoving = true;

    public float panSpeed = 35.0f;
    public float panBorderThick = 20.0f;

    public float scrollSpeed;
    public float minY;
    public float maxY;
    #endregion
    #region VariablesANDRIOD
    Vector3 startTouch;
    [Header("Andriod Variables")]
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    #endregion
        

    void Update()
    {
        cameraMovement();
        if (Input.GetMouseButtonDown(0))
        {
            startTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = startTouch - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void zoom(float increment)
    {
        //Camera.main.orthographicSize = Mathf.Clamp(Camera.main.p - increment, zoomOutMin, zoomOutMax);
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
