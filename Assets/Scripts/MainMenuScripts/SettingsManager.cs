using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace MainMenu
{
    
    public class SettingsManager : MonoBehaviour
    {   
        #region Values
        public static SettingsManager current;

        [SerializeField]
        private StringSO SO;

        [SerializeField]
        private GameObject NodeButtonsParent;

        
        [SerializeField]
        private Button Snapturn;

        [SerializeField]
        private Button Smoothturn;

        [SerializeField]
        private Slider Volume;

        [SerializeField]
        private TextMeshProUGUI VolumeNum;

        Color32 chosenOption = new Color32(0, 65, 150, 255);
        Color32 unchosenOption = new Color32(0, 65, 150, 50);

        #endregion
        // Start is called before the first frame update
        void Start()
        {   
            current = this;
            checkNodeLimit();
            checkTurnMethod();
            checkVolume();
        }

        #region Setting functions
            
        public void checkNodeLimit()
        {   
            foreach(Transform button in NodeButtonsParent.transform)
            {
            
                if(SO.Limiter.ToString() == button.GetComponentInChildren<TextMeshProUGUI>().text)
                {
                    button.GetComponent<Image>().color = chosenOption;
                    button.Find("Border").GetComponent<Image>().enabled = true; 
                }
                else
                {
                    button.GetComponent<Image>().color = unchosenOption;
                    button.Find("Border").GetComponent<Image>().enabled = false;               
                }
            }   
        }
        public void checkTurnMethod()
        {           
            if(SO.Snapturn)
            {   
                Snapturn.GetComponent<Image>().color = chosenOption;
                Snapturn.transform.Find("Border").GetComponent<Image>().enabled = true;
                Smoothturn.GetComponent<Image>().color = unchosenOption;
                Smoothturn.transform.Find("Border").GetComponent<Image>().enabled = false; 
            }
            else
            {
                Snapturn.GetComponent<Image>().color = unchosenOption;
                Snapturn.transform.Find("Border").GetComponent<Image>().enabled = false;
                Smoothturn.GetComponent<Image>().color = chosenOption;
                Smoothturn.transform.Find("Border").GetComponent<Image>().enabled = true;               
            }
            MenuManager.current.checkTurn();
        }

        public void checkVolume()
        {
            Volume.value = SO.Volume;
            VolumeNum.text = Volume.value.ToString();
        }

        public void changeVolume()
        {
            VolumeNum.text = Volume.value.ToString();
            SO.Volume = Volume.value;
        }

        public void VolumeUp()
        {
            Volume.value += 1;
            changeVolume();
        }
        public void VolumeDown()
        {
            Volume.value -= 1;
            changeVolume();
        }
        #endregion
    }
}
