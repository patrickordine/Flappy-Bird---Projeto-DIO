using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{

    void FixedUpdate()
    {
        // Get GameManager
        var gameManager = GameManager.Instance;
        //Ignore if game is over
        if(gameManager.IsGameOver()){
            return;
        }

        float x = GameManager.Instance.obstacleSpeed * Time.fixedDeltaTime;
        transform.position -= new Vector3(x,0,0);

        if(transform.position.x <= -GameManager.Instance.obstacleOffsetX){
            Destroy(gameObject);
        }

    }
}
