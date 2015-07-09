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
using Terraria.DataStructures;
using Terraria.GameContent.Achievements;
using Terraria;

namespace Terraria
{
    public class Chest
    {
        public static int[] chestTypeToIcon = new int[52];
        public static int[] chestItemSpawn = new int[52];
        public static int[] dresserTypeToIcon = new int[28];
        public static int[] dresserItemSpawn = new int[28];
        public const int maxChestTypes = 52;
        public const int maxDresserTypes = 28;
        public const int maxItems = 40;
        public const int MaxNameLength = 20;
        public Item[] item;
        public int x;
        public int y;
        public bool bankChest;
        public string name;
        public int frameCounter;
        public int frame;

        public Chest(bool bank = false)
        {
            this.item = new Item[40];
            this.bankChest = bank;
            this.name = string.Empty;
        }

        public override string ToString()
        {
            int num = 0;
            for (int index = 0; index < this.item.Length; ++index)
            {
                if (this.item[index].stack > 0)
                    ++num;
            }
            return string.Format("{{X: {0}, Y: {1}, Count: {2}}}", (object)this.x, (object)this.y, (object)num);
        }

        public static void Initialize()
        {
            Chest.chestTypeToIcon[0] = Chest.chestItemSpawn[0] = 48;
            Chest.chestTypeToIcon[1] = Chest.chestItemSpawn[1] = 306;
            Chest.chestTypeToIcon[2] = 327;
            Chest.chestItemSpawn[2] = 306;
            Chest.chestTypeToIcon[3] = Chest.chestItemSpawn[3] = 328;
            Chest.chestTypeToIcon[4] = 329;
            Chest.chestItemSpawn[4] = 328;
            Chest.chestTypeToIcon[5] = Chest.chestItemSpawn[5] = 343;
            Chest.chestTypeToIcon[6] = Chest.chestItemSpawn[6] = 348;
            Chest.chestTypeToIcon[7] = Chest.chestItemSpawn[7] = 625;
            Chest.chestTypeToIcon[8] = Chest.chestItemSpawn[8] = 626;
            Chest.chestTypeToIcon[9] = Chest.chestItemSpawn[9] = 627;
            Chest.chestTypeToIcon[10] = Chest.chestItemSpawn[10] = 680;
            Chest.chestTypeToIcon[11] = Chest.chestItemSpawn[11] = 681;
            Chest.chestTypeToIcon[12] = Chest.chestItemSpawn[12] = 831;
            Chest.chestTypeToIcon[13] = Chest.chestItemSpawn[13] = 838;
            Chest.chestTypeToIcon[14] = Chest.chestItemSpawn[14] = 914;
            Chest.chestTypeToIcon[15] = Chest.chestItemSpawn[15] = 952;
            Chest.chestTypeToIcon[16] = Chest.chestItemSpawn[16] = 1142;
            Chest.chestTypeToIcon[17] = Chest.chestItemSpawn[17] = 1298;
            Chest.chestTypeToIcon[18] = Chest.chestItemSpawn[18] = 1528;
            Chest.chestTypeToIcon[19] = Chest.chestItemSpawn[19] = 1529;
            Chest.chestTypeToIcon[20] = Chest.chestItemSpawn[20] = 1530;
            Chest.chestTypeToIcon[21] = Chest.chestItemSpawn[21] = 1531;
            Chest.chestTypeToIcon[22] = Chest.chestItemSpawn[22] = 1532;
            Chest.chestTypeToIcon[23] = 1533;
            Chest.chestItemSpawn[23] = 1528;
            Chest.chestTypeToIcon[24] = 1534;
            Chest.chestItemSpawn[24] = 1529;
            Chest.chestTypeToIcon[25] = 1535;
            Chest.chestItemSpawn[25] = 1530;
            Chest.chestTypeToIcon[26] = 1536;
            Chest.chestItemSpawn[26] = 1531;
            Chest.chestTypeToIcon[27] = 1537;
            Chest.chestItemSpawn[27] = 1532;
            Chest.chestTypeToIcon[28] = Chest.chestItemSpawn[28] = 2230;
            Chest.chestTypeToIcon[29] = Chest.chestItemSpawn[29] = 2249;
            Chest.chestTypeToIcon[30] = Chest.chestItemSpawn[30] = 2250;
            Chest.chestTypeToIcon[31] = Chest.chestItemSpawn[31] = 2526;
            Chest.chestTypeToIcon[32] = Chest.chestItemSpawn[32] = 2544;
            Chest.chestTypeToIcon[33] = Chest.chestItemSpawn[33] = 2559;
            Chest.chestTypeToIcon[34] = Chest.chestItemSpawn[34] = 2574;
            Chest.chestTypeToIcon[35] = Chest.chestItemSpawn[35] = 2612;
            Chest.chestTypeToIcon[36] = 327;
            Chest.chestItemSpawn[36] = 2612;
            Chest.chestTypeToIcon[37] = Chest.chestItemSpawn[37] = 2613;
            Chest.chestTypeToIcon[38] = 327;
            Chest.chestItemSpawn[38] = 2613;
            Chest.chestTypeToIcon[39] = Chest.chestItemSpawn[39] = 2614;
            Chest.chestTypeToIcon[40] = 327;
            Chest.chestItemSpawn[40] = 2614;
            Chest.chestTypeToIcon[41] = Chest.chestItemSpawn[41] = 2615;
            Chest.chestTypeToIcon[42] = Chest.chestItemSpawn[42] = 2616;
            Chest.chestTypeToIcon[43] = Chest.chestItemSpawn[43] = 2617;
            Chest.chestTypeToIcon[44] = Chest.chestItemSpawn[44] = 2618;
            Chest.chestTypeToIcon[45] = Chest.chestItemSpawn[45] = 2619;
            Chest.chestTypeToIcon[46] = Chest.chestItemSpawn[46] = 2620;
            Chest.chestTypeToIcon[47] = Chest.chestItemSpawn[47] = 2748;
            Chest.chestTypeToIcon[48] = Chest.chestItemSpawn[48] = 2814;
            Chest.chestTypeToIcon[49] = Chest.chestItemSpawn[49] = 3180;
            Chest.chestTypeToIcon[50] = Chest.chestItemSpawn[50] = 3125;
            Chest.chestTypeToIcon[51] = Chest.chestItemSpawn[51] = 3181;
            Chest.dresserTypeToIcon[0] = Chest.dresserItemSpawn[0] = 334;
            Chest.dresserTypeToIcon[1] = Chest.dresserItemSpawn[1] = 647;
            Chest.dresserTypeToIcon[2] = Chest.dresserItemSpawn[2] = 648;
            Chest.dresserTypeToIcon[3] = Chest.dresserItemSpawn[3] = 649;
            Chest.dresserTypeToIcon[4] = Chest.dresserItemSpawn[4] = 918;
            Chest.dresserTypeToIcon[5] = Chest.dresserItemSpawn[5] = 2386;
            Chest.dresserTypeToIcon[6] = Chest.dresserItemSpawn[6] = 2387;
            Chest.dresserTypeToIcon[7] = Chest.dresserItemSpawn[7] = 2388;
            Chest.dresserTypeToIcon[8] = Chest.dresserItemSpawn[8] = 2389;
            Chest.dresserTypeToIcon[9] = Chest.dresserItemSpawn[9] = 2390;
            Chest.dresserTypeToIcon[10] = Chest.dresserItemSpawn[10] = 2391;
            Chest.dresserTypeToIcon[11] = Chest.dresserItemSpawn[11] = 2392;
            Chest.dresserTypeToIcon[12] = Chest.dresserItemSpawn[12] = 2393;
            Chest.dresserTypeToIcon[13] = Chest.dresserItemSpawn[13] = 2394;
            Chest.dresserTypeToIcon[14] = Chest.dresserItemSpawn[14] = 2395;
            Chest.dresserTypeToIcon[15] = Chest.dresserItemSpawn[15] = 2396;
            Chest.dresserTypeToIcon[16] = Chest.dresserItemSpawn[16] = 2529;
            Chest.dresserTypeToIcon[17] = Chest.dresserItemSpawn[17] = 2545;
            Chest.dresserTypeToIcon[18] = Chest.dresserItemSpawn[18] = 2562;
            Chest.dresserTypeToIcon[19] = Chest.dresserItemSpawn[19] = 2577;
            Chest.dresserTypeToIcon[20] = Chest.dresserItemSpawn[20] = 2637;
            Chest.dresserTypeToIcon[21] = Chest.dresserItemSpawn[21] = 2638;
            Chest.dresserTypeToIcon[22] = Chest.dresserItemSpawn[22] = 2639;
            Chest.dresserTypeToIcon[23] = Chest.dresserItemSpawn[23] = 2640;
            Chest.dresserTypeToIcon[24] = Chest.dresserItemSpawn[24] = 2816;
            Chest.dresserTypeToIcon[25] = Chest.dresserItemSpawn[25] = 3132;
            Chest.dresserTypeToIcon[26] = Chest.dresserItemSpawn[26] = 3134;
            Chest.dresserTypeToIcon[27] = Chest.dresserItemSpawn[27] = 3133;
        }

