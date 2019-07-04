using UnityEngine;

public class CameraController : MonoBehaviour
{

    private bool isMoving = true;

    public float panSpeed = 35.0f;
    public float panBorderThick = 20.0f;

    public float scrollSpeed;
    public float minY;
    public float maxY;

   void Update()
   {
        cameraMovement();
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

        #region Camera Movement
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
