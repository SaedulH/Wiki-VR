using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace WebData
{
    public class SavedListButton : MonoBehaviour
    {

        [SerializeField]
        private TextMeshProUGUI buttonText;

        [SerializeField]
        public WebCall webCall;

        [SerializeField]
        private StringSO SO;

        public Animator Fade;

        public void setText(string textString)
        {
            buttonText.text = textString;
        }

        public void OnButtonPress()
        {
            StartCoroutine(ToSavedArticle());
            //showthesaved page
        }

        IEnumerator ToSavedArticle()
        {
            SO.PageName = buttonText.text;
            Fade.SetTrigger("Start");

            yield return new WaitForSeconds(0.5F);

            SceneManager.LoadScene("WikiPage");

        }
    }
}