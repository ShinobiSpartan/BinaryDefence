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

    public GameObject GameObjectCamera;

    Vector2 StartPos;
    Vector2 StartDragPos;
    Vector2 NewDragPos;
    Vector2 FingerPos;

    private float DistanceBetweenFingers;
    private bool ZoomInOut;

    #endregion


    void Update()
    {
        cameraMovementPhone();
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

    void cameraMovementPhone()
    {
        //checking for input in touchcount is equal to 0 and for zooming in and out
        //set ZoomInOut to false
        if(Input.touchCount == 0 && ZoomInOut)
        {
            ZoomInOut = false;
        }

        if(Input.touchCount == 1)
        {
            if(!ZoomInOut)
            {
                if (Input.GetTouch(1).phase == TouchPhase.Moved)
                {
                    Vector2 NewPos = WorldPos();
                    Vector2 PosDifference = NewPos - StartPos;
                    GameObjectCamera.transform.Translate(-PosDifference);
                }
                StartPos = WorldPos();
            }
        }
        else if (Input.touchCount == 2)
        {
            if (Input.GetTouch(1).phase ==TouchPhase.Moved)
            {
                ZoomInOut = true;

                NewDragPos = WorldPosFinger(1);
                Vector2 positionDifference = NewDragPos - StartDragPos;

                if(Vector2.Distance(NewDragPos, FingerPos) < DistanceBetweenFingers)
                {
                    GameObjectCamera.GetComponent<Camera>().orthographicSize += (positionDifference.magnitude);
                }
                if (Vector2.Distance(NewDragPos, FingerPos) >= DistanceBetweenFingers)
                {
                    GameObjectCamera.GetComponent<Camera>().orthographicSize -= (positionDifference.magnitude);
                }

                DistanceBetweenFingers = Vector2.Distance(NewDragPos, FingerPos);

            }

            StartDragPos = WorldPosFinger(1);
            FingerPos = WorldPosFinger(0);

        }
                              
    }

    Vector2 WorldPos()
    {
        return GameObjectCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
    }
    
    Vector2 WorldPosFinger(int FingerIndex)
    {
        return GameObjectCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.GetTouch(FingerIndex).position);
    }


}
