using UnityEngine;

[RequireComponent(typeof(PlayerFallSystem))]
public class SphereCheckVisualizer : MonoBehaviour
{
    private PlayerFallSystem _playerFallSystem;

    [Header("Visualization Settings")]
    public bool showGizmos = true;
    public Color groundCheckColor = new Color(0f, 1f, 0f, 0.5f); 
    public Color jumpCheckColor = new Color(0f, 0.5f, 1f, 0.5f); 
    public Color notGroundedColor = new Color(1f, 0f, 0f, 0.5f); 
    public Color cannotJumpColor = new Color(1f, 0.5f, 0f, 0.5f);

    private void Awake()
    {
        _playerFallSystem = GetComponent<PlayerFallSystem>();
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos || !Application.isPlaying)
            return;

        DrawDebugSpheres();
    }

    private void OnDrawGizmosSelected()
    {
        if (!showGizmos || Application.isPlaying)
            return;

        DrawDebugSpheres();
    }

    private void DrawDebugSpheres()
    {
        if (_playerFallSystem == null)
        {
            _playerFallSystem = GetComponent<PlayerFallSystem>();
            if (_playerFallSystem == null)
                return;
        }

        var playerFeetField = _playerFallSystem.GetType().GetField("_playerSphereCheckPoint",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var groundCheckRadiusField = _playerFallSystem.GetType().GetField("GROUND_CHECK_RADIUS",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var jumpCheckRadiusField = _playerFallSystem.GetType().GetField("JUMP_CHECK_RADIUS",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var playerPropertiesField = _playerFallSystem.GetType().GetField("_playerProperties",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (playerFeetField == null || groundCheckRadiusField == null ||
            jumpCheckRadiusField == null || playerPropertiesField == null)
            return;

        Transform playerFeet = (Transform)playerFeetField.GetValue(_playerFallSystem);
        float groundCheckRadius = (float)groundCheckRadiusField.GetValue(_playerFallSystem);
        float jumpCheckRadius = (float)jumpCheckRadiusField.GetValue(_playerFallSystem);
        PlayerProperties playerProperties = (PlayerProperties)playerPropertiesField.GetValue(_playerFallSystem);

        if (playerFeet == null || playerProperties == null)
            return;

        Gizmos.color = playerProperties.Grounded ? groundCheckColor : notGroundedColor;
        Gizmos.DrawSphere(playerFeet.position, groundCheckRadius);

        Gizmos.color = playerProperties.CanJump ? jumpCheckColor : cannotJumpColor;
        Gizmos.DrawWireSphere(playerFeet.position, jumpCheckRadius);

#if UNITY_EDITOR
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 12;
        style.fontStyle = FontStyle.Bold;

        Vector3 textPosition = playerFeet.position + Vector3.up * 0.2f;
        string statusText = $"Grounded: {playerProperties.Grounded}, CanJump: {playerProperties.CanJump}";
        UnityEditor.Handles.Label(textPosition, statusText, style);
#endif
    }
}