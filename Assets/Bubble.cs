using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public int moveForwardFrame = 6;
    public float speed = 0.7f;
    new public Rigidbody2D rigidbody2D;
    public float gravityScale = -0.7f;
    //�� �������� �̵�, 6������ �����̰� ���� ���� �̵�(�߷¿� ����)

    IEnumerator Start()
    {
        for (int i = 0; i < moveForwardFrame; i++)
        {
            transform.Translate(speed, 0, 0);
            yield return null;
        }
        rigidbody2D.gravityScale = gravityScale;
    }

}
