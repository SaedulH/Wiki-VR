// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// namespace WebData
// {
//     public class ContentListControl : MonoBehaviour
//     {
//         [SerializeField]
//         [Tooltip("References the parent holding all buttons.")]
//         private GameObject ButtonsParent;

//         [SerializeField]
//         private GameObject buttonTemplate;  

//         [SerializeField]
//         private WebCall webCall;  

//         public void generateContentButtons()
//         {   
//             foreach (var contentData in webCall.ListofContents)
//             {
//                 GameObject button = Instantiate(buttonTemplate, ButtonsParent.transform) as GameObject;
//                 button.SetActive(true);

//                 button.GetComponent<ContentListButton>().setText(contentData.Key + ": " + contentData.Value);
                
//             }
//         }
//     }    
// }

