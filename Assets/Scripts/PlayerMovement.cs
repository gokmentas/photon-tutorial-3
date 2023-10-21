using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    PhotonView m_View;

    public float moveSpeed = 2;

    private float movement;

    // Start is called before the first frame update
    void Start()
    {
        m_View = GetComponent<PhotonView>();

        if (m_View.IsMine)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                transform.localPosition = new Vector3(-7, 6, 0);
                InvokeRepeating("PlayerControl", 0, 0.5f);
                transform.rotation = Quaternion.Euler(0, 90, 90);
            }
            else if(!PhotonNetwork.IsMasterClient)
            {
                transform.localPosition = new Vector3(7, 6, 0);
                transform.rotation = Quaternion.Euler(0, 90, 90);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_View.IsMine)
        {
            MovePlayer();
        }
    }

    void PlayerControl()
    {
        if(PhotonNetwork.PlayerList.Length == 2)
        {
            GameObject.Find("Ball").GetComponent<PhotonView>().RPC("GameStart", RpcTarget.All, null);
            CancelInvoke("PlayerControl");
        }
    }

    void MovePlayer()
    {
        movement = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
    
        transform.Translate(0, movement, 0);
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, -3.75f, 3.75f));
    }

} // class
