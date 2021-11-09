using System.Collections.Generic;
using UnityEngine;

namespace UpdateCollections
{
    public class UpdateCollection : MonoBehaviour
    {
        private List<IGameUpdate> _updates;
        private bool _isUpdating;

        private void Awake()
        {
            _updates = new List<IGameUpdate>();
        }

        public void AddToUpdateQueue(IGameUpdate gameUpdate)
        {
            _updates.Add(gameUpdate);
        }

        private void Update()
        {
            if (!_isUpdating) return;

            for (int i = 0; i < _updates.Count; i++)
            {
                _updates[i].GameUpdate(Time.deltaTime);
            }
        }

        public void StartUpdate()
        {
            ResumeUpdate();
        }

        public void StopUpdate()
        {
            _isUpdating = false;
        }

        public void ResumeUpdate()
        {
            _isUpdating = true;
        }
    }
}