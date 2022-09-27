using UnityEngine;
using TMPro;

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