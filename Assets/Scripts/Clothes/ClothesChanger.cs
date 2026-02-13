using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clothes
{
    public class ClothesChanger : MonoBehaviour
    {
        public SkinnedMeshRenderer skinnedMeshRenderer;
        public Texture2D texture;
        public string shaderIdName = "_EmissionMap";
        
        private Texture2D _defaultTexture;

        private void Awake()
        {
            _defaultTexture = (Texture2D) skinnedMeshRenderer.materials[0].GetTexture(shaderIdName);
        }

        [NaughtyAttributes.Button]
        void ChangeTexture()
        {
            skinnedMeshRenderer.materials[0].SetTexture(shaderIdName, texture);
        }

        public void ChangeTexture(ClothesSetup setup)
        {
            skinnedMeshRenderer.materials[0].SetTexture(shaderIdName, setup.texture);
        }
        
        public void ResetTexture()
        {
            skinnedMeshRenderer.materials[0].SetTexture(shaderIdName, _defaultTexture);
        }
    }
}