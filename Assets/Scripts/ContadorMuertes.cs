using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ContadorMuertes : MonoBehaviour
{
    private float muertes = 0;
    public TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = "Muertes: " + muertes;
    }

    private void Update()
    {
        
    }

    public void IncrementarMuertes()
    {
        muertes++;
        textMesh.text = "Muertes: " + muertes;
    }
}