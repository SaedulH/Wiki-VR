using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Search
{

    public class SearchListButton : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI buttonText;

        public void setText(string textString)
        {
            buttonText.text = textString;
        }

    }
}