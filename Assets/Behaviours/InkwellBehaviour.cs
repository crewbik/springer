using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InkwellBehaviour : MonoBehaviour
{
    public Texture InkWellTexture;
    public GameObject InkDropletGameObject;
    private int MaxAmount;
    private int ReservedAmount;
    private int RegenerationRate;
    private List<InkDropletController> InkDropletControllers;

    private int CurentInkAmount
    {
        get{ return InkDropletControllers.Where(d => d.IsAvailable).Count(); }
    }

    private bool IsLow { get; set; }

    void Start()
    {
        MaxAmount = GameVariables.Instance.InkAmount;
        RegenerationRate = GameVariables.Instance.InkRegenerationRate;
        ReservedAmount = MaxAmount / 10;

        InkDropletControllers = new List<InkDropletController>();
        for (int i=0; i<MaxAmount; i++)
        {
            InkDropletController inkDropletController = new InkDropletController(InkDropletGameObject);
            InkDropletControllers.Add(inkDropletController);
        }
        StartAutoRefill();
    }

    private void RefilInkwell()
    {
        InkDropletController inkDropletController = InkDropletControllers.FirstOrDefault(o => !o.IsAvailable);
        if (inkDropletController != null)
        {
            inkDropletController.Free();
            if (InkDropletControllers.Where(c => c.IsAvailable).Count() > ReservedAmount)
                IsLow = false;
        }
    }

    public void StopAutoRefill()
    {
        CancelInvoke();
    }

    public void StartAutoRefill()
    {
        RefilInkwell();
        InvokeRepeating("RefilInkwell", 0f, 1f / RegenerationRate);
    }

    public InkDropletController GetInkController()
    {
        InkDropletController inkDropletController = InkDropletControllers.OrderBy(c => c.Load).FirstOrDefault(c => c.IsAvailable);

        if (inkDropletController == null)
            IsLow = true;
        return !IsLow ? inkDropletController : null;
    }

    void OnGUI()
    {
        float totalObjects = MaxAmount;
        float remainingObject = CurentInkAmount;
        float totalObjectsWidth = Screen.width / 4;
        float remainingObjectWidth = totalObjectsWidth * (remainingObject / totalObjects);
        GUI.DrawTexture(new Rect(Screen.width * 3 / 4 - 10, 8, remainingObjectWidth, 12), InkWellTexture, ScaleMode.StretchToFill, true, 1.0f);
    }
}