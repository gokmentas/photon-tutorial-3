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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void GameStart()
    {
        rb.velocity = new Vector3(5, 5, 0);
        ShowScore();
    }

    public void ShowScore()
    {
        player_1_score_text.text = PhotonNetwork.PlayerList[0].NickName + " : " + player_1_score;
        player_1_score_text.text = PhotonNetwork.PlayerList[0].NickName + " : " + player_1_score;
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
        rb.velocity = new Vector3(5, 5, 0);
    }

}
