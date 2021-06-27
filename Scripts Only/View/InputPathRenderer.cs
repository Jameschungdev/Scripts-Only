using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles the 3D rendering of the trajectory lines using the LineRender.
/// </summary>
public class InputPathRenderer : MonoBehaviour {

    // Reference to InputPath Renderer Component
    LineRenderer inputPathLine;




    // Start is called before the first frame update
    void Start() {
        // Link to component.
        inputPathLine = GetComponent<LineRenderer>();
    }




    /// <summary>
    /// Draws lines given a set of 3D coordinates using the LineRenderer.
    /// </summary>
    /// <param name="points"> float[n,3] of 3D coordinates (x,y,z)</param>
    public void RenderLines(float[,] points) {

        //Convert float[,] into Vector3[]
        Vector3[] inputPathPositions = new Vector3[points.GetLength(0)];

        for (int i = 0; i < points.GetLength(0); i++) {
            float xt = points[i, 0];
            float yt = points[i, 1];
            float zt = points[i, 2];
            inputPathPositions[i] = new Vector3(xt, zt, yt);
        }

        inputPathLine.positionCount = inputPathPositions.Length;
        inputPathLine.SetPositions(inputPathPositions);
        inputPathLine.alignment = LineAlignment.View;
        inputPathLine.startWidth = inputPathLine.endWidth = 0.01f;
    }



}
