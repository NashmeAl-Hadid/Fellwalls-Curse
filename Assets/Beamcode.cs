using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beamcode : MonoBehaviour
{
    public LineRenderer laser;
    public Transform shotPos;
    public Transform EndPos;

    private void Update()
    {
        laser.SetPosition(0, shotPos.position);
        laser.SetPosition(1, EndPos.position);
    }


}
