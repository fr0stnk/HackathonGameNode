using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <remarks>Set in editor.</remarks>
    public Map Map;

    // Use this for initialization
    private void Start ()
	{
	    Random.InitState(54);

	}

	// Update is called once per frame
    private void Update ()
	{

	}
}
