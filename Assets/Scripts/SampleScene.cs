using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class SampleScene : MonoBehaviourPunCallbacks
{
    public Transform spawnPoint1; // 1人目のスポーン位置
    public Transform spawnPoint2; // 2人目のスポーン位置

    private void Start() {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.NickName = "Player";
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster() {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom() {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerCount % 2 == 1) {
            // 1人目のプレイヤーはspawnPoint1にアバターを生成する
            PhotonNetwork.Instantiate("SouAgent", spawnPoint1.position, Quaternion.identity);
        } else if (playerCount % 2 == 0) {
            // 2人目のプレイヤーはspawnPoint2にアバターを生成する
            PhotonNetwork.Instantiate("SouAgent", spawnPoint2.position, Quaternion.identity);
        }
        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        // var position = new Vector3(Random.Range(-3f, 3f), 0.5f,Random.Range(-3f, 3f));
        // PhotonNetwork.Instantiate("SouAgent", position, Quaternion.identity);
    }
}