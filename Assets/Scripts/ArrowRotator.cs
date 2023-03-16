using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotator : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private int playerNum;
    [SerializeField] private GameObject arrow;
    private GameObject ball;
    private Camera playerCam;
    public bool visible = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player " + playerNum.ToString());
        ball = GameObject.Find("Soccer");
        playerCam = player.transform.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(visible){
            Vector3 ballPosFlat = new Vector3(
                ball.transform.position.x - player.transform.position.x,
                0,
                ball.transform.position.z - player.transform.position.z
            );

            Vector3 playerPosFlat = new Vector3(
                player.transform.forward.x,
                0,
                player.transform.forward.z
            );

            float checkAngle = Vector3.Angle(playerPosFlat, ballPosFlat);

            Vector3 screenPoint = playerCam.WorldToViewportPoint(ball.transform.position);
            bool onScreen = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            
            arrow.SetActive(!onScreen || checkAngle > 90);

            float angle = Mathf.Atan2(screenPoint.y - 0.5f, screenPoint.x - 0.5f);

            this.transform.rotation = Quaternion.Euler(
                this.transform.rotation.eulerAngles.x,
                this.transform.rotation.eulerAngles.y,
                angle * Mathf.Rad2Deg + 180 * (checkAngle > 90 ? 1 : 0)
            );
        }
    }
}
