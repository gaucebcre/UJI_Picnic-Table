using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectSwitcher : MonoBehaviour
{
    // Variable pública para asignar los objetos desde el inspector
    public GameObject[] images;

    // Tiempo que cada objeto estará visible
    public int displayTime = 2; // Tiempo en segundos

    private List<int> availableIndices; // Lista de índices disponibles para seleccionar
    private int previousIndex = -1;    // Índice del objeto mostrado anteriormente
    private Coroutine switchCoroutine; // Referencia a la corrutina activa

    void Start()
    {
        if (images.Length != 7)
        {
            Debug.LogError("Debes asignar exactamente 7 objetos al array.");
            return;
        }

        // Inicializar la lista de índices disponibles
        ResetAvailableIndices();

        // Iniciar la corrutina para cambiar objetos
        switchCoroutine = StartCoroutine(SwitchObjectsOverTime());
    }

    IEnumerator SwitchObjectsOverTime()
    {
        while (availableIndices.Count > 0)
        {
            SwitchToRandomObject();
            yield return new WaitForSeconds(displayTime);
        }

        Debug.Log("No hay más objetos disponibles para mostrar.");

        // Mostrar permanentemente el objeto en images[1]
        ShowObject(1);
    }

    void SwitchToRandomObject()
    {
        if (availableIndices.Count == 0)
        {
            Debug.Log("No hay más objetos disponibles para mostrar.");
            return;
        }

        // Filtrar la lista de índices para cumplir con las restricciones
        List<int> filteredIndices = new List<int>();

        foreach (int index in availableIndices)
        {
            if (previousIndex == -1 || IsValidNextObject(previousIndex, index))
            {
                filteredIndices.Add(index);
            }
        }

        if (filteredIndices.Count == 0)
        {
            Debug.Log("No hay objetos válidos disponibles después de aplicar las restricciones.");
            return;
        }

        // Seleccionar un índice aleatorio de los índices filtrados
        int randomIndex = filteredIndices[Random.Range(0, filteredIndices.Count)];

        // Mostrar el objeto correspondiente
        ShowObject(randomIndex);

        // Guardar el índice actual como el índice anterior
        previousIndex = randomIndex;

        // Eliminar el índice seleccionado de la lista de disponibles
        availableIndices.Remove(randomIndex);
    }

    void ShowObject(int index)
    {
        // Ocultar todos los objetos
        foreach (GameObject obj in images)
        {
            obj.SetActive(false);
        }

        // Activar el objeto seleccionado
        images[index].SetActive(true);
    }

    bool IsValidNextObject(int previous, int next)
    {
        // Regla 1: Si el anterior es 0 o 3, el siguiente no puede ser 0 o 3
        if ((previous == 0 || previous == 3) && (next == 0 || next == 3))
        {
            return false;
        }

        // Regla 2: Si el anterior es 1 o 4, el siguiente no puede ser 1 o 4
        if ((previous == 1 || previous == 4) && (next == 1 || next == 4))
        {
            return false;
        }

        // Regla 3: Si el anterior es 2 o 5, el siguiente no puede ser 2 o 5
        if ((previous == 2 || previous == 5) && (next == 2 || next == 5))
        {
            return false;
        }

        return true;
    }

    void ResetAvailableIndices()
    {
        availableIndices = new List<int>();

        for (int i = 0; i < images.Length; i++)
        {
            availableIndices.Add(i);
        }
    }
}

