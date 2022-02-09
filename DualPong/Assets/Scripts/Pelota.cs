using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Pelota : MonoBehaviour
{
    [Header("Referencias")]
    //REFERENCIAS
    public GameObject _psArena;
    public GameObject _psHielo;
    Rigidbody rb;
    public GameObject _mapController;
    public GameManager gameManager;
    public Cinemachine.CinemachineVirtualCamera _vcam;

    [Header("Configuración")]
    //CONFIGURACIÓN
    public float speed;
    float _startSpeed;
    public float speedRotation;
    public float wait;
    float rotation;

    Vector3 startPosition;
    Vector3 direccion;

    [Header("Controladores")]
    [HideInInspector]
    bool lanzar = false;
    bool rotar = false;
    bool move = true;
    bool _inGame = false;
    bool Jugador1;
    Vector3 target;

    [Header("Audio")]
    //AUDIO
    public AudioSource aSource;
    public AudioClip reboteJugador;
    public AudioClip gol;
    void Start()
    {
        _startSpeed = speed;
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        float random = Random.Range(0, 2);
        if (random < 1)
        {
            Jugador1 = true;
        }
        else
        {
            Jugador1 = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_inGame)
        {
            rotation += Time.deltaTime * speedRotation;
            transform.rotation = Quaternion.Euler(0, 0, rotation);
            if (move == true)
            {
                Mover();
            }
            if (rotar == true)
            {
                transform.RotateAround(startPosition, Vector3.forward, 360 * Time.deltaTime);

            }

            if (lanzar == true)
            {

                Lanzar();
            }
        }
    }
    void Lanzar()
    {
        StartCoroutine(Wait());
    }

    void Mover()
    {
        float step = speed * Time.deltaTime;
        if (Jugador1 == true)
        {
            target = startPosition;
            target.x += 1;
        }
        else
        {
            target = startPosition;
            target.x -= 1;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            GetComponent<Collider>().isTrigger = false;
            move = false;
            lanzar = true;
            rotar = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 2;
            Jugador1 = collision.gameObject.GetComponent<Jugadores>().jugador1;

            aSource.clip = reboteJugador;
            aSource.Play();

            if (Jugador1)
            {
                GameObject newParticle = Instantiate(_psHielo);
                newParticle.transform.position = transform.position;
                StartCoroutine(ParticleTime(newParticle));
            }
            else
            {
                GameObject newParticle = Instantiate(_psArena);
                newParticle.transform.position = transform.position;
                StartCoroutine(ParticleTime(newParticle));
            }
            speed += 0.5f;
            float y = Random.Range(-0.5f, 0.5f);
            if (Jugador1)
            {
                rb.velocity = new Vector2(1 * speed, y * speed);
            }
            else
            {
                rb.velocity = new Vector2(-1 * speed, y * speed);
            }

        }
        if (collision.gameObject.tag == "Meta")
        {
            aSource.clip = gol;
            aSource.Play();

            if (transform.position.normalized.x > 0)
            {
                gameManager.puntosJ1 += 1;
                _mapController.GetComponent<MapController>().moverFrontera(true);
                Jugador1 = true;
            }
            else
            {
                gameManager.puntosJ2 += 1;
                _mapController.GetComponent<MapController>().moverFrontera(false);
                Jugador1 = false;
            }
            Reset();
            speed = _startSpeed;
        }

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Meta")
        {
            Reset();
        }
    }
    public void Reset()
    {
        move = true;
        GetComponent<Collider>().isTrigger = true;
        rb.velocity = (Vector3.zero);
    }

    IEnumerator Wait()
    {
        lanzar = false;

        gameManager.countDown.gameObject.SetActive(true);
        gameManager.countDown.text = "3";
        yield return new WaitForSeconds(1);
        gameManager.countDown.text = "2";
        yield return new WaitForSeconds(1);
        gameManager.countDown.text = "1";
        yield return new WaitForSeconds(1);
        gameManager.countDown.gameObject.SetActive(false);
        rotar = false;

        int x;
        if (Jugador1 == true)
        {
            x = 1;
        }
        else
        {
            x = -1;
        }
        float y = Random.Range(-0.5f, 0.5f);
        if (y == 0)
        {
            y = 1;
        }

        Vector3 direccion = new Vector3(x, y, 0);
        rb.velocity = new Vector2((direccion.x * speed), (direccion.y * speed));
        lanzar = false;
    }

    IEnumerator ParticleTime(GameObject particle)
    {
        yield return new WaitForSeconds(0.5f);
        _vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        Destroy(particle);
    }

    public void setInGame(bool value)
    {
        _inGame = value;
    }
    public void setLanzar(bool value)
    {
        lanzar = value;
    }
}
