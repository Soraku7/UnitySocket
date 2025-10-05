using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerInfoItem : MonoBehaviour
    {
        private Text _nameTxt;
        private Slider _slider;

        public void Set(string nameStr , int hp)
        {
            _nameTxt.text = nameStr;
            _slider.value = hp;
        }

        public void UpdateHp(int v)
        {
            _slider.value = v;
        }
    }
}