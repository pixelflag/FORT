using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pixelflag.UI
{
    public class PixelObject : MonoBehaviour
    {
        private Transform _transform;

        public virtual void Initialize()
        {
            _transform = transform;
        }

        public Vector3 position
        {
            get
            {
                return _transform.localPosition;
            }
            set
            {
                _transform.localPosition = value;
            }
        }

        public float x
        {
            get
            {
                return _transform.localPosition.x;
            }
            set
            {
                Vector3 pos = _transform.localPosition;
                pos.x = value;
                _transform.localPosition = pos;
            }
        }

        public float y
        {
            get
            {
                return _transform.localPosition.y;
            }
            set
            {
                Vector3 pos = _transform.localPosition;
                pos.y = value;
                _transform.localPosition = pos;
            }
        }

        public float z
        {
            get
            {
                return _transform.localPosition.z;
            }
            set
            {
                Vector3 pos = _transform.localPosition;
                pos.z = value;
                _transform.localPosition = pos;
            }
        }
    }
}