using UnityEngine;
using TMPro;

namespace WebData
{
    public class ContentListButton : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI buttonText;

        [SerializeField]
        public WebCall webCall;

        public void setText(string textString)
        {
            buttonText.text = textString;
        }
        // Make appropriate API call on press
        public void OnButtonPress()
        {
            
            if(buttonText.text == "Brief Description")
            {
                StartCoroutine(webCall.GetIntroRequest());
                webCall.Selectedfromlist("Brief Description");
            }
            else
            {   
                foreach(var item in webCall.ListofContents)
                    if(buttonText.text == item.Value)
                    {
                        StartCoroutine(webCall.GetSectionRequest(item.Key));
                        webCall.Selectedfromlist(item.Value);
                    }
                    else
                    {
                        //do nothing
                    }
            }
        }
    }
}