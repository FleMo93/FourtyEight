using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Ext
{
    public static bool isSameAs(this Color32 p, Color32 p2)
    {
        return (p.r == p2.r && 
            p.g == p2.g && 
            p.b == p2.b && 
            p.a == p2.a );
    }
}
