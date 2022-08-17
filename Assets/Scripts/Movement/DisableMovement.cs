using UnityEngine;


public class DisableMovement : MonoBehaviour
{
    
    [SerializeField]
    private UIcheckerSO UIchecker;

    [SerializeField]
    private UnityEngine.XR.Interaction.Toolkit.ActionBasedContinuousMoveProvider moveProvider;
    
    [SerializeField]
    private Rigidbody _body;
    // Start is called before the first frame update
    void Start()
    {
       UIchecker.showingUI = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if(UIchecker.showingUI)
        {
            _body.velocity = new Vector3(0,0,0);
            moveProvider.enabled = false;
        }
        else
        {
            moveProvider.enabled = true;
        }
    }
}
