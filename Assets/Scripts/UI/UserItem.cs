using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UserItem : MonoBehaviour
    {
        private Text _userNameTex;

        private void Awake()
        {
            _userNameTex = transform.Find("UserNameTex").GetComponent<Text>();
        }

        public void SetUserInfo(string name)
        {
            _userNameTex.text = name;
        }
    }
}