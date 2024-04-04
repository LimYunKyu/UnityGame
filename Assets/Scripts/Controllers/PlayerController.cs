using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Vector3 _direction = Vector3.zero;
    float _speed = 5.0f;

    [SerializeField]
    float _jumpForce = 5f;

    private Rigidbody _rb;
    private bool isGrounded = true;

    [SerializeField]
    protected Define.State _state = Define.State.Idle;

    Vector3 _angle = Vector3.zero;


    //DirectionRot

    float _forwardRot = 0f;
    float _backRot = 360f;
    float _leftRot = 90f;
    float _rightRot = 270f;
    float _frRot = 45f;
    float _rbRot = 135f;
    float _lbRot = 225f;
    float _flRot = 315f;
   

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
                    anim.CrossFade("RUN_F", 0.1f);
                    break;
                case Define.State.Skill:
                    anim.CrossFade("ATTACK", 0.1f, -1, 0);
                    break;
                case Define.State.Jump:
                    anim.CrossFade("JUMP", 0.1f);
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

        if (!isGrounded)
            return;
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        isGrounded = false;



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

        //Animator anim = GetComponent<Animator>();
        //anim.CrossFade("WAIT", 0.1f);

        //return;
    }

    private void UpdateMoving()
    {

        float applySpeed = _speed;
        //�ϵ�
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {

            _direction = transform.forward + transform.right;
            _direction.Normalize();
            
        }


        //����
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            _direction = transform.right - transform.forward;
            _direction.Normalize();
        }
        //����
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            _direction = -transform.right - transform.forward;
            _direction.Normalize();
        }
        //�ϼ�
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            _direction = -transform.right + transform.forward;
            _direction.Normalize();
        }

        //��
        else if (Input.GetKey(KeyCode.W))
        {
            _direction = transform.forward;


        }
        //��
        else if (Input.GetKey(KeyCode.D))
        {
            _direction = transform.right;

        }
        //��
        else if (Input.GetKey(KeyCode.S))
        {
            _direction = -transform.forward;


        }
        //��
        else if (Input.GetKey(KeyCode.A))
        {
            _direction = -transform.right;

        }
        else
        {
            applySpeed = 0;
        }

        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_direction), 40 * Time.deltaTime);

        gameObject.transform.position += _direction * applySpeed * Time.deltaTime;

        
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
        Managers.Input.NotKeyAction -= OffKeyBoardEvent;
        Managers.Input.NotKeyAction += OffKeyBoardEvent;

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
        UseKeyBoard();

    }

    void OffKeyBoardEvent()
    {

        UsetNotKeyboard();


    }
    void OnCollisionEnter(Collision collision)
    {
        // ĳ���Ͱ� ���� ����� �� isGrounded�� true�� ����
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            State = Define.State.Idle;
        }
    }


    void UseKeyBoard()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S))
        {

            if (State != Define.State.Moving)
                State = Define.State.Moving;

        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
                State = Define.State.Jump;

        }

    }

    void UsetNotKeyboard()
    {
        if (isGrounded)
            State = Define.State.Idle;

    }
}
