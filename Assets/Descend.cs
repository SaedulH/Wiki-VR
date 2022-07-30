
using UnityEngine;
using UnityEngine.InputSystem;

public class Descend : MonoBehaviour
{
    [SerializeField]
    private InputActionReference DescendReference;

    [SerializeField]
    private GameObject player;

    Vector3 moveDirection = new Vector3(0, 5, 0);
    

    void Start()
    {

        DescendReference.asset.Enable();
        DescendReference.action.performed += OnDescend;
    }

    public void OnDescend(InputAction.CallbackContext context)
    {
        moveDirection = transform.TransformDirection(moveDirection);
        player.transform.position -= moveDirection;
    }

    private void OnDestroy()
    {
        DescendReference.action.performed -= OnDescend;
    }
}
