using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrenoShader : MonoBehaviour
{
    public Material _mat;
    GameObject _frontera;
    // Start is called before the first frame update
    void Start()
    {
        _frontera = GameObject.FindGameObjectWithTag("frontera");
    }

    // Update is called once per frame
    void Update()
    {
        _mat.SetFloat("_fronteraPos", _frontera.transform.position.x);
    }
}
