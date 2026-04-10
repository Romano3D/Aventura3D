using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;
using TMPro;


namespace Cloth
{

    public class ClothChanger : MonoBehaviour
    {
        public SkinnedMeshRenderer mesh;

        public int currentClothId;
        public List<ClothSetup> cloths;

        public Texture2D texture;
        public string shaderIdName = "_EmissionMap";

        private Texture2D _defaultTexture;

        private void Awake()
        {
            _defaultTexture = (Texture2D) mesh.materials[0].GetTexture(shaderIdName);
        }

        [NaughtyAttributes.Button]
        private void ChangeTexture()
        {
            mesh.materials[0].SetTexture(shaderIdName, texture);
        }
     
        public void ChangeTexture(ClothSetup setup)
        {
            mesh.materials[0].SetTexture(shaderIdName, setup.texture);
            currentClothId = setup.id;
        }

        public void ResetTexture()
        {
            mesh.materials[0].SetTexture(shaderIdName, _defaultTexture);
        }
        public void ApplyClothById(int id)
        {
            ClothSetup setup = cloths.Find(c => c.id == id);

            if (setup != null)
            {
                ChangeTexture(setup);
            }
            else
            {
                ResetTexture();
            }
        }
            private void Start()
        {
            SaveManager.Instance.FileLoaded += OnLoad;
        }

        private void OnLoad(SaveSetup setup)
        {
            ApplyClothById(setup.equippedOutfit);
        }

        public void OnSelectCloth(ClothSetup setup)
        {
            ChangeTexture(setup);
            SaveManager.Instance.SaveOutfit();
        }
    }
   }

