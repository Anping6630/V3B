using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezeirCurve : MonoBehaviour
{
    public Transform[] points = new Transform[3];
    public int accuracy = 20; //精確度
    public ArrayList owo = new ArrayList();

    void Start()
    {
        points[1] = transform;
    }

    void Update()
    {
        Vector3 prev_pos = points[0].position;
        for (int i = 0; i <= accuracy; ++i)
        {
            Vector3 to = formula(i / (float)accuracy);
            owo.Add(formula(i / (float)accuracy));
            Debug.DrawLine(prev_pos, to);
            prev_pos = to;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        for (int i = 0; i < points.Length; ++i)
        {
            if (i < points.Length - 1)
            {
                if (4 == points.Length && i == 1)
                {
                    continue;
                }
                Vector3 current = points[i].position;
                Vector3 next = points[i + 1].position;

                Gizmos.DrawLine(current, next);
            }
        }
    }

    public Vector3 formula(float t)
    {
        switch (points.Length)
        {
            case 3: return quardaticBezier(t);

        }
        return Vector3.zero;
    }

    public Vector3 quardaticBezier(float t)
    {
        Vector3 a = points[0].position;
        Vector3 b = points[1].position;
        Vector3 c = points[2].position;

        Vector3 aa = a + (b - a) * t;
        Vector3 bb = b + (c - b) * t;
        return aa + (bb - aa) * t;
    }
}
