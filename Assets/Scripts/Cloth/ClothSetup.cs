using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    [System.Serializable]
    public class ClothSetup
    {
        public int id;
        public Texture2D texture;

        public ClothType clothType; 
    }
}