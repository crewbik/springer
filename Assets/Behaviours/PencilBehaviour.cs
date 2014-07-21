using UnityEngine;
using System.Collections;

public class PencilBehaviour : MonoBehaviour
{
    private Vector3 LastMousePosition{ get; set; }

    public float InkDropletMaxDistance = 0.1f;

    private bool IsDrawing { get; set; }

    private InkwellBehaviour Inkwell { get; set; }

    private Camera Camera{ get; set; }
    
    void Start()
    {
        Inkwell = GameObject.Find("_inkwell").GetComponent<InkwellBehaviour>();
        if (Camera == null)
            Camera = Camera.main;
    }

    void Update(){
        if (Input.touchCount > 0) {
            Debug.Log("Simple " + Input.GetTouch(0).fingerId + " " + Input.GetTouch(0).position);
        }
        if (Input.touchCount > 1) {
            Debug.Log("Simple " + Input.GetTouch(1).fingerId + " " + Input.GetTouch(1).position);
        }
    }

    void OnGUI()
    {

        DrawInk();
        Event e = Event.current;
        switch (e.type)
        {
            case EventType.MouseDown:
                LastMousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
                IsDrawing = true;
                Inkwell.StopAutoRefill();
                break;
            case EventType.MouseUp:
                IsDrawing = false;
                Inkwell.StartAutoRefill();
                break;
        }

    }

    void DrawInk()
    {
        if (IsDrawing) // check if is not overlapping the main ball;
        {
            Vector3 mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
            float distance = Vector3.Distance(LastMousePosition, mousePosition);
            if(distance < InkDropletMaxDistance * 2)
                return;
            float angle = Mathf.Atan2(mousePosition.y - LastMousePosition.y, mousePosition.x - LastMousePosition.x) * Mathf.Rad2Deg;
            int steps = (int)(distance / InkDropletMaxDistance);
            float stepX = (mousePosition.x - LastMousePosition.x) / steps;
            float stepY = (mousePosition.y - LastMousePosition.y) / steps;
            for (int i = 0; i<steps; i++)
            {
                if (!DrawInkDroplet(new Vector2(LastMousePosition.x + stepX * i, LastMousePosition.y + stepY * i), angle))
                {
                    break;
                }
            }
            LastMousePosition = mousePosition;
        }
    }

    bool DrawInkDroplet(Vector2 position, float angle)
    {

        InkDropletController inkDropletController = Inkwell.GetInkController();
        if (inkDropletController != null)
        {
            inkDropletController.AddInk(position, angle);
            return true;
        } else
        {
            IsDrawing = false;
            Inkwell.StartAutoRefill();
            return false;
        }
    }
}
