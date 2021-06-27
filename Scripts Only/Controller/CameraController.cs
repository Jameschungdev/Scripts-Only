using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles the camera movements.
/// </summary>
public class CameraController : MonoBehaviour
{

    // Reference to Cameras
    public Camera CameraMain;
    public Camera CameraTop;


    /// <summary>
    /// This function is called when the 'Toggle Camera' button is pressed. 
    /// This switches the current view betwwen perspective and top.
    /// </summary>
    public void Toggle_Camera() {
        if (CameraMain.enabled == true) {
            CameraMain.enabled = false;
            CameraTop.enabled = true;
        } else {
            CameraMain.enabled = true;
            CameraTop.enabled = false;
        }
    }

}
