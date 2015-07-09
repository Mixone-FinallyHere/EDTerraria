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
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;

namespace Terraria.GameContent.Tile_Entities
{
    internal class TETrainingDummy : TileEntity
    {
        private static Dictionary<int, Rectangle> playerBox = new Dictionary<int, Rectangle>();
        private static bool playerBoxFilled = false;
        public int npc;

        public TETrainingDummy()
        {
            this.npc = -1;
        }

        public static void Initialize()
        {
            TileEntity._UpdateStart += new Action(TETrainingDummy.ClearBoxes);
        }

        public static void ClearBoxes()
        {
            TETrainingDummy.playerBox.Clear();
            TETrainingDummy.playerBoxFilled = false;
        }

        public override void Update()
        {
            Rectangle rectangle = new Rectangle(0, 0, 32, 48);
            rectangle.Inflate(1600, 1600);
            int num1 = rectangle.X;
            int num2 = rectangle.Y;
            if (this.npc != -1)
            {
                if (Main.npc[this.npc].active && Main.npc[this.npc].type == 488 && ((double)Main.npc[this.npc].ai[0] == (double)this.Position.X && (double)Main.npc[this.npc].ai[1] == (double)this.Position.Y))
                    return;
                this.Deactivate();
            }
            else
            {
                TETrainingDummy.FillPlayerHitboxes();
                rectangle.X = (int)this.Position.X * 16 + num1;
                rectangle.Y = (int)this.Position.Y * 16 + num2;
                bool flag = false;
                foreach (KeyValuePair<int, Rectangle> keyValuePair in TETrainingDummy.playerBox)
                {
                    if (keyValuePair.Value.Intersects(rectangle))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                    return;
                this.Activate();
            }
        }

        private static void FillPlayerHitboxes()
        {
            if (TETrainingDummy.playerBoxFilled)
                return;
            for (int index = 0; index < (int)byte.MaxValue; ++index)
            {
                if (Main.player[index].active)
                    TETrainingDummy.playerBox[index] = Main.player[index].getRect();
            }
            TETrainingDummy.playerBoxFilled = true;
        }

        public static bool ValidTile(int x, int y)
        {
            return Main.tile[x, y].active() && (int)Main.tile[x, y].type == 378 && ((int)Main.tile[x, y].frameY == 0 && (int)Main.tile[x, y].frameX % 36 == 0);
        }

        public static int Place(int x, int y)
        {
            TETrainingDummy teTrainingDummy = new TETrainingDummy();
            teTrainingDummy.Position = new Point16(x, y);
            teTrainingDummy.ID = TileEntity.AssignNewID();
            teTrainingDummy.type = (byte)0;
            TileEntity.ByID[teTrainingDummy.ID] = (TileEntity)teTrainingDummy;
            TileEntity.ByPosition[teTrainingDummy.Position] = (TileEntity)teTrainingDummy;
            return teTrainingDummy.ID;
        }

        public static int Hook_AfterPlacement(int x, int y, int type = 21, int style = 0, int direction = 1)
        {
            if (Main.netMode != 1)
                return TETrainingDummy.Place(x - 1, y - 2);
            NetMessage.SendTileSquare(Main.myPlayer, x - 1, y - 1, 3);
            NetMessage.SendData(87, -1, -1, "", x - 1, (float)(y - 2), 0.0f, 0.0f, 0, 0, 0);
            return -1;
        }

        public static void Kill(int x, int y)
        {
            TileEntity tileEntity;
            if (!TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) || (int)tileEntity.type != 0)
                return;
            TileEntity.ByID.Remove(tileEntity.ID);
            TileEntity.ByPosition.Remove(new Point16(x, y));
        }

        public static int Find(int x, int y)
        {
            TileEntity tileEntity;
            if (TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) && (int)tileEntity.type == 0)
                return tileEntity.ID;
            return -1;
        }

        public override void WriteExtraData(BinaryWriter writer)
        {
            writer.Write((short)this.npc);
        }

        public override void ReadExtraData(BinaryReader reader)
        {
            this.npc = (int)reader.ReadInt16();
        }

        public void Activate()
        {
            int index = NPC.NewNPC((int)this.Position.X * 16 + 16, (int)this.Position.Y * 16 + 48, 488, 100, 0.0f, 0.0f, 0.0f, 0.0f, (int)byte.MaxValue);
            Main.npc[index].ai[0] = (float)this.Position.X;
            Main.npc[index].ai[1] = (float)this.Position.Y;
            Main.npc[index].netUpdate = true;
            this.npc = index;
            if (Main.netMode == 1)
                return;
            NetMessage.SendData(86, -1, -1, "", this.ID, (float)this.Position.X, (float)this.Position.Y, 0.0f, 0, 0, 0);
        }

        public void Deactivate()
        {
            if (this.npc != -1)
                Main.npc[this.npc].active = false;
            this.npc = -1;
            if (Main.netMode == 1)
                return;
            NetMessage.SendData(86, -1, -1, "", this.ID, (float)this.Position.X, (float)this.Position.Y, 0.0f, 0, 0, 0);
        }

        public override string ToString()
        {
            return (string)(object)this.Position.X + (object)"x  " + (string)(object)this.Position.Y + "y npc: " + (string)(object)this.npc;
        }
    }
}