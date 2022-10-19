using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    [SerializeField] bool DebugMode = false;
    [Range(0f, 360f)] [SerializeField] float ViewAngle = 0f;
    [SerializeField] float ViewRadius = 1f;
    [SerializeField] LayerMask TargetMask;
    [SerializeField] LayerMask ObstacleMask;
    List<Collider> hitTargetList = new List<Collider>();

    public bool isInAttackRange = false;
    void Start()
    {
        isInAttackRange = false;
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 myPos = transform.position + Vector3.up * 0.5f;
        Gizmos.DrawWireSphere(myPos, ViewRadius);
        float lookingAngle = transform.eulerAngles.y;  //ĳ���Ͱ� �ٶ󺸴� ������ ����
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + ViewAngle * 0.5f); //���ʰ� 
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - ViewAngle * 0.5f); //������ ��
        Vector3 lookDir = AngleToDir(lookingAngle); //�ٶ󺸴� ����

        Debug.DrawRay(myPos, rightDir * ViewRadius, Color.blue);
        Debug.DrawRay(myPos, leftDir * ViewRadius, Color.blue);
        Debug.DrawRay(myPos, lookDir * ViewRadius, Color.cyan);
    }
    void Update()
    {
        if (!DebugMode) return;
        Vector3 myPos = transform.position + Vector3.up * 0.5f;
        Gizmos.DrawWireSphere(myPos, ViewRadius);

        float lookingAngle = transform.eulerAngles.y;  //ĳ���Ͱ� �ٶ󺸴� ������ ����
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + ViewAngle * 0.5f); //���ʰ� 
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - ViewAngle * 0.5f); //������ ��
        Vector3 lookDir = AngleToDir(lookingAngle); //�ٶ󺸴� ����

        Debug.DrawRay(myPos, rightDir * ViewRadius, Color.blue);
        Debug.DrawRay(myPos, leftDir * ViewRadius, Color.blue);
        Debug.DrawRay(myPos, lookDir * ViewRadius, Color.cyan);

        hitTargetList.Clear();
        Collider[] Targets = Physics.OverlapSphere(myPos, ViewRadius, TargetMask);

        if (Targets.Length == 0) return;
        foreach (Collider EnemyColli in Targets)
        {
            Vector3 targetPos = EnemyColli.transform.position;
            Vector3 targetDir = (targetPos - myPos).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;
            if (targetAngle <= ViewAngle * 0.5f && !Physics.Raycast(myPos, targetDir, ViewRadius, ObstacleMask))
            {
                hitTargetList.Add(EnemyColli);
                if (DebugMode) Debug.DrawLine(myPos, targetPos, Color.red);
            }
        }
    }
    public void SetAngle(float angle)
    {
        ViewAngle = angle;
    }
    public Vector3 AngleToDir(float angle) //������ ������ ������ ���Ͱ����� �ٲ��ִ� �Լ�
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }
}
