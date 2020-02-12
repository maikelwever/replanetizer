using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using RatchetEdit.Models;
using RatchetEdit.Headers;
using RatchetEdit.LevelObjects;
using RatchetEdit.Models.Animations;
using static RatchetEdit.DataFunctions;

namespace RatchetEdit.Parsers
{
    public class RatchetFileParser
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        protected FileStream fileStream;
        public Decoder decoder;


        protected RatchetFileParser(Decoder decoder, string filePath)
        {
            this.decoder = decoder;
            try
            {
                fileStream = File.OpenRead(filePath);
            }
            catch (Exception e)
            {
                Logger.Info(e);
                Logger.Info("Couldn't load engine file.");
                Application.Exit();
                return;
            }
        }

        protected List<Model> GetMobyModels(int mobyModelPointer)
        {
            //Get the moby count from the start of the section
            int mobyModelCount = decoder.Int(ReadBlock(fileStream, mobyModelPointer, 4), 0);

            Logger.Info("Parsing {0} moby models from address {1}...", mobyModelCount, mobyModelPointer);
            List<Model> mobyModels = new List<Model>(mobyModelCount);

            //Each moby is stored as a [MobyID, offset] pair
            byte[] mobyIDBlock = ReadBlock(fileStream, mobyModelPointer + 4, mobyModelCount * 8);
            for (int i = 0; i < mobyModelCount; i++)
            {
                short modelID = decoder.Short(mobyIDBlock, (i * 8) + 2);
                int offset = decoder.Int(mobyIDBlock, (i * 8) + 4);
                mobyModels.Add(new MobyModel(decoder, fileStream, modelID, offset));
            }
            Logger.Info("Added {0} moby models", mobyModels.Count);
            return mobyModels;
        }

        protected List<Model> GetTieModels(int tieModelPointer, int tieModelCount)
        {
            Logger.Info("Parsing {0} tie models from address {1}...", tieModelCount, tieModelPointer);
            List<Model> tieModelList = new List<Model>(tieModelCount);

            //Read the whole header block, and add models based on the count
            byte[] levelBlock = ReadBlock(fileStream, tieModelPointer, tieModelCount * 0x40);
            for (int i = 0; i < tieModelCount; i++)
            {
                tieModelList.Add(new TieModel(decoder, fileStream, levelBlock, i));
            }

            Logger.Info("Added {0} tie models", tieModelList.Count);
            return tieModelList;
        }

        protected List<Model> GetShrubModels(int shrubModelPointer, int shrubModelCount)
        {
            Logger.Info("Parsing {0} shrub models from address {1}...", shrubModelCount, shrubModelPointer);
            List<Model> shrubModelList = new List<Model>(shrubModelCount);

            //Read the whole header block, and add models based on the count
            byte[] shrubBlock = ReadBlock(fileStream, shrubModelPointer, shrubModelCount * 0x40);
            for (int i = 0; i < shrubModelCount; i++)
            {
                shrubModelList.Add(new ShrubModel(decoder, fileStream, shrubBlock, i));
            }
            Logger.Info("Added {0} shrub models", shrubModelList.Count);
            return shrubModelList;
        }

        protected List<Texture> GetTextures(int texturePointer, int textureCount)
        {
            Logger.Info("Parsing {0} textures from address {1}...", textureCount, texturePointer);
            List<Texture> textureList = new List<Texture>(textureCount);

            //Read the whole texture header block, and add textures based on the count
            byte[] textureBlock = ReadBlock(fileStream, texturePointer, textureCount * Texture.TEXTUREELEMSIZE);
            for (int i = 0; i < textureCount; i++)
            {
                textureList.Add(new Texture(textureBlock, i));
            }
            Logger.Info("Added {0} textures", textureList.Count);
            return textureList;
        }

        protected List<Tie> GetTies(List<Model> tieModels, int tiePointer, int tieCount)
        {
            Logger.Info("Parsing {0} ties from address {1}...", tieCount, tiePointer);
            List<Tie> ties = new List<Tie>(tieCount);

            //Read the whole texture header block, and add textures based on the count
            byte[] tieBlock = ReadBlock(fileStream, tiePointer, tieCount * 0x70);
            for (int i = 0; i < tieCount; i++)
            {
                Tie tie = new Tie(decoder, tieBlock, i, tieModels, fileStream);
                ties.Add(tie);
            }
            Logger.Info("Added {0} ties", ties.Count);
            return ties;
        }

