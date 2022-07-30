
using UnityEngine;
using UnityEngine.InputSystem;

public class Ascend : MonoBehaviour
{
    [SerializeField]
    private InputActionReference AscendReference;

    [SerializeField]
    private GameObject player;

    Vector3 moveDirection = new Vector3(0, 5, 0);
    

    void Start()
    {
        AscendReference.asset.Enable();
        AscendReference.action.performed += OnAscend;
    }

    public void OnAscend(InputAction.CallbackContext context)
    {
        moveDirection = transform.TransformDirection(moveDirection);
        player.transform.position += moveDirection;
    }

    private void OnDestroy()
    {
        AscendReference.action.performed -= OnAscend;
    }
}
