using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerController : MonoBehaviour
{

    private Animator animator;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 moveVector = new Vector3 (moveX, moveY, 0f);

        transform.Translate(moveVector.normalized * Time.deltaTime * 5f);

        if(moveX!=0 ||moveY!=0)
        {
        }
        else
        {
        }
    }
}
