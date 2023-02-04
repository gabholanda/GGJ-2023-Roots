using UnityEngine.InputSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "Input Reader", menuName = "ScriptableObjects/Input Reader")]
public class InputReader : ScriptableObject
{
    public InputAction OnFire;
    public InputAction OnTeleport;
}