using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class OldController : MonoBehaviour
{
    public bool LockedOn = false;
    public Transform target;
    int animNum = 0;
    public Animator anim;
    float timeOfFirstButton = 0f;
    bool pressedRecently = false;
    public Rigidbody rb;
    public float speed = 6;
    private Vector3 input;
    public Vector3 moveDir;
    public Transform camera;
    public float rotate;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("InCombo", true);
            pressedRecently = true;
        }
        if (Input.GetMouseButton(1))
        {
            LockedOn = true;
            LockOn();
        }
        else
        {
            LockedOn = false;
            camera.gameObject.GetComponent<CinemachineBrain>().enabled = true;
        }
        if (!LockedOn)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                rb.velocity = transform.forward * speed;
                anim.SetBool("Walking", true);
            }
            else
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);

                anim.SetBool("Walking", false);
            }
        }
        else
        {
            input += transform.forward * Input.GetAxis("Vertical") * speed;
            input += transform.right * Input.GetAxis("Horizontal") * speed;
            rb.velocity = input;
        }
        input = new Vector3(0, 0, 0);
    }
    void LockOn()
    {
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z), Vector3.up);
        camera.gameObject.GetComponent<CinemachineBrain>().enabled = false;
        camera.LookAt(new Vector3(target.position.x, transform.position.y+0.5f, target.position.z), Vector3.up);
    }
    void StartCycle()
    {
        pressedRecently = false;
    }
    void End()
    {
        if (!pressedRecently)
        {
            anim.SetBool("InCombo", false);
        }
        pressedRecently = false;
    }
    void End2()
    {
        print(pressedRecently);
        if (!pressedRecently)
        {
            anim.SetBool("InCombo", false);
        }
        pressedRecently = false;
    }
    void End3()
    {
        anim.SetBool("InCombo", false);
        pressedRecently = false;
    }
}
