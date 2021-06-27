using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the calculations such as Inverse Kinematics and determining the  final arm positions. 
/// </summary>
public class ScaraArm {
    public ScaraArm() {

    }




    /// <summary>
    /// Calculates the IK radians given the desired (x,y) coordinates and (a1,a2) arm lengths.
    /// The output of the two radians can be used to next to calculate the final positions of the arms. 
    /// </summary>
    /// <param name="xf"> x desired final coordinate of end-effector </param>
    /// <param name="yf"> y desired final coordinate of end-effector </param>
    /// <param name="a1"> a1 arm length </param>
    /// <param name="a2"> a2 arm length </param>
    /// <returns> An float[2] containing theta1 and theta2 that can be used to calculate arm1 and arm2 final positions </returns>
    public float[] Calculate_IK_radians(float xf, float yf, float a1, float a2) {
        if (Is_within_boundary(xf, yf, a1, a2) == false) return new float[] { 0, 0 };

        float r1 = Mathf.Sqrt((xf * xf) + (yf * yf)); // Eq.1
        float phi_1 = Mathf.Acos(((a2 * a2) - (a1 * a1) - (r1 * r1)) / (-2.0f * a1 * r1)); // Eq.2
        float phi_2 = Mathf.Atan(yf / xf); // Eq.3
        float theta_1 = phi_2 - phi_1; // Eq.4

        if (xf < 0) theta_1 = Mathf.PI + theta_1; // Second Quadrant angle adjustment (atan gives negative angle when x is -ve, need to add PI to get the desired angle.)

        //Debug.Log("Calculate_IK_radians, phi2 - phi1: " + phi_2 + "-" + phi_1);

        float phi_3 = Mathf.Acos(((r1 * r1) - (a1 * a1) - (a2 * a2)) / (-2.0f * a1 * a2)); // Eq.5
        float theta_2 = Mathf.PI - phi_3; // Eq.6

        return new float[] { theta_1, theta_2 };
    }




    /// <summary>
    /// Using IK radians, root coordinates and the arm lengths, get the final arm positions via trigonometry.
    /// </summary>
    /// <param name="x0"> root x-coor of arm1 </param>
    /// <param name="y0"> root y-coor of arm1 </param>
    /// <param name="z0"> root z-coor of arm1 </param>
    /// <param name="a1"> a1 arm length </param>
    /// <param name="a2"> a2 arm length </param>
    /// <param name="theta_1"> IK radian next to arm 1</param>
    /// <param name="theta_2"> IK radian next to arm 2</param>
    /// <returns> An Vector3[2] of arm1 and arm2 (x,y) coordinates respectively. </returns>
    public Vector3[] Calculate_arm_xy_positions(float x0, float y0, float z0, float a1, float a2, float theta_1, float theta_2) {

        float x1 = a1 * Mathf.Cos(theta_1) + x0;
        float y1 = a1 * Mathf.Sin(theta_1) + y0;

        float x2 = a2 * Mathf.Cos(theta_1 + theta_2) + x1;
        float y2 = a2 * Mathf.Sin(theta_1 + theta_2) + y1;

        Vector3 a1EndPosition = new Vector3(x1, z0, y1);
        Vector3 a2EndPosition = new Vector3(x2, z0, y2);

        return new Vector3[] { a1EndPosition, a2EndPosition };
    }




    /// <summary>
    /// Checks if desired (x,y) end-effector coordinate point is reachable by the given arm parameters. It checks 3 conditions.
    /// 1. Outer wide circle limit (a1 length and a2 length added together).
    /// 2. Inner circle limit caused by shorter first arm (second arm overreaches origin).
    /// 3. Inner circle limit caused by shorter second arm (second arm connt reach origin).
    /// </summary>
    /// <param name="xf"> desired x-coor </param>
    /// <param name="yf"> desired x-coor </param>
    /// <param name="a1"> a1 arm length </param>
    /// <param name="a2"> a2 arm length </param>
    /// <returns> bool true if reachable, false if unreachable by robot.</returns>
    bool Is_within_boundary(float xf, float yf, float a1, float a2) {
        // Checks three conditions
        // 1. Outer wide circle limit
        float r1 = Mathf.Sqrt(xf * xf + yf * yf);

        // ai is the radius of the inner unreachable circle limit if the two arm lengths are not equal. 
        // This is a placeholder value for ai (if a1 length equals a2 length, skip conditions #2 #3)
        float ai = r1;

        // 2. Inner circle limit caused by shorter first arm (second arm overreaches origin)
        if (a2 > a1)
            ai = a2 - a1;

        // 3. Inner circle limit caused by shorter second arm
        if (a2 < a1)
            ai = a1 - a2;

        if (r1 > (a1 + a2) || r1 < ai) {
            Debug.LogError("is_inside_boundary - Desired point unreachable, it is outside the workspace boundary.");
            return false;
        } else {
            return true;
        }
    }


}
