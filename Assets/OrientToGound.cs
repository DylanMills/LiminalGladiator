using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class OrientToGound : MonoBehaviour
{
    void Update()
    {

        Ray ray = new Ray(transform.position, -Vector3.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            transform.rotation = Quaternion.Euler(hit.normal+new Vector3(90,0,0));
        }
    }
}
