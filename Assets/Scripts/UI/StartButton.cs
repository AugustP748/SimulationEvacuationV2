using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.ShaderData;

public class StartButton : MonoBehaviour
{
    public GameObject evacuationSimulation;
    public GameObject UIContadores;
    public GameObject player;
    public TMP_InputField inputField;
    [SerializeField] private TiempoSimulacion tiempoSimulacion;
    [SerializeField] private GameObject Pausa;

    public void Start()
    {
        if (inputField == null)
        {
            inputField = GetComponentInChildren<TMP_InputField>();
        }

        Pausa.SetActive(false);

        // Add a listener to the input field to validate input
        //inputField.onValueChanged.AddListener(ValidateInput);
    }

    public void SpawnFire()
    {
        // Get the value from the input field
        int agentCount = int.Parse(inputField.text);

        UIContadores.SetActive(true);

        // Call the SpawnAgents function in the evacuationSimulation object
        evacuationSimulation.GetComponent<EvacuationSImulation>().SpawnAgents(agentCount);
        player.GetComponent<PlayerController>().StartSimulation();
        GameManager.Instance.AgentCount = agentCount;

        evacuationSimulation.GetComponent<EvacuationSImulation>().StartSimulation();
        tiempoSimulacion.StartSimulation();
        Destroy(gameObject);
    }

    private void ValidateInput(string input)
    {
        // Remove any non-numeric characters from the input
        string numericInput = "";
        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                numericInput += c;
            }
        }

        // Update the input field text with the validated input
        inputField.text = numericInput;
    }
}
