
public abstract class State<T> 
{
	protected StateMachine<T> stateMachine;
	protected T stateMachineOwnerClass; // stateMachine�� ���� �ִ� Ŭ������ ����Ŵ
	public State() { }
	// internal : ���� ������Ʈ? �������� �����ְ� ����? �� ���� ����� ������ ���� �ӽò��̰���
	internal void SetMachineWithClass(StateMachine<T> stateMachine, T stateMachineClass)
	{
		this.stateMachine = stateMachine;
		this.stateMachineOwnerClass = stateMachineClass;

		OnAwake();
	}

	/// <summary>
	/// �ش� ���¸� ���� �� �ʱ�ȭ �� �� ȣ��
	/// </summary>
	public virtual void OnAwake() { }
	/// <summary>
	/// �ش� ���¸� ������ �� 1ȸ ȣ��
	/// </summary>
	public abstract void Enter();

	/// <summary>
	/// �ش� ���¸� ������Ʈ�� �� �� ������ ȣ��
	/// </summary>
	public abstract void Execute();

	/// <summary>
	/// �ش� ���¸� ������ �� 1ȸ ȣ��
	/// </summary>
	public abstract void Exit();
}

