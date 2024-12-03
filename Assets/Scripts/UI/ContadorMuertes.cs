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

    public void IncrementarMuertes()
    {
        muertes++;
        textMesh.text = "Muertes: " + muertes;
        //GameManager.Instance.RegistrarMuerte();
    }

    public string ObtenerMuertes()
    {
        return muertes.ToString();
    }
}
