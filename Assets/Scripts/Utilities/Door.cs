using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//[RequireComponent(typeof(NavMeshObstacle))]
public class Door : MonoBehaviour
{
    private NavMeshObstacle Obstacle;

    public bool IsOpen = false;

    [SerializeField]
    private float Speed = 1f;
    [SerializeField]
    private float RotationAmount = 90f;
    [SerializeField]
    private float ForwardDirection = 0;

    private Vector3 StartRotation;
    private Vector3 Forward;

    private Coroutine AnimationCoroutine;

    private Transform DoorFrame;

    private void Awake()
    {
        Obstacle = GetComponent<NavMeshObstacle>();
        Obstacle.carveOnlyStationary = false;
        Obstacle.carving = IsOpen;
        Obstacle.enabled = IsOpen;

        DoorFrame = transform.parent;
        StartRotation = DoorFrame.rotation.eulerAngles;
        // Since "Forward" is actually pointing into the door frame, choose a direction to think about as "Forward"
        Forward = DoorFrame.right;
    }

    public void Open(Vector3 UserPosition)
    {
        if (!IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            float dot = Vector3.Dot(Forward, (UserPosition - DoorFrame.position).normalized);
            AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
        }
    }

    private IEnumerator DoRotationOpen(float ForwardAmount)
    {
        Quaternion startRotation = DoorFrame.rotation;
        Quaternion endRotation;

        if (ForwardAmount >= ForwardDirection)
        {
            endRotation = Quaternion.Euler(new Vector3(StartRotation.x, StartRotation.y - RotationAmount, StartRotation.z));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(StartRotation.x, StartRotation.y + RotationAmount, StartRotation.z));
        }

        IsOpen = true;

        float time = 0;
        while (time < 1)
        {
            DoorFrame.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }

        Obstacle.enabled = true;
        Obstacle.carving = true;
    }

    public void Close()
    {
        if (IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            AnimationCoroutine = StartCoroutine(DoRotationClose());
        }
    }

    private IEnumerator DoRotationClose()
    {
        Obstacle.carving = false;
        Obstacle.enabled = false;

        Quaternion startRotation = DoorFrame.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);

        IsOpen = false;

        float time = 0;
        while (time < 1)
        {
            DoorFrame.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }
}
