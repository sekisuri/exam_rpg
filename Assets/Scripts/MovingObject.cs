using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    static public MovingObject instance;

    public string currentMapName;

    private BoxCollider2D boxCollider;
    public LayerMask layerMask;

    public float speed;
    private Vector3 vector;
    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;


    public int walkCount;
    private int currentWalkCount;

    private bool canMove = true;
    // speed = 2.4, walkCount = 20
    // 2.4 * 20 = 48
    // While

    private Animator animator;

    private void Start()
    {
        if(instance != null)
        {

            Destroy(this.gameObject);
        }
        else
        {
    
            DontDestroyOnLoad(this.gameObject);
            animator = GetComponent<Animator>();
            boxCollider = GetComponent<BoxCollider2D>();
            instance = this;
        }
        
    }
    IEnumerator MoveCoroutine()
    {
        while(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if(vector.x != 0)
            {
                vector.y = 0;
            }

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;

            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);
            boxCollider.enabled = false;

            hit = Physics2D.Linecast(start, end, layerMask);
            boxCollider.enabled = true;
            if(hit.transform != null)
            {
                break;
            }
            animator.SetBool("Walking", true);


            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
                if (applyRunFlag)
                {
                    currentWalkCount++;
                }
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;

            
        }
        animator.SetBool("Walking", false);
        canMove = true;

    }
    private void Update()
    {
        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }
        
    }

}