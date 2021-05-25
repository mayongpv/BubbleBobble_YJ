using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 0.1f;
    new public Rigidbody2D rigidbody2D;
    new public Collider2D collider2D;
    public Animator animator;
    public float jumpForce = 300f;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        FireBubble();

        Move();

        Jump();

    }

  

    //���߿��� ������ ���� �ʹ�.



    private void Jump()
    {

        if (rigidbody2D.velocity.y < 0)
            collider2D.isTrigger = false;

        if (rigidbody2D.velocity.y == 0) 
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                rigidbody2D.AddForce(new Vector2(0, jumpForce));
                collider2D.isTrigger = true; //������ �� ���� �հ� �ʹ�.

            }
        }
    }

    public GameObject bubble;
    public Transform bubbleSpawnPos;
    private void FireBubble()
    {
        // �����̽� ������ ���� ������.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bubble, bubbleSpawnPos.position, transform.rotation);
        }
    }

    public float minX, maxX;
    private void Move()
    {

        // WASD, W����, A����,S�Ʒ�, D������
        float moveX = 0;
      
  
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveX = -1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveX = 1;
        Vector3 position = transform.position;
        position.x = position.x + moveX * speed;
        position.x = Mathf.Max(minX, position.x);
        position.x = Mathf.Min(maxX, position.x);
        transform.position = position;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") == false)
        {
            if (moveX != 0)
            {//moveX�� ����� 180 �����̼� �ƴϸ� 0�� �����̼�
                float rotateY = 0;
                if (moveX > 0)
                    rotateY = 180;

                var rotation = transform.rotation;
                rotation.y = rotateY;
                transform.rotation = rotation;
                animator.Play("Run");
            }
            else
                animator.Play("Idle");
        }


    }
}
