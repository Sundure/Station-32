using UnityEngine;
using System.Collections.Generic;

public class PlayerMoveSystem : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private float _speed;

    private static readonly Dictionary<MonoBehaviour, float> SpeedMultiplierList = new();

    private void Awake()
    {
        PlayerInputSystem.OnAxisMoveInputXY += Move;
    }
    private void Move(float x, float y)
    {
        Vector3 vector3 = transform.right * x + transform.forward * y;

        float speed = _speed;

        if (SpeedMultiplierList.Count > 0)
            foreach (float multiplier in SpeedMultiplierList.Values)
            {
                speed *= multiplier;
            }

        _player.CharacterController.Move(0.1f * speed * Time.deltaTime * vector3);
    }

    /// <summary>
    /// Add Only One Speed Multiplier From Class Key
    /// </summary>
    /// <param name="classKey"></param>
    /// <param name="multiplier"></param>
    public static void AddSpeedMultipler(MonoBehaviour classKey, float multiplier)
    {
        if (SpeedMultiplierList.ContainsKey(classKey))
            return;

        SpeedMultiplierList.Add(classKey, multiplier);
    }


    /// <summary>
    /// Remove Key Multiplier From Class Key
    /// </summary>
    /// <param name="classKey"></param>
    public static void RemoveSpeedMultipler(MonoBehaviour classKey)
    {
        if (SpeedMultiplierList.ContainsKey(classKey))
            SpeedMultiplierList.Remove(classKey);
    }

    private void OnDestroy()
    {
        PlayerInputSystem.OnAxisMoveInputXY -= Move;
    }
}
