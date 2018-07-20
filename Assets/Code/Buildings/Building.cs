using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    /// <summary>Build times per level (level- index in the list).</summary>
    public List<int> BuildTimes;

    /// <summary>Upgrade prices per level (level- index in the list).</summary>
    public List<int> Prices;
}
