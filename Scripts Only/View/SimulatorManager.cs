using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script manages everything related to the simulation/animation. 
/// </summary>
public class SimulatorManager : MonoBehaviour {

    // Instantiate ScaraArm Object to use for calculations.
    ScaraArm scaraArm = new ScaraArm();

    // Link to ArmRenderer
    public ArmRenderer armRenderer;

    /// <summary>
    /// This simulates the positions of the robots arm step-by-step. 
    /// Alternatively all the positions can be calculated in a batch before rendering/animation.
    /// But this allows the users to change variables during the animations like the arm lengths and see the effects. 
    /// </summary>
    /// <param name="desiredPath"> a float[n,3] of 3D coordinates the end-effector is to follow.</param>
    /// <returns></returns>
    public IEnumerator Simulator(float[,] desiredPath) {

        Vector3 baseCoor = new Vector3(main.x0, main.z0, main.y0); // root coordinates of the arms.

        for (int i = 0; i < desiredPath.Length; i++) {

            float xf = desiredPath[i, 0];
            float yf = desiredPath[i, 1];
            float zf = desiredPath[i, 2];

            float[] thetas = scaraArm.Calculate_IK_radians(xf, yf, main.a1, main.a2);
            Vector3[] finalArmPositions = scaraArm.Calculate_arm_xy_positions(main.x0, main.y0, main.z0, main.a1, main.a2, thetas[0], thetas[1]);

            Vector3 a1End = finalArmPositions[0];
            Vector3 a2End = finalArmPositions[1];
            Vector3 a3End = finalArmPositions[1]; // a3 has the same (x,y) with a2, but same (z) and desired path.
            a3End.y = zf;

            Vector3 a3Start = a3End;
            a3Start.y += main.a3; // add the height of the end-effector.

            // Update the new positions
            armRenderer.a1Line.SetPositions(new Vector3[] { baseCoor, a1End });
            armRenderer.a2Line.SetPositions(new Vector3[] { a1End, a2End });
            armRenderer.a3Line.SetPositions(new Vector3[] { a3Start, a3End });

            //Wait for x seconds
            yield return new WaitForSeconds(0.01f);
        }

    }
}
