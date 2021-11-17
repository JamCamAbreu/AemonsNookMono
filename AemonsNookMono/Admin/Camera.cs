using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Admin
{
    public class Camera
    {
        #region Singleton Implementation
        private static Camera instance;
        private static object _lock = new object();
        public static Camera Current
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new Camera();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion
        #region Public Properties
        const float DEFAULT_ZOOM = 2.5f;
        const float MAX_ZOOM_OUT = 1.85f;
        private Vector2 Position { get; set; }
        public Vector2 TargetPosition { get; set; }
        private float Zoom { get; set; }
        public float TargetZoom { get; set; }
        public int ViewportWidth { get; set; }
        public int ViewportHeight { get; set; }
        public Vector2 ViewportCenter
        {
            get
            {
                return new Vector2(ViewportWidth * 0.5f, ViewportHeight * 0.5f);
            }
        }
        public Matrix TranslationMatrix
        {
            get
            {
                return Matrix.CreateTranslation(
                   -(int)Position.X,
                   -(int)Position.Y, 
                   0) *
                   Matrix.CreateRotationZ(0.0f) *
                   Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                   Matrix.CreateTranslation(new Vector3(ViewportCenter, 0));
            }
        }

        #endregion

        #region Constructor
        public Camera()
        {
            this.Position = Vector2.Zero;
            this.TargetPosition = Vector2.Zero;
            this.Zoom = DEFAULT_ZOOM;
            this.TargetZoom = DEFAULT_ZOOM;
            this.ViewportWidth = Graphics.Current.Device.Viewport.Width;
            this.ViewportHeight = Graphics.Current.Device.Viewport.Height;
        }
        #endregion

        #region Interface
        public void Update()
        {
            //int dist = Cursor.Current.CurDistanceFromCenter;
            //int threshold = 500;

            //if (dist <= threshold)
            //{
            //    this.TargetZoom = DEFAULT_ZOOM;
            //}
            //else
            //{
            //    this.TargetZoom = Math.Max(DEFAULT_ZOOM - ((dist - threshold) / 500f), MAX_ZOOM_OUT);
            //}

            this.Zoom = Global.Ease(this.Zoom, this.TargetZoom, 0.02f);
            this.Position = Global.Ease(this.Position, this.TargetPosition, 0.05f);
        }
        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition, TranslationMatrix);
        }
        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            if (World.Current.hero == null) { return screenPosition; }

            return Vector2.Transform(screenPosition,
                Matrix.Invert(TranslationMatrix));
        }
        #endregion
    }
}
