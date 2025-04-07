using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using System.IO.Pipes;
using System;

public class ExternalControl : MonoBehaviour
{
    [SerializeField] private GameObject bicicletaEstatica;
    [SerializeField] private GameObject manager;

    private string path = Application.dataPath + "StreamingAssets/ControlVars.json";

    // NOTA: para ayadir mas variables, hay que añadirlas en el script ControlVars.cs

    private void Start()
    {
        //Don't destroy
        DontDestroyOnLoad(this.gameObject);

        // espera a que la posicion de la bici sea detectada, con el mando
        System.Threading.Thread.Sleep(2000);

        // asigna los valores por primera vez
        ControlVars controlVars = new ControlVars();
        controlVars.biciVars.rotation = bicicletaEstatica.transform.position;
        controlVars.biciVars.rotation = bicicletaEstatica.transform.rotation.eulerAngles;
        string jsonString = JsonUtility.ToJson(controlVars);

        using (StreamWriter outputFile = new StreamWriter(Path.Combine(path)))
        {
            outputFile.WriteLine(jsonString);
        }

        StartCoroutine(ReadFileEverySecond());
    }

    private void Update()
    {
    }

    // Infinitamente lee el archivo cada segundo y asigna los valores a la bici, haya o no cambios
    private IEnumerator ReadFileEverySecond()
    {
        yield return new WaitForSeconds(1f);
        ControlVars controlVars = JsonUtility.FromJson<ControlVars>(path);
        // desafortunadamente hay que escribir codigo personalizado para cada variable

        // biciPosition
        bicicletaEstatica.transform.position = controlVars.biciVars.rotation;

        // biciRotation
        bicicletaEstatica.transform.rotation = Quaternion.Euler(controlVars.biciVars.rotation);

        // biciSpeed
        bicicletaEstatica.GetComponent<Rigidbody>().velocity = controlVars.biciVars.speed * bicicletaEstatica.transform.forward;

        // userCode
        manager.GetComponent<Manager>().userCode = controlVars.managerVars.userCode;

        StartCoroutine(ReadFileEverySecond());
    }
}