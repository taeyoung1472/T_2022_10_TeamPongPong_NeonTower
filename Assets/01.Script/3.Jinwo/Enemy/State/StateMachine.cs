using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class StateMachine<T>
{
	private	T			ownerEntity;	// StateMachine�� ������
	private	State<T>	currentState;	// ���� ����
	private	State<T>	previousState;	// ���� ����
	private	State<T>	globalState;    // ���� ����
	
	private Dictionary<Type, State<T>> stateList = new Dictionary<Type, State<T>>();

	public State<T> getNowState => currentState;
	public State<T> getBeforeState => previousState;

	private float stateDurationTime = 0.0f;
	public float getStateDurationTime => stateDurationTime;

	public StateMachine(T stateMachine, State<T> initState)
	{
		this.ownerEntity = stateMachine;

		AddStateList(initState);
		currentState = initState;
		currentState.Enter();
		globalState = null;
	}

	public void AddStateList(State<T> state)
	{
		state.SetMachineWithClass(this, ownerEntity);
		stateList[state.GetType()] = state;
	}
	
	public void Execute()
	{
		if ( globalState != null )
		{
			globalState.Execute();
		}

		if ( currentState != null )
		{
			currentState.Execute();
		}
	}

	public Q ChangeState<Q>() where Q : State<T>
	{
		var newType = typeof(Q);

		//Debug.Log(newType);
		//Debug.Log(stateList.ContainsKey(typeof(StateMeleeAttack<Q>)));

		// ���� �ٲٷ��� ���°� �Ȱ����� ���¸� �ٲ��� �ʴ´�
		if (currentState.GetType() == newType)
			return currentState as Q;

		// ���� ������� ���°� ������ Exit() �޼ҵ� ȣ��
		if ( currentState != null )
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
	public void SetGlobalState(State<T> newState)
	{
		globalState = newState;
	}

    //public void RevertToPreviousState()
    //{
    //	ChangeState<>();
    //}
}

