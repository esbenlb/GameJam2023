using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public enum ResourseType
    {
        Rock,
        Water,
        Nitrogen,
        SpawingPoint,
    }
    public ResourseType CurrentResourceType = ResourseType.Rock;
}
