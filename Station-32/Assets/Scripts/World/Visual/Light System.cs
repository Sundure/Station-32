using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This Class Are Chained With LightSystemCustomEditor.cs"
//Check LightSystemCustomEditor.cs To Get More Information

public class LightSystem : MonoBehaviour
{
    [SerializeField, HideInInspector] private List<float> _lightSwitchInterval = new();     //These Variabls Are Used In LightSystemCustomEditor.cs Check LightSystemCustomEditor.cs To Get More Information
    [SerializeField, HideInInspector] private List<bool> _enabled = new();                  //Change String Values In LightSystemCustomEditor.cs If You Want To Change This Variable Names

    [SerializeField] private Light _light;

    [SerializeField] private Material _enabledLightMaterial;
    [SerializeField] private Material _disabledLightMaterial;

    [SerializeField] private GameObject _lampTextureObject;

    private MeshRenderer _renderer;

    private byte _currentCycle;

    private IEnumerator _switchLightCoroutine;

    private void Awake()
    {
        _renderer = _lampTextureObject.GetComponent<MeshRenderer>();
    }

    private IEnumerator SwitchLight()
    {
        while (true)
        {

            if (_enabled[_currentCycle])
            {
                _light.enabled = true;

                _renderer.material = _enabledLightMaterial;
            }
            else
            {
                _light.enabled = false;

                _renderer.material = _disabledLightMaterial;
            }

            yield return new WaitForSeconds(Mathf.Abs(_lightSwitchInterval[_currentCycle]));

            _currentCycle++;

            if (_currentCycle > _lightSwitchInterval.Count - 1)
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
