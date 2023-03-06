using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Get360Points
{
    /// <summary>
    /// This function is for getting 360 point
    /// </summary>
    /// <param name="selfPos">center of the 360 point</param>
    /// <param name="radius">distance between eatch point vs center</param>
    /// <param name="amount">number of point you want to have in 360</param>
    /// <returns></returns>
    public static List<Vector3> Angle360(Vector3 selfPos, float radius, int amount)
    {
        List<Vector3> vec = new List<Vector3>();
        float angle = 0;
        for (int i = 0; i < amount; i++)
        {
            float x = Mathf.Sin(angle);
            float y = Mathf.Cos(angle);
            float z = Mathf.Cos(angle);
            angle += 2 * Mathf.PI / amount;

            //Vector3 dir = new Vector3(transform.position.x + x, transform.position.y + y, 0);
            Vector3 dir = new Vector3(selfPos.x + x, selfPos.y + y, selfPos.z + z);

            dir = dir * radius;
            dir.y = selfPos.y;
            vec.Add(dir);
            
        }
        return vec;

    }
}
