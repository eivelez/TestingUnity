using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class click : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject  canvas;
    TMPro.TextMeshProUGUI texto;
    void Start()
    {
        canvas = this.transform.GetChild(0).gameObject;
        texto = canvas.GetComponent<TMPro.TextMeshProUGUI>();
        texto.text = "hola";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        Behaviour halo  = (Behaviour)GetComponent("Halo");
        if(halo.enabled)
        {
            halo.enabled = false;
        }
        else
        {
            halo.enabled = true;
        }
        
    }
}
