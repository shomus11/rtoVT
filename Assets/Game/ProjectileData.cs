using UnityEngine;

[CreateAssetMenu(fileName = "PlayerShipData", menuName = "SO/Projectile")]
public class ProjectileData : ScriptableObject
{
    [Tooltip("General Component")]
    public Sprite Sprite;
    public float FireRate;
    public float Dmg;
    public float MoveSpeed;
    public float LiveTime;
}
