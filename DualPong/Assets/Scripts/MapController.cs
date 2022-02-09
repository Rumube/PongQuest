using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject _frontera;
    public float _currentPos;
    public float _moveSpeed;
    public float _movimiento;
    public Cinemachine.CinemachineVirtualCamera _vcam;
    bool _isShake = false;
    public float _shakeStrenght;
    public GameObject _terrenoD;
    public GameObject _terrenoH;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateFrontera();
        _terrenoH.SetActive(true);
    }

    void UpdateFrontera()
    {
        if (_frontera.transform.position.x != _currentPos)
        {
            if (!_isShake)
            {
                _isShake = true;
                _vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = _shakeStrenght;
            }
            float _diferencia = Mathf.Abs(_currentPos - _frontera.transform.position.x);
            float _currentSpeed = (_moveSpeed + _diferencia) * Time.deltaTime;
            _frontera.transform.position = Vector3.MoveTowards(_frontera.transform.position, new Vector3(_currentPos, 0, 3.5f), _moveSpeed * Time.deltaTime);
        }
        else
        {
            if (_isShake)
            {
                _vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
                _isShake = false;
            }
        }
    }

    public void ResetMap()
    {
        _currentPos = 0;
        _terrenoH.transform.localPosition = new Vector3(0, 0, 8);
        _terrenoD.transform.localPosition = new Vector3(0, 0, -8);
    }

    public void moverFrontera(bool isPlayerOne)
    {
        if (isPlayerOne)
        {
            _currentPos += _movimiento;
            _terrenoH.transform.localPosition = new Vector3(_terrenoH.transform.localPosition.x, _terrenoH.transform.localPosition.y, _terrenoH.transform.localPosition.z - _movimiento);
            _terrenoD.transform.localPosition = new Vector3(_terrenoD.transform.localPosition.x, _terrenoD.transform.localPosition.y, _terrenoD.transform.localPosition.z - _movimiento);
        }
        else
        {
            _currentPos -= _movimiento;
            _terrenoH.transform.localPosition = new Vector3(_terrenoH.transform.localPosition.x, _terrenoH.transform.localPosition.y, _terrenoH.transform.localPosition.z + _movimiento);
            _terrenoD.transform.localPosition = new Vector3(_terrenoD.transform.localPosition.x, _terrenoD.transform.localPosition.y, _terrenoD.transform.localPosition.z + _movimiento);
        }
    }
}
