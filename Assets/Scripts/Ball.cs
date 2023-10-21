using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private PhotonView pw;
    private Rigidbody rb;

    private int player_1_score = 0;
    private int player_2_score = 0;

    public TMPro.TextMeshProUGUI player_1_score_text;
    public TMPro.TextMeshProUGUI player_2_score_text;

    // Start is called before the first frame update
    void Start()
    {
        pw = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();

        //StartCoroutine(ApplyRandomForceAfterDelay(5f));
    }

    //private IEnumerator ApplyRandomForceAfterDelay(float delay)
    //{
    //    yield return new WaitForSeconds(delay);

    //    // Check if the ball is still in play (not scored)
    //    if (pw.IsMine && gameObject.activeSelf)
    //    {
    //        // Apply a random force to the ball
    //        Vector3 randomForce = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
    //        rb.AddForce(randomForce, ForceMode.Impulse);

    //        // Restart the coroutine to apply random force after another 5 seconds
    //        StartCoroutine(ApplyRandomForceAfterDelay(5f));
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void GameStart()
    {
        Vector3 randomForce = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
        rb.AddForce(randomForce, ForceMode.Impulse);
        ShowScore();
    }

    public void ShowScore()
    {
        player_1_score_text.text = PhotonNetwork.PlayerList[0].NickName + " : " + player_1_score;
        player_2_score_text.text = PhotonNetwork.PlayerList[1].NickName + " : " + player_2_score;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (pw.IsMine)
        {
            if(collision.gameObject.name == "Goal_1")
            {
                pw.RPC("Score", RpcTarget.All, 0, 1);
            }
            else if(collision.gameObject.name == "Goal_2")
            {
                pw.RPC("Score", RpcTarget.All, 1, 0);
            }
        }
    }

    [PunRPC]
    public void Score(int player_1, int player_2)
    {
        player_1_score += player_1;
        player_2_score += player_2;

        ShowScore();
        AfterScore();
    }

    public void AfterScore()
    {
        rb.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        StartCoroutine(ResetBallVelocityAfterDelay(0.1f)); // Delay the velocity reset for a short duration
    }

    private IEnumerator ResetBallVelocityAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = new Vector3(5, 0, 5);
    }

}
