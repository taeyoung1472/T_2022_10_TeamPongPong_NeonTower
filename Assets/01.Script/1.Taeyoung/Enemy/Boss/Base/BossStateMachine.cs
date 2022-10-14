using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class BossStateMachine<T>
{
    private T ownerEntity;  // StateMachine의 소유주
    private BossState<T> currentState;  // 현재 상태
    private BossState<T> previousState; // 이전 상태
    private BossState<T> globalState;   // 전역 상태

    private Dictionary<Type, BossState<T>> stateList = new();

    public BossState<T> GetNowState => currentState;
    public BossState<T> GetBeforeState => previousState;

    private float stateDurationTime = 0.0f;
    public float GetStateDurationTime => stateDurationTime;

    public BossStateMachine(T stateMachine, BossState<T> initState)
    {
        this.ownerEntity = stateMachine;

        AddStateList(initState);
        currentState = initState;
        currentState.Enter();
        globalState = null;
    }

    public void AddStateList(BossState<T> state)
    {
        state.SetMachineWithClass(this, ownerEntity);
        stateList[state.GetType()] = state;
    }

    public void Execute()
    {
        if (globalState != null)
        {
            globalState.Execute();
        }

        if (currentState != null)
        {
            currentState.Execute();
            stateDurationTime += Time.deltaTime;
        }
    }

    public Q ChangeState<Q>() where Q : BossState<T>
    {
        var newType = typeof(Q);

        //Debug.Log(newType);
        //Debug.Log(stateList.ContainsKey(typeof(StateMeleeAttack<Q>)));

        // 새로 바꾸려는 상태가 똑같으면 상태를 바꾸지 않는다
        if (currentState.GetType() == newType)
            return currentState as Q;

        // 현재 재생중인 상태가 있으면 Exit() 메소드 호출
        if (currentState != null)
        {
            currentState.Exit();
        }
        // 상태가 변경되면 현재 상태는 이전 상태가 되기 때문에 previousState에 저장
        previousState = currentState;

        // 새로운 상태로 변경하고, 새로 바뀐 상태의 Enter() 메소드 호출]
        currentState = stateList[newType];
        currentState.Enter();

        stateDurationTime = 0.0f;

        return currentState as Q;
    }
    public void ReturnDic()
    {

        foreach (var item in stateList)
        {
            Debug.Log(item.Key.Name);
        }
    }
    public void SetGlobalState(BossState<T> newState)
    {
        globalState = newState;
    }
}