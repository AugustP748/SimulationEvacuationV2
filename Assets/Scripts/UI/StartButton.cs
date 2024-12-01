using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public GameObject evacuationSimulation;
    public TMP_InputField inputField;
    [SerializeField] private TiempoSimulacion tiempoSimulacion;

    public void Start()
    {
        if (inputField == null)
        {
            inputField = GetComponentInChildren<TMP_InputField>();
        }
        // Add a listener to the input field to validate input
        //inputField.onValueChanged.AddListener(ValidateInput);
    }

    public void SpawnFire()
    {
        // Get the value from the input field
        int agentCount = int.Parse(inputField.text);

        // Call the SpawnAgents function in the evacuationSimulation object
        evacuationSimulation.GetComponent<EvacuationSImulation>().SpawnAgents(agentCount);


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
