using System;
using UnityEngine;

namespace Warframe_Puzzle_Related
{
    public class Hex : MonoBehaviour
    {
        [SerializeField] private GameObject[] Sticks;
        public static event Action<Transform> RotateHex;

        public void OnClickHex()
        {
            RotateHex?.Invoke(transform);
        }
    }
}
