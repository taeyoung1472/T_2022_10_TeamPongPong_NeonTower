
public abstract class BossState<T>
{
    protected BossStateMachine<T> stateMachine;
    protected T stateMachineOwnerClass; // stateMachine�� ���� �ִ� Ŭ������ ����Ŵ
    public BossState() { }
    // internal : ���� ������Ʈ? �������� �����ְ� ����? �� ���� ����� ������ ���� �ӽò��̰���
    internal void SetMachineWithClass(BossStateMachine<T> stateMachine, T stateMachineClass)
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

