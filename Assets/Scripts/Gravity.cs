using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravityForce = 9.81f; // ���� ����������
    private Vector3 velocity; // �������� �������
    [Range(0, 5f)]
    public float distToGround;

    private void Update()
    {
        // ��������� ����������
        ApplyGravity();

        // ��������� ������� �������
        transform.position += velocity * Time.deltaTime;
    }

    private void ApplyGravity()
    {
        // ����������� �������� ����
        velocity.y -= gravityForce * Time.deltaTime;

        // ��������� ������������ � ������ (��������, y = 0)
        if (Physics.Raycast(transform.position, Vector3.down, distToGround))
            velocity.y = 0; // �������� �������� ��� ������������ � ������
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - distToGround, transform.position.z));
    }

}
