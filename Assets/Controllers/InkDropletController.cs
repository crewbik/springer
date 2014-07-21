using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class InkDropletController
{
    public int Load{ get { return GameObjects.Where(go => go.activeInHierarchy).Count(); } }
    private List<GameObject> GameObjects;
    public bool IsAvailable { get ; set; }

    public InkDropletController(GameObject gameObject)
    {
        GameObjects = new List<GameObject>();
        GameObjects.Add((GameObject)Object.Instantiate(gameObject));
        IsAvailable = true;
    }

    public void AddInk(Vector2 position, float angle)
    {
        if (!IsAvailable)
        {
            return;
        }
        Block();
        GameObject gameObject = GameObjects.FirstOrDefault(go => !go.activeInHierarchy); 
        if (gameObject == null)
        {
            gameObject = (GameObject)Object.Instantiate(GameObjects.First());
            GameObjects.Add(gameObject);
        }
        gameObject.SetActive(true);
        gameObject.transform.position = new Vector3(position.x, position.y, 0f);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Block()
    {
        IsAvailable = false;
    }

    public void Free()
    {
        IsAvailable = true;

    }

}

