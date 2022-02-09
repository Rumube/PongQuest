using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugadores : MonoBehaviour
{
    GameObject _pelota;
    GameManager _gm;
    public int speed;
    public bool jugador1;
    float translation;
    Vector3 startposition;
    Rigidbody rb;
    public float maxY;
    public float minY;
    // Start is called before the first frame update
    void Start()
    {
        _pelota = GameObject.FindGameObjectWithTag("Pelota");
        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        startposition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jugador1)
        {
            moveP1();
        }
        else
        {
            moveP2();
        }

        transform.Translate(0, translation, 0);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);

    }

    void moveP1()
    {
        if (_gm.getIsPlayer1IA())
        {
            IAController();
        }
        else
        {
            translation = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        }
    }

    void moveP2()
    {
        if (_gm.getIsPlayer2IA())
        {
            IAController();
        }
        else
        {
            translation = Input.GetAxisRaw("Vertical2") * speed * Time.deltaTime;
        }
    }

    void IAController()
    {
        if (_pelota.transform.position.y > transform.position.y)
        {
            translation = 1 * speed * Time.deltaTime;
        }
        else if (_pelota.transform.position.y < transform.position.y)
        {
            translation = -1 * speed * Time.deltaTime;
        }
        else
        {
            translation = 0 * speed * Time.deltaTime;
        }
    }

}