using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class BossStateMachine<T>
{
    private T ownerEntity;  // StateMachine�� ������
    private BossState<T> currentState;  // ���� ����
    private BossState<T> previousState; // ���� ����
    private BossState<T> globalState;   // ���� ����

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

        // ���� �ٲٷ��� ���°� �Ȱ����� ���¸� �ٲ��� �ʴ´�
        if (currentState.GetType() == newType)
            return currentState as Q;

        // ���� ������� ���°� ������ Exit() �޼ҵ� ȣ��
        if (currentState != null)
        {
            currentState.Exit();
        }
        // ���°� ����Ǹ� ���� ���´� ���� ���°� �Ǳ� ������ previousState�� ����
        previousState = currentState;

        // ���ο� ���·� �����ϰ�, ���� �ٲ� ������ Enter() �޼ҵ� ȣ��]
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