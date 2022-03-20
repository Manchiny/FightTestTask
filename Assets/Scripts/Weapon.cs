using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Unit Unit { get; private set; }
    Ray shootRay;                                   
    RaycastHit shootHit;                            
    LineRenderer gunLine;

    private float _timeBetweenBullets = 0.1f;
    private float _timer;
    private void Awake()
    {
        Unit = GetComponentInParent<Unit>();
        gunLine = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        _timer = 0;
        StopShoot();
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
    }
    public void Shoot(Transform enemyTransform)
    {
        if (_timer >= 0)
        {
        //    StopShoot();
            return;
        }

        _timer = _timeBetweenBullets;

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = enemyTransform.transform.position - transform.position + new Vector3(0, 0.5f, 0);  
        if (Physics.Raycast(shootRay, out shootHit, Unit.AtackRange))
        {
            if (shootHit.collider.GetComponent<Unit>() != null)
            {
                var damagedObject = shootHit.collider.GetComponent<Unit>();
                if (damagedObject.TeamID != Unit.TeamID)
                {
                    damagedObject.OnDamaged(Unit.Damage, Unit);
                }
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * (Unit.AtackRange));
        }
    }

    public void StopShoot()
    {
        gunLine.enabled = false;
    }
}
