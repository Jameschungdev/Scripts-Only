using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// These manages the rendering of the 2 arms and 1 prismatic joint end-effector of the robot.
/// </summary>
public class ArmRenderer : MonoBehaviour
{

    // Reference to arm 1, 2, 3 objects and renderer components
    public GameObject a1Object;
    public GameObject a2Object;
    public GameObject a3Object;
    public LineRenderer a1Line; // This must be public as it is updated by the SimulationManager
    public LineRenderer a2Line;
    public LineRenderer a3Line;




    // Start is called before the first frame update
    void Start()
    {
        // Link to line rendering components and initialization,
        a1Line = a1Object.GetComponent<LineRenderer>();
        a1Line.positionCount = 2;
        a1Line.alignment = LineAlignment.View;
        a1Line.startWidth = a1Line.endWidth = 0.1f;


        a2Line = a2Object.GetComponent<LineRenderer>();
        a2Line.positionCount = 2;
        a2Line.alignment = LineAlignment.View;
        a2Line.startWidth = a2Line.endWidth = 0.1f;


        a3Line = a3Object.GetComponent<LineRenderer>();
        a3Line.positionCount = 2;
        a3Line.alignment = LineAlignment.View;
        a3Line.startWidth = a3Line.endWidth = 0.1f;
    }

}