        private static bool IsPlayerInChest(int i)
        {
            for (int index = 0; index < (int)byte.MaxValue; ++index)
            {
                if (Main.player[index].chest == i)
                    return true;
            }
            return false;
        }

        public static bool isLocked(int x, int y)
        {
            return Main.tile[x, y] == null || (int)Main.tile[x, y].frameX >= 72 && (int)Main.tile[x, y].frameX <= 106 || ((int)Main.tile[x, y].frameX >= 144 && (int)Main.tile[x, y].frameX <= 178 || (int)Main.tile[x, y].frameX >= 828 && (int)Main.tile[x, y].frameX <= 1006) || ((int)Main.tile[x, y].frameX >= 1296 && (int)Main.tile[x, y].frameX <= 1330 || (int)Main.tile[x, y].frameX >= 1368 && (int)Main.tile[x, y].frameX <= 1402 || (int)Main.tile[x, y].frameX >= 1440 && (int)Main.tile[x, y].frameX <= 1474);
        }

        public static void ServerPlaceItem(int plr, int slot)
        {
            Main.player[plr].inventory[slot] = Chest.PutItemInNearbyChest(Main.player[plr].inventory[slot], Main.player[plr].Center);
            NetMessage.SendData(5, -1, -1, "", plr, (float)slot, (float)Main.player[plr].inventory[slot].prefix, 0.0f, 0, 0, 0);
        }

