﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 0.1f;
    public Animator animator;
    new public Collider2D collider2D;
    new public Rigidbody2D rigidbody2D;
    public float jumpForce = 100f;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        FireBubble();

        // WASD, A왼쪽,, D오른쪽
        Move();

        // 점프
        Jump();

        // 아래로점프
        DownJump();
    }

    public LayerMask wallLayer;
    public float downWallCheckY = -2.1f;
    private void DownJump()
    {
        // s키 누르면 아래로 점프
        if (Input.GetKeyDown(KeyCode.S))
        {
            // 점프 가능한 상황인지 확인
            //  아래로 광선을 쏘아서 벽이 있다면 아래로 점프를 하자
            var hit = Physics2D.Raycast(
                transform.position + new Vector3(0, downWallCheckY, 0)
                , new Vector2(0, -1), 100, wallLayer);
            if (hit.transform)
            {
                //Debug.Log($"{hit.point}, {hit.transform.name}");

                ingDownJump = true;
                collider2D.isTrigger = true;
            }
        }
    }
    bool ingDownJump = false;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ingDownJump)
        {
            ingDownJump = false;
            collider2D.isTrigger = false;
        }
    }

    private void Jump()
    {
        // 낙하할때는 지면과 충돌하도록 isTrigger를 꺼주자.
        if (ingDownJump == false)
        {
            if (rigidbody2D.velocity.y < 0)
                collider2D.isTrigger = false;
        }

        // 방향위혹은 W키 누르면 점프 하자.
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            // 우리가 바닥에 붙어 있으면 점프 할 수 있게 하자.
            bool isGround = IsGround();
            if (isGround)
            {
                rigidbody2D.velocity = Vector2.zero;
                rigidbody2D.AddForce(new Vector2(0, jumpForce));
                collider2D.isTrigger = true; // 점프할때 벽을 뚫고 싶다.
            }
        }
    }

    public float groundCheckOffsetX = 0.4f;
    private bool IsGround()
    {
        //return rigidbody2D.velocity.y == 0;

        // 밑으로 광선을 쏘아서 부딪히는게 있으면 우리는 바닥에 있다.
        // 자기위치, 그리고 좌우로 0.4f만큼 바닥이 있으면 나는 바닥에 있다.

        if (IsGroundCheckRay(transform.position))
            return true;

        // 좌.
        if (IsGroundCheckRay(transform.position + new Vector3(-groundCheckOffsetX, 0, 0)))
            return true;

        // 우.
        if (IsGroundCheckRay(transform.position + new Vector3(groundCheckOffsetX, 0, 0)))
            return true;

        return false;
    }
    private void OnDrawGizmos()
    {
        DrawRay(transform.position);

        // 좌.
        DrawRay(transform.position + new Vector3(-groundCheckOffsetX, 0, 0));

        // 우.
        DrawRay(transform.position + new Vector3(groundCheckOffsetX, 0, 0));
    }

    private void DrawRay(Vector3 position)
    {
        Gizmos.DrawRay(position + new Vector3(0, downWallCheckY, 0), new Vector2(0, -1) * 100f);
    }

    bool IsGroundCheckRay(Vector3 pos)
    {
        var hit = Physics2D.Raycast(pos, new Vector2(0, -1), 1.1f, wallLayer);
        if (hit.transform)
            return true;

        return false;
    }


    public GameObject bubble;
    public Transform bubbleSpawnPos;
    private void FireBubble()
    {
        // 스페이스 누르면 버블 날리기.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bubble, bubbleSpawnPos.position, transform.rotation);
        }
    }
    public float minX = -12.3f, maxX = 12.3f;
    private void Move()
    {
        float moveX = 0;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveX = -1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveX = 1;
        Vector3 position = transform.position;
        position.x = position.x + moveX * speed;
        position.x = Mathf.Max(minX, position.x);
        position.x = Mathf.Min(maxX, position.x);
        transform.position = position;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack") == false)
        {
            if (moveX != 0)
            {
                //moveX 양수이면 180 로테이션 아니면 0도 로테이션 적용.
                float rotateY = 0;
                if (moveX < 0)
                    rotateY = 180;

                var rotation = transform.rotation;
                rotation.y = rotateY;
                transform.rotation = rotation;

                animator.Play("run");
            }
            else
                animator.Play("idle");
        }
    }
}
