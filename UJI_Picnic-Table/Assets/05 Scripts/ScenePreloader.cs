using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScenePreloader : MonoBehaviour
{
    private AsyncOperation asyncLoad;
    public GameObject button;

    void Start()
    {
        // Inicia la carga asíncrona de la escena al inicio del juego, pero no la activa
        StartCoroutine(PreloadScene("Evaluation-scene_Nature"));
    }

    private IEnumerator PreloadScene(string sceneName)
    {
        // Comienza a cargar la escena en segundo plano
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // No activamos la escena hasta que lo decidamos

        while (!asyncLoad.isDone)
        {
            // Puedes verificar el progreso de carga si quieres
            Debug.Log($"Cargando... {asyncLoad.progress * 100}%");

            // Cuando la carga esté casi completa (alrededor de 99%)
            if (asyncLoad.progress >= 0.90f)
            {
                button.SetActive(true);
            }

            yield return null; // Espera un frame
        }
    }

    // Método que podrás llamar para activar la escena cuando lo desees
    public void ActivateScene()
    {
        if (asyncLoad != null && asyncLoad.isDone)
        {
            asyncLoad.allowSceneActivation = true; // Activamos la escena cuando lo desees
        }
    }
}


