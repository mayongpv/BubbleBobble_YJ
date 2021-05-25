using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 0.1f;

    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        FireBubble();

        Move();
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

    private void Move()
    {

        // WASD, W����, A����,S�Ʒ�, D������
        float moveX = 0;
      
  
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveX = -1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveX = 1;
        Vector3 position = transform.position;
        position.x = position.x + moveX * speed * Time.deltaTime;
      
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
