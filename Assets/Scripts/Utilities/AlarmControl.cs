using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AlarmControl : MonoBehaviour
{
    private Transform firetarget;
    public Color fireDetectedColor;
    private Renderer objectRenderer;
    private AudioSource alarmAudioSource;
    private bool isActive = false;
    public float detectionRadius = 30;

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        alarmAudioSource = GetComponent<AudioSource>();
        if (alarmAudioSource != null)
        {
            alarmAudioSource.loop = true;
            alarmAudioSource.spatialBlend = 1.0f; // Make the sound 3D
            alarmAudioSource.Stop(); // Ensure the alarm is not playing at the start
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Fire"))
                {
                    ActivateAllAlarms();
                    ChangeColor(fireDetectedColor);
                    isActive = true;
                    break;
                }
            }
        }
    }

    private void ActivateAllAlarms()
    {
        GameObject[] alarms = GameObject.FindGameObjectsWithTag("Alarm");
        foreach (var alarm in alarms)
        {
            AlarmControl alarmControl = alarm.GetComponent<AlarmControl>();
            if (alarmControl != null && !alarmControl.isActive)
            {
                alarmControl.ActivateAlarm();
                alarmControl.ChangeColor(fireDetectedColor);
                alarmControl.isActive = true;
            }
        }
    }

    public void ActivateAlarm()
    {
        //Debug.Log("¡Alarma activada!");

        GetComponent<AudioSource>().Play();
    }

    private bool IsFireNearby()
    {
        GameObject leaderObject = GameObject.FindGameObjectWithTag("Fire");
        if (leaderObject != null)
        {
            firetarget = leaderObject.transform;
        }
        RaycastHit hit;
        if (firetarget != null && Physics.Raycast(transform.position, (firetarget.position - transform.position).normalized, out hit))
        {
            if (hit.transform == firetarget)
            {
                return true;
            }
        }
        return false;
    }

    private void ChangeColor(Color newColor)
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = newColor;
        }
    }
}
