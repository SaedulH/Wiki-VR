using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MainMenu
{
    public class NodebuttonSelect : MonoBehaviour
    {

        [SerializeField]
        private StringSO SO;

        public void nodeButtonPressed()
        {   
            SO.Limiter = float.Parse(gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
            SettingsManager.current.checkNodeLimit();
        }

        public void TurnButtonPressed()
        {   
            if(gameObject.GetComponentInChildren<TextMeshProUGUI>().text == "Snap")
            {
                SO.Snapturn = true;
            }
            else
            {
                SO.Snapturn = false;
            }
            
            SettingsManager.current.checkTurnMethod();
        }  

    }
}