        protected List<Shrub> GetShrubs(List<Model> shrubModels, int shrubPointer, int shrubCount)
        {
            Logger.Info("Parsing {0} Shrubs from address {1}...", shrubCount, shrubPointer);
            List<Shrub> shrubs = new List<Shrub>(shrubCount);

            //Read the whole texture header block, and add models based on the count
            byte[] shrubBlock = ReadBlock(fileStream, shrubPointer, shrubCount * 0x70);
            for (int i = 0; i < shrubCount; i++)
            {
                Shrub shrub = new Shrub(shrubBlock, i, shrubModels);
                shrubs.Add(shrub);
            }
            Logger.Info("Added {0} Shrubs", shrubs.Count);
            return shrubs;
        }

        protected List<Light> GetLights(int lightPointer, int lightCount)
        {
            Logger.Info("Parsing {0} Lights from address {1}...", lightCount, lightPointer);
            List<Light> lightList = new List<Light>(lightCount);

            //Read the whole header block, and add lights based on the count
            byte[] lightBlock = ReadBlock(fileStream, lightPointer, lightCount * 0x40);
            for (int i = 0; i < lightCount; i++)
            {
                lightList.Add(new Light(lightBlock, i));
            }
            Logger.Info("Added {0} lights", lightList.Count);
            return lightList;
        }


        protected List<TerrainFragment> GetTerrainModels(int terrainModelPointer)
        {
            Logger.Info("Parsing terrain elements header from address {0}...", terrainModelPointer);
            List<TerrainFragment> tFrags = new List<TerrainFragment>();

            //Read the whole terrain header
            byte[] terrainBlock = ReadBlock(fileStream, terrainModelPointer, 0x60);
            TerrainHead head = new TerrainHead(terrainBlock);

            Logger.Info("Parsing {0} terrain elements from address {1}...", head.headCount, terrainModelPointer + 0x60);
            byte[] tfragBlock = ReadBlock(fileStream, terrainModelPointer + 0x60, head.headCount * 0x30);

            for (int i = 0; i < head.headCount; i++)
            {
                tFrags.Add(new TerrainFragment(decoder, fileStream, head, tfragBlock, i));
            }

            /*
            List<TerrainHeader> pointerList = new List<TerrainHeader>(terrainHeadCount);

            byte[] terrainHeadBlock = ReadBlock(fileStream, terrainHeadPointer, terrainHeadCount * 0x30);
            for (int i = 0; i < terrainHeadCount; i++)
            {
                TerrainFragHeader head = new TerrainFragHeader(fileStream, terrainHeadBlock, i);
                if (pointerList.Count < head.slotNum + 1)
                {
                    pointerList.Add(new TerrainHeader(terrainBlock, (head.slotNum * 4)));
                }
                pointerList[head.slotNum].vertexCount += head.vertexCount;
                pointerList[head.slotNum].heads.Add(head);
            }

            List<TerrainModel> terrainModels = new List<TerrainModel>(pointerList.Count);
            foreach (TerrainHeader hd in pointerList)
            {
                terrainModels.Add(new TerrainModel(fileStream, hd));
            }*/

            Logger.Info("Added {0} terrain elements", tFrags.Count);
            return tFrags;
        }

        protected SkyboxModel GetSkyboxModel(int skyboxPointer)
        {
            Logger.Info("Parsing skybox from address {0}...", skyboxPointer);
            SkyboxModel result = new SkyboxModel(decoder, fileStream, skyboxPointer);
            Logger.Info("Success");
            return result;
        }

        protected GameType DetectGame(int offset)
        {
            uint magic = ReadUint(ReadBlock(fileStream, offset, 4), 0);
            switch (magic)
            {
                case 0x00000001:
                    return new GameType(1);
                case 0xEAA90001:
                    return new GameType(2);
                case 0xEAA60001:
                    return new GameType(3);
                default:
                    return new GameType(3);
            }
        }

        protected List<UiElement> GetUiElements(int offset)
        {
            Logger.Info("Parsing UI Elements header from address {0}...", offset);
            byte[] headBlock = ReadBlock(fileStream, offset, 0x10);
            short elemCount = ReadShort(headBlock, 0x00);
            short spriteCount = ReadShort(headBlock, 0x02);
            int elemOffset = ReadInt(headBlock, 0x04);
            int spriteOffset = ReadInt(headBlock, 0x08);

            Logger.Info("Parsing {0} UI Elements and {1} Sprites...", elemCount, spriteCount);
            byte[] elemBlock = ReadBlock(fileStream, elemOffset, elemCount * 8);
            byte[] spriteBlock = ReadBlock(fileStream, spriteOffset, spriteCount * 4);

            var list = new List<UiElement>(elemCount);
            for(int i = 0; i < elemCount; i++)
            {
                list.Add(new UiElement(elemBlock, i, spriteBlock));
            }
            Logger.Info("Added {0} UI elements", list.Count);
            return list;
        }

