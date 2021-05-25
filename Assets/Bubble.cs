using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public int moveForwardFrame = 6;
    public float speed = 0.7f;
    new public Rigidbody2D rigidbody2D;
    public float gravityScale = -0.7f;
    //앞 방향으로 이동, 6프레임 움직이고 나서 위로 이동(중력에 의해)

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
