using System;
using System.Collections.Generic;

public class StateMachine<T>
{
	private	T			ownerEntity;	// StateMachine의 소유주
	private	State<T>	currentState;	// 현재 상태
	private	State<T>	previousState;	// 이전 상태
	private	State<T>	globalState;    // 전역 상태

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
		// 새로 바꾸려는 상태가 비어있으면 상태를 바꾸지 않는다
		if ( newState == null ) return;

		// 현재 재생중인 상태가 있으면 Exit() 메소드 호출
		if ( currentState != null )
		{
			// 상태가 변경되면 현재 상태는 이전 상태가 되기 때문에 previousState에 저장
			previousState = currentState;

			currentState.Exit();
		}

		// 새로운 상태로 변경하고, 새로 바뀐 상태의 Enter() 메소드 호출
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

