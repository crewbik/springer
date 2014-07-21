using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour
{
    
    public Camera Camera{ get; set; }
    
    private float? LastPositionX{ get; set; }
    
    private float LastVerticalDistance { get; set; }

    private float MinLastVerticalDistance { get; set; }
    
    void Start()
    {
        if (Camera == null)
            Camera = Camera.main;

        MinLastVerticalDistance = (float)GameVariables.Instance.CameraPhase / 33;
    }
    
    public void SetPosition(Vector3 position)
    {
        if (LastPositionX == null)
        {
            LastPositionX = position.x;
        }

        float currentVerticalDistance = position.x - LastPositionX.Value;
        float? newPositionX = null;
        if (LastVerticalDistance * 0.99 > currentVerticalDistance)
        {
            LastVerticalDistance = (LastVerticalDistance < MinLastVerticalDistance ? MinLastVerticalDistance : LastVerticalDistance) * 0.99f;
            newPositionX = LastVerticalDistance + Camera.transform.position.x;
        } 

        if (LastVerticalDistance != 0 && LastVerticalDistance * 1.01 < currentVerticalDistance)// && LastVerticalDistance * 1.02 > currentVerticalDistance)
        {
            LastVerticalDistance = currentVerticalDistance * 1.01f;
            newPositionX = LastVerticalDistance + Camera.transform.position.x;
        }
        if (newPositionX == null)
        {
            LastVerticalDistance = currentVerticalDistance;
            newPositionX = position.x;
        }
        Camera.transform.position = new Vector3(newPositionX.Value, Camera.transform.position.y, Camera.transform.position.z);
        LastPositionX = newPositionX;
    }
}
