using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace WebData
{
    public class ContentListButton : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI buttonText;

        public void setText(string textString)
        {
            buttonText.text = textString;
        }

        public void OnClick()
        {
            
        }
    }
}