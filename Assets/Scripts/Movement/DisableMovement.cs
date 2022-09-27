using UnityEngine;


public class DisableMovement : MonoBehaviour
{
    
    [SerializeField]
    private UIcheckerSO UIchecker;

    [SerializeField]
    private StringSO SO;

    [SerializeField]
    private UnityEngine.XR.Interaction.Toolkit.ActionBasedContinuousMoveProvider moveProvider;

    [SerializeField]
    private UnityEngine.XR.Interaction.Toolkit.ActionBasedContinuousTurnProvider smoothProvider;

    [SerializeField]
    private UnityEngine.XR.Interaction.Toolkit.ActionBasedSnapTurnProvider snapProvider;
    
    [SerializeField]
    private Rigidbody _body;
    // Start is called before the first frame update
    void Start()
    {
       UIchecker.showingUI = false;
       checkTurn();
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

    void checkTurn()
    {
        if(SO.Snapturn)
        {
            //do nothing
        }
        else 
        {   
            snapProvider.enabled = false;
            smoothProvider.enabled = true;
        }
        
    }
}
