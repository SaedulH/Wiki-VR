using UnityEngine;
using TMPro;

namespace Search
{

    public class SearchListButton : MonoBehaviour
    {
        [SerializeField]
        private GameObject ConfirmPanel;

        [SerializeField]
        private TextMeshProUGUI buttonText;

        [SerializeField]
        private TextMeshProUGUI confirmationText;

        [SerializeField]
        private TextMeshProUGUI resultText;

        public void setText(string textString)
        {
            buttonText.text = textString;
        }
        public void selectMade()
        {   
            GameObject.FindGameObjectWithTag("Audio").GetComponentInChildren<AudioManager>().Play("Forward");

            ConfirmPanel.SetActive(true);
            if(!SearchManager.thissearchManager.showingPage)
            {
                confirmationText.text = "Load graph for";   
            }
            else
            {
                confirmationText.text = "Load page for";
            }
            resultText.text = GetComponentInChildren<TextMeshProUGUI>().text;            
        }
    }

    
}