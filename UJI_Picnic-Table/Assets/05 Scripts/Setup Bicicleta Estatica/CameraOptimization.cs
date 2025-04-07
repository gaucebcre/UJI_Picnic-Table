using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraOptimization : MonoBehaviour // TODO revisar
{
    // Start is called before the first frame update
    private void Start()
    {
        // Obtener todas las cámaras en la escena
        Camera[] cameras = Camera.allCameras;

        foreach (Camera cam in cameras)
        {
            // Establecer la distancia de recorte a 250
            cam.farClipPlane = 250;

            // Obtener el componente UniversalAdditionalCameraData
            UniversalAdditionalCameraData cameraData = cam.GetComponent<UniversalAdditionalCameraData>();
            if (cameraData != null)
            {
                // Establecer el antialiasing a TAA
                cameraData.antialiasing = AntialiasingMode.TemporalAntiAliasing;
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}