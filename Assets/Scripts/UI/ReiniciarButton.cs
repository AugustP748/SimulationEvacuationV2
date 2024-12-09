using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReiniciarButton : MonoBehaviour
{

   // public GameObject GameManagerObject;
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(GameManagerObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Called when the button is pressed
    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;    
        AudioListener.pause = false;
        GameManager.Instance.simulationEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
