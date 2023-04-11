using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector2 movement;
    Rigidbody2D rigidbody2D;
    Animator animator;
    public ParticleSystem smokeEffect;

    public float changeTime = 3.0f;
    float timer;
    int direction = 1;

    private bool broken = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!broken) return;

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        var normalMoveVec = movement.normalized * direction;
        animator.SetFloat("Move X", normalMoveVec.x);
        animator.SetFloat("Move Y", normalMoveVec.y);
    }

    void FixedUpdate()
    {
        if (!broken) return;

        Vector2 position = rigidbody2D.position;
        position += movement * direction / changeTime * Time.deltaTime;

        rigidbody2D.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player == null) return;

        player.ChangeHealth(-1);
    }

    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
    }

}
