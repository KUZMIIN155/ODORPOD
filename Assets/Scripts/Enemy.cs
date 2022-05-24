using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float speedBoost = 1.0f;
    [SerializeField] private float jump = 3.0f;
    [SerializeField] private float distanceAngry = 5.0f;
    [SerializeField] private float distancePatrol = 6.0f;
    [SerializeField] private float distanceAttack;
    [SerializeField] private int damage;
    [SerializeField] private float reboot;
    [SerializeField] private float attackRange;
    [SerializeField] private Transform attackPose;
    [SerializeField] private LayerMask enemyMask;

    private float minDistance;
    private float maxDistance;
    private Rigidbody2D rb;
    private HealthManager healthManager;
    private bool patrol = true;
    private bool attack = true;
    private Transform player;

    private void Start()
    {
        player = GameObject.Find("HeroKnight").transform;
        rb = GetComponent<Rigidbody2D>();
        healthManager = GetComponent<HealthManager>();
        minDistance = transform.position.x - distancePatrol;
        maxDistance = transform.position.x + distancePatrol;
    }

    private void Update()
    {
        if (patrol == true)
            Patrol();
        else
            Angry();
        if (Vector2.Distance(transform.position, player.position) < distanceAngry)
        {
            patrol = false;
        }
    }

    private void Patrol()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
        if (transform.position.x > maxDistance)
        {
            speed = -speed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (transform.position.x < minDistance)
        {
            speed = -speed;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void Attack()
    {
        if (attack == true)
        {
            attack = false;
            Collider2D[] enemiscToDamage = Physics2D.OverlapCircleAll(attackPose.position, attackRange, enemyMask);
            for (int i = 0; i < enemiscToDamage.Length; i++)
            {
                enemiscToDamage[i].GetComponent<HeroKnight>().Damage(damage);
            }
            Invoke("AttackReset", reboot);
        }
    }

    private void Angry()
    {
        if(patrol == false)
        {
            Vector2 moveVector = Vector2.MoveTowards(transform.position, player.position, speedBoost * Mathf.Abs (speed) * Time.deltaTime);
            transform.position = new Vector2(moveVector.x, transform.position.y);
            if (Vector2.Distance (transform.position, player.position) < distanceAttack)
            {
                Attack();
            }
            if (transform.position.x > player.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (transform.position.x < player.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
    public void Damage(int damage)
    {
        healthManager.health -= damage;
        healthManager.UpdateHealth();
        if (healthManager.health <= 0)
            Destroy(gameObject);
    }

    private void AttackReset()
    {
        attack = true;
    }
}
