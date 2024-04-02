using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolderController : MonoBehaviour
{


    Vector3 _angle = Vector3.zero;


    void Start()
    {
        Init();   
    }

    
    void Update()
    {
        
    }

    void Init()
    {
        Managers.Input.KeyAction -= OnHolderMouseEvent;
        Managers.Input.KeyAction += OnHolderMouseEvent;



    }
    Quaternion accumulatedRotation = Quaternion.identity;

    void OnHolderMouseEvent()
    {
        //X
        if (Input.GetKey(KeyCode.Q))
        {

            _angle.x += Time.deltaTime * 50;
            

        }

        //Y
        if (Input.GetKey(KeyCode.E))
        {

            _angle.y += Time.deltaTime * 50;
            

        }


        Quaternion rotation = Quaternion.Euler(_angle);
        transform.rotation = rotation;
    }
}
