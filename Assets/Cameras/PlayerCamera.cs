using Models;
using UnityEngine;

namespace Cameras
{
    public class PlayerCamera : MonoBehaviour
    {
        public Human player;
        public void Update()
        {
            Camera.current.transform.position =
                player.transform.position - player.transform.forward * 10 + player.transform.up * 3;
            Camera.current.transform.LookAt(player.transform.position);
            Camera.current.transform.parent = player.transform;
        }
    }
}