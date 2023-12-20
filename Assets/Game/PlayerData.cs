using UnityEngine;

[CreateAssetMenu(fileName = "PlayerShipData", menuName = "SO/Player")]
public class PlayerData : ScriptableObject
{
    [Tooltip("General")]
    public float healthPoint;
    public float moveSpeed;
    public float fireRate;

}
