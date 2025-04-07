using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargarVarsBici : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Manager manager = GameObject.Find("Manager").GetComponent<Manager>();
        manager.posicionBici = transform.position;
        manager.rotacionBiciY = transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}