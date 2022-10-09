using System;
using System.Collections.Generic;

public class StateMachine<T>
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

	public void ChangeState(State<T> newState)
	{
		// ���� �ٲٷ��� ���°� ��������� ���¸� �ٲ��� �ʴ´�
		if ( newState == null ) return;

		// ���� ������� ���°� ������ Exit() �޼ҵ� ȣ��
		if ( currentState != null )
		{
			// ���°� ����Ǹ� ���� ���´� ���� ���°� �Ǳ� ������ previousState�� ����
			previousState = currentState;

			currentState.Exit();
		}

		// ���ο� ���·� �����ϰ�, ���� �ٲ� ������ Enter() �޼ҵ� ȣ��
		currentState = stateList[newState.GetType()];
		currentState.Enter();

		stateDurationTime = 0.0f;
	}

	
	public void SetGlobalState(State<T> newState)
	{
		globalState = newState;
	}

	public void RevertToPreviousState()
	{
		ChangeState(previousState);
	}
}

