using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationController : MonoBehaviour
{
   
    void Start()
    {
        Init();
    }

    
    void Update()
    {
        
    }

    void Init()
    {
        transform.parent.GetComponent<PlayerMoveController>()._KeyBoardAction -= OnLocalRotation;
        transform.parent.GetComponent<PlayerMoveController>()._KeyBoardAction += OnLocalRotation;


    }


    void OnLocalRotation(float angle)
    {
        Vector3 _angle = new Vector3(0, angle, 0);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(_angle), 40 * Time.deltaTime);

    }

}
