using UnityEngine;
[CreateAssetMenu(fileName ="",menuName ="")]
public class EnemyData : ScriptableObject
{
    public int health;
    public int lootValue;
    public float speed;
    [SerializeField] DamageType[] resistances;
    [SerializeField] DamageType[] Weakness;
    private Sprite spriteImage;
}