        protected List<Animation> GetPlayerAnimations(int offset, MobyModel ratchet)
        {
            int count = ratchet.animations.Count;
            Logger.Info("Parsing {0} player animations from address {1}...", count, offset);
            List<Animation> animations = new List<Animation>(count);
            if(offset > 0)
            {
                byte boneCount = (byte)ratchet.boneMatrices.Count;

                byte[] headBlock = ReadBlock(fileStream, offset, count * 0x04);

                for (int i = 0; i < count; i++)
                {
                    animations.Add(new Animation(decoder, fileStream, decoder.Int(headBlock, i * 4), 0, boneCount, true));
                }
            }

            Logger.Info("Added {0} player animations", animations.Count);
            return animations;
        }

        protected List<Model> GetWeapons(int weaponPointer, int count)
        {
            Logger.Info("Parsing {0} weapons from address {1}...", count, weaponPointer);
            List<Model> weaponModels = new List<Model>(count);

            //Each moby is stored as a [MobyID, offset] pair
            byte[] mobyIDBlock = ReadBlock(fileStream, weaponPointer, count * 0x10);
            for (int i = 0; i < count; i++)
            {
                short modelID = decoder.Short(mobyIDBlock, (i * 0x10) + 2);
                int offset = decoder.Int(mobyIDBlock, (i * 0x10) + 4);
                weaponModels.Add(new MobyModel(decoder, fileStream, modelID, offset));
            }
            Logger.Info("Added {0} weapons", weaponModels.Count);
            return weaponModels;
        }

        protected byte[] GetLightConfig(int lightConfigOffset)
        {
            return ReadBlock(fileStream, lightConfigOffset, 0x60);
        }

        protected List<int> GetTextureConfigMenu(int textureConfigMenuOffset, int textureConfigMenuCount)
        {
            List<int> textureConfigMenuList = new List<int>(textureConfigMenuCount);
            byte[] textureConfigMenuBlock = ReadBlock(fileStream, textureConfigMenuOffset, textureConfigMenuCount * 4);

            for (int i = 0; i < textureConfigMenuCount; i++)
            {
                textureConfigMenuList.Add(ReadInt(textureConfigMenuBlock, i * 4));
            }
            return textureConfigMenuList;
        }

        protected byte[] GetTerrainBytes(int terrainPointer, int terrainLength)
        {

            return new byte[]
            {
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
            };

            byte[] terrainBlock = ReadBlock(fileStream, terrainPointer, terrainLength);

            int headOffset = ReadInt(terrainBlock, 0x00) - 0x60;
            int off_08 = ReadInt(terrainBlock, 0x08);
            int off_18 = ReadInt(terrainBlock, 0x18);
            int off_28 = ReadInt(terrainBlock, 0x28);
            int off_38 = ReadInt(terrainBlock, 0x38);

            WriteInt(terrainBlock, 0x00, 0x60);
            WriteInt(terrainBlock, 0x08, off_08 - headOffset);
            WriteInt(terrainBlock, 0x18, off_18 - headOffset);
            WriteInt(terrainBlock, 0x28, off_28 - headOffset);
            WriteInt(terrainBlock, 0x38, off_38 - headOffset);

            short headCount = ReadShort(terrainBlock, 0x06);

            int texCount = 0;
            for (int i = 0; i < headCount; i++)
            {
                int texOffset = ReadInt(terrainBlock, 0x70 + i * 0x30);
                WriteInt(terrainBlock, 0x70 + i * 0x30, texOffset - headOffset);
                texCount += ReadShort(terrainBlock, 0x76 + i * 0x30);
            }

            int texOffset0 = ReadInt(terrainBlock, 0x70);

            int lowestOffset = 0xffff;
            for (int i = 0; i < texCount; i++)
            {
                int texId = ReadInt(terrainBlock, texOffset0 + i * 0x10);
                if (texId < lowestOffset) lowestOffset = texId;
            }

            for (int i = 0; i < texCount; i++)
            {
                int texId = ReadInt(terrainBlock, texOffset0 + i * 0x10);
                WriteInt(terrainBlock, texOffset0 + i * 0x10, texId - lowestOffset);
            }

            Logger.Info("Lowest offset: " + lowestOffset);
            Logger.Info("Texture cout: " + texCount);
            Logger.Info("tex0offset: " + texOffset0);

            return terrainBlock;
        }

        protected Model GetCollisionModel(int collisionOffset)
        {
            return new Collision(fileStream, collisionOffset);
        }

        protected byte[] ReadArbBytes(int offset, int length)
        {
            return ReadBlock(fileStream, offset, length);
        }

        public void Close()
        {
            fileStream.Close();
        }
    }
}
