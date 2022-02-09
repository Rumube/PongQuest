using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public int puntosJ1;
    public int puntosJ2;

    public Pelota pelota;
    public GameObject _map;

    public GameObject panelPrincipal;
    public GameObject playerElection;
    public GameObject IAMode;

    public TMP_Text countDown;

    public bool _isPlayer1IA;
    public bool _isPlayer2IA;

    public Text _victoriaText;
    public GameObject _menuFinal;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(puntosJ1 - puntosJ2 == 5)
        {
            _menuFinal.SetActive(true);
            _victoriaText.text = "Player 1 win";
            pelota.setInGame(false);
        }else if(puntosJ2 - puntosJ1 == 5)
        {
            pelota.setInGame(false);
            _menuFinal.SetActive(true);
            _victoriaText.text = "Player 2 win";
        }

    }

    public void PlayGame()
    {
        playerElection.SetActive(true);
        panelPrincipal.SetActive(false);
    }
    public void SinglePlayer()
    {
        playerElection.SetActive(false);
        IAMode.SetActive(true);
    }
    public void Multiplayer()
    {
        pelota.setInGame(true);
        pelota.setLanzar(true);
        panelPrincipal.SetActive(false);
        playerElection.SetActive(false);
    }
    public void Return()
    {
        playerElection.SetActive(false);
        panelPrincipal.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReiniciarJuego()
    {
        puntosJ1 = 0;
        puntosJ2 = 0;
        _map.GetComponent<MapController>().ResetMap();
        pelota.Reset();
        pelota.setInGame(true);
    }

    public void ReiniciarScene()
    {
        SceneManager.LoadScene("RubenCopia");
    }

    public void StartGameIA(bool isPlayer1)
    {
        IAMode.SetActive(false);
        if (isPlayer1)
        {
            setIsPlayer1IA(true);
            setIsPlayer2IA(false);
        }
        else
        {
            setIsPlayer1IA(false);
            setIsPlayer2IA(true);
        }
        pelota.setInGame(true);
        pelota.setLanzar(true);
    }

    public void setIsPlayer1IA(bool value)
    {
        _isPlayer1IA = value;
    }

    public void setIsPlayer2IA(bool value)
    {
        _isPlayer2IA = value;
    }

    public bool getIsPlayer1IA()
    {
        return _isPlayer1IA;
    }

    public bool getIsPlayer2IA()
    {
        return _isPlayer2IA;
    }
}