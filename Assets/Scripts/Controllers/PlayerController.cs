using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Vector3 _direction = Vector3.zero;
    float _speed = 5.0f;

    float _jumpForce = 5f;

    private Rigidbody _rb;
    private bool isGrounded = true;

    [SerializeField]
    protected Define.State _state = Define.State.Idle;

    Vector3 _angle = Vector3.zero;

    public virtual Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();
            switch (_state)
            {
                case Define.State.Die:
                    break;
                case Define.State.Idle:
                    anim.CrossFade("WAIT", 0.1f);
                    break;
                case Define.State.Moving:
                    anim.CrossFade("RUN", 0.1f);
                    break;
                case Define.State.Skill:
                    anim.CrossFade("ATTACK", 0.1f, -1, 0);
                    break;
            }
        }
    }

    void Start()
    {
        Init();

    }

    
    void Update()
    {

        return;
        switch (State)
        {
            case Define.State.Die:
                UpdateDie();
                break;
            case Define.State.Moving:
                UpdateMoving();
                break;
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Skill:
                UpdateSkill();
                break;
            case Define.State.Wait:
                UpdateWait();
                break;
            case Define.State.Jump:
                UpdateJump();
                break;
        }
    }

    private void UpdateJump()
    {
        return;
    }

    private void UpdateWait()
    {
        return;
    }

    private void UpdateSkill()
    {
        return;
    }

    private void UpdateIdle()
    {
        return;
    }

    private void UpdateMoving()
    {
        return;
    }

    private void UpdateDie()
    {
        return;
    }

    public void Init()
    {
       
        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
        Managers.Input.KeyAction -= OnKeyBoardEvent;
        Managers.Input.KeyAction += OnKeyBoardEvent;

        _rb = GetComponent<Rigidbody>();

    }
    void OnMouseEvent(Define.MouseEvent evt)
    {
        //switch (State)
        //{
        //    case Define.State.Idle:
        //        OnMouseEvent_IdleRun(evt);
        //        break;
        //    case Define.State.Moving:
        //        OnMouseEvent_IdleRun(evt);
        //        break;
        //    case Define.State.Skill:
        //        {
        //            if (evt == Define.MouseEvent.PointerUp)
        //                _stopSkill = true;
        //        }
        //        break;
        //}
    }

    void OnKeyBoardEvent()
    {

        ///////8방향///// 

        //북동
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            _direction.Set(1, 0, 1);
            _direction.Normalize();        
        }


        //남동
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            _direction.Set(1, 0, -1);
            _direction.Normalize();
        }
        //남서
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            _direction.Set(-1, 0, -1);
            _direction.Normalize();
        }
        //북서
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            _direction.Set(-1, 0, 1);
            _direction.Normalize();
        }

        //북
        if (Input.GetKey(KeyCode.W))
        {
            _direction.Set(0, 0, 1);
            gameObject.transform.position += _direction * _speed * Time.deltaTime;

        }
        //동
        if (Input.GetKey(KeyCode.D))
        {
            _direction.Set(1, 0, 0);
            gameObject.transform.position += _direction * _speed * Time.deltaTime;

        }
        //남
        if (Input.GetKey(KeyCode.S))
        {
            _direction.Set(0, 0, -1);
            gameObject.transform.position += _direction * _speed * Time.deltaTime;

        }
        //서
        if (Input.GetKey(KeyCode.A))
        {
            _direction.Set(-1, 0, 0);
            gameObject.transform.position += _direction * _speed * Time.deltaTime;

        }




        //점프 만들기
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded) 
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            isGrounded = false;


        }

    }

    void OnCollisionEnter(Collision collision)
    {
        // 캐릭터가 땅에 닿았을 때 isGrounded를 true로 설정
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
