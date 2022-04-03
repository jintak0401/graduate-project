using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class ObjectInfo
    {
        public GameObject goPrefab;
        public int count;
        public Transform tfPoolParent;
    }

    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool Instance;

        [SerializeField] private ObjectInfo[] objectInfos;

        // public Queue<GameObject> LeftNoteQueue = new Queue<GameObject>();
        public Queue<GameObject> NoteQueue = new Queue<GameObject>();
        // public Queue<GameObject> RightNoteQueue = new Queue<GameObject>();

        private void Awake()
        {
            Instance = this;
            NoteQueue = InsertQueue(objectInfos[0]);
            // LeftNoteQueue = InsertQueue(objectInfos[1]);
            // RightNoteQueue = InsertQueue(objectInfos[2]);
        }

        private Queue<GameObject> InsertQueue(ObjectInfo objectInfo)
        {
            var queue = new Queue<GameObject>();
            for (var i = 0; i < objectInfo.count; i++)
            {
                var clone = Instantiate(objectInfo.goPrefab, transform.position, Quaternion.identity);
                clone.SetActive(false);
                clone.transform.SetParent(objectInfo.tfPoolParent != null ? objectInfo.tfPoolParent : transform);
                queue.Enqueue(clone);
            }

            return queue;
        }
    }
}