using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float moveSpeed;
    static float startSpeed=2f;
    private Vector2 movement;
    ScoreScript scoreSC;
    public GameObject EnemyDestroyEffect;

    // Start is called before the first frame update
    void Start()
    {
        scoreSC = GameObject.Find("Main Camera").GetComponent<ScoreScript>();
        player = GameObject.Find("Hero").transform;
        rb = this.GetComponent<Rigidbody2D>();
        if (scoreSC.score > 20)
        {
            moveSpeed = startSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        MoveEnemy(movement);
    }

    void MoveEnemy(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Ball")
        {
            Instantiate(EnemyDestroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            scoreSC.score++;
            ChangeSpeed();
            Debug.Log(moveSpeed);
        }
    }
    void ChangeSpeed()
    {
        if (scoreSC.score > 20)
        {
            startSpeed += 0.04f;
        }
        else startSpeed = 1f;
    }
}
