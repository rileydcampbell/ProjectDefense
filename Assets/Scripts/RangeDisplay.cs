using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class RangeDisplay : MonoBehaviour
{
    [Range(0, 50)]
    public int segments = 50;

    public float xradius = 20;
    public float yradius = 20;
    public GameObject tower;
    LineRenderer line;

    void Start()
    {
        xradius = tower.GetComponent<Turret>().range * 3.5f;
        yradius = tower.GetComponent<Turret>().range * 3.5f;
        line = gameObject.GetComponent<LineRenderer>();

        line.positionCount = (segments + 1);
        line.useWorldSpace = false;
        CreatePoints();
    }

    void CreatePoints()
    {
        float x;
        float y;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, 0, z));

            angle += (360f / segments);
        }
    }
}
