using UnityEngine;

[CreateAssetMenu(menuName = "Fight/UserData")]
public class UserData : ScriptableObject
{
    [SerializeField] private int _speed;
    [SerializeField] private int _rotationSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private int _attackRange;
    [SerializeField] private int _maxHealth;
    public float Speed => _speed;
    public float RotationSpeed => _rotationSpeed;
    public int Damage => _damage;
    public float AtackRange => _attackRange;
    public int MaxHealth => _maxHealth;
}
