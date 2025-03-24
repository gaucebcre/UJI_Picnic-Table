using UnityEngine;
using UnityEngine.Events;

public class EyeInteractable2d : MonoBehaviour
{
    public bool IsHovered { get; set; }

    public bool isHovered;

    void Start()
    {
        isHovered = false;
    }

    void FixedUpdate()
    {
        if (IsHovered)
        {
            isHovered = true;
        }

        else
        {
            isHovered = false;
        }
    }
}
