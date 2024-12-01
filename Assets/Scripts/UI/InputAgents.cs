using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputAgents : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject evacuationSimulation;

    private void Start()
    {
        // Add a listener to the input field to validate input
        inputField.onValueChanged.AddListener(ValidateInput);
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

    public void StartButtonClicked()
    {
        // Get the value from the input field
        int agentCount = int.Parse(inputField.text);

        // Call the SpawnAgents function in the evacuationSimulation object
        evacuationSimulation.GetComponent<EvacuationSImulation>().SpawnAgents(agentCount);
    }
}
