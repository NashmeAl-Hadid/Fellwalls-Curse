using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anemy : MonoBehaviour

{
    public float HorizontalSpeed;
    public float VerticalSpeed;
    public float Aplitude;

    float startTime, totalDistance;

    public Vector3 startPos, endPos;
    public bool repetable = false;

    // Vector3 tempPosition;
    // Start is called before the first frame update
    IEnumerator Start()
    // void Start()
    {
        startPos = transform.position;

        startTime = Time.time;
        totalDistance = Vector3.Distance(startPos, endPos);
        while (repetable)
        {
            startPos.x += HorizontalSpeed;
            endPos.y = Mathf.Sin(Time.realtimeSinceStartup * VerticalSpeed) * Aplitude;
            transform.position = transform.position;
            yield return RepratLerp(startPos, endPos, 6.0f);
            yield return RepratLerp(endPos, startPos, 6.0f);

        }
    }
    void Update()
    {
        if (!repetable)
        {
            float currentDuraction = (Time.time - startTime) * HorizontalSpeed;
            float journeyFraction = currentDuraction / totalDistance;
            this.transform.position = Vector3.Lerp(startPos, endPos, journeyFraction);
        }
    }
    public IEnumerator RepratLerp(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * HorizontalSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            this.transform.position = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}


