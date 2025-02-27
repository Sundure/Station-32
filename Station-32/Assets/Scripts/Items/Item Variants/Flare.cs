using UnityEngine;
public class Flare : Item
{
    [SerializeField] private float _lifeTime;

    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Light _light;
    [SerializeField] private AudioSource _audioSource;

    private float _currentLifeTime;

    private float _maxLightIntensity;

    private bool _used;

    protected override void Awake()
    {
        base.Awake();

        enabled = false;

        _currentLifeTime = _lifeTime;
        _maxLightIntensity = _light.intensity;
    }


    public override void Use()
    {
        if (_used) return;

        _used = true;
        _light.enabled = true;
        _audioSource.loop = true;
        _particleSystem.Play();

        enabled = true;

        _audioSource.Play();
    }

    private void Update()
    {
        float lifeStrenght = CustomMathf.GetPrecent(_currentLifeTime, _lifeTime) / 100;

        _light.intensity = _maxLightIntensity * lifeStrenght;
        _audioSource.volume = lifeStrenght;
        //  _particleSystem.

        if (_currentLifeTime - Time.deltaTime > 0)
            _currentLifeTime -= Time.deltaTime;
        else
        {
            _light.enabled = false;
            _audioSource.loop = false;
            _particleSystem.Stop();
        }
    }

    private void OnEnable()
    {
        if (_used && _currentLifeTime > 0)
            _particleSystem.Play();
    }

    protected override void OnItemDrop()
    {

    }
}
