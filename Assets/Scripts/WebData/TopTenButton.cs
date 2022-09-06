using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace WebData
{
    public class TopTenButton : MonoBehaviour
    {

        [SerializeField]
        private TextMeshProUGUI buttonText;

        [SerializeField]
        private GameObject ConfirmCanvas;

        [SerializeField]
        private TextMeshProUGUI pageName;

        public void setText(string textString)
        {
            buttonText.text = textString;
        }

        public void onClick()
        {
            ConfirmCanvas.SetActive(true);
            pageName.text = buttonText.text;
        }

    }
}