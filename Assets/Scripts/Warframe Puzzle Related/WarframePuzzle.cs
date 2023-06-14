using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Random = UnityEngine.Random;

namespace Warframe_Puzzle_Related
{
    public class WarframePuzzle : MonoBehaviour
    {
        [Serializable]
        public struct StickPairs
        {
            public GameObject Stick1;
            public GameObject Stick2;
        }

        [SerializeField] private StickPairs[] StickPairsArray;
        public static event Action OnCreatePuzzle;

        private bool _isAvailable;

        private void Start()
        {
            Hex.RotateHex += RotateHex;
            _isAvailable = true;
        }

        private void RotateHex(Transform obj)
        {
            if (!_isAvailable)
                return;
            
            _isAvailable = false;
            obj.DORotate(Vector3.forward * 60, 0.2f, RotateMode.LocalAxisAdd)
                .SetEase(Ease.Linear).OnComplete(() =>
                {
                    _isAvailable = true;
                });
        }

        private void PuzzleCreate()
        {
            int startNode = Random.Range(0, 6);
            
            OnCreatePuzzle?.Invoke();
        }
    }
}
