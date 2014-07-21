using UnityEngine;
using System.Collections;

public class GameVariables : MonoBehaviour {

    public int InkAmount = 500;
    public int InkRegenerationRate = 100;
    public float BallMass = 1;
    public int BallMaxSpeed = 10;
    public int BallAcceleration = 10;
    public int CameraPhase = 5;
    public static GameVariables Instance;

    GameVariables(){
        Instance = this;
    }
}
