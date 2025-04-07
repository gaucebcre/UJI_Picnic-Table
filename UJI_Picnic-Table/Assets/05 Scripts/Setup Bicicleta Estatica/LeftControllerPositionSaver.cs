using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;

public class LeftControllerPositionSaver : MonoBehaviour
{
    [SerializeField]
    private GameObject leftController;

    [SerializeField]
    private GameObject manager;

    private int activeFrameCount = 0; // Contador de fotogramas activos
    private const int requiredActiveFrames = 10; // Número de fotogramas activos requeridos
    private bool guardadoFinalizado = false; // Que no intente guardar más de una vez

    private void Update()
    {
        if (leftController.GetComponent<ControllerActiveState>().Active)
        {
            activeFrameCount++;

            if (activeFrameCount >= requiredActiveFrames)
            {
                Manager managerComponent = manager.GetComponent<Manager>();
                Transform leftOVRController = leftController.GetNamedChild("OVRControllerVisual").transform;

                Vector3 currentPosition = leftOVRController.position;
                float currentRotationY = leftOVRController.rotation.eulerAngles.y;

                // Actualizar el componente manager con la posición y rotación actuales, para guardar
                managerComponent.posicionBici = currentPosition;
                managerComponent.rotacionBiciY = currentRotationY;

                if (activeFrameCount >= requiredActiveFrames && !guardadoFinalizado)
                {
                    guardadoFinalizado = true;
                }
            }
        }
        else
        {
            activeFrameCount = 0; // Reiniciar el contador si el controlador no está activo
        }
    }
}