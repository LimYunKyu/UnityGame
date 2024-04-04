using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuarterView;
    [SerializeField]
    private GameObject _cameraHolder = null;
    [SerializeField]
    private float _distance = 3.0f;


    float offsetY;
    

    void Start()
    {
        
        _cameraHolder.GetComponent<CameraHolderController>().AfterMoveAction -= OnUpdateCameraPos;
        _cameraHolder.GetComponent<CameraHolderController>().AfterMoveAction += OnUpdateCameraPos;
        _distance = 3.0f;

    }

    public void SetQuarterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuarterView;
       
      
    }

    public void OnUpdateCameraPos()
    {

        
        Vector3 dir = _cameraHolder.transform.forward.normalized * _distance;
      
        if (_mode == Define.CameraMode.QuarterView)
        {
            if (_cameraHolder.activeSelf == false)
            {
                return;
            }

            RaycastHit hit;
            

            if (Physics.Raycast(_cameraHolder.transform.position, -dir.normalized, out hit, dir.magnitude, 1 << (int)Define.Layer.Block))
            {

                float dist = (hit.point - _cameraHolder.transform.position).magnitude * 0.9f;
                transform.position = _cameraHolder.transform.position + -dir.normalized * dist;  
                transform.position = new Vector3(transform.position.x, offsetY, transform.position.z);
                Vector3 target = _cameraHolder.transform.position - transform.position;
                transform.rotation = Quaternion.LookRotation(target);

               
               

            }
            else
            {

                offsetY = transform.position.y;
                transform.position = _cameraHolder.transform.position + -dir;
                
                if (Physics.Raycast(transform.position, Vector3.down, out hit, Vector3.down.magnitude, 1 << (int)Define.Layer.Block))
                {
                    if ((hit.point - transform.position).magnitude < 0.5)
                    { 
                        transform.position = new Vector3(transform.position.x,offsetY,transform.position.z);
                        
                    
                    }
                
                }

                transform.LookAt(_cameraHolder.transform);
            }


           
        }
    }
}
