using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropController : MonoBehaviour
{
    public GameObject _hielo;
    public GameObject _desierto;
    GameObject _frontera;

    // Start is called before the first frame update
    void Start()
    {
        _frontera = GameObject.FindGameObjectWithTag("frontera");
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < _frontera.transform.position.x)
        {
            //Hielo
            _hielo.SetActive(true);
            _desierto.SetActive(false);
        }
        else if(transform.position.x > _frontera.transform.position.x)
        {
            //Desierto
            _hielo.SetActive(false);
            _desierto.SetActive(true);
        }
    }
}
