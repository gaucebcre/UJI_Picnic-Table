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

    private bool evaluationStarted = false;

    void Start()
    {
        //Don't destroy
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(player);      
    }

    void Update()
    {       
        if (!string.IsNullOrEmpty(userCode) && Input.GetKeyDown(KeyCode.A))
        {
            ChangeScene();
        }

        if (SceneManager.GetActiveScene().name != "Intro-scene_2D" && SceneManager.GetActiveScene().name != "Intro-scene_3D" && SceneManager.GetActiveScene().name != "Intro-scene_Nature")
        {
            StartEvaluation();
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
            if(SceneManager.GetActiveScene().name != "Evaluation-scene_2D")
            {
                gameObject.GetComponent<PositionRecorder>().enabled = true;
            }

            gameObject.GetComponent<EyeTrackingRecorder>().enabled = true;

            evaluationStarted = true;
        }
        
        else if (evaluationStarted && Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Eye tracking data exported");
            gameObject.GetComponent<EyeTrackingRecorder>().dataExported = true;
            Debug.Log("Position data exported");
            gameObject.GetComponent<PositionRecorder>().dataExported = true;
        }
    }
}
