using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // Asigna el Canvas del men� de pausa

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pausa el juego
            pauseMenuUI.SetActive(true); // Activa el men� de pausa
        }
        else
        {
            Time.timeScale = 1f; // Reanuda el juego
            pauseMenuUI.SetActive(false); // Desactiva el men� de pausa
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        // L�gica para salir del juego
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}