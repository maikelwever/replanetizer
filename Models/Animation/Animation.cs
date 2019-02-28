﻿using System.Collections.Generic;
using System.IO;
using static RatchetEdit.DataFunctions;

namespace RatchetEdit.Models.Animations
{
    public class Animation
    {
        public float unk1 { get; set; }
        public float unk2 { get; set; }
        public float unk3 { get; set; }
        public float unk4 { get; set; }
        public byte unk5 { get; set; }
        public byte unk7 { get; set; }

        public uint null1 { get; set; }
        public float speed { get; set; }

        public List<Frame> frames { get; set; } = new List<Frame>();
        public List<int> sounds { get; set; } = new List<int>();


        public Animation()
        {

        }
        public Animation(FileStream fs, int modelOffset, int animationOffset, byte boneCount, bool force=false)
        {
            //Only try to parse if the offset is non-zero
            if (animationOffset == 0 && !force)
                return;

            if (modelOffset == 0)
                return;

            // Header
            byte[] header = ReadBlock(fs, modelOffset + animationOffset, 0x1C);
            unk1 = ReadFloat(header, 0x00);
            unk2 = ReadFloat(header, 0x04);
            unk3 = ReadFloat(header, 0x08);
            unk4 = ReadFloat(header, 0x0C);

            byte frameCount = header[0x10];
            unk5 = header[0x11];
            byte soundsCount = header[0x12];
            unk7 = header[0x13];

            null1 = ReadUint(header, 0x14);
            speed = ReadFloat(header, 0x18);

            // Frames
            byte[] animationPointerBlock = ReadBlock(fs, modelOffset + animationOffset + 0x1C, frameCount * 0x04);
            for (int i = 0; i < frameCount; i++)
            {
                frames.Add(new Frame(fs, modelOffset + ReadInt(animationPointerBlock, i * 0x04), boneCount));
            }

            // Sound configs
            byte[] extrasBlock = ReadBlock(fs, (modelOffset + animationOffset) + 0x1C + frameCount * 0x04, soundsCount * 4);
            for (int i = 0; i < soundsCount; i++)
            {
                sounds.Add(ReadInt(extrasBlock, i * 4));
            }
        }

        public byte[] Serialize(int baseOffset = 0)
        {
            // Head
            byte[] head = new byte[0x1C];
            WriteFloat(ref head, 0x00, unk1);
            WriteFloat(ref head, 0x04, unk2);
            WriteFloat(ref head, 0x08, unk3);
            WriteFloat(ref head, 0x0C, unk4);
            head[0x10] = (byte)frames.Count;
            head[0x11] = unk5;
            head[0x12] = (byte)sounds.Count;
            head[0x13] = unk7;
            WriteUint(ref head, 0x14, null1);
            WriteFloat(ref head, 0x18, speed);

            // Sound configs
            byte[] soundBytes = new byte[sounds.Count * 4];
            for (int i = 0; i < sounds.Count; i++)
            {
                WriteInt(ref soundBytes, i * 4, sounds[i]);
            }

            // Frames
            int framesSize = 0;
            var frameBytes = new List<byte[]>();
            foreach(Frame frame in frames)
            {
                byte[] frameByte = frame.Serialize();
                frameBytes.Add(frameByte);
                framesSize += frameByte.Length;
            }

            int offs = GetLength(0x1C + frames.Count * 4 + soundBytes.Length);


            // Make out array and copy to it
            byte[] outBytes = new byte[offs + framesSize];
            head.CopyTo(outBytes, 0);
            soundBytes.CopyTo(outBytes, 0x1C + frames.Count * 4);
            for(int i = 0; i < frameBytes.Count; i++)
            {
                WriteInt(ref outBytes, 0x1C + i * 4, offs + baseOffset);
                frameBytes[i].CopyTo(outBytes, offs);
                offs += frameBytes[i].Length;
            }

            return outBytes;
        }
    }
}
