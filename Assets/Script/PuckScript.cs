using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckScript : MonoBehaviour
{
    // Start is called before the first frame update
    public ScoreScript scoreScriptInstance;
    public static bool WasGoal {  get; private set; }
    private Rigidbody2D rb;
    public float maxSpeed;
    public AudioManager audioManager;
     void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        WasGoal = false;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(!WasGoal)
        {
            if(other.tag =="AiGoal")
            {
                scoreScriptInstance.Increment(ScoreScript.Score.PlayerScore);
                WasGoal = true;
                audioManager.PlayGoal();
                StartCoroutine(ResetPuck(false));
            }
            else if(other.tag =="PlayerGoal")
            {
                scoreScriptInstance.Increment(ScoreScript.Score.AiScore);
                WasGoal = true;
                audioManager.PlayGoal();
                StartCoroutine(ResetPuck(true));
            }



        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioManager.PlayPuckCollision();
    }
    private IEnumerator ResetPuck(bool didAiScore)
    {
        yield return new WaitForSecondsRealtime(1);
        WasGoal=false;
        rb.velocity = rb.position = new Vector2(0, 0);
        if (didAiScore)
            rb.position = new Vector2(1, 0);
        else
            rb.position = new Vector2(-1, 0);
    }
    public void CenterPuck()
    {
        rb.position = new Vector2 (0, 0);

    }
    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }
}
