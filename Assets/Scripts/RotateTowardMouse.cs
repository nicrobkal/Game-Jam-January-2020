using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardMouse : MonoBehaviour
{
    private Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 180);

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

}
