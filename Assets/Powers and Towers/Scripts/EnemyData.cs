using UnityEngine;
[CreateAssetMenu(fileName ="new Enemy Data",menuName = "ScriptableObject/Enemies/new Enemy")]
public class EnemyData : ScriptableObject
{
    public int health;
    public int lootValue;
    public float speed;
    public int EnemyID;
    [SerializeField] DamageType[] resistances;
    [SerializeField] DamageType[] Weakness;
    private Sprite spriteImage;

}