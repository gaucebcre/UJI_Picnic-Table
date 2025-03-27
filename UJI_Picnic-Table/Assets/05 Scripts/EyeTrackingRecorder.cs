using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine;

public class EyeTrackingRecorder : MonoBehaviour
{
    [Header("Blink")]
    public OVRFaceExpressions faceExpression;
    public OVRFaceExpressions.FaceExpression leftEyeBlink;
    public OVRFaceExpressions.FaceExpression rightEyeBlink;

    private float weightL;
    private float weightR;

    [Header("Volumes of interest")]
    public GameObject[] volumesOfInterest;

    [Header("Active image")]
    public GameObject activeImage;

    [Header("Frequence (hz)")]
    [Tooltip("In hertz)")]
    public int frequence = 120;

    private float realFrequency;

    [Header("Export data to HMD?")]
    public bool toHMD = false;

    [Header("Other")]
    public bool dataExported = false;

    private List<string> voi = new();
    private List<string> images = new();
    private string filename;

    void Start()
    {
        if (!toHMD)
        {
            filename = Application.streamingAssetsPath + "\\" + gameObject.GetComponent<Manager>().userCode + "_Eye-tracking" + ".csv";
        }

        else
        {
            filename = Application.persistentDataPath + "\\" + gameObject.GetComponent<Manager>().userCode + "_Eye-tracking" + ".csv";
        }
        UpdateInvoke();
    }
    void UpdateInvoke()
    {
        if (frequence <= 0)
        {
            Debug.LogError("Frecuencia debe ser mayor que 0");
            return;

        }
        realFrequency = 1f / frequence;
        CancelInvoke("ReadVolume");
        InvokeRepeating("ReadVolume", 0, realFrequency);

        if (SceneManager.GetActiveScene().name == "Evaluation-scene_2D")
        {
            CancelInvoke("ReadImage");
            InvokeRepeating("ReadImage", 0, realFrequency);
        }
    }
    void Update()
    {
        volumesOfInterest = GameObject.FindGameObjectsWithTag("VoI");
        activeImage = GameObject.FindGameObjectWithTag("Image");

        // Update face expression
        weightL = faceExpression[leftEyeBlink];
        weightR = faceExpression[rightEyeBlink];

        if (dataExported)
        {
            Invoke("ExportData", 0);
        }
    }

    public void ReadVolume()
    {
        bool stimuliAddedToList = false;
        bool isBlinking = false;
        if (!dataExported)
        {
            if (weightL >= 0.6f && weightR >= 0.6f)
            {
                isBlinking = true;
            }
            for (int i = 0; i < volumesOfInterest.Length; i++)
            {
                if (volumesOfInterest[i].GetComponent<EyeInteractable>().isHovered == true && isBlinking == false)
                {
                    voi.Add(volumesOfInterest[i].name);
                    stimuliAddedToList = true;
                }
            }
            //Blink detection
            if (weightL >= 0.6f && weightR >= 0.6f)
            {
                voi.Add("blink");
                //isHovered should be false for each volume of interest

                for (int a = 0; a < volumesOfInterest.Length; a++)
                {
                    volumesOfInterest[a].GetComponent<EyeInteractable>().isHovered = false;
                }
            }
            else if (stimuliAddedToList == false && isBlinking == false)
            {
                voi.Add("Environment");
            }
        }
        Debug.Log("Último valor añadido: " + voi[voi.Count - 1]);
    }
    public void ReadImage()
    {
        if (!dataExported)
        {
            images.Add(activeImage.name);
        }
    }
    public void ExportData()
    {
        if (SceneManager.GetActiveScene().name == "Evaluation-scene_2D")
        {
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("Volume" + ";" + "Image");
            for (int i = 0; i < voi.Count; ++i)
            {
                tw.WriteLine(voi[i] + ";" + images[i]);
            }
            tw.Close();
        }
        else
        {
            TextWriter tw = new StreamWriter(filename, false);

            tw.WriteLine("Volume");

            for (int i = 0; i < voi.Count; ++i)
            {
                tw.WriteLine(voi[i]);
            }
            tw.Close();
        }
    }
}

