using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuarterView;

    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);


    Vector3 _cameraDir = Vector3.zero;

    [SerializeField]
    GameObject _player = null;

    [SerializeField]
    GameObject _cameraHolder = null;

    GameObject _offertObj;

    
    public void SetPlayer(GameObject player) { _player = player; }

    void Start()
    {
        _offertObj = new GameObject();
    }

    void LateUpdate()
    {


        Vector3 dir = _cameraHolder.transform.forward * 3;
        
        transform.position = -dir * 3;
        transform.LookAt(_cameraHolder.transform);

        if (_mode == Define.CameraMode.QuarterView)
        {
            if (_cameraHolder.activeSelf == false)
            {
                return;
            }

            RaycastHit hit;

            if (Physics.Raycast(_cameraHolder.transform.position, -dir.normalized, out hit, dir.magnitude, 1 << (int)Define.Layer.Block))
            {


                float dist = (hit.point  - _cameraHolder.transform.position).magnitude * 0.8f;
                transform.position = _cameraHolder.transform.position + -dir.normalized * dist;
            }
            else
            {

                _offertObj.transform.position = _cameraHolder.transform.position;

                transform.position = _cameraHolder.transform.position + -dir;
                transform.LookAt(_offertObj.transform);
            }
        }
    }

    public void SetQuarterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuarterView;
        _delta = delta;
    }
}
