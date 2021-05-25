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
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0;
        rigidbody2D.AddForce(new Vector2(speed, 0));
        for (int i = 0; i < moveForwardFrame; i++)
        {
            transform.Translate(speed, 0, 0);
            yield return null;
        }
        rigidbody2D.gravityScale = gravityScale;
    }

    private void FixedUpdate()
    {
        if (currenrent)
        {
            var pos = rigidbody2D.position;
            pos.x += (speed * transform.forward.z);
            rigidbody2D.position = pos;
        }
        else
        {
            rigidbody2D.gravityScale = gravityScale;
            enabled = false;
        }

       
        }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }






}
