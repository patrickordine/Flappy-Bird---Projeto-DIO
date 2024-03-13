using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float jumpPower = 10f;
    public float jumpInterval = 0.2f;
    private float jumpCooldown = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Update cooldown
        jumpCooldown -= Time.deltaTime;
        bool IsGameActive = GameManager.Instance.IsGameActive();
        bool canJump = jumpCooldown <= 0f && IsGameActive;

        //Jump
        if(canJump){
            bool jumpInput = Input.GetKey(KeyCode.Space);
            if(jumpInput){
                Jump();
            }
        }

        //toggle gravity
        rb.useGravity = IsGameActive;
    }

    void OnTriggerEnter(Collider other) {
        OnCustomCollisionEnter(other.gameObject);
    }
    void OnCollisionEnter(Collision other) {
         OnCustomCollisionEnter(other.gameObject);
    }
    void OnCustomCollisionEnter(GameObject other){
        bool isSensor = other.CompareTag("Sensor");
        if(isSensor){
            //pontua
            GameManager.Instance.score++;
            Debug.Log("Score: "+ GameManager.Instance.score);

        }else{
            //game over
            GameManager.Instance.EndGame();
        }

    }


    private void Jump(){
        jumpCooldown = jumpInterval;

        rb.velocity = Vector3.zero;
        rb.AddForce(new Vector3(0f,jumpPower,0f), ForceMode.Impulse);
    }
}
