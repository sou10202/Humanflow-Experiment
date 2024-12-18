using System.Threading;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class AvatarController : MonoBehaviourPunCallbacks, IPunObservable
{
    private const float MaxStamina = 6f;
    private const float movespeed = 3f;
    private const float rotatespeed = 60f;
    private float currentStamina = MaxStamina;

    private void Update() {
        if (photonView.IsMine) {
            float movedirection = Input.GetAxis("Vertical");
            transform.Translate(Vector3.forward * movespeed * movedirection * Time.deltaTime);
            float rotatedirection = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * rotatespeed * rotatedirection * Time.deltaTime);
        }
    }
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            // 自身のアバターのスタミナを送信する
            stream.SendNext(currentStamina);
        } else {
            // 他プレイヤーのアバターのスタミナを受信する
            currentStamina = (float)stream.ReceiveNext();
        }
    }
}