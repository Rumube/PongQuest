using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pared : MonoBehaviour
{
     public AudioSource aSource;
    public AudioClip reboteJugador;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Pelota"){
            aSource.clip = reboteJugador;         
            aSource.Play();
        }
    }
}
