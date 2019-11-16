using UnityEngine;

public class CameraPan : MonoBehaviour
{
    Vector3 touchStart;
    [Tooltip("Boundaries for the Pan on the X axis (X = Min, Y = Max)")]
    public Vector2 xAxisBoundaries;
    //public Vector2 yAxisBoundaries;
    [Tooltip("Set CamPos Y & Z to the Camera's current Y & Z")]
    public float CamPosY, CamPosZ, CamPosX;

    void Update()
    {
        Vector3 pos = Camera.main.transform.position;
       // Vector3 posY = Camera.main.transform.position;

        if(Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if(Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos += direction;

            //Vector3 dir = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //posY += dir;
        }

        pos.x = Mathf.Clamp(pos.x, xAxisBoundaries.x, xAxisBoundaries.y);

        pos.y = CamPosY;
        pos.z = CamPosZ;
        //posY.z = Mathf.Clamp(pos.z, yAxisBoundaries.x, yAxisBoundaries.y);
        //
        //
        //posY.x = CamPosX;
        //posY.z = CamPosZ;

        Camera.main.transform.position = pos;
        //Camera.main.transform.position = posY;
    }
}
