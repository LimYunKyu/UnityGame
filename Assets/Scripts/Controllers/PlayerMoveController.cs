using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{

    
    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private float _jumpForce = 5f;

    private Rigidbody _rb;
    private bool isGrounded = true;
    Vector3 _direction = Vector3.zero;


    [SerializeField]
    protected Define.State _state = Define.State.Idle;

    private GameObject _cameraHolder;
    Animator anim = null;

    public Action<float> _KeyBoardAction;

    public virtual Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;

           
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

        Vector3 dir = (Vector3.up + _direction);
        _rb.AddForce(dir * _jumpForce, ForceMode.Impulse);
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

    }

    private void UpdateMoving()
    {

        float applySpeed = _speed;
        //�ϵ�
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {

            _direction = transform.forward + transform.right;
            _direction.Normalize();
            _KeyBoardAction.Invoke(45);

        }


        //����
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            _direction = transform.right - transform.forward;
            _direction.Normalize();
            _KeyBoardAction.Invoke(135);
        }
        //����
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            _direction = -transform.right - transform.forward;
            _direction.Normalize();
            _KeyBoardAction.Invoke(225);
        }
        //�ϼ�
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            _direction = -transform.right + transform.forward;
            _direction.Normalize();
            _KeyBoardAction.Invoke(315);
        }

        //��
        else if (Input.GetKey(KeyCode.W))
        {
            _direction = transform.forward;
            _KeyBoardAction.Invoke(0);

        }
        //��
        else if (Input.GetKey(KeyCode.D))
        {
            _direction = transform.right;
            _KeyBoardAction.Invoke(90);

        }
        //��
        else if (Input.GetKey(KeyCode.S))
        {
            _direction = -transform.forward;
            _KeyBoardAction.Invoke(180);


        }
        //��
        else if (Input.GetKey(KeyCode.A))
        {
            _direction = -transform.right;
            _KeyBoardAction.Invoke(270);

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

       
        Managers.Input.KeyAction -= OnKeyBoardEvent;
        Managers.Input.KeyAction += OnKeyBoardEvent;
        Managers.Input.NotKeyAction -= OffKeyBoardEvent;
        Managers.Input.NotKeyAction += OffKeyBoardEvent;

        _rb = GetComponent<Rigidbody>();
        _cameraHolder = transform.Find("CameraHolder").gameObject;
        anim = GetAnimatorFromChild();


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

            if (State != Define.State.Moving && isGrounded)
                State = Define.State.Moving;

            Vector3 _dir = new Vector3(_cameraHolder.transform.forward.x, 0, _cameraHolder.transform.forward.z);
            _dir.Normalize();
            transform.rotation = Quaternion.LookRotation(_dir);

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
                State = Define.State.Jump;

        }

    }

    void UsetNotKeyboard()
    {
        if (isGrounded)
            State = Define.State.Idle;

        _direction = Vector3.zero;

    }

    Animator GetAnimatorFromChild()
    {
        Transform[] children = GetComponentsInChildren<Transform>(true);
        Animator animator = null;
        foreach (Transform child in children)
        {
            
            if (child.GetComponent<Animator>())
            {
                animator = child.GetComponent<Animator>();
            }
        }

        return animator;
    }
}
