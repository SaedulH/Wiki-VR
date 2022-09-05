using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

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

                

                // string number = new string(buttonText.text.SkipWhile(c=>!char.IsDigit(c))
                //          .TakeWhile(c=>char.IsDigit(c))
                //          .ToArray());
                //     Debug.Log(buttonText.text + " to "+ number);
                //     StartCoroutine(webCall.GetSectionRequest(number));
            }
        }
    }
}