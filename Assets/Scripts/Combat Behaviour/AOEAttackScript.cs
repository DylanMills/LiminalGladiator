using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class AOEAttackScript : MonoBehaviour
{
    public int AOEDamage = 10;
    [SerializeField]
    private AttackZone attackZone;
    [SerializeField] InputAction _AOEattackInput;
    void Awake()
    {
        _AOEattackInput.performed += PerformAOEAttack;
    }

    private void OnEnable()
    {
        EnableControls();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableControls()
    {
        _AOEattackInput.Enable();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void PerformAOEAttack(InputAction.CallbackContext ctx)
    {
        attackZone.EnableHitbox(AOEDamage);
        GetComponent<Animator>().SetTrigger("AOEAttack");
    }
}
