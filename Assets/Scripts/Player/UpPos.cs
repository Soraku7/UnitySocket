using System;
using Request;
using UnityEngine;

namespace Player
{
    public class UpPos : MonoBehaviour
    {
        private UpPosRequest _upPosRequest;

        private Transform gunTransform;
        
        private void Start()
        {
            _upPosRequest = GetComponent<UpPosRequest>();
            
            gunTransform = transform.Find("HandGun");
            InvokeRepeating("UpPosFun" , 1 , 1f/30f);
        }

        private void UpPosFun()
        {
            Vector2 pos = transform.position;
            float characterRot = transform.eulerAngles.z;
            float gunRot = gunTransform.eulerAngles.z;
            
            _upPosRequest.SendRequest(pos , characterRot, gunRot);
        }
    }
}