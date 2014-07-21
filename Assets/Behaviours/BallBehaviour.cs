using UnityEngine;
using System.Collections;

public class BallBehaviour : MonoBehaviour
{
    public int Level = 1;

    private CameraBehaviour MainCamera { get; set; }

    private float MaxSpeed{ get; set; }

    private float Acceleration { get; set; }

    void Start()
    {
        rigidbody2D.mass = GameVariables.Instance.BallMass;
        MainCamera = GameObject.Find("mainCamera").GetComponent<CameraBehaviour>();
        MaxSpeed = GameVariables.Instance.BallMaxSpeed;
        Acceleration = GameVariables.Instance.BallAcceleration;
    }
    
    void FixedUpdate()
    {
        MainCamera.SetPosition(transform.position);
        if (transform.position.y != 0)
            rigidbody2D.gravityScale = transform.position.y > 0 ? Mathf.Ceil(transform.position.y / 3) : Mathf.Floor(transform.position.y * 3);

        if (rigidbody2D.velocity.x > MaxSpeed + MaxSpeed / 10)
            rigidbody2D.AddForce(new Vector3(-Acceleration/10, 0f, 0f));
    }

    void OnBecameInvisible()
    {
        if (transform.position.x < MainCamera.Camera.transform.position.x - 5) // scrren width / 2 
            Application.LoadLevel(Application.loadedLevel);
    }

    void OnCollisionEnter2D(Collision2D hit){
        if (hit.gameObject.tag == "Lava")
        {
            Application.LoadLevel (Application.loadedLevel);
        }
    }

    void OnCollisionExit2D(Collision2D hit)
    {
        if (hit.gameObject.tag == "InkDroplet" || hit.gameObject.tag == "SpeedingWall")
            Accelerate();

    }

    void OnCollisionStay2D(Collision2D hit)
    {
        if (hit.gameObject.tag == "InkDroplet" || hit.gameObject.tag == "SpeedingWall")
            Accelerate();
    }

    private void Accelerate()
    {
        if (rigidbody2D.velocity.x < MaxSpeed)
        {
            rigidbody2D.AddForce(new Vector3(Acceleration, 0f, 0f));
        }
    }
}
