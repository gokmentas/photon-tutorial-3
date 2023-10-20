using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PhotonView m_View;

    public float moveSpeed = 1f;

    private float movementX, movementY;

    private int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        m_View = GetComponent<PhotonView>();

        if (!m_View.IsMine)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_View.IsMine)
        {
            MovePlayer();
            Shoot();
        }
    }

    void MovePlayer()
    {
        movementX = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        movementY = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;

        transform.Translate(movementX, 0, movementY);
    }

    void Shoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if(Physics.Raycast(transform.position, transform.forward, out hit, 100.0f))
            {
                hit.collider.gameObject.GetComponent<PhotonView>().RPC("DestroyObject", RpcTarget.All, 25);
            }
        }
    }

    [PunRPC]
    public void DestroyObject(int damage)
    {
        health -= damage;
        Debug.Log(health);

        if(health <= 0)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

}
