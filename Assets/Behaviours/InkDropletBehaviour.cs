using UnityEngine;
using System.Collections;

public class InkDropletBehaviour : MonoBehaviour
{
    void OnBecameInvisible()
    {
        CancelInvoke("Disable");
        Disable();
    }

    void OnEnable(){
        Invoke("Disable", 1.3f);
    }

    void OnDisable(){
        CancelInvoke("Disable");
    }

    private void Disable(){
        gameObject.SetActive(false);
    }
}