        public static Item PutItemInNearbyChest(Item item, Vector2 position)
        {
            if (Main.netMode == 1)
                return item;
            for (int i = 0; i < 1000; ++i)
            {
                bool flag1 = false;
                bool flag2 = false;
                if (Main.chest[i] != null && !Chest.IsPlayerInChest(i) && !Chest.isLocked(Main.chest[i].x, Main.chest[i].y) && (double)(new Vector2((float)(Main.chest[i].x * 16 + 16), (float)(Main.chest[i].y * 16 + 16)) - position).Length() < 200.0)
                {
                    for (int index = 0; index < Main.chest[i].item.Length; ++index)
                    {
                        if (Main.chest[i].item[index].type > 0 && Main.chest[i].item[index].stack > 0)
                        {
                            if (item.IsTheSameAs(Main.chest[i].item[index]))
                            {
                                flag1 = true;
                                int num = Main.chest[i].item[index].maxStack - Main.chest[i].item[index].stack;
                                if (num > 0)
                                {
                                    if (num > item.stack)
                                        num = item.stack;
                                    item.stack -= num;
                                    Main.chest[i].item[index].stack += num;
                                    if (item.stack <= 0)
                                    {
                                        item.SetDefaults(0, false);
                                        return item;
                                    }
                                }
                            }
                        }
                        else
                            flag2 = true;
                    }
                    if (flag1 && flag2 && item.stack > 0)
                    {
                        for (int index = 0; index < Main.chest[i].item.Length; ++index)
                        {
                            if (Main.chest[i].item[index].type == 0 || Main.chest[i].item[index].stack == 0)
                            {
                                Main.chest[i].item[index] = item.Clone();
                                item.SetDefaults(0, false);
                                return item;
                            }
                        }
                    }
                }
            }
            return item;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public static bool Unlock(int X, int Y)
        {
            if (Main.tile[X, Y] == null)
                return false;
            short num;
            int Type;
            switch ((int)Main.tile[X, Y].frameX / 36)
            {
                case 2:
                    num = (short)36;
                    Type = 11;
                    AchievementsHelper.NotifyProgressionEvent(19);
                    break;
                case 4:
                    num = (short)36;
                    Type = 11;
                    break;
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                    if (!NPC.downedPlantBoss)
                        return false;
                    num = (short)180;
                    Type = 11;
                    AchievementsHelper.NotifyProgressionEvent(20);
                    break;
                case 36:
                case 38:
                case 40:
                    num = (short)36;
                    Type = 11;
                    break;
                default:
                    return false;
            }
            Main.PlaySound(22, X * 16, Y * 16, 1);
            for (int index1 = X; index1 <= X + 1; ++index1)
            {
                for (int index2 = Y; index2 <= Y + 1; ++index2)
                {
                    Main.tile[index1, index2].frameX -= num;
                    for (int index3 = 0; index3 < 4; ++index3)
                        Dust.NewDust(new Vector2((float)(index1 * 16), (float)(index2 * 16)), 16, 16, Type, 0.0f, 0.0f, 0, new Color(), 1f);
                }
            }
            return true;
        }

        public static int UsingChest(int i)
        {
            if (Main.chest[i] != null)
            {
                for (int index = 0; index < (int)byte.MaxValue; ++index)
                {
                    if (Main.player[index].active && Main.player[index].chest == i)
                        return index;
                }
            }
            return -1;
        }

        public static int FindChest(int X, int Y)
        {
            for (int index = 0; index < 1000; ++index)
            {
                if (Main.chest[index] != null && Main.chest[index].x == X && Main.chest[index].y == Y)
                    return index;
            }
            return -1;
        }

        public static int FindEmptyChest(int x, int y, int type = 21, int style = 0, int direction = 1)
        {
            int num = -1;
            for (int index = 0; index < 1000; ++index)
            {
                Chest chest = Main.chest[index];
                if (chest != null)
                {
                    if (chest.x == x && chest.y == y)
                        return -1;
                }
                else if (num == -1)
                    num = index;
            }
            return num;
        }

        public static bool NearOtherChests(int x, int y)
        {
            for (int i = x - 25; i < x + 25; ++i)
            {
                for (int j = y - 8; j < y + 8; ++j)
                {
                    Tile tileSafely = Framing.GetTileSafely(i, j);
                    if (tileSafely.active() && (int)tileSafely.type == 21)
                        return true;
                }
            }
            return false;
        }

        public static int AfterPlacement_Hook(int x, int y, int type = 21, int style = 0, int direction = 1)
        {
            Point16 baseCoords = new Point16(x, y);
            TileObjectData.OriginToTopLeft(type, style, ref baseCoords);
            int emptyChest = Chest.FindEmptyChest((int)baseCoords.X, (int)baseCoords.Y, 21, 0, 1);
            if (emptyChest == -1)
                return -1;
            if (Main.netMode != 1)
            {
                Chest chest = new Chest(false);
                chest.x = (int)baseCoords.X;
                chest.y = (int)baseCoords.Y;
                for (int index = 0; index < 40; ++index)
                    chest.item[index] = new Item();
                Main.chest[emptyChest] = chest;
            }
            else if (type == 21)
                NetMessage.SendData(34, -1, -1, "", 0, (float)x, (float)y, (float)style, 0, 0, 0);
            else
                NetMessage.SendData(34, -1, -1, "", 2, (float)x, (float)y, (float)style, 0, 0, 0);
            return emptyChest;
        }

        public static int CreateChest(int X, int Y, int id = -1)
        {
            int index1 = id;
            if (index1 == -1)
            {
                index1 = Chest.FindEmptyChest(X, Y, 21, 0, 1);
                if (index1 == -1)
                    return -1;
                if (Main.netMode == 1)
                    return index1;
            }
            Main.chest[index1] = new Chest(false);
            Main.chest[index1].x = X;
            Main.chest[index1].y = Y;
            for (int index2 = 0; index2 < 40; ++index2)
                Main.chest[index1].item[index2] = new Item();
            return index1;
        }

        public static bool CanDestroyChest(int X, int Y)
        {
            for (int index1 = 0; index1 < 1000; ++index1)
            {
                Chest chest = Main.chest[index1];
                if (chest != null && chest.x == X && chest.y == Y)
                {
                    for (int index2 = 0; index2 < 40; ++index2)
                    {
                        if (chest.item[index2] != null && chest.item[index2].type > 0 && chest.item[index2].stack > 0)
                            return false;
                    }
                    return true;
                }
            }
            return true;
        }

        public static bool DestroyChest(int X, int Y)
        {
            for (int index1 = 0; index1 < 1000; ++index1)
            {
                Chest chest = Main.chest[index1];
                if (chest != null && chest.x == X && chest.y == Y)
                {
                    for (int index2 = 0; index2 < 40; ++index2)
                    {
                        if (chest.item[index2] != null && chest.item[index2].type > 0 && chest.item[index2].stack > 0)
                            return false;
                    }
                    Main.chest[index1] = (Chest)null;
                    if (Main.player[Main.myPlayer].chest == index1)
                        Main.player[Main.myPlayer].chest = -1;
                    return true;
                }
            }
            return true;
        }

        public static void DestroyChestDirect(int X, int Y, int id)
        {
            if (id < 0)
                return;
            if (id >= Main.chest.Length)
                return;
            try
            {
                Chest chest = Main.chest[id];
                if (chest == null || chest.x != X || chest.y != Y)
                    return;
                Main.chest[id] = (Chest)null;
                if (Main.player[Main.myPlayer].chest != id)
                    return;
                Main.player[Main.myPlayer].chest = -1;
            }
            catch
            {
            }
        }

        public void AddShop(Item newItem)
        {
            for (int index = 0; index < 39; ++index)
            {
                if (this.item[index] == null || this.item[index].type == 0)
                {
                    this.item[index] = newItem.Clone();
                    this.item[index].favorited = false;
                    this.item[index].buyOnce = true;
                    if (this.item[index].value <= 0)
                        break;
                    this.item[index].value = this.item[index].value / 5;
                    if (this.item[index].value >= 1)
                        break;
                    this.item[index].value = 1;
                    break;
                }
            }
        }

        public static void SetupTravelShop()
        {
            for (int index = 0; index < 40; ++index)
                Main.travelShop[index] = 0;
            int num1 = Main.rand.Next(4, 7);
            if (Main.rand.Next(4) == 0)
                ++num1;
            if (Main.rand.Next(8) == 0)
                ++num1;
            if (Main.rand.Next(16) == 0)
                ++num1;
            if (Main.rand.Next(32) == 0)
                ++num1;
            if (Main.expertMode && Main.rand.Next(2) == 0)
                ++num1;
            int index1 = 0;
            int num2 = 0;
            int[] numArray = new int[6]
      {
        100,
        200,
        300,
        400,
        500,
        600
      };
            while (num2 < num1)
            {
                int num3 = 0;
                if (Main.rand.Next(numArray[4]) == 0)
                    num3 = 3309;
                if (Main.rand.Next(numArray[3]) == 0)
                    num3 = 3314;
                if (Main.rand.Next(numArray[5]) == 0)
                    num3 = 1987;
                if (Main.rand.Next(numArray[4]) == 0 && Main.hardMode)
                    num3 = 2270;
                if (Main.rand.Next(numArray[4]) == 0)
                    num3 = 2278;
                if (Main.rand.Next(numArray[4]) == 0)
                    num3 = 2271;
                if (Main.rand.Next(numArray[3]) == 0 && Main.hardMode && NPC.downedPlantBoss)
                    num3 = 2223;
                if (Main.rand.Next(numArray[3]) == 0)
                    num3 = 2272;
                if (Main.rand.Next(numArray[3]) == 0)
                    num3 = 2219;
                if (Main.rand.Next(numArray[3]) == 0)
                    num3 = 2276;
                if (Main.rand.Next(numArray[3]) == 0)
                    num3 = 2284;
                if (Main.rand.Next(numArray[3]) == 0)
                    num3 = 2285;
                if (Main.rand.Next(numArray[3]) == 0)
                    num3 = 2286;
                if (Main.rand.Next(numArray[3]) == 0)
                    num3 = 2287;
                if (Main.rand.Next(numArray[3]) == 0)
                    num3 = 2296;
                if (Main.rand.Next(numArray[2]) == 0 && WorldGen.shadowOrbSmashed)
                    num3 = 2269;
                if (Main.rand.Next(numArray[2]) == 0)
                    num3 = 2177;
                if (Main.rand.Next(numArray[2]) == 0)
                    num3 = 1988;
                if (Main.rand.Next(numArray[2]) == 0)
                    num3 = 2275;
                if (Main.rand.Next(numArray[2]) == 0)
                    num3 = 2279;
                if (Main.rand.Next(numArray[2]) == 0)
                    num3 = 2277;
                if (Main.rand.Next(numArray[2]) == 0 && NPC.downedBoss1)
                    num3 = 3262;
                if (Main.rand.Next(numArray[2]) == 0 && NPC.downedMechBossAny)
                    num3 = 3284;
                if (Main.rand.Next(numArray[2]) == 0 && Main.hardMode && NPC.downedMoonlord)
                    num3 = 3596;
                if (Main.rand.Next(numArray[2]) == 0 && Main.hardMode && NPC.downedMartians)
                    num3 = 2865;
                if (Main.rand.Next(numArray[2]) == 0 && Main.hardMode && NPC.downedMartians)
                    num3 = 2866;
                if (Main.rand.Next(numArray[2]) == 0 && Main.hardMode && NPC.downedMartians)
                    num3 = 2867;
                if (Main.rand.Next(numArray[2]) == 0 && Main.xMas)
                    num3 = 3055;
                if (Main.rand.Next(numArray[2]) == 0 && Main.xMas)
                    num3 = 3056;
                if (Main.rand.Next(numArray[2]) == 0 && Main.xMas)
                    num3 = 3057;
                if (Main.rand.Next(numArray[2]) == 0 && Main.xMas)
                    num3 = 3058;
                if (Main.rand.Next(numArray[2]) == 0 && Main.xMas)
                    num3 = 3059;
                if (Main.rand.Next(numArray[1]) == 0)
                    num3 = 2214;
                if (Main.rand.Next(numArray[1]) == 0)
                    num3 = 2215;
                if (Main.rand.Next(numArray[1]) == 0)
                    num3 = 2216;
                if (Main.rand.Next(numArray[1]) == 0)
                    num3 = 2217;
                if (Main.rand.Next(numArray[1]) == 0)
                    num3 = 2273;
                if (Main.rand.Next(numArray[1]) == 0)
                    num3 = 2274;
                if (Main.rand.Next(numArray[0]) == 0)
                    num3 = 2266;
                if (Main.rand.Next(numArray[0]) == 0)
                    num3 = 2267;
                if (Main.rand.Next(numArray[0]) == 0)
                    num3 = 2268;
                if (Main.rand.Next(numArray[0]) == 0)
                    num3 = 2281 + Main.rand.Next(3);
                if (Main.rand.Next(numArray[0]) == 0)
                    num3 = 2258;
                if (Main.rand.Next(numArray[0]) == 0)
                    num3 = 2242;
                if (Main.rand.Next(numArray[0]) == 0)
                    num3 = 2260;
                if (Main.rand.Next(numArray[0]) == 0)
                    num3 = 3119;
                if (Main.rand.Next(numArray[0]) == 0)
                    num3 = 3118;
                if (Main.rand.Next(numArray[0]) == 0)
                    num3 = 3099;
                if (num3 != 0)
                {
                    for (int index2 = 0; index2 < 40; ++index2)
                    {
                        if (Main.travelShop[index2] == num3)
                        {
                            num3 = 0;
                            break;
                        }
                    }
                }
                if (num3 != 0)
                {
                    ++num2;
                    Main.travelShop[index1] = num3;
                    ++index1;
                    if (num3 == 2260)
                    {
                        Main.travelShop[index1] = 2261;
                        int index2 = index1 + 1;
                        Main.travelShop[index2] = 2262;
                        index1 = index2 + 1;
                    }
                }
            }
        }

        public void SetupShop(int type)
        {
            for (int index = 0; index < 40; ++index)
                this.item[index] = new Item();
            int index1 = 0;
            if (type == 1)
            {
                this.item[index1].SetDefaults("Mining Helmet");
                int index2 = index1 + 1;
                this.item[index2].SetDefaults("Piggy Bank");
                int index3 = index2 + 1;
                this.item[index3].SetDefaults("Iron Anvil");
                int index4 = index3 + 1;
                this.item[index4].SetDefaults(1991, false);
                int index5 = index4 + 1;
                this.item[index5].SetDefaults("Copper Pickaxe");
                int index6 = index5 + 1;
                this.item[index6].SetDefaults("Copper Axe");
                int index7 = index6 + 1;
                this.item[index7].SetDefaults("Torch");
                int index8 = index7 + 1;
                this.item[index8].SetDefaults("Lesser Healing Potion");
                int index9 = index8 + 1;
                this.item[index9].SetDefaults("Lesser Mana Potion");
                int index10 = index9 + 1;
                this.item[index10].SetDefaults("Wooden Arrow");
                int index11 = index10 + 1;
                this.item[index11].SetDefaults("Shuriken");
                int index12 = index11 + 1;
                this.item[index12].SetDefaults("Rope");
                int index13 = index12 + 1;
                if (Main.player[Main.myPlayer].ZoneSnow)
                {
                    this.item[index13].SetDefaults(967, false);
                    ++index13;
                }
                if (Main.bloodMoon)
                {
                    this.item[index13].SetDefaults("Throwing Knife");
                    ++index13;
                }
                if (!Main.dayTime)
                {
                    this.item[index13].SetDefaults("Glowstick");
                    ++index13;
                }
                if (NPC.downedBoss3)
                {
                    this.item[index13].SetDefaults("Safe");
                    ++index13;
                }
                if (Main.hardMode)
                {
                    this.item[index13].SetDefaults(488, false);
                    ++index13;
                }
                for (int index14 = 0; index14 < 58; ++index14)
                {
                    if (Main.player[Main.myPlayer].inventory[index14].type == 930)
                    {
                        this.item[index13].SetDefaults(931, false);
                        int index15 = index13 + 1;
                        this.item[index15].SetDefaults(1614, false);
                        index13 = index15 + 1;
                        break;
                    }
                }
                this.item[index13].SetDefaults(1786, false);
                index1 = index13 + 1;
                if (Main.hardMode)
                {
                    this.item[index1].SetDefaults(1348, false);
                    ++index1;
                }
                if (Main.player[Main.myPlayer].HasItem(3107))
                {
                    this.item[index1].SetDefaults(3108, false);
                    ++index1;
                }
                if (Main.halloween)
                {
                    Item[] objArray1 = this.item;
                    int index14 = index1;
                    int num1 = 1;
                    int num2 = index14 + num1;
                    objArray1[index14].SetDefaults(3242, false);
                    Item[] objArray2 = this.item;
                    int index15 = num2;
                    int num3 = 1;
                    int num4 = index15 + num3;
                    objArray2[index15].SetDefaults(3243, false);
                    Item[] objArray3 = this.item;
                    int index16 = num4;
                    int num5 = 1;
                    index1 = index16 + num5;
                    objArray3[index16].SetDefaults(3244, false);
                }
            }
            else if (type == 2)
            {
                this.item[index1].SetDefaults("Musket Ball");
                int index2 = index1 + 1;
                if (Main.bloodMoon || Main.hardMode)
                {
                    this.item[index2].SetDefaults("Silver Bullet");
                    ++index2;
                }
                if (NPC.downedBoss2 && !Main.dayTime || Main.hardMode)
                {
                    this.item[index2].SetDefaults(47, false);
                    ++index2;
                }
                this.item[index2].SetDefaults("Flintlock Pistol");
                int index3 = index2 + 1;
                this.item[index3].SetDefaults("Minishark");
                index1 = index3 + 1;
                if (!Main.dayTime)
                {
                    this.item[index1].SetDefaults(324, false);
                    ++index1;
                }
                if (Main.hardMode)
                {
                    this.item[index1].SetDefaults(534, false);
                    ++index1;
                }
                if (Main.hardMode)
                {
                    this.item[index1].SetDefaults(1432, false);
                    ++index1;
                }
                if (Main.player[Main.myPlayer].HasItem(1258))
                {
                    this.item[index1].SetDefaults(1261, false);
                    ++index1;
                }
                if (Main.player[Main.myPlayer].HasItem(1835))
                {
                    this.item[index1].SetDefaults(1836, false);
                    ++index1;
                }
                if (Main.player[Main.myPlayer].HasItem(3107))
                {
                    this.item[index1].SetDefaults(3108, false);
                    ++index1;
                }
                if (Main.player[Main.myPlayer].HasItem(1782))
                {
                    this.item[index1].SetDefaults(1783, false);
                    ++index1;
                }
                if (Main.player[Main.myPlayer].HasItem(1784))
                {
                    this.item[index1].SetDefaults(1785, false);
                    ++index1;
                }
                if (Main.halloween)
                {
                    this.item[index1].SetDefaults(1736, false);
                    int index4 = index1 + 1;
                    this.item[index4].SetDefaults(1737, false);
                    int index5 = index4 + 1;
                    this.item[index5].SetDefaults(1738, false);
                    index1 = index5 + 1;
                }
            }
            else if (type == 3)
            {
                int index2;
                if (Main.bloodMoon)
                {
                    if (WorldGen.crimson)
                    {
                        this.item[index1].SetDefaults(2171, false);
                        index2 = index1 + 1;
                    }
                    else
                    {
                        this.item[index1].SetDefaults(67, false);
                        int index3 = index1 + 1;
                        this.item[index3].SetDefaults(59, false);
                        index2 = index3 + 1;
                    }
                }
                else
                {
                    this.item[index1].SetDefaults("Purification Powder");
                    int index3 = index1 + 1;
                    this.item[index3].SetDefaults("Grass Seeds");
                    int index4 = index3 + 1;
                    this.item[index4].SetDefaults("Sunflower");
                    index2 = index4 + 1;
                }
                this.item[index2].SetDefaults("Acorn");
                int index5 = index2 + 1;
                this.item[index5].SetDefaults(114, false);
                int index6 = index5 + 1;
                this.item[index6].SetDefaults(1828, false);
                int index7 = index6 + 1;
                this.item[index7].SetDefaults(745, false);
                int index8 = index7 + 1;
                this.item[index8].SetDefaults(747, false);
                index1 = index8 + 1;
                if (Main.hardMode)
                {
                    this.item[index1].SetDefaults(746, false);
                    ++index1;
                }
                if (Main.hardMode)
                {
                    this.item[index1].SetDefaults(369, false);
                    ++index1;
                }
                if (Main.shroomTiles > 50)
                {
                    this.item[index1].SetDefaults(194, false);
                    ++index1;
                }
                if (Main.halloween)
                {
                    this.item[index1].SetDefaults(1853, false);
                    int index3 = index1 + 1;
                    this.item[index3].SetDefaults(1854, false);
                    index1 = index3 + 1;
                }
                if (NPC.downedSlimeKing)
                {
                    this.item[index1].SetDefaults(3215, false);
                    ++index1;
                }
                if (NPC.downedQueenBee)
                {
                    this.item[index1].SetDefaults(3216, false);
                    ++index1;
                }
                if (NPC.downedBoss1)
                {
                    this.item[index1].SetDefaults(3219, false);
                    ++index1;
                }
                if (NPC.downedBoss2)
                {
                    if (WorldGen.crimson)
                    {
                        this.item[index1].SetDefaults(3218, false);
                        ++index1;
                    }
                    else
                    {
                        this.item[index1].SetDefaults(3217, false);
                        ++index1;
                    }
                }
                if (NPC.downedBoss3)
                {
                    this.item[index1].SetDefaults(3220, false);
                    int index3 = index1 + 1;
                    this.item[index3].SetDefaults(3221, false);
                    index1 = index3 + 1;
                }
                if (Main.hardMode)
                {
                    this.item[index1].SetDefaults(3222, false);
                    ++index1;
                }
            }
            else if (type == 4)
            {
                this.item[index1].SetDefaults("Grenade");
                int index2 = index1 + 1;
                this.item[index2].SetDefaults("Bomb");
                int index3 = index2 + 1;
                this.item[index3].SetDefaults("Dynamite");
                index1 = index3 + 1;
                if (Main.hardMode)
                {
                    this.item[index1].SetDefaults("Hellfire Arrow");
                    ++index1;
                }
                if (Main.hardMode && NPC.downedPlantBoss && NPC.downedPirates)
                {
                    this.item[index1].SetDefaults(937, false);
                    ++index1;
                }
                if (Main.hardMode)
                {
                    this.item[index1].SetDefaults(1347, false);
                    ++index1;
                }
            }
            else if (type == 5)
            {
                this.item[index1].SetDefaults(254, false);
                int index2 = index1 + 1;
                this.item[index2].SetDefaults(981, false);
                int index3 = index2 + 1;
                if (Main.dayTime)
                {
                    this.item[index3].SetDefaults(242, false);
                    ++index3;
                }
                if (Main.moonPhase == 0)
                {
                    this.item[index3].SetDefaults(245, false);
                    int index4 = index3 + 1;
                    this.item[index4].SetDefaults(246, false);
                    index3 = index4 + 1;
                    if (!Main.dayTime)
                    {
                        Item[] objArray1 = this.item;
                        int index5 = index3;
                        int num1 = 1;
                        int num2 = index5 + num1;
                        objArray1[index5].SetDefaults(1288, false);
                        Item[] objArray2 = this.item;
                        int index6 = num2;
                        int num3 = 1;
                        index3 = index6 + num3;
                        objArray2[index6].SetDefaults(1289, false);
                    }
                }
                else if (Main.moonPhase == 1)
                {
                    this.item[index3].SetDefaults(325, false);
                    int index4 = index3 + 1;
                    this.item[index4].SetDefaults(326, false);
                    index3 = index4 + 1;
                }
                this.item[index3].SetDefaults(269, false);
                int index7 = index3 + 1;
                this.item[index7].SetDefaults(270, false);
                int index8 = index7 + 1;
                this.item[index8].SetDefaults(271, false);
                index1 = index8 + 1;
                if (NPC.downedClown)
                {
                    this.item[index1].SetDefaults(503, false);
                    int index4 = index1 + 1;
                    this.item[index4].SetDefaults(504, false);
                    int index5 = index4 + 1;
                    this.item[index5].SetDefaults(505, false);
                    index1 = index5 + 1;
                }
                if (Main.bloodMoon)
                {
                    this.item[index1].SetDefaults(322, false);
                    ++index1;
                    if (!Main.dayTime)
                    {
                        Item[] objArray1 = this.item;
                        int index4 = index1;
                        int num1 = 1;
                        int num2 = index4 + num1;
                        objArray1[index4].SetDefaults(3362, false);
                        Item[] objArray2 = this.item;
                        int index5 = num2;
                        int num3 = 1;
                        index1 = index5 + num3;
                        objArray2[index5].SetDefaults(3363, false);
                    }
                }
                if (NPC.downedAncientCultist)
                {
                    if (Main.dayTime)
                    {
                        Item[] objArray1 = this.item;
                        int index4 = index1;
                        int num1 = 1;
                        int num2 = index4 + num1;
                        objArray1[index4].SetDefaults(2856, false);
                        Item[] objArray2 = this.item;
                        int index5 = num2;
                        int num3 = 1;
                        index1 = index5 + num3;
                        objArray2[index5].SetDefaults(2858, false);
                    }
                    else
                    {
                        Item[] objArray1 = this.item;
                        int index4 = index1;
                        int num1 = 1;
                        int num2 = index4 + num1;
                        objArray1[index4].SetDefaults(2857, false);
                        Item[] objArray2 = this.item;
                        int index5 = num2;
                        int num3 = 1;
                        index1 = index5 + num3;
                        objArray2[index5].SetDefaults(2859, false);
                    }
                }
                if (NPC.AnyNPCs(441))
                {
                    Item[] objArray1 = this.item;
                    int index4 = index1;
                    int num1 = 1;
                    int num2 = index4 + num1;
                    objArray1[index4].SetDefaults(3242, false);
                    Item[] objArray2 = this.item;
                    int index5 = num2;
                    int num3 = 1;
                    int num4 = index5 + num3;
                    objArray2[index5].SetDefaults(3243, false);
                    Item[] objArray3 = this.item;
                    int index6 = num4;
                    int num5 = 1;
                    index1 = index6 + num5;
                    objArray3[index6].SetDefaults(3244, false);
                }
                if (Main.player[Main.myPlayer].ZoneSnow)
                {
                    this.item[index1].SetDefaults(1429, false);
                    ++index1;
                }
                if (Main.halloween)
                {
                    this.item[index1].SetDefaults(1740, false);
                    ++index1;
                }
                if (Main.hardMode)
                {
                    if (Main.moonPhase == 2)
                    {
                        this.item[index1].SetDefaults(869, false);
                        ++index1;
                    }
                    if (Main.moonPhase == 4)
                    {
                        this.item[index1].SetDefaults(864, false);
                        int index4 = index1 + 1;
                        this.item[index4].SetDefaults(865, false);
                        index1 = index4 + 1;
                    }
                    if (Main.moonPhase == 6)
                    {
                        this.item[index1].SetDefaults(873, false);
                        int index4 = index1 + 1;
                        this.item[index4].SetDefaults(874, false);
                        int index5 = index4 + 1;
                        this.item[index5].SetDefaults(875, false);
                        index1 = index5 + 1;
                    }
                }
                if (NPC.downedFrost)
                {
                    this.item[index1].SetDefaults(1275, false);
                    int index4 = index1 + 1;
                    this.item[index4].SetDefaults(1276, false);
                    index1 = index4 + 1;
                }
                if (Main.halloween)
                {
                    Item[] objArray1 = this.item;
                    int index4 = index1;
                    int num1 = 1;
                    int num2 = index4 + num1;
                    objArray1[index4].SetDefaults(3246, false);
                    Item[] objArray2 = this.item;
                    int index5 = num2;
                    int num3 = 1;
                    index1 = index5 + num3;
                    objArray2[index5].SetDefaults(3247, false);
                }
            }
            else if (type == 6)
            {
                this.item[index1].SetDefaults(128, false);
                int index2 = index1 + 1;
                this.item[index2].SetDefaults(486, false);
                int index3 = index2 + 1;
                this.item[index3].SetDefaults(398, false);
                int index4 = index3 + 1;
                this.item[index4].SetDefaults(84, false);
                int index5 = index4 + 1;
                this.item[index5].SetDefaults(407, false);
                int index6 = index5 + 1;
                this.item[index6].SetDefaults(161, false);
                index1 = index6 + 1;
            }
            else if (type == 7)
            {
                this.item[index1].SetDefaults(487, false);
                int index2 = index1 + 1;
                this.item[index2].SetDefaults(496, false);
                int index3 = index2 + 1;
                this.item[index3].SetDefaults(500, false);
                int index4 = index3 + 1;
                this.item[index4].SetDefaults(507, false);
                int index5 = index4 + 1;
                this.item[index5].SetDefaults(508, false);
                int index6 = index5 + 1;
                this.item[index6].SetDefaults(531, false);
                int index7 = index6 + 1;
                this.item[index7].SetDefaults(576, false);
                int index8 = index7 + 1;
                this.item[index8].SetDefaults(3186, false);
                index1 = index8 + 1;
                if (Main.halloween)
                {
                    this.item[index1].SetDefaults(1739, false);
                    ++index1;
                }
            }
            else if (type == 8)
            {
                this.item[index1].SetDefaults(509, false);
                int index2 = index1 + 1;
                this.item[index2].SetDefaults(850, false);
                int index3 = index2 + 1;
                this.item[index3].SetDefaults(851, false);
                int index4 = index3 + 1;
                this.item[index4].SetDefaults(510, false);
                int index5 = index4 + 1;
                this.item[index5].SetDefaults(530, false);
                int index6 = index5 + 1;
                this.item[index6].SetDefaults(513, false);
                int index7 = index6 + 1;
                this.item[index7].SetDefaults(538, false);
                int index8 = index7 + 1;
                this.item[index8].SetDefaults(529, false);
                int index9 = index8 + 1;
                this.item[index9].SetDefaults(541, false);
                int index10 = index9 + 1;
                this.item[index10].SetDefaults(542, false);
                int index11 = index10 + 1;
                this.item[index11].SetDefaults(543, false);
                int index12 = index11 + 1;
                this.item[index12].SetDefaults(852, false);
                int index13 = index12 + 1;
                this.item[index13].SetDefaults(853, false);
                int index14 = index13 + 1;
                this.item[index14].SetDefaults(2739, false);
                int index15 = index14 + 1;
                this.item[index15].SetDefaults(849, false);
                int num1 = index15 + 1;
                Item[] objArray = this.item;
                int index16 = num1;
                int num2 = 1;
                index1 = index16 + num2;
                objArray[index16].SetDefaults(2799, false);
                if (NPC.AnyNPCs(369) && Main.hardMode && Main.moonPhase == 3)
                {
                    this.item[index1].SetDefaults(2295, false);
                    ++index1;
                }
            }
            else if (type == 9)
            {
                this.item[index1].SetDefaults(588, false);
                int index2 = index1 + 1;
                this.item[index2].SetDefaults(589, false);
                int index3 = index2 + 1;
                this.item[index3].SetDefaults(590, false);
                int index4 = index3 + 1;
                this.item[index4].SetDefaults(597, false);
                int index5 = index4 + 1;
                this.item[index5].SetDefaults(598, false);
                int index6 = index5 + 1;
                this.item[index6].SetDefaults(596, false);
                index1 = index6 + 1;
                for (int Type = 1873; Type < 1906; ++Type)
                {
                    this.item[index1].SetDefaults(Type, false);
                    ++index1;
                }
            }
            else if (type == 10)
            {
                if (NPC.downedMechBossAny)
                {
                    this.item[index1].SetDefaults(756, false);
                    int index2 = index1 + 1;
                    this.item[index2].SetDefaults(787, false);
                    index1 = index2 + 1;
                }
                this.item[index1].SetDefaults(868, false);
                int index3 = index1 + 1;
                if (NPC.downedPlantBoss)
                {
                    this.item[index3].SetDefaults(1551, false);
                    ++index3;
                }
                this.item[index3].SetDefaults(1181, false);
                int index4 = index3 + 1;
                this.item[index4].SetDefaults(783, false);
                index1 = index4 + 1;
            }
            else if (type == 11)
            {
                this.item[index1].SetDefaults(779, false);
                int index2 = index1 + 1;
                int index3;
                if (Main.moonPhase >= 4)
                {
                    this.item[index2].SetDefaults(748, false);
                    index3 = index2 + 1;
                }
                else
                {
                    this.item[index2].SetDefaults(839, false);
                    int index4 = index2 + 1;
                    this.item[index4].SetDefaults(840, false);
                    int index5 = index4 + 1;
                    this.item[index5].SetDefaults(841, false);
                    index3 = index5 + 1;
                }
                if (NPC.downedGolemBoss)
                {
                    this.item[index3].SetDefaults(948, false);
                    ++index3;
                }
                this.item[index3].SetDefaults(995, false);
                int index6 = index3 + 1;
                if (NPC.downedBoss1 && NPC.downedBoss2 && NPC.downedBoss3)
                {
                    this.item[index6].SetDefaults(2203, false);
                    ++index6;
                }
                if (WorldGen.crimson)
                {
                    this.item[index6].SetDefaults(2193, false);
                    ++index6;
                }
                this.item[index6].SetDefaults(1263, false);
                int index7 = index6 + 1;
                if (Main.eclipse || Main.bloodMoon)
                {
                    if (WorldGen.crimson)
                    {
                        this.item[index7].SetDefaults(784, false);
                        index1 = index7 + 1;
                    }
                    else
                    {
                        this.item[index7].SetDefaults(782, false);
                        index1 = index7 + 1;
                    }
                }
                else if (Main.player[Main.myPlayer].ZoneHoly)
                {
                    this.item[index7].SetDefaults(781, false);
                    index1 = index7 + 1;
                }
                else
                {
                    this.item[index7].SetDefaults(780, false);
                    index1 = index7 + 1;
                }
                if (Main.hardMode)
                {
                    this.item[index1].SetDefaults(1344, false);
                    ++index1;
                }
                if (Main.halloween)
                {
                    this.item[index1].SetDefaults(1742, false);
                    ++index1;
                }
            }
            else if (type == 12)
            {
                this.item[index1].SetDefaults(1037, false);
                int index2 = index1 + 1;
                this.item[index2].SetDefaults(2874, false);
                int index3 = index2 + 1;
                this.item[index3].SetDefaults(1120, false);
                index1 = index3 + 1;
                if (Main.netMode == 1)
                {
                    this.item[index1].SetDefaults(1969, false);
                    ++index1;
                }
                if (Main.halloween)
                {
                    this.item[index1].SetDefaults(3248, false);
                    int index4 = index1 + 1;
                    this.item[index4].SetDefaults(1741, false);
                    index1 = index4 + 1;
                }
                if (Main.moonPhase == 0)
                {
                    this.item[index1].SetDefaults(2871, false);
                    int index4 = index1 + 1;
                    this.item[index4].SetDefaults(2872, false);
                    index1 = index4 + 1;
                }
            }
            else if (type == 13)
            {
                this.item[index1].SetDefaults(859, false);
                int index2 = index1 + 1;
                this.item[index2].SetDefaults(1000, false);
                int index3 = index2 + 1;
                this.item[index3].SetDefaults(1168, false);
                int index4 = index3 + 1;
                this.item[index4].SetDefaults(1449, false);
                int index5 = index4 + 1;
                this.item[index5].SetDefaults(1345, false);
                int index6 = index5 + 1;
                this.item[index6].SetDefaults(1450, false);
                int num1 = index6 + 1;
                Item[] objArray1 = this.item;
                int index7 = num1;
                int num2 = 1;
                int num3 = index7 + num2;
                objArray1[index7].SetDefaults(3253, false);
                Item[] objArray2 = this.item;
                int index8 = num3;
                int num4 = 1;
                int num5 = index8 + num4;
                objArray2[index8].SetDefaults(2700, false);
                Item[] objArray3 = this.item;
                int index9 = num5;
                int num6 = 1;
                index1 = index9 + num6;
                objArray3[index9].SetDefaults(2738, false);
                if (Main.player[Main.myPlayer].HasItem(3548))
                {
                    this.item[index1].SetDefaults(3548, false);
                    ++index1;
                }
                if (NPC.AnyNPCs(229))
                    this.item[index1++].SetDefaults(3369, false);
                if (Main.hardMode)
                {
                    this.item[index1].SetDefaults(3214, false);
                    int index10 = index1 + 1;
                    this.item[index10].SetDefaults(2868, false);
                    int index11 = index10 + 1;
                    this.item[index11].SetDefaults(970, false);
                    int index12 = index11 + 1;
                    this.item[index12].SetDefaults(971, false);
                    int index13 = index12 + 1;
                    this.item[index13].SetDefaults(972, false);
                    int index14 = index13 + 1;
                    this.item[index14].SetDefaults(973, false);
                    index1 = index14 + 1;
                }
            }
            else if (type == 14)
            {
                this.item[index1].SetDefaults(771, false);
                ++index1;
                if (Main.bloodMoon)
                {
                    this.item[index1].SetDefaults(772, false);
                    ++index1;
                }
                if (!Main.dayTime || Main.eclipse)
                {
                    this.item[index1].SetDefaults(773, false);
                    ++index1;
                }
                if (Main.eclipse)
                {
                    this.item[index1].SetDefaults(774, false);
                    ++index1;
                }
                if (Main.hardMode)
                {
                    this.item[index1].SetDefaults(760, false);
                    ++index1;
                }
                if (Main.hardMode)
                {
                    this.item[index1].SetDefaults(1346, false);
                    ++index1;
                }
                if (Main.halloween)
                {
                    this.item[index1].SetDefaults(1743, false);
                    int index2 = index1 + 1;
                    this.item[index2].SetDefaults(1744, false);
                    int index3 = index2 + 1;
                    this.item[index3].SetDefaults(1745, false);
                    index1 = index3 + 1;
                }
                if (NPC.downedMartians)
                {
                    Item[] objArray1 = this.item;
                    int index2 = index1;
                    int num1 = 1;
                    int num2 = index2 + num1;
                    objArray1[index2].SetDefaults(2862, false);
                    Item[] objArray2 = this.item;
                    int index3 = num2;
                    int num3 = 1;
                    index1 = index3 + num3;
                    objArray2[index3].SetDefaults(3109, false);
                }
            }
            else if (type == 15)
            {
                this.item[index1].SetDefaults(1071, false);
                int index2 = index1 + 1;
                this.item[index2].SetDefaults(1072, false);
                int index3 = index2 + 1;
                this.item[index3].SetDefaults(1100, false);
                int index4 = index3 + 1;
                for (int Type = 1073; Type <= 1084; ++Type)
                {
                    this.item[index4].SetDefaults(Type, false);
                    ++index4;
                }
                this.item[index4].SetDefaults(1097, false);
                int index5 = index4 + 1;
                this.item[index5].SetDefaults(1099, false);
                int index6 = index5 + 1;
                this.item[index6].SetDefaults(1098, false);
                int index7 = index6 + 1;
                this.item[index7].SetDefaults(1966, false);
                int index8 = index7 + 1;
                if (Main.hardMode)
                {
                    this.item[index8].SetDefaults(1967, false);
                    int index9 = index8 + 1;
                    this.item[index9].SetDefaults(1968, false);
                    index8 = index9 + 1;
                }
                this.item[index8].SetDefaults(1490, false);
                int index10 = index8 + 1;
                if (Main.moonPhase <= 1)
                {
                    this.item[index10].SetDefaults(1481, false);
                    index1 = index10 + 1;
                }
                else if (Main.moonPhase <= 3)
                {
                    this.item[index10].SetDefaults(1482, false);
                    index1 = index10 + 1;
                }
                else if (Main.moonPhase <= 5)
                {
                    this.item[index10].SetDefaults(1483, false);
                    index1 = index10 + 1;
                }
                else
                {
                    this.item[index10].SetDefaults(1484, false);
                    index1 = index10 + 1;
                }
                if (Main.player[Main.myPlayer].ZoneCrimson)
                {
                    this.item[index1].SetDefaults(1492, false);
                    ++index1;
                }
                if (Main.player[Main.myPlayer].ZoneCorrupt)
                {
                    this.item[index1].SetDefaults(1488, false);
                    ++index1;
                }
                if (Main.player[Main.myPlayer].ZoneHoly)
                {
                    this.item[index1].SetDefaults(1489, false);
                    ++index1;
                }
                if (Main.player[Main.myPlayer].ZoneJungle)
                {
                    this.item[index1].SetDefaults(1486, false);
                    ++index1;
                }
                if (Main.player[Main.myPlayer].ZoneSnow)
                {
                    this.item[index1].SetDefaults(1487, false);
                    ++index1;
                }
                if (Main.sandTiles > 1000)
                {
                    this.item[index1].SetDefaults(1491, false);
                    ++index1;
                }
                if (Main.bloodMoon)
                {
                    this.item[index1].SetDefaults(1493, false);
                    ++index1;
                }
                if ((double)Main.player[Main.myPlayer].position.Y / 16.0 < Main.worldSurface * 0.349999994039536)
                {
                    this.item[index1].SetDefaults(1485, false);
                    ++index1;
                }
                if ((double)Main.player[Main.myPlayer].position.Y / 16.0 < Main.worldSurface * 0.349999994039536 && Main.hardMode)
                {
                    this.item[index1].SetDefaults(1494, false);
                    ++index1;
                }
                if (Main.xMas)
                {
                    for (int Type = 1948; Type <= 1957; ++Type)
                    {
                        this.item[index1].SetDefaults(Type, false);
                        ++index1;
                    }
                }
                for (int Type = 2158; Type <= 2160; ++Type)
                {
                    if (index1 < 39)
                        this.item[index1].SetDefaults(Type, false);
                    ++index1;
                }
                for (int Type = 2008; Type <= 2014; ++Type)
                {
                    if (index1 < 39)
                        this.item[index1].SetDefaults(Type, false);
                    ++index1;
                }
            }
            else if (type == 16)
            {
                this.item[index1].SetDefaults(1430, false);
                int index2 = index1 + 1;
                this.item[index2].SetDefaults(986, false);
                int index3 = index2 + 1;
                if (NPC.AnyNPCs(108))
                    this.item[index3++].SetDefaults(2999, false);
                if (Main.hardMode && NPC.downedPlantBoss)
                {
                    if (Main.player[Main.myPlayer].HasItem(1157))
                    {
                        this.item[index3].SetDefaults(1159, false);
                        int index4 = index3 + 1;
                        this.item[index4].SetDefaults(1160, false);
                        int index5 = index4 + 1;
                        this.item[index5].SetDefaults(1161, false);
                        index3 = index5 + 1;
                        if (!Main.dayTime)
                        {
                            this.item[index3].SetDefaults(1158, false);
                            ++index3;
                        }
                        if (Main.player[Main.myPlayer].ZoneJungle)
                        {
                            this.item[index3].SetDefaults(1167, false);
                            ++index3;
                        }
                    }
                    this.item[index3].SetDefaults(1339, false);
                    ++index3;
                }
                if (Main.hardMode && Main.player[Main.myPlayer].ZoneJungle)
                {
                    this.item[index3].SetDefaults(1171, false);
                    ++index3;
                    if (!Main.dayTime)
                    {
                        this.item[index3].SetDefaults(1162, false);
                        ++index3;
                    }
                }
                this.item[index3].SetDefaults(909, false);
                int index6 = index3 + 1;
                this.item[index6].SetDefaults(910, false);
                int index7 = index6 + 1;
                this.item[index7].SetDefaults(940, false);
                int index8 = index7 + 1;
                this.item[index8].SetDefaults(941, false);
                int index9 = index8 + 1;
                this.item[index9].SetDefaults(942, false);
                int index10 = index9 + 1;
                this.item[index10].SetDefaults(943, false);
                int index11 = index10 + 1;
                this.item[index11].SetDefaults(944, false);
                int index12 = index11 + 1;
                this.item[index12].SetDefaults(945, false);
                index1 = index12 + 1;
                if (Main.player[Main.myPlayer].HasItem(1835))
                {
                    this.item[index1].SetDefaults(1836, false);
                    ++index1;
                }
                if (Main.player[Main.myPlayer].HasItem(1258))
                {
                    this.item[index1].SetDefaults(1261, false);
                    ++index1;
                }
                if (Main.halloween)
                {
                    this.item[index1].SetDefaults(1791, false);
                    ++index1;
                }
            }
            else if (type == 17)
            {
                this.item[index1].SetDefaults(928, false);
                int index2 = index1 + 1;
                this.item[index2].SetDefaults(929, false);
                int index3 = index2 + 1;
                this.item[index3].SetDefaults(876, false);
                int index4 = index3 + 1;
                this.item[index4].SetDefaults(877, false);
                int index5 = index4 + 1;
                this.item[index5].SetDefaults(878, false);
                int index6 = index5 + 1;
                this.item[index6].SetDefaults(2434, false);
                index1 = index6 + 1;
                int num = (int)(((double)Main.screenPosition.X + (double)(Main.screenWidth / 2)) / 16.0);
                if ((double)Main.screenPosition.Y / 16.0 < Main.worldSurface + 10.0 && (num < 380 || num > Main.maxTilesX - 380))
                {
                    this.item[index1].SetDefaults(1180, false);
                    ++index1;
                }
                if (Main.hardMode && NPC.downedMechBossAny && NPC.AnyNPCs(208))
                {
                    this.item[index1].SetDefaults(1337, false);
                    ++index1;
                }
            }
            else if (type == 18)
            {
                this.item[index1].SetDefaults(1990, false);
                int index2 = index1 + 1;
                this.item[index2].SetDefaults(1979, false);
                index1 = index2 + 1;
                if (Main.player[Main.myPlayer].statLifeMax >= 400)
                {
                    this.item[index1].SetDefaults(1977, false);
                    ++index1;
                }
                if (Main.player[Main.myPlayer].statManaMax >= 200)
                {
                    this.item[index1].SetDefaults(1978, false);
                    ++index1;
                }
                long num = 0L;
                for (int index3 = 0; index3 < 54; ++index3)
                {
                    if (Main.player[Main.myPlayer].inventory[index3].type == 71)
                        num += (long)Main.player[Main.myPlayer].inventory[index3].stack;
                    if (Main.player[Main.myPlayer].inventory[index3].type == 72)
                        num += (long)(Main.player[Main.myPlayer].inventory[index3].stack * 100);
                    if (Main.player[Main.myPlayer].inventory[index3].type == 73)
                        num += (long)(Main.player[Main.myPlayer].inventory[index3].stack * 10000);
                    if (Main.player[Main.myPlayer].inventory[index3].type == 74)
                        num += (long)(Main.player[Main.myPlayer].inventory[index3].stack * 1000000);
                }
                if (num >= 1000000L)
                {
                    this.item[index1].SetDefaults(1980, false);
                    ++index1;
                }
                if (Main.moonPhase % 2 == 0 && Main.dayTime || Main.moonPhase % 2 == 1 && !Main.dayTime)
                {
                    this.item[index1].SetDefaults(1981, false);
                    ++index1;
                }
                if (Main.player[Main.myPlayer].team != 0)
                {
                    this.item[index1].SetDefaults(1982, false);
                    ++index1;
                }
                if (Main.hardMode)
                {
                    this.item[index1].SetDefaults(1983, false);
                    ++index1;
                }
                if (NPC.AnyNPCs(208))
                {
                    this.item[index1].SetDefaults(1984, false);
                    ++index1;
                }
                if (Main.hardMode && NPC.downedMechBoss1 && (NPC.downedMechBoss2 && NPC.downedMechBoss3))
                {
                    this.item[index1].SetDefaults(1985, false);
                    ++index1;
                }
                if (Main.hardMode && NPC.downedMechBossAny)
                {
                    this.item[index1].SetDefaults(1986, false);
                    ++index1;
                }
                if (Main.hardMode && NPC.downedMartians)
                {
                    this.item[index1].SetDefaults(2863, false);
                    int index3 = index1 + 1;
                    this.item[index3].SetDefaults(3259, false);
                    index1 = index3 + 1;
                }
            }
            else if (type == 19)
            {
                for (int index2 = 0; index2 < 40; ++index2)
                {
                    if (Main.travelShop[index2] != 0)
                    {
                        this.item[index1].netDefaults(Main.travelShop[index2]);
                        ++index1;
                    }
                }
            }
            else if (type == 20)
            {
                if (Main.moonPhase % 2 == 0)
                    this.item[index1].SetDefaults(3001, false);
                else
                    this.item[index1].SetDefaults(28, false);
                int index2 = index1 + 1;
                if (!Main.dayTime || Main.moonPhase == 0)
                    this.item[index2].SetDefaults(3002, false);
                else
                    this.item[index2].SetDefaults(282, false);
                int index3 = index2 + 1;
                if (Main.time % 60.0 * 60.0 * 6.0 <= 10800.0)
                    this.item[index3].SetDefaults(3004, false);
                else
                    this.item[index3].SetDefaults(8, false);
                int index4 = index3 + 1;
                if (Main.moonPhase == 0 || Main.moonPhase == 1 || (Main.moonPhase == 4 || Main.moonPhase == 5))
                    this.item[index4].SetDefaults(3003, false);
                else
                    this.item[index4].SetDefaults(40, false);
                int index5 = index4 + 1;
                if (Main.moonPhase % 4 == 0)
                    this.item[index5].SetDefaults(3310, false);
                else if (Main.moonPhase % 4 == 1)
                    this.item[index5].SetDefaults(3313, false);
                else if (Main.moonPhase % 4 == 2)
                    this.item[index5].SetDefaults(3312, false);
                else
                    this.item[index5].SetDefaults(3311, false);
                int index6 = index5 + 1;
                this.item[index6].SetDefaults(166, false);
                int index7 = index6 + 1;
                this.item[index7].SetDefaults(965, false);
                index1 = index7 + 1;
                if (Main.hardMode)
                {
                    if (Main.moonPhase < 4)
                        this.item[index1].SetDefaults(3316, false);
                    else
                        this.item[index1].SetDefaults(3315, false);
                    int index8 = index1 + 1;
                    this.item[index8].SetDefaults(3334, false);
                    index1 = index8 + 1;
                    if (Main.bloodMoon)
                    {
                        this.item[index1].SetDefaults(3258, false);
                        ++index1;
                    }
                }
                if (Main.moonPhase == 0 && !Main.dayTime)
                {
                    this.item[index1].SetDefaults(3043, false);
                    ++index1;
                }
            }
            if (!Main.player[Main.myPlayer].discount)
                return;
            for (int index2 = 0; index2 < index1; ++index2)
                this.item[index2].value = (int)((double)this.item[index2].value * 0.800000011920929);
        }

        public static void UpdateChestFrames()
        {
            bool[] flagArray = new bool[1000];
            for (int index = 0; index < (int)byte.MaxValue; ++index)
            {
                if (Main.player[index].active && Main.player[index].chest >= 0 && Main.player[index].chest < 1000)
                    flagArray[Main.player[index].chest] = true;
            }
            for (int index = 0; index < 1000; ++index)
            {
                Chest chest = Main.chest[index];
                if (chest != null)
                {
                    if (flagArray[index])
                        ++chest.frameCounter;
                    else
                        --chest.frameCounter;
                    if (chest.frameCounter < 0)
                        chest.frameCounter = 0;
                    if (chest.frameCounter > 10)
                        chest.frameCounter = 10;
                    chest.frame = chest.frameCounter != 0 ? (chest.frameCounter != 10 ? 1 : 2) : 0;
                }
            }
        }
    }
}
