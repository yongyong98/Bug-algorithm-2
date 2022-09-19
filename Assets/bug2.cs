using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bug2 : MonoBehaviour
{
    public Transform goal;
    private Vector3 mline;
    RaycastHit2D[] hitData;
    public GameObject leavepoint;
    private Transform obstacle = null;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        mline = (goal.position - transform.position).normalized;
        Debug.Log(mline);
        hitData = Physics2D.RaycastAll(goal.position,mline*-1);

        foreach(RaycastHit2D data in hitData)
        {
            Instantiate(leavepoint,(Vector3)data.point,Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
         if(obstacle != null)
        {
            Vector3 toObstacle = obstacle.position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position,toObstacle.normalized*2f+ Quaternion.Euler(0,0,90) * toObstacle + transform.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, goal.position, speed * Time.deltaTime);
        }
        //Debug.Log(hitData[0].collider.gameObject);

    }
    void OnCollisionEnter2D(Collision2D collision) //collision에 부딫친 object가 들어옴  ## 부딫쳤을때
    {
        if(obstacle == null){
            //Instantiate(hitpoint, transform.position, Quaternion.identity);
            obstacle = collision.transform;
        }
    }
    void OnTriggerEnter2D(Collider2D other) {  //겹쳤을때
        obstacle = null;
    }
    
}
