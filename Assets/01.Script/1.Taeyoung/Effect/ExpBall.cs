using UnityEngine;

public class ExpBall : PoolAbleObject
{
    float speed = 0;
    int value = 0;
    void Update()
    {
        speed += Time.deltaTime;
        transform.position = Vector3.Slerp(transform.position, Define.Instance.playerController.transform.position, Time.deltaTime * (2.5f + speed));
        if (Vector3.Distance(transform.position, Define.Instance.playerController.transform.position) < 1f)
        {
            EXPManager.Instance.AddExp(value);
            PoolManager.Instance.Push(PoolType, gameObject);
        }
    }

    public void SetExpValue(int _value)
    {
        value = _value;
    }

    public override void Init_Pop()
    {
        speed = 0;
    }

    public override void Init_Push()
    {

    }
}