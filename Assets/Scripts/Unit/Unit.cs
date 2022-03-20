using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UserData _useData;
    public int TeamID { get; private set; }
    public float Speed { get; private set; }
    public float RotationSpeed { get; private set; }
    public int Damage { get; private set; }
    public float AtackRange { get; private set; } 
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    public Unit EnemyTarget { get; private set; }

    private UnitState _currentState;
    private UnitMoveToTargetState _moveToTargetState;
    private UnitShootingState _unitShootingState;

    private Weapon _weapon;
    public bool IsInited { get; private set; }
    private void Awake()
    {
        _weapon = GetComponentInChildren<Weapon>();
        _moveToTargetState = new UnitMoveToTargetState();
        _unitShootingState = new UnitShootingState();
    }

    private void Update()
    {
        if (CurrentHealth <= 0 || !IsInited)
        {
            StopShoot();
            return;
        }
            

        if (_currentState != null && _currentState.IsFinished == false)
            _currentState?.OnUpdate();
        else
        {
            if (EnemyTarget != null && EnemyTarget.CurrentHealth >0)
            {
                if (IsEnoughDistance(EnemyTarget.transform.position) == false)
                {
                    SetState(_moveToTargetState);
                }
                else
                {
                    SetState(_unitShootingState);
                }
            }
            else
            {
                FindTarget();
            }
        }
    }

    public void Init(int teamId, Material material, UserData userData = null)
    {
        if (userData != null)
            _useData = userData;

        Speed = _useData.Speed;
        RotationSpeed = _useData.RotationSpeed;
        Damage = _useData.Damage;
        AtackRange = _useData.AtackRange;
        MaxHealth = _useData.MaxHealth;

        CurrentHealth = MaxHealth;
        TeamID = teamId;
        var childrenRenderers = transform.GetComponentsInChildren<MeshRenderer>();
        foreach (var renderer in childrenRenderers)
        {
            renderer.sharedMaterial = material;
        }

        IsInited = true;
    }
    private void RotateToTarget(Vector3 target)
    {
        Vector3 targetDirection = target - transform.position;
        float singleStep = RotationSpeed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public void MoveTo(Vector3 target)
    {
        transform.position += (target - transform.position).normalized * Speed * Time.deltaTime;
        RotateToTarget(target);
    }

    private void FindTarget()
    {
        var unit = Game.Instance.FindEnemyForUnit(this);
        if (unit != null)
            EnemyTarget = unit;
    }

    public bool IsEnoughDistance(Vector3 target)
    {
        Vector3 offset = target - transform.position;
        var sqrDistance = offset.sqrMagnitude;
        if (sqrDistance < AtackRange * AtackRange)
        {
            return true;
        }

        return false;
    }

    public void OnDamaged(int damage, Unit enemy)
    {
        if (CurrentHealth >= 0)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
                Die();
        }   
    }
    private void Die()
    {
        IsInited = false;
        StopShoot();
        gameObject.SetActive(false);
    }

    private void SetState(UnitState state)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        state.Init(this);
        _currentState = state;
    }
    public void Shoot()
    {
        if (EnemyTarget == null)
            return;

        RotateToTarget(EnemyTarget.transform.position);
        _weapon.Shoot(EnemyTarget.transform);
    }
    public void StopShoot()
    {
        _weapon.StopShoot();
    }

    public void Restart()
    {
        EnemyTarget = null;
        CurrentHealth = MaxHealth;
        IsInited = true;
        gameObject.SetActive(true);
    }
}
