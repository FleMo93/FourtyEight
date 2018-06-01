using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ext_Quaternion
{

    public static Quaternion Copy(this Quaternion q)
    {
        return new Quaternion(q.x, q.y, q.z, q.w);
    }
}
