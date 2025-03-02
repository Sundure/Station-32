using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSystem : MonoBehaviour
{
    [SerializeField] private List<float> _lightSwitchInterval = new();                   //Change String Values In LightSystemCustomEditor If You Want To Change This Variable Names
    [SerializeField] private List<bool> _enabled = new();    
    
    [SerializeField] private Light _light;      
    
    [SerializeField] private Material _enabledLightMaterial;                                               
    [SerializeField] private Material _disabledLightMaterial;

    [SerializeField] private GameObject _lampGlass;

    private MeshRenderer _renderer;

    private byte _currentCycle;

    private IEnumerator _switchLightCoroutine;

    private void Awake()
    {
        _renderer = _lampGlass.GetComponent<MeshRenderer>();
    }

    private IEnumerator SwitchLight()
    {
        while (true)
        {
            yield return new WaitForSeconds(Mathf.Abs(_lightSwitchInterval[_currentCycle]));

            if (_enabled[_currentCycle] )
            {
                _light.enabled = true;

                _renderer.material = _enabledLightMaterial;
            }
            else
            {
                _light.enabled = false;

                _renderer.material = _disabledLightMaterial;
            }

            _currentCycle++;

            if (_currentCycle > _lightSwitchInterval.Count -1)
            {
                _currentCycle = 0;
            }
        }
    }

    private void OnEnable()
    {
        _switchLightCoroutine = SwitchLight();

        StartCoroutine(_switchLightCoroutine);
    }

    private void OnDisable() => StopCoroutine(_switchLightCoroutine);
}
