using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherRotation : MonoBehaviour
{
    [SerializeField] private int HandleOffsetDegrees;
    [SerializeField] private bool rotating;

    private void FixedUpdate()
    {
        if (rotating)
        {
            RotateToMouse();
        }
    }

    public void StartRotating()
    {
        if(!rotating) rotating = true;
    }
    public void StopRotating()
    {
        if(rotating) rotating = false;
    }
    private void RotateToMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 angleVector;
        angleVector.x = mousePos.x - transform.position.x;
        angleVector.y = mousePos.y - transform.position.y;
        angleVector.z = 0;

        float angleDegrees = Mathf.Atan2(angleVector.y, angleVector.x) * Mathf.Rad2Deg;
//        print("Rotating " + angleDegrees);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleDegrees));
        transform.Rotate(Vector3.zero, HandleOffsetDegrees);
    }
}
