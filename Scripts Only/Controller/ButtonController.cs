using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This handles the GUI buttons and sliders actions.
/// </summary>
public class ButtonController : MonoBehaviour {




    // Link to InputPath Renderer
    public GameObject inputPathObject;
    InputPathRenderer inputPathRender;

    // Instantiate a testGenerator object.
    TestGenerator testGenerator = new TestGenerator();

    // Link to Simulation Manager
    public GameObject simulationManagerObject;
    SimulatorManager simulatorManager;




    // Start is called before the first frame update
    void Start() {
        // Grab Input Path Renderer Component
        inputPathRender = inputPathObject.GetComponent<InputPathRenderer>();

        // Grab Simulation Manager Component
        simulatorManager = simulationManagerObject.GetComponent<SimulatorManager>();
    }



    /// <summary>
    /// These button functions stops all simulations, generates test data and runs the simulation on that data. 
    /// </summary>
    public void Start_Test_Simulation_1() {
        StopAllCoroutines();
        float[,] testPath = testGenerator.Generate_circle_test_path(0, 0, 2.0f, 3.0f);
        inputPathRender.RenderLines(testPath);
        StartCoroutine(simulatorManager.Simulator(testPath));
    }
    public void Start_Test_Simulation_2() {
        StopAllCoroutines();
        float[,] testPath = testGenerator.Generate_squiggly_circle_test_path(0, 0, 2.0f, 3.0f);
        inputPathRender.RenderLines(testPath);
        StartCoroutine(simulatorManager.Simulator(testPath));
    }
    public void Start_Test_Simulation_3() {
        StopAllCoroutines();
        float[,] testPath = testGenerator.Generate_squiggly_circle_test_path_3D(0, 0, 2.0f, 3.0f);
        inputPathRender.RenderLines(testPath);
        StartCoroutine(simulatorManager.Simulator(testPath));
    }




    /// <summary>
    /// These button functions updates the global a1 and a2 length variables declared in main.cs (which acts as a config file). 
    /// </summary>
    /// <param name="a1Length"></param>
    public void a1_Length_Slider(float a1Length) {
        main.a1 = a1Length;
    }
    public void a2_Length_Slider(float a2Length) {
        main.a2 = a2Length;
    }



}
