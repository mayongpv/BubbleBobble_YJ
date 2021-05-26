using UnityEngine;

public class Bubble : MonoBehaviour
{
    public int moveForwardFrame = 6;
    public int currentFrame = 0;
    public float speed = 0.7f; //한 프레임당 움직이는 속도
    new public Rigidbody2D rigidbody2D;
    public float gravityScale = -0.7f;
    //(중력보다 조금 늦게 올라가도록)
    //앞 방향으로 이동, 6프레임 움직이고 나서 위로 이동(중력에 의해)
    //위로 올라가야 하기 때문에 중력 크기 : 마이너스, 
    // 중력 크기 조절 가능하도록 만들어줌

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0;
        /*0을 곱해준다는건 값을 0으로 만들어 준다는 뜻. -> 중력이 작용하지 않는다
         * 원래 중력이 -9.8 이니까. 
         * 근데 나중에 -0.7 곱해주면 양수가 되어서 위로 올라감
         */
        //rigidbody2D.AddForce(new Vector2(speed, 0)); //x를 스피드 만큼 움직여라
    }

    private void FixedUpdate()
    {
        if (currentFrame++ < moveForwardFrame)
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
        // 버블이 터질만큼 만힝 붙어 있다면 터트리자.
        //if(collision 이 벽인가?)
        ////collision.contacts[0].point  와 나와의 거리를 확인하자. -> 특정 값보다 작다면 터트리자.
    }
}






