using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class holds some sample tests path generators for the simulation to follow. 
/// </summary>
public class TestGenerator
{

    public TestGenerator() {

    }




    /// <summary>
    /// Generates Circle Test Data for simulation
    /// </summary>
    /// <param name="x_center"> x-center of circle </param>
    /// <param name="y_center"> y-center of circle </param>
    /// <param name="h"> height of circle </param>
    /// <param name="r"> radius of circle </param>
    /// <returns> A float[360,3] of circle point data for robot to follow. </returns>
    public float[,] Generate_circle_test_path(float x_center, float y_center, float h, float r) {

        float[,] points = new float[360, 3];

        for (int i = 0; i < 360; i++) {
            float radian = (float)i * Mathf.PI / 180;
            float x = x_center + r * Mathf.Cos(radian);
            float y = y_center + r * Mathf.Sin(radian);
            points[i, 0] = x;
            points[i, 1] = y;
            points[i, 2] = h;
        }

        return points;
    }




    /// <summary>
    /// Generates Squiggly Circle Test Data for simulation
    /// </summary>
    /// <param name="x_center"> x-center of circle </param>
    /// <param name="y_center"> y-center of circle </param>
    /// <param name="h"> height of circle </param>
    /// <param name="r"> radius of circle </param>
    /// <returns> A float[360,3] of circle point data for robot to follow. </returns>
    public float[,] Generate_squiggly_circle_test_path(float x_center, float y_center, float h, float r) {

        float[,] points = new float[360, 3];

        // let radius expand and contract around the circle.
        float r_max = r * 1.1f;
        float r_min = r * 0.9f;
        r = r_min;
        float increment = r * 0.01f;

        for (int i = 0; i < 360; i++) {
            float radian = (float)i * Mathf.PI / 180;
            float x = x_center + r * Mathf.Cos(radian);
            float y = y_center + r * Mathf.Sin(radian);
            points[i, 0] = x;
            points[i, 1] = y;
            points[i, 2] = h;

            if (r > r_max) increment *= -1;
            else if (r < r_min) increment *= -1;

            r += increment;
        }

        return points;
    }




    /// <summary>
    /// Generates Squiggly Circle 3D with z-axis variation Test Data for simulation
    /// </summary>
    /// <param name="x_center"> x-center of circle </param>
    /// <param name="y_center"> y-center of circle </param>
    /// <param name="h"> height of circle </param>
    /// <param name="r"> radius of circle </param>
    /// <returns> A float[360,3] of circle point data for robot to follow. </returns>
    public float[,] Generate_squiggly_circle_test_path_3D(float x_center, float y_center, float h, float r) {

        float[,] points = new float[360, 3];

        // let radius expand and contract around the circle.
        float r_max = r * 1.1f;
        float r_min = r * 0.9f;
        r = r_min;
        float increment = r * 0.01f;

        // let height vary up and down a given height
        float h_max = h; // It cannot be higher than z0 as scara is fixed. 
        float h_min = h * 0.7f;
        h = h_min;
        float increment_h = h * 0.01f;

        for (int i = 0; i < 360; i++) {
            float radian = (float)i * Mathf.PI / 180;
            float x = x_center + r * Mathf.Cos(radian);
            float y = y_center + r * Mathf.Sin(radian);
            points[i, 0] = x;
            points[i, 1] = y;
            points[i, 2] = h;

            if (r > r_max) increment *= -1;
            else if (r < r_min) increment *= -1;
            r += increment;

            if (h > h_max) increment_h *= -1;
            else if (h < h_min) increment_h *= -1;
            h += increment;
        }

        return points;
    }
}
