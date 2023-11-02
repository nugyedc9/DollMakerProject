using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class Sound : MonoBehaviour
    {
        public Sound(Vector3 _pos, float _range)
        {
            pos =  new Vector3(_pos.x, 0f, _pos.z);
            range = _range;
        }

        public readonly Vector3 pos;
        public readonly float range;
    }
}
