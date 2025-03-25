using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro.Examples;

public class Manager : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;

    [Header("Scene name")]
    public string sceneName;

    [Header("User code")]
    public string userCode;

    [Header("Evaluation Timer")]
    public float evaluationTime = 35f;

    private bool evaluationStarted = false;
    private float evaluationTimer;

    void Start()
    {
        //Don't destroy
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(player);
    }

    void Update()
    {
        if (!string.IsNullOrEmpty(userCode) && Input.GetKeyDown(KeyCode.Space))
        {
            ChangeScene();
        }

        if (!evaluationStarted && SceneManager.GetActiveScene().name != "Intro-scene_2D" && SceneManager.GetActiveScene().name != "Intro-scene_3D" && SceneManager.GetActiveScene().name != "Intro-scene_Nature")
        {
            StartEvaluation();
        }

        if (evaluationStarted)
        {
            evaluationTimer -= Time.deltaTime;
            if (evaluationTimer <= 0 || Input.GetKeyDown(KeyCode.A))
            {
                ExportData();
            }
        }
    }

    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void StartEvaluation()
    {
        if (!evaluationStarted)
        {
            if (SceneManager.GetActiveScene().name != "Evaluation-scene_2D")
            {
                gameObject.GetComponent<PositionRecorder>().enabled = true;
            }

            gameObject.GetComponent<EyeTrackingRecorder>().enabled = true;

            evaluationStarted = true;
            evaluationTimer = evaluationTime; // Iniciar el temporizador
        }
    }

    private void ExportData()
    {
        Debug.Log("Eye tracking data exported");
        gameObject.GetComponent<EyeTrackingRecorder>().dataExported = true;
        Debug.Log("Position data exported");
        gameObject.GetComponent<PositionRecorder>().dataExported = true;

        // Detener evaluación después de exportar los datos
        evaluationStarted = false;
    }
}

