﻿/*
  _____                 ____                 
 | ____|_ __ ___  _   _|  _ \  _____   _____ 
 |  _| | '_ ` _ \| | | | | | |/ _ \ \ / / __|
 | |___| | | | | | |_| | |_| |  __/\ V /\__ \
 |_____|_| |_| |_|\__,_|____/ \___| \_/ |___/
          <http://emudevs.com>
             Terraria 1.3
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Terraria;
using Terraria.UI.Chat;

namespace Terraria.Graphics.Capture
{
    internal class CaptureInterface
    {
        private static Dictionary<int, CaptureInterface.CaptureInterfaceMode> Modes = CaptureInterface.FillModes();
        public static bool JustActivated = false;
        public static bool EdgeAPinned = false;
        public static bool EdgeBPinned = false;
        public static Point EdgeA = new Point();
        public static Point EdgeB = new Point();
        public static bool CameraLock = false;
        private static float CameraFrame = 0.0f;
        private static float CameraWaiting = 0.0f;
        private const Keys KeyToggleActive = Keys.F1;
        private const float CameraMaxFrame = 5f;
        private const float CameraMaxWait = 60f;
        public bool Active;
        private bool KeyToggleActiveHeld;
        public int SelectedMode;
        public int HoveredMode;
        private static CaptureSettings CameraSettings;

        private static Dictionary<int, CaptureInterface.CaptureInterfaceMode> FillModes()
        {
            return new Dictionary<int, CaptureInterface.CaptureInterfaceMode>()
      {
        {
          0,
          (CaptureInterface.CaptureInterfaceMode) new CaptureInterface.ModeEdgeSelection()
        },
        {
          1,
          (CaptureInterface.CaptureInterfaceMode) new CaptureInterface.ModeDragBounds()
        },
        {
          2,
          (CaptureInterface.CaptureInterfaceMode) new CaptureInterface.ModeChangeSettings()
        }
      };
        }

        public static Rectangle GetArea()
        {
            return new Rectangle(Math.Min(CaptureInterface.EdgeA.X, CaptureInterface.EdgeB.X), Math.Min(CaptureInterface.EdgeA.Y, CaptureInterface.EdgeB.Y), Math.Abs(CaptureInterface.EdgeA.X - CaptureInterface.EdgeB.X) + 1, Math.Abs(CaptureInterface.EdgeA.Y - CaptureInterface.EdgeB.Y) + 1);
        }

        public void Update()
        {
            this.UpdateCamera();
            if (CaptureInterface.CameraLock)
                return;
            bool flag = Main.keyState.IsKeyDown(Keys.F1);
            if (flag && !this.KeyToggleActiveHeld && (Main.mouseItem.type == 0 || this.Active) && !Main.CaptureModeDisabled)
                this.ToggleCamera(!this.Active);
            this.KeyToggleActiveHeld = flag;
            if (!this.Active)
                return;
            Main.blockMouse = true;
            if (CaptureInterface.JustActivated && Main.mouseLeftRelease && !Main.mouseLeft)
                CaptureInterface.JustActivated = false;
            if (this.UpdateButtons(new Vector2((float)Main.mouseX, (float)Main.mouseY)) && Main.mouseLeft)
                return;
            foreach (KeyValuePair<int, CaptureInterface.CaptureInterfaceMode> keyValuePair in CaptureInterface.Modes)
            {
                keyValuePair.Value.Selected = keyValuePair.Key == this.SelectedMode;
                keyValuePair.Value.Update();
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if (!this.Active)
                return;
            foreach (CaptureInterface.CaptureInterfaceMode captureInterfaceMode in CaptureInterface.Modes.Values)
                captureInterfaceMode.Draw(sb);
            Main.mouseText = false;
            Main.instance.GUIBarsDraw();
            this.DrawButtons(sb);
            Main.instance.DrawMouseOver();
            Utils.DrawBorderStringBig(sb, Lang.inter[81], new Vector2((float)Main.screenWidth * 0.5f, 100f), Color.White, 1f, 0.5f, 0.5f, -1);
            Utils.DrawCursorSingle(sb, Main.cursorColor, float.NaN, Main.cursorScale, new Vector2(), 0, 0);
            this.DrawCameraLock(sb);
        }

        public void ToggleCamera(bool On = true)
        {
            if (CaptureInterface.CameraLock)
                return;
            bool flag = this.Active;
            this.Active = CaptureInterface.Modes.ContainsKey(this.SelectedMode) && On;
            if (flag != this.Active)
                Main.PlaySound(12, -1, -1, 1);
            foreach (KeyValuePair<int, CaptureInterface.CaptureInterfaceMode> keyValuePair in CaptureInterface.Modes)
                keyValuePair.Value.ToggleActive(this.Active && keyValuePair.Key == this.SelectedMode);
            if (!On || flag)
                return;
            CaptureInterface.JustActivated = true;
        }

        private bool UpdateButtons(Vector2 mouse)
        {
            this.HoveredMode = -1;
            bool flag1 = !Main.graphics.IsFullScreen;
            int num1 = 9;
            for (int index = 0; index < num1; ++index)
            {
                if (new Rectangle(24 + 46 * index, 24, 42, 42).Contains(Utils.ToPoint(mouse)))
                {
                    this.HoveredMode = index;
                    bool flag2 = Main.mouseLeft && Main.mouseLeftRelease;
                    int num2 = 0;
                    int num3 = index;
                    int num4 = num2;
                    int num5 = 1;
                    int num6 = num4 + num5;
                    if (num3 == num4 && flag2)
                    {
                        CaptureSettings settings = new CaptureSettings();
                        Point point1 = Utils.ToTileCoordinates(Main.screenPosition);
                        Point point2 = Utils.ToTileCoordinates(Main.screenPosition + new Vector2((float)Main.screenWidth, (float)Main.screenHeight));
                        settings.Area = new Rectangle(point1.X, point1.Y, point2.X - point1.X + 1, point2.Y - point1.Y + 1);
                        settings.Biome = CaptureBiome.Biomes[CaptureInterface.Settings.BiomeChoice];
                        settings.CaptureBackground = !CaptureInterface.Settings.TransparentBackground;
                        settings.CaptureEntities = CaptureInterface.Settings.IncludeEntities;
                        settings.UseScaling = CaptureInterface.Settings.PackImage;
                        CaptureInterface.StartCamera(settings);
                    }
                    int num7 = index;
                    int num8 = num6;
                    int num9 = 1;
                    int num10 = num8 + num9;
                    if (num7 == num8 && flag2 && (CaptureInterface.EdgeAPinned && CaptureInterface.EdgeBPinned))
                        CaptureInterface.StartCamera(new CaptureSettings()
                        {
                            Area = CaptureInterface.GetArea(),
                            Biome = CaptureBiome.Biomes[CaptureInterface.Settings.BiomeChoice],
                            CaptureBackground = !CaptureInterface.Settings.TransparentBackground,
                            CaptureEntities = CaptureInterface.Settings.IncludeEntities,
                            UseScaling = CaptureInterface.Settings.PackImage
                        });
                    int num11 = index;
                    int num12 = num10;
                    int num13 = 1;
                    int num14 = num12 + num13;
                    if (num11 == num12 && flag2 && this.SelectedMode != 0)
                    {
                        this.SelectedMode = 0;
                        this.ToggleCamera(true);
                    }
                    int num15 = index;
                    int num16 = num14;
                    int num17 = 1;
                    int num18 = num16 + num17;
                    if (num15 == num16 && flag2 && this.SelectedMode != 1)
                    {
                        this.SelectedMode = 1;
                        this.ToggleCamera(true);
                    }
                    int num19 = index;
                    int num20 = num18;
                    int num21 = 1;
                    int num22 = num20 + num21;
                    if (num19 == num20 && flag2)
                        CaptureInterface.ResetFocus();
                    int num23 = index;
                    int num24 = num22;
                    int num25 = 1;
                    int num26 = num24 + num25;
                    if (num23 == num24 && flag2 && Main.mapEnabled)
                        Main.mapFullscreen = !Main.mapFullscreen;
                    int num27 = index;
                    int num28 = num26;
                    int num29 = 1;
                    int num30 = num28 + num29;
                    if (num27 == num28 && flag2 && this.SelectedMode != 2)
                    {
                        this.SelectedMode = 2;
                        this.ToggleCamera(true);
                    }
                    int num31 = index;
                    int num32 = num30;
                    int num33 = 1;
                    int num34 = num32 + num33;
                    if (num31 == num32 && flag2 && flag1)
                        Process.Start(string.Concat(new object[4]
            {
              (object) Main.SavePath,
              (object) Path.DirectorySeparatorChar,
              (object) "Captures",
              (object) Path.DirectorySeparatorChar
            }));
                    int num35 = index;
                    int num36 = num34;
                    int num37 = 1;
                    int num38 = num36 + num37;
                    if (num35 == num36 && flag2)
                    {
                        this.ToggleCamera(false);
                        Main.blockMouse = true;
                        Main.mouseLeftRelease = false;
                    }
                    return true;
                }
            }
            return false;
        }

        private void DrawButtons(SpriteBatch sb)
        {
            Vector2 vector2 = new Vector2((float)Main.mouseX, (float)Main.mouseY);
            int num = 9;
            for (int index = 0; index < num; ++index)
            {
                Texture2D texture2D = Main.inventoryBackTexture;
                float scale = 0.8f;
                Vector2 position = new Vector2((float)(24 + 46 * index), 24f);
                Color color = Main.inventoryBack * 0.8f;
                if (this.SelectedMode == 0 && index == 2)
                    texture2D = Main.inventoryBack14Texture;
                else if (this.SelectedMode == 1 && index == 3)
                    texture2D = Main.inventoryBack14Texture;
                else if (this.SelectedMode == 2 && index == 6)
                    texture2D = Main.inventoryBack14Texture;
                else if (index >= 2 && index <= 3)
                    texture2D = Main.inventoryBack2Texture;
                sb.Draw(texture2D, position, new Rectangle?(), color, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                switch (index)
                {
                    case 0:
                        texture2D = Main.cameraTexture[7];
                        break;
                    case 1:
                        texture2D = Main.cameraTexture[0];
                        break;
                    case 2:
                    case 3:
                    case 4:
                        texture2D = Main.cameraTexture[index];
                        break;
                    case 5:
                        texture2D = Main.mapFullscreen ? Main.mapIconTexture[0] : Main.mapIconTexture[4];
                        break;
                    case 6:
                        texture2D = Main.cameraTexture[1];
                        break;
                    case 7:
                        texture2D = Main.cameraTexture[6];
                        break;
                    case 8:
                        texture2D = Main.cameraTexture[5];
                        break;
                }
                sb.Draw(texture2D, position + new Vector2(26f) * scale, new Rectangle?(), Color.White, 0.0f, Utils.Size(texture2D) / 2f, 1f, SpriteEffects.None, 0.0f);
                bool flag = false;
                switch (index)
                {
                    case 1:
                        if (!CaptureInterface.EdgeAPinned || !CaptureInterface.EdgeBPinned)
                        {
                            flag = true;
                            break;
                        }
                        break;
                    case 5:
                        if (!Main.mapEnabled)
                        {
                            flag = true;
                            break;
                        }
                        break;
                    case 7:
                        if (Main.graphics.IsFullScreen)
                        {
                            flag = true;
                            break;
                        }
                        break;
                }
                if (flag)
                    sb.Draw(Main.cdTexture, position + new Vector2(26f) * scale, new Rectangle?(), Color.White * 0.65f, 0.0f, Utils.Size(Main.cdTexture) / 2f, 1f, SpriteEffects.None, 0.0f);
            }
            string cursorText = "";
            switch (this.HoveredMode)
            {
                case -1:
                    switch (this.HoveredMode)
                    {
                        case 1:
                            if (!CaptureInterface.EdgeAPinned || !CaptureInterface.EdgeBPinned)
                            {
                                cursorText = cursorText + "\n" + Lang.inter[112];
                                break;
                            }
                            break;
                        case 5:
                            if (!Main.mapEnabled)
                            {
                                cursorText = cursorText + "\n" + Lang.inter[114];
                                break;
                            }
                            break;
                        case 7:
                            if (Main.graphics.IsFullScreen)
                            {
                                cursorText = cursorText + "\n" + Lang.inter[113];
                                break;
                            }
                            break;
                    }
                    if (!(cursorText != ""))
                        break;
                    Main.instance.MouseText(cursorText, 0, (byte)0);
                    break;
                case 0:
                    cursorText = Lang.inter[111];
                    goto case -1;
                case 1:
                    cursorText = Lang.inter[67];
                    goto case -1;
                case 2:
                    cursorText = Lang.inter[69];
                    goto case -1;
                case 3:
                    cursorText = Lang.inter[70];
                    goto case -1;
                case 4:
                    cursorText = Lang.inter[78];
                    goto case -1;
                case 5:
                    cursorText = Main.mapFullscreen ? Lang.inter[109] : Lang.inter[108];
                    goto case -1;
                case 6:
                    cursorText = Lang.inter[68];
                    goto case -1;
                case 7:
                    cursorText = Lang.inter[110];
                    goto case -1;
                case 8:
                    cursorText = Lang.inter[71];
                    goto case -1;
                default:
                    cursorText = "???";
                    goto case -1;
            }
        }

        private static bool GetMapCoords(int PinX, int PinY, int Goal, out Point result)
        {
            if (!Main.mapFullscreen)
            {
                result = new Point(-1, -1);
                return false;
            }

            float num3 = 2f;
            int num4 = Main.maxTilesX / Main.textureMaxWidth;
            int num5 = Main.maxTilesY / Main.textureMaxHeight;
            float num6 = 10f;
            float num7 = 10f;
            float num8 = (float)(Main.maxTilesX - 10);
            float num9 = (float)(Main.maxTilesY - 10);
            num3 = Main.mapFullscreenScale;
            float num10 = (float)((double)Main.screenWidth / (double)Main.maxTilesX * 0.800000011920929);
            if ((double)Main.mapFullscreenScale < (double)num10)
                Main.mapFullscreenScale = num10;
            if ((double)Main.mapFullscreenScale > 16.0)
                Main.mapFullscreenScale = 16f;
            float num11 = Main.mapFullscreenScale;
            if ((double)Main.mapFullscreenPos.X < (double)num6)
                Main.mapFullscreenPos.X = num6;
            if ((double)Main.mapFullscreenPos.X > (double)num8)
                Main.mapFullscreenPos.X = num8;
            if ((double)Main.mapFullscreenPos.Y < (double)num7)
                Main.mapFullscreenPos.Y = num7;
            if ((double)Main.mapFullscreenPos.Y > (double)num9)
                Main.mapFullscreenPos.Y = num9;
            float num12 = Main.mapFullscreenPos.X;
            float num13 = Main.mapFullscreenPos.Y;
            float num14 = num12 * num11;
            float num15 = num13 * num11;
            float num16 = -num14 + (float)(Main.screenWidth / 2);
            float num17 = -num15 + (float)(Main.screenHeight / 2);
            float x1 = num16 + num6 * num11;
            float y1 = num17 + num7 * num11;
            float num18 = (float)(Main.maxTilesX / 840) * Main.mapFullscreenScale;
            float num19 = x1;
            float num20 = y1;
            float num21 = (float)Main.mapTexture.Width;
            float num22 = (float)Main.mapTexture.Height;
            float num23;
            float num24;
            float num25;
            float num26;
            if (Main.maxTilesX == 8400)
            {
                float num27 = num18 * 0.999f;
                num23 = num19 - 40.6f * num27;
                num24 = y1 - 5f * num27;
                num25 = (num21 - 8.045f) * num27;
                float num28 = (num22 + 0.12f) * num27;
                if ((double)num27 < 1.2)
                    num26 = num28 + 1f;
            }
            else if (Main.maxTilesX == 6400)
            {
                float num27 = num18 * 1.09f;
                num23 = num19 - 38.8f * num27;
                num24 = y1 - 3.85f * num27;
                num25 = (num21 - 13.6f) * num27;
                float num28 = (num22 - 6.92f) * num27;
                if ((double)num27 < 1.2)
                    num26 = num28 + 2f;
            }
            else if (Main.maxTilesX == 6300)
            {
                float num27 = num18 * 1.09f;
                num23 = num19 - 39.8f * num27;
                num24 = y1 - 4.08f * num27;
                num25 = (num21 - 26.69f) * num27;
                float num28 = (num22 - 6.92f) * num27;
                if ((double)num27 < 1.2)
                    num26 = num28 + 2f;
            }
            else if (Main.maxTilesX == 4200)
            {
                float num27 = num18 * 0.998f;
                num23 = num19 - 37.3f * num27;
                num24 = num20 - 1.7f * num27;
                num25 = (num21 - 16f) * num27;
                num26 = (num22 - 8.31f) * num27;
            }
            if (Goal == 0)
            {
                int x2 = (int)((-(double)x1 + (double)PinX) / (double)num11 + (double)num6);
                int y2 = (int)((-(double)y1 + (double)PinY) / (double)num11 + (double)num7);
                bool flag = false;
                if ((double)x2 < (double)num6)
                    flag = true;
                if ((double)x2 >= (double)num8)
                    flag = true;
                if ((double)y2 < (double)num7)
                    flag = true;
                if ((double)y2 >= (double)num9)
                    flag = true;
                if (!flag)
                {
                    result = new Point(x2, y2);
                    return true;
                }
                result = new Point(-1, -1);
                return false;
            }
            if (Goal == 1)
            {
                Vector2 vector2_1 = new Vector2(x1, y1);
                Vector2 vector2_2 = new Vector2((float)PinX, (float)PinY) * num11 - new Vector2(10f * num11);
                result = Utils.ToPoint(vector2_1 + vector2_2);
                return true;
            }
            result = new Point(-1, -1);
            return false;
        }

        private static void ConstraintPoints()
        {
            int fluff = Lighting.offScreenTiles;
            if (CaptureInterface.EdgeAPinned)
                CaptureInterface.PointWorldClamp(ref CaptureInterface.EdgeA, fluff);
            if (!CaptureInterface.EdgeBPinned)
                return;
            CaptureInterface.PointWorldClamp(ref CaptureInterface.EdgeB, fluff);
        }

        private static void PointWorldClamp(ref Point point, int fluff)
        {
            if (point.X < fluff)
                point.X = fluff;
            if (point.X > Main.maxTilesX - 1 - fluff)
                point.X = Main.maxTilesX - 1 - fluff;
            if (point.Y < fluff)
                point.Y = fluff;
            if (point.Y <= Main.maxTilesY - 1 - fluff)
                return;
            point.Y = Main.maxTilesY - 1 - fluff;
        }

        public bool UsingMap()
        {
            if (CaptureInterface.CameraLock)
                return true;
            return CaptureInterface.Modes[this.SelectedMode].UsingMap();
        }

        public static void ResetFocus()
        {
            CaptureInterface.EdgeAPinned = false;
            CaptureInterface.EdgeBPinned = false;
            CaptureInterface.EdgeA = new Point(-1, -1);
            CaptureInterface.EdgeB = new Point(-1, -1);
        }

        public void Scrolling()
        {
            int num1 = (Main.mouseState.ScrollWheelValue - Main.oldMouseWheel) / 120 % 30;
            if (num1 < 0)
                num1 += 30;
            int num2 = this.SelectedMode;
            this.SelectedMode -= num1;
            while (this.SelectedMode < 0)
                this.SelectedMode += 2;
            while (this.SelectedMode > 2)
                this.SelectedMode -= 2;
            if (this.SelectedMode == num2)
                return;
            Main.PlaySound(12, -1, -1, 1);
        }

        private void UpdateCamera()
        {
            if (CaptureInterface.CameraLock && (double)CaptureInterface.CameraFrame == 4.0)
                CaptureManager.Instance.Capture(CaptureInterface.CameraSettings);
            CaptureInterface.CameraFrame += (float)Utils.ToDirectionInt(CaptureInterface.CameraLock);
            if ((double)CaptureInterface.CameraFrame < 0.0)
                CaptureInterface.CameraFrame = 0.0f;
            if ((double)CaptureInterface.CameraFrame > 5.0)
                CaptureInterface.CameraFrame = 5f;
            if ((double)CaptureInterface.CameraFrame == 5.0)
                ++CaptureInterface.CameraWaiting;
            if ((double)CaptureInterface.CameraWaiting <= 60.0)
                return;
            CaptureInterface.CameraWaiting = 60f;
        }

        private void DrawCameraLock(SpriteBatch sb)
        {
            if ((double)CaptureInterface.CameraFrame == 0.0)
                return;
            sb.Draw(Main.magicPixel, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Rectangle?(new Rectangle(0, 0, 1, 1)), Color.Black * (CaptureInterface.CameraFrame / 5f));
            if ((double)CaptureInterface.CameraFrame != 5.0)
                return;
            float num1 = (float)((double)CaptureInterface.CameraWaiting - 60.0 + 5.0);
            if ((double)num1 <= 0.0)
                return;
            float num2 = num1 / 5f;
            float num3 = CaptureManager.Instance.GetProgress() * 100f;
            if ((double)num3 > 100.0)
                num3 = 100f;
            string text1 = num3.ToString("##") + " ";
            string text2 = "/ 100%";
            Vector2 vector2_1 = Main.fontDeathText.MeasureString(text1);
            Vector2 vector2_2 = Main.fontDeathText.MeasureString(text2);
            Vector2 vector2_3 = new Vector2(-vector2_1.X, (float)(-(double)vector2_1.Y / 2.0));
            Vector2 vector2_4 = new Vector2(0.0f, (float)(-(double)vector2_2.Y / 2.0));
            ChatManager.DrawColorCodedStringWithShadow(sb, Main.fontDeathText, text1, new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / 2f + vector2_3, Color.White * num2, 0.0f, Vector2.Zero, Vector2.One, -1f, 2f);
            ChatManager.DrawColorCodedStringWithShadow(sb, Main.fontDeathText, text2, new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / 2f + vector2_4, Color.White * num2, 0.0f, Vector2.Zero, Vector2.One, -1f, 2f);
        }

        public static void StartCamera(CaptureSettings settings)
        {
            Main.PlaySound(40, -1, -1, 1);
            CaptureInterface.CameraSettings = settings;
            CaptureInterface.CameraLock = true;
            CaptureInterface.CameraWaiting = 0.0f;
        }

        public static void EndCamera()
        {
            CaptureInterface.CameraLock = false;
        }

        public static class Settings
        {
            public static bool PackImage = true;
            public static bool IncludeEntities = true;
            public static bool TransparentBackground = false;
            public static int BiomeChoice = 0;
            public static int ScreenAnchor = 0;
            public static Color MarkedAreaColor = new Color(0.8f, 0.8f, 0.8f, 0.0f) * 0.3f;
        }

        private abstract class CaptureInterfaceMode
        {
            public bool Selected;

            public abstract void Update();

            public abstract void Draw(SpriteBatch sb);

            public abstract void ToggleActive(bool tickedOn);

            public abstract bool UsingMap();
        }

        private class ModeEdgeSelection : CaptureInterface.CaptureInterfaceMode
        {
            public override void Update()
            {
                if (!this.Selected)
                    return;
                this.EdgePlacement(new Vector2((float)Main.mouseX, (float)Main.mouseY));
            }

            public override void Draw(SpriteBatch sb)
            {
                if (!this.Selected)
                    return;
                this.DrawMarkedArea(sb);
                this.DrawCursors(sb);
            }

            public override void ToggleActive(bool tickedOn)
            {
            }

            public override bool UsingMap()
            {
                return true;
            }

            private void EdgePlacement(Vector2 mouse)
            {
                if (CaptureInterface.JustActivated)
                    return;
                if (!Main.mapFullscreen)
                {
                    if (Main.mouseLeft)
                    {
                        CaptureInterface.EdgeAPinned = true;
                        CaptureInterface.EdgeA = Utils.ToTileCoordinates(Main.MouseWorld);
                    }
                    if (Main.mouseRight)
                    {
                        CaptureInterface.EdgeBPinned = true;
                        CaptureInterface.EdgeB = Utils.ToTileCoordinates(Main.MouseWorld);
                    }
                }
                else
                {
                    Point result;
                    if (CaptureInterface.GetMapCoords((int)mouse.X, (int)mouse.Y, 0, out result))
                    {
                        if (Main.mouseLeft)
                        {
                            CaptureInterface.EdgeAPinned = true;
                            CaptureInterface.EdgeA = result;
                        }
                        if (Main.mouseRight)
                        {
                            CaptureInterface.EdgeBPinned = true;
                            CaptureInterface.EdgeB = result;
                        }
                    }
                }
                CaptureInterface.ConstraintPoints();
            }

            private void DrawMarkedArea(SpriteBatch sb)
            {
                if (!CaptureInterface.EdgeAPinned || !CaptureInterface.EdgeBPinned)
                    return;
                int PinX = Math.Min(CaptureInterface.EdgeA.X, CaptureInterface.EdgeB.X);
                int PinY = Math.Min(CaptureInterface.EdgeA.Y, CaptureInterface.EdgeB.Y);
                int num1 = Math.Abs(CaptureInterface.EdgeA.X - CaptureInterface.EdgeB.X);
                int num2 = Math.Abs(CaptureInterface.EdgeA.Y - CaptureInterface.EdgeB.Y);
                if (!Main.mapFullscreen)
                {
                    Rectangle rectangle1 = Main.ReverseGravitySupport(new Rectangle(PinX * 16, PinY * 16, (num1 + 1) * 16, (num2 + 1) * 16));
                    Rectangle rectangle2 = Main.ReverseGravitySupport(new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth + 1, Main.screenHeight + 1));
                    Rectangle result;
                    Rectangle.Intersect(ref rectangle2, ref rectangle1, out result);
                    if (result.Width == 0 || result.Height == 0)
                        return;
                    result.Offset(-rectangle2.X, -rectangle2.Y);
                    sb.Draw(Main.magicPixel, result, CaptureInterface.Settings.MarkedAreaColor);
                    for (int index = 0; index < 2; ++index)
                    {
                        sb.Draw(Main.magicPixel, new Rectangle(result.X, result.Y + (index == 1 ? result.Height : -2), result.Width, 2), Color.White);
                        sb.Draw(Main.magicPixel, new Rectangle(result.X + (index == 1 ? result.Width : -2), result.Y, 2, result.Height), Color.White);
                    }
                }
                else
                {
                    Point result1;
                    CaptureInterface.GetMapCoords(PinX, PinY, 1, out result1);
                    Point result2;
                    CaptureInterface.GetMapCoords(PinX + num1 + 1, PinY + num2 + 1, 1, out result2);
                    Rectangle rectangle1 = new Rectangle(result1.X, result1.Y, result2.X - result1.X, result2.Y - result1.Y);
                    Rectangle rectangle2 = new Rectangle(0, 0, Main.screenWidth + 1, Main.screenHeight + 1);
                    Rectangle result3;
                    Rectangle.Intersect(ref rectangle2, ref rectangle1, out result3);
                    if (result3.Width == 0 || result3.Height == 0)
                        return;
                    result3.Offset(-rectangle2.X, -rectangle2.Y);
                    sb.Draw(Main.magicPixel, result3, CaptureInterface.Settings.MarkedAreaColor);
                    for (int index = 0; index < 2; ++index)
                    {
                        sb.Draw(Main.magicPixel, new Rectangle(result3.X, result3.Y + (index == 1 ? result3.Height : -2), result3.Width, 2), Color.White);
                        sb.Draw(Main.magicPixel, new Rectangle(result3.X + (index == 1 ? result3.Width : -2), result3.Y, 2, result3.Height), Color.White);
                    }
                }
            }

            private void DrawCursors(SpriteBatch sb)
            {
                float num1 = 1f / Main.cursorScale;
                float num2 = 0.8f / num1;
                Vector2 min = Main.screenPosition + new Vector2(30f);
                Vector2 max = min + new Vector2((float)Main.screenWidth, (float)Main.screenHeight) - new Vector2(60f);
                if (Main.mapFullscreen)
                {
                    min -= Main.screenPosition;
                    max -= Main.screenPosition;
                }
                Vector3 vector3 = Main.rgbToHsl(Main.cursorColor);
                Main.hslToRgb((float)(((double)vector3.X + 0.330000013113022) % 1.0), vector3.Y, vector3.Z);
                Main.hslToRgb((float)(((double)vector3.X - 0.330000013113022) % 1.0), vector3.Y, vector3.Z);
                Color white;
                Color color = white = Color.White;
                bool flag = (double)Main.player[Main.myPlayer].gravDir == -1.0;
                if (!CaptureInterface.EdgeAPinned)
                {
                    Utils.DrawCursorSingle(sb, color, 3.926991f, Main.cursorScale * num1 * num2, new Vector2((float)((double)Main.mouseX - 5.0 + 12.0), (float)((double)Main.mouseY + 2.5 + 12.0)), 4, 0);
                }
                else
                {
                    int specialMode = 0;
                    float num3 = 0.0f;
                    Vector2 vector2_1 = Vector2.Zero;
                    if (!Main.mapFullscreen)
                    {
                        Vector2 vector2_2 = Utils.ToVector2(CaptureInterface.EdgeA) * 16f;
                        float num4;
                        Vector2 vector2_3;
                        if (!CaptureInterface.EdgeBPinned)
                        {
                            specialMode = 1;
                            Vector2 vector2_4 = vector2_2 + Vector2.One * 8f;
                            vector2_1 = vector2_4;
                            num4 = Utils.ToRotation(-vector2_4 + Main.ReverseGravitySupport(new Vector2((float)Main.mouseX, (float)Main.mouseY), 0.0f) + Main.screenPosition);
                            if (flag)
                                num4 = -num4;
                            vector2_3 = Vector2.Clamp(vector2_4, min, max);
                            if (vector2_3 != vector2_4)
                                num4 = Utils.ToRotation(vector2_4 - vector2_3);
                        }
                        else
                        {
                            Vector2 vector2_4 = new Vector2((float)(Utils.ToInt(CaptureInterface.EdgeA.X > CaptureInterface.EdgeB.X) * 16), (float)(Utils.ToInt(CaptureInterface.EdgeA.Y > CaptureInterface.EdgeB.Y) * 16));
                            Vector2 vector2_5 = vector2_2 + vector2_4;
                            vector2_3 = Vector2.Clamp(vector2_5, min, max);
                            num4 = Utils.ToRotation(Utils.ToVector2(CaptureInterface.EdgeB) * 16f + new Vector2(16f) - vector2_4 - vector2_3);
                            if (vector2_3 != vector2_5)
                            {
                                num4 = Utils.ToRotation(vector2_5 - vector2_3);
                                specialMode = 1;
                            }
                            if (flag)
                                num4 *= -1f;
                        }
                        Utils.DrawCursorSingle(sb, color, num4 - 1.570796f, Main.cursorScale * num1, Main.ReverseGravitySupport(vector2_3 - Main.screenPosition, 0.0f), 4, specialMode);
                    }
                    else
                    {
                        Point result1 = CaptureInterface.EdgeA;
                        if (CaptureInterface.EdgeBPinned)
                        {
                            int num4 = Utils.ToInt(CaptureInterface.EdgeA.X > CaptureInterface.EdgeB.X);
                            int num5 = Utils.ToInt(CaptureInterface.EdgeA.Y > CaptureInterface.EdgeB.Y);
                            result1.X += num4;
                            result1.Y += num5;
                            CaptureInterface.GetMapCoords(result1.X, result1.Y, 1, out result1);
                            Point result2 = CaptureInterface.EdgeB;
                            result2.X += 1 - num4;
                            result2.Y += 1 - num5;
                            CaptureInterface.GetMapCoords(result2.X, result2.Y, 1, out result2);
                            Vector2 vector2_2 = Vector2.Clamp(Utils.ToVector2(result1), min, max);
                            num3 = Utils.ToRotation(Utils.ToVector2(result2) - vector2_2);
                        }
                        else
                            CaptureInterface.GetMapCoords(result1.X, result1.Y, 1, out result1);
                        Utils.DrawCursorSingle(sb, color, num3 - 1.570796f, Main.cursorScale * num1, Utils.ToVector2(result1), 4, 0);
                    }
                }
                if (!CaptureInterface.EdgeBPinned)
                {
                    Utils.DrawCursorSingle(sb, white, 0.7853982f, Main.cursorScale * num1 * num2, new Vector2((float)((double)Main.mouseX + 2.5 + 12.0), (float)((double)Main.mouseY - 5.0 + 12.0)), 5, 0);
                }
                else
                {
                    int specialMode = 0;
                    float num3 = 0.0f;
                    Vector2 vector2_1 = Vector2.Zero;
                    if (!Main.mapFullscreen)
                    {
                        Vector2 vector2_2 = Utils.ToVector2(CaptureInterface.EdgeB) * 16f;
                        float num4;
                        Vector2 vector2_3;
                        if (!CaptureInterface.EdgeAPinned)
                        {
                            specialMode = 1;
                            Vector2 vector2_4 = vector2_2 + Vector2.One * 8f;
                            vector2_1 = vector2_4;
                            num4 = Utils.ToRotation(-vector2_4 + Main.ReverseGravitySupport(new Vector2((float)Main.mouseX, (float)Main.mouseY), 0.0f) + Main.screenPosition);
                            if (flag)
                                num4 = -num4;
                            vector2_3 = Vector2.Clamp(vector2_4, min, max);
                            if (vector2_3 != vector2_4)
                                num4 = Utils.ToRotation(vector2_4 - vector2_3);
                        }
                        else
                        {
                            Vector2 vector2_4 = new Vector2((float)(Utils.ToInt(CaptureInterface.EdgeB.X >= CaptureInterface.EdgeA.X) * 16), (float)(Utils.ToInt(CaptureInterface.EdgeB.Y >= CaptureInterface.EdgeA.Y) * 16));
                            Vector2 vector2_5 = vector2_2 + vector2_4;
                            vector2_3 = Vector2.Clamp(vector2_5, min, max);
                            num4 = Utils.ToRotation(Utils.ToVector2(CaptureInterface.EdgeA) * 16f + new Vector2(16f) - vector2_4 - vector2_3);
                            if (vector2_3 != vector2_5)
                            {
                                num4 = Utils.ToRotation(vector2_5 - vector2_3);
                                specialMode = 1;
                            }
                            if (flag)
                                num4 *= -1f;
                        }
                        Utils.DrawCursorSingle(sb, white, num4 - 1.570796f, Main.cursorScale * num1, Main.ReverseGravitySupport(vector2_3 - Main.screenPosition, 0.0f), 5, specialMode);
                    }
                    else
                    {
                        Point result1 = CaptureInterface.EdgeB;
                        if (CaptureInterface.EdgeAPinned)
                        {
                            int num4 = Utils.ToInt(CaptureInterface.EdgeB.X >= CaptureInterface.EdgeA.X);
                            int num5 = Utils.ToInt(CaptureInterface.EdgeB.Y >= CaptureInterface.EdgeA.Y);
                            result1.X += num4;
                            result1.Y += num5;
                            CaptureInterface.GetMapCoords(result1.X, result1.Y, 1, out result1);
                            Point result2 = CaptureInterface.EdgeA;
                            result2.X += 1 - num4;
                            result2.Y += 1 - num5;
                            CaptureInterface.GetMapCoords(result2.X, result2.Y, 1, out result2);
                            Vector2 vector2_2 = Vector2.Clamp(Utils.ToVector2(result1), min, max);
                            num3 = Utils.ToRotation(Utils.ToVector2(result2) - vector2_2);
                        }
                        else
                            CaptureInterface.GetMapCoords(result1.X, result1.Y, 1, out result1);
                        Utils.DrawCursorSingle(sb, white, num3 - 1.570796f, Main.cursorScale * num1, Utils.ToVector2(result1), 5, 0);
                    }
                }
            }
        }

        private class ModeDragBounds : CaptureInterface.CaptureInterfaceMode
        {
            public int currentAim = -1;
            private int caughtEdge = -1;
            private bool dragging;
            private bool inMap;

            public override void Update()
            {
                if (!this.Selected || CaptureInterface.JustActivated)
                    return;
                this.DragBounds(new Vector2((float)Main.mouseX, (float)Main.mouseY));
            }

            public override void Draw(SpriteBatch sb)
            {
                if (!this.Selected)
                    return;
                this.DrawMarkedArea(sb);
            }

            public override void ToggleActive(bool tickedOn)
            {
                if (tickedOn)
                    return;
                this.currentAim = -1;
            }

            public override bool UsingMap()
            {
                return this.caughtEdge != -1;
            }

            private void DragBounds(Vector2 mouse)
            {
                if (!CaptureInterface.EdgeAPinned || !CaptureInterface.EdgeBPinned)
                {
                    bool flag1 = false;
                    if (Main.mouseLeft)
                        flag1 = true;
                    if (flag1)
                    {
                        bool flag2 = true;
                        Point result;
                        if (!Main.mapFullscreen)
                            result = Utils.ToTileCoordinates(Main.screenPosition + mouse);
                        else
                            flag2 = CaptureInterface.GetMapCoords((int)mouse.X, (int)mouse.Y, 0, out result);
                        if (flag2)
                        {
                            if (!CaptureInterface.EdgeAPinned)
                            {
                                CaptureInterface.EdgeAPinned = true;
                                CaptureInterface.EdgeA = result;
                            }
                            if (!CaptureInterface.EdgeBPinned)
                            {
                                CaptureInterface.EdgeBPinned = true;
                                CaptureInterface.EdgeB = result;
                            }
                        }
                        this.currentAim = 3;
                        this.caughtEdge = 1;
                    }
                }
                int PinX = Math.Min(CaptureInterface.EdgeA.X, CaptureInterface.EdgeB.X);
                int PinY = Math.Min(CaptureInterface.EdgeA.Y, CaptureInterface.EdgeB.Y);
                int num1 = Math.Abs(CaptureInterface.EdgeA.X - CaptureInterface.EdgeB.X);
                int num2 = Math.Abs(CaptureInterface.EdgeA.Y - CaptureInterface.EdgeB.Y);
                bool flag = (double)Main.player[Main.myPlayer].gravDir == -1.0;
                int num3 = 1 - Utils.ToInt(flag);
                int num4 = Utils.ToInt(flag);
                Rectangle rectangle1;
                Rectangle rectangle2;
                if (!Main.mapFullscreen)
                {
                    rectangle1 = Main.ReverseGravitySupport(new Rectangle(PinX * 16, PinY * 16, (num1 + 1) * 16, (num2 + 1) * 16));
                    rectangle2 = Main.ReverseGravitySupport(new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth + 1, Main.screenHeight + 1));
                    Rectangle result;
                    Rectangle.Intersect(ref rectangle2, ref rectangle1, out result);
                    if (result.Width == 0 || result.Height == 0)
                        return;
                    result.Offset(-rectangle2.X, -rectangle2.Y);
                }
                else
                {
                    Point result1;
                    CaptureInterface.GetMapCoords(PinX, PinY, 1, out result1);
                    Point result2;
                    CaptureInterface.GetMapCoords(PinX + num1 + 1, PinY + num2 + 1, 1, out result2);
                    rectangle1 = new Rectangle(result1.X, result1.Y, result2.X - result1.X, result2.Y - result1.Y);
                    rectangle2 = new Rectangle(0, 0, Main.screenWidth + 1, Main.screenHeight + 1);
                    Rectangle result3;
                    Rectangle.Intersect(ref rectangle2, ref rectangle1, out result3);
                    if (result3.Width == 0 || result3.Height == 0)
                        return;
                    result3.Offset(-rectangle2.X, -rectangle2.Y);
                }
                this.dragging = false;
                if (!Main.mouseLeft)
                    this.currentAim = -1;
                if (this.currentAim != -1)
                {
                    this.dragging = true;
                    Point point2;
                    if (!Main.mapFullscreen)
                    {
                        point2 = Utils.ToTileCoordinates(Main.MouseWorld);
                    }
                    else
                    {
                        Point result;
                        if (!CaptureInterface.GetMapCoords((int)mouse.X, (int)mouse.Y, 0, out result))
                            return;
                        point2 = result;
                    }
                    switch (this.currentAim)
                    {
                        case 0:
                        case 1:
                            if (this.caughtEdge == 0)
                                CaptureInterface.EdgeA.Y = point2.Y;
                            if (this.caughtEdge == 1)
                            {
                                CaptureInterface.EdgeB.Y = point2.Y;
                                break;
                            }
                            break;
                        case 2:
                        case 3:
                            if (this.caughtEdge == 0)
                                CaptureInterface.EdgeA.X = point2.X;
                            if (this.caughtEdge == 1)
                            {
                                CaptureInterface.EdgeB.X = point2.X;
                                break;
                            }
                            break;
                    }
                }
                else
                {
                    this.caughtEdge = -1;
                    Rectangle drawbox = rectangle1;
                    drawbox.Offset(-rectangle2.X, -rectangle2.Y);
                    this.inMap = drawbox.Contains(Utils.ToPoint(mouse));
                    for (int boundIndex = 0; boundIndex < 4; ++boundIndex)
                    {
                        Rectangle bound = this.GetBound(drawbox, boundIndex);
                        bound.Inflate(8, 8);
                        if (bound.Contains(Utils.ToPoint(mouse)))
                        {
                            this.currentAim = boundIndex;
                            if (boundIndex == 0)
                            {
                                this.caughtEdge = CaptureInterface.EdgeA.Y >= CaptureInterface.EdgeB.Y ? num3 : num4;
                                break;
                            }
                            if (boundIndex == 1)
                            {
                                this.caughtEdge = CaptureInterface.EdgeA.Y < CaptureInterface.EdgeB.Y ? num3 : num4;
                                break;
                            }
                            if (boundIndex == 2)
                            {
                                this.caughtEdge = CaptureInterface.EdgeA.X >= CaptureInterface.EdgeB.X ? 1 : 0;
                                break;
                            }
                            if (boundIndex == 3)
                            {
                                this.caughtEdge = CaptureInterface.EdgeA.X < CaptureInterface.EdgeB.X ? 1 : 0;
                                break;
                            }
                            break;
                        }
                    }
                }
                CaptureInterface.ConstraintPoints();
            }

            private Rectangle GetBound(Rectangle drawbox, int boundIndex)
            {
                if (boundIndex == 0)
                    return new Rectangle(drawbox.X, drawbox.Y - 2, drawbox.Width, 2);
                if (boundIndex == 1)
                    return new Rectangle(drawbox.X, drawbox.Y + drawbox.Height, drawbox.Width, 2);
                if (boundIndex == 2)
                    return new Rectangle(drawbox.X - 2, drawbox.Y, 2, drawbox.Height);
                if (boundIndex == 3)
                    return new Rectangle(drawbox.X + drawbox.Width, drawbox.Y, 2, drawbox.Height);
                return Rectangle.Empty;
            }

            public void DrawMarkedArea(SpriteBatch sb)
            {
                if (!CaptureInterface.EdgeAPinned || !CaptureInterface.EdgeBPinned)
                    return;
                int PinX = Math.Min(CaptureInterface.EdgeA.X, CaptureInterface.EdgeB.X);
                int PinY = Math.Min(CaptureInterface.EdgeA.Y, CaptureInterface.EdgeB.Y);
                int num1 = Math.Abs(CaptureInterface.EdgeA.X - CaptureInterface.EdgeB.X);
                int num2 = Math.Abs(CaptureInterface.EdgeA.Y - CaptureInterface.EdgeB.Y);
                Rectangle result1;
                if (!Main.mapFullscreen)
                {
                    Rectangle rectangle1 = Main.ReverseGravitySupport(new Rectangle(PinX * 16, PinY * 16, (num1 + 1) * 16, (num2 + 1) * 16));
                    Rectangle rectangle2 = Main.ReverseGravitySupport(new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth + 1, Main.screenHeight + 1));
                    Rectangle.Intersect(ref rectangle2, ref rectangle1, out result1);
                    if (result1.Width == 0 || result1.Height == 0)
                        return;
                    result1.Offset(-rectangle2.X, -rectangle2.Y);
                }
                else
                {
                    Point result2;
                    CaptureInterface.GetMapCoords(PinX, PinY, 1, out result2);
                    Point result3;
                    CaptureInterface.GetMapCoords(PinX + num1 + 1, PinY + num2 + 1, 1, out result3);
                    Rectangle rectangle1 = new Rectangle(result2.X, result2.Y, result3.X - result2.X, result3.Y - result2.Y);
                    Rectangle rectangle2 = new Rectangle(0, 0, Main.screenWidth + 1, Main.screenHeight + 1);
                    Rectangle.Intersect(ref rectangle2, ref rectangle1, out result1);
                    if (result1.Width == 0 || result1.Height == 0)
                        return;
                    result1.Offset(-rectangle2.X, -rectangle2.Y);
                }
                sb.Draw(Main.magicPixel, result1, CaptureInterface.Settings.MarkedAreaColor);
                Rectangle r = Rectangle.Empty;
                for (int index = 0; index < 2; ++index)
                {
                    if (this.currentAim != index)
                        this.DrawBound(sb, new Rectangle(result1.X, result1.Y + (index == 1 ? result1.Height : -2), result1.Width, 2), 0);
                    else
                        r = new Rectangle(result1.X, result1.Y + (index == 1 ? result1.Height : -2), result1.Width, 2);
                    if (this.currentAim != index + 2)
                        this.DrawBound(sb, new Rectangle(result1.X + (index == 1 ? result1.Width : -2), result1.Y, 2, result1.Height), 0);
                    else
                        r = new Rectangle(result1.X + (index == 1 ? result1.Width : -2), result1.Y, 2, result1.Height);
                }
                if (!(r != Rectangle.Empty))
                    return;
                this.DrawBound(sb, r, 1 + Utils.ToInt(this.dragging));
            }

            private void DrawBound(SpriteBatch sb, Rectangle r, int mode)
            {
                if (mode == 0)
                    sb.Draw(Main.magicPixel, r, Color.Silver);
                else if (mode == 1)
                {
                    Rectangle destinationRectangle = new Rectangle(r.X - 2, r.Y, r.Width + 4, r.Height);
                    sb.Draw(Main.magicPixel, destinationRectangle, Color.White);
                    destinationRectangle = new Rectangle(r.X, r.Y - 2, r.Width, r.Height + 4);
                    sb.Draw(Main.magicPixel, destinationRectangle, Color.White);
                    sb.Draw(Main.magicPixel, r, Color.White);
                }
                else
                {
                    if (mode != 2)
                        return;
                    Rectangle destinationRectangle = new Rectangle(r.X - 2, r.Y, r.Width + 4, r.Height);
                    sb.Draw(Main.magicPixel, destinationRectangle, Color.Gold);
                    destinationRectangle = new Rectangle(r.X, r.Y - 2, r.Width, r.Height + 4);
                    sb.Draw(Main.magicPixel, destinationRectangle, Color.Gold);
                    sb.Draw(Main.magicPixel, r, Color.Gold);
                }
            }
        }

        private class ModeChangeSettings : CaptureInterface.CaptureInterfaceMode
        {
            private int hoveredButton = -1;
            private const int ButtonsCount = 7;
            private bool inUI;

            private Rectangle GetRect()
            {
                Rectangle rectangle = new Rectangle(0, 0, 224, 170);
                if (CaptureInterface.Settings.ScreenAnchor == 0)
                {
                    rectangle.X = 227 - rectangle.Width / 2;
                    rectangle.Y = 80;
                    int index = 0;
                    Player player = Main.player[Main.myPlayer];
                    while (index < player.buffTime.Length && player.buffTime[index] > 0)
                        ++index;
                    int num = index / 11 + (index % 11 >= 3 ? 1 : 0);
                    rectangle.Y += 48 * num;
                }
                return rectangle;
            }

            private void ButtonDraw(int button, ref string key, ref string value)
            {
                switch (button)
                {
                    case 0:
                        key = Lang.inter[74];
                        value = Lang.inter[73 - Utils.ToInt(CaptureInterface.Settings.PackImage)];
                        break;
                    case 1:
                        key = Lang.inter[75];
                        value = Lang.inter[73 - Utils.ToInt(CaptureInterface.Settings.IncludeEntities)];
                        break;
                    case 2:
                        key = Lang.inter[76];
                        value = Lang.inter[73 - Utils.ToInt(!CaptureInterface.Settings.TransparentBackground)];
                        break;
                    case 6:
                        key = "      " + Lang.menu[86];
                        value = "";
                        break;
                }
            }

            private void PressButton(int button)
            {
                switch (button)
                {
                    case 0:
                        CaptureInterface.Settings.PackImage = !CaptureInterface.Settings.PackImage;
                        break;
                    case 1:
                        CaptureInterface.Settings.IncludeEntities = !CaptureInterface.Settings.IncludeEntities;
                        break;
                    case 2:
                        CaptureInterface.Settings.TransparentBackground = !CaptureInterface.Settings.TransparentBackground;
                        break;
                    case 6:
                        CaptureInterface.Settings.PackImage = false;
                        CaptureInterface.Settings.IncludeEntities = true;
                        CaptureInterface.Settings.TransparentBackground = false;
                        CaptureInterface.Settings.BiomeChoice = 0;
                        break;
                }
            }

            private void DrawWaterChoices(SpriteBatch spritebatch, Point start, Point mouse)
            {
                Rectangle r = new Rectangle(0, 0, 20, 20);
                for (int index1 = 0; index1 < 2; ++index1)
                {
                    for (int index2 = 0; index2 < 5; ++index2)
                    {
                        if (index1 != 1 || index2 != 3)
                        {
                            int index3 = index2 + index1 * 5;
                            r.X = start.X + 24 * index2 + 12 * index1;
                            r.Y = start.Y + 24 * index1;
                            if (index1 == 1 && index2 == 4)
                                r.X -= 24;
                            int num1 = 0;
                            if (r.Contains(mouse))
                            {
                                if (Main.mouseLeft && Main.mouseLeftRelease)
                                    CaptureInterface.Settings.BiomeChoice = this.BiomeWater(index3);
                                ++num1;
                            }
                            if (CaptureInterface.Settings.BiomeChoice == this.BiomeWater(index3))
                                num1 += 2;
                            Texture2D texture = Main.liquidTexture[this.BiomeWater(index3)];
                            int x = (int)Main.wFrame * 18;
                            Color white = Color.White;
                            float num2 = 1f;
                            if (num1 < 2)
                                num2 *= 0.5f;
                            if (num1 % 2 == 1)
                                spritebatch.Draw(Main.magicPixel, Utils.TopLeft(r), new Rectangle?(new Rectangle(0, 0, 1, 1)), Color.Gold, 0.0f, Vector2.Zero, new Vector2(20f), SpriteEffects.None, 0.0f);
                            else
                                spritebatch.Draw(Main.magicPixel, Utils.TopLeft(r), new Rectangle?(new Rectangle(0, 0, 1, 1)), Color.White * num2, 0.0f, Vector2.Zero, new Vector2(20f), SpriteEffects.None, 0.0f);
                            spritebatch.Draw(texture, Utils.TopLeft(r) + new Vector2(2f), new Rectangle?(new Rectangle(x, 0, 16, 16)), Color.White * num2);
                        }
                    }
                }
            }

            private int BiomeWater(int index)
            {
                switch (index)
                {
                    case 0:
                        return 0;
                    case 1:
                        return 2;
                    case 2:
                        return 3;
                    case 3:
                        return 4;
                    case 4:
                        return 5;
                    case 5:
                        return 6;
                    case 6:
                        return 7;
                    case 7:
                        return 8;
                    case 8:
                        return 9;
                    case 9:
                        return 10;
                    default:
                        return 0;
                }
            }

            public override void Update()
            {
                if (!this.Selected || CaptureInterface.JustActivated)
                    return;
                Point point = new Point(Main.mouseX, Main.mouseY);
                this.hoveredButton = -1;
                Rectangle rect = this.GetRect();
                this.inUI = rect.Contains(point);
                rect.Inflate(-20, -20);
                rect.Height = 16;
                int num = rect.Y;
                for (int index = 0; index < 7; ++index)
                {
                    rect.Y = num + index * 20;
                    if (rect.Contains(point))
                    {
                        this.hoveredButton = index;
                        break;
                    }
                }
                if (!Main.mouseLeft || !Main.mouseLeftRelease || this.hoveredButton == -1)
                    return;
                this.PressButton(this.hoveredButton);
            }

            public override void Draw(SpriteBatch sb)
            {
                if (!this.Selected)
                    return;
                ((CaptureInterface.ModeDragBounds)CaptureInterface.Modes[1]).currentAim = -1;
                ((CaptureInterface.ModeDragBounds)CaptureInterface.Modes[1]).DrawMarkedArea(sb);
                Rectangle rect = this.GetRect();
                Utils.DrawInvBG(sb, rect, new Color(63, 65, 151, (int)byte.MaxValue) * 0.485f);
                for (int button = 0; button < 7; ++button)
                {
                    string key = "";
                    string text = "";
                    this.ButtonDraw(button, ref key, ref text);
                    Color baseColor = Color.White;
                    if (button == this.hoveredButton)
                        baseColor = Color.Gold;
                    ChatManager.DrawColorCodedStringWithShadow(sb, Main.fontItemStack, key, Utils.TopLeft(rect) + new Vector2(20f, (float)(20 + 20 * button)), baseColor, 0.0f, Vector2.Zero, Vector2.One, -1f, 2f);
                    ChatManager.DrawColorCodedStringWithShadow(sb, Main.fontItemStack, text, Utils.TopRight(rect) + new Vector2(-20f, (float)(20 + 20 * button)), baseColor, 0.0f, Main.fontItemStack.MeasureString(text) * Vector2.UnitX, Vector2.One, -1f, 2f);
                }
                this.DrawWaterChoices(sb, Utils.ToPoint(Utils.TopLeft(rect) + new Vector2((float)(rect.Width / 2 - 58), 90f)), Utils.ToPoint(Main.MouseScreen));
            }

            public override void ToggleActive(bool tickedOn)
            {
                if (!tickedOn)
                    return;
                this.hoveredButton = -1;
            }

            public override bool UsingMap()
            {
                return this.inUI;
            }
        }
    }
}
