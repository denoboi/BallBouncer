using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;

namespace HCB.GridSystem
{
    public class GridObject : PlaceableBase
    {
       
        [Header("Grid Object")]
        [SerializeField] private GridObjectData _gridObjectData;
        public GridObjectData GridObjectData => _gridObjectData;
        private Collider[] _collider;
        public Collider[] Collider => _collider ??= _collider = GetComponentsInChildren<Collider>();

        private Renderer[] _renderer;
        public Renderer[] Renderer => _renderer ??= _renderer = GetComponentsInChildren<Renderer>();

        private Color _defaultColor;
        
        protected override void Awake()
        {
            base.Awake();
            Activate();
           
        }

        private void Start()
        {
            for (int i = 0; i < Renderer.Length; i++)
            {
                _defaultColor = Renderer[i].material.color;
            }
        }

        protected override void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            GridObjectManager.Instance.AddGridObject(this);
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            GridObjectManager.Instance.RemoveGridObject(this);
            base.OnDisable();
        }

        public void InitializeFromSaveData(ITile lastPlacedPivotTile) 
        {
            InitialPlacement(lastPlacedPivotTile);
        }       

        private void InitialPlacement(ITile lastPlacedPivotTile) 
        {
            LastPlacedPivotTile = lastPlacedPivotTile;
            Place(LastPlacedPivotTile);

            for (int i = 0; i < GridPoints.Count; i++)
            {
                int x = LastPlacedPivotTile.TileInitializeData.Grid.x + GridPoints[i].x;
                int z = LastPlacedPivotTile.TileInitializeData.Grid.y + GridPoints[i].y;

                ITile tile = TileManager.Instance.GetTile(x, z);
                if (tile != null)
                {
                    LastPlacedTiles.Add(tile);
                    tile.PlaceItem(this);
                }
            }
        }

        private void Activate()
        {
            IsActive = true;
            CanSelectable = true;
        }

        public override bool Select()
        {
            int gridLayer = LayerMask.NameToLayer("NonTouchable");
            for (int i = 0; i < Collider.Length; i++)
            {
                Collider[i].gameObject.layer = gridLayer;
            }

            for (int i = 0; i < Renderer.Length; i++)
            {
                Renderer[i].material.color = Color.gray;
            }
          
            return base.Select();
            
        }
        
        public override bool Deselect()
        {
            int defaultLayer = LayerMask.NameToLayer("Default");
            
            for (int i = 0; i < Collider.Length; i++)
            {
                Collider[i].gameObject.layer = defaultLayer;
            }
            
            for (int i = 0; i < Renderer.Length; i++)
            {
                Renderer[i].material.color = _defaultColor;
            }
            
            return base.Deselect();
        }
    }
}
