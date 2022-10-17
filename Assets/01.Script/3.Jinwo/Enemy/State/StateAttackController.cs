using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackController : MonoBehaviour
{
    public delegate void OnStartStateAttackController();
    public OnStartStateAttackController stateAttackControllerStartHandler;

    public delegate void OnEndStateAttackController();
    public OnEndStateAttackController stateAttackControllerEndHandler;

    public bool getFlagStateAttackController
    {
        get;
        private set;
    }

    

    void Start()
    {
        stateAttackControllerStartHandler = new OnStartStateAttackController(StateAttackControllerEnter);
        stateAttackControllerEndHandler = new OnEndStateAttackController(StateAttackControllerEnd);
    }

    private void StateAttackControllerEnter()
    {
        Debug.Log("Attack Start!");



    }

    private void StateAttackControllerEnd()
    {
        Debug.Log("Attack End!");

        
    }

    public void EventStateAttackEnter()
    {
        getFlagStateAttackController = true;
        if (stateAttackControllerStartHandler != null)
            stateAttackControllerStartHandler();
    }

    public void EventStateAttackEnd()
    {
        getFlagStateAttackController = false;
        if (stateAttackControllerEndHandler != null)
            stateAttackControllerEndHandler();
    }
}
