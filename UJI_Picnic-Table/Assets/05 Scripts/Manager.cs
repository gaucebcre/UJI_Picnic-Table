using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Manager : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;

    [Header("Scene name")]
    public string sceneName;

    [Header("User code")]
    public string userCode;

    [Header("Other")]
    public TextMeshProUGUI letterDropdown;
    public TextMeshProUGUI numberDropdown;

    [Header("Final canvas")]
    public GameObject finalCanvas;

    private bool evaluationStarted = false;

    void Start()
    {
        //Don't destroy
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(player);

        //Save first code
        userCode = letterDropdown.text + numberDropdown.text;        
    }

    void Update()
    {
        StartEvaluation();
    }

    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void SaveUserCode()
    {
       userCode = letterDropdown.text + numberDropdown.text;
    }

    public void StartEvaluation()
    {
        if (SceneManager.GetActiveScene().name == sceneName && !evaluationStarted)
        {
            finalCanvas = GameObject.FindGameObjectWithTag("FinalCanvas");
            finalCanvas.SetActive(false);

            gameObject.GetComponent<PositionRecorder>().enabled = true;
            gameObject.GetComponent<EyeTrackingRecorder>().enabled = true;
            evaluationStarted = true;
        }
        
        else if (evaluationStarted && OVRInput.GetDown(OVRInput.Button.One))
        {
            //Export data
            Debug.Log("Eye tracking data exported");
            gameObject.GetComponent<EyeTrackingRecorder>().dataExported = true;
            Debug.Log("Position data exported");
            gameObject.GetComponent<PositionRecorder>().dataExported = true;

            //Disable product and enable final canvas
            GameObject.FindGameObjectWithTag("Product").SetActive(false);
            finalCanvas.SetActive(true);
        }
    }
}
