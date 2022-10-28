using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Walker : MonoBehaviour
{
    NavMeshAgent agent;

    public PlayerControlsManager controlsManager;

    void Start()
    {
        controlsManager.controls.Player.Walk.performed += ctx => GoToDestination();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void GoToDestination() 
    {
        agent.SetDestination(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
    }
}
