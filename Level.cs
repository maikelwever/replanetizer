using System;
using System.Collections.Generic;
using System.IO;
using RatchetEdit.LevelObjects;
using RatchetEdit.Models;
using RatchetEdit.Parsers;
using RatchetEdit.Headers;
using RatchetEdit.Models.Animations;

namespace RatchetEdit
{
    public class Level
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public bool valid;
        public bool isVita = false;
        public Decoder decoder;

        public string path;
        public EngineHeader engineHeader;

        public GameType game;

        //Models
        public List<Model> mobyModels;
        public List<Model> tieModels;
        public List<Model> shrubModels;
        public List<Model> weaponModels;
        public Model collisionModel;
        public List<Model> chunks;
        public List<Texture> textures;
        public SkyboxModel skybox;

        public byte[] terrainBytes;
        public byte[] renderDefBytes;
        public byte[] collBytes;
        public byte[] billboardBytes;
        public byte[] soundConfigBytes;

        public List<Animation> playerAnimations;
        public List<UiElement> uiElements;


        //Level objects
        public List<Moby> mobs;
        public List<Tie> ties;
        public List<Shrub> shrubs;
        public List<Light> lights;
        public List<Spline> splines;
        public List<TerrainFragment> terrains;
        public List<int> textureConfigMenus;

        public LevelVariables levelVariables;
        public OcclusionData occlusionData;

        public Dictionary<int, String> english;
        public Dictionary<int, String> lang2;
        public Dictionary<int, String> french;
        public Dictionary<int, String> german;
        public Dictionary<int, String> spanish;
        public Dictionary<int, String> italian;
        public Dictionary<int, String> lang7;
        public Dictionary<int, String> lang8;

        public byte[] unk6;
        public byte[] unk7;
        public byte[] unk9;
        public byte[] unk13;
        public byte[] unk17;
        public byte[] unk14;

        public byte[] lightConfig;

        public List<KeyValuePair<int, int>> type50s;
        public List<KeyValuePair<int, int>> type5Cs;

        public byte[] tieData;
        public byte[] shrubData;

        public List<Type04> type04s;
        public List<Type0C> type0Cs;
        public List<Type64> type64s;
        public List<Type68> type68s;
        public List<Type7C> type7Cs;
        public List<Type80> type80s;
        public List<Type88> type88s;

        public List<byte[]> pVars;
        public List<Cuboid> cuboids;
        public List<GameCamera> gameCameras;

        public List<int> mobyIds;
        public List<int> tieIds;
        public List<int> shrubIds;

        ~Level()
        {
            Logger.Info("Level destroyed");
        }

        //New file constructor
        public Level() { }

        //Engine file constructor
        public Level(string enginePath)
        {

            path = Path.GetDirectoryName(enginePath);
            var vitaVertFile = Path.Combine(path, "engine_vert.ps3");

            if (File.Exists(vitaVertFile)) {
                Logger.Info("Using Vita parsing logic.");
                isVita = true;
                decoder = new Decoder(true);
            } else {
                decoder = new Decoder(false);
            }
            
            using (EngineParser engineParser = new EngineParser(decoder, enginePath))
            {
                ParseEngineData(engineParser);
            }

            // Gameplay elements
            using(GameplayParser gameplayParser = new GameplayParser(game, path + @"/gameplay_ntsc"))
            {
                ParseGameplayData(gameplayParser);
            }

            VramParser vramParser = new VramParser(path + @"/vram.ps3");
            if (!vramParser.valid)
            {
                valid = false;
                return;
            }

            vramParser.GetTextures(textures);
            vramParser.Close();


            Logger.Info("Level parsing done");
            valid = true;
        }

        void ParseEngineData(EngineParser engineParser) {
            game = engineParser.DetectGame();

            //REMOVE THESE ASAP!!!!!111
            terrainBytes = engineParser.GetTerrainBytes();
            renderDefBytes = engineParser.GetRenderDefBytes();
            collBytes = engineParser.GetCollisionBytes();
            billboardBytes = engineParser.GetBillboardBytes();
            soundConfigBytes = engineParser.GetSoundConfigBytes();

            Logger.Info("Parsing skybox...");
            skybox = engineParser.GetSkyboxModel(decoder);
            Logger.Info("Success");

            Logger.Info("Parsing moby models...");
            mobyModels = engineParser.GetMobyModels(decoder);
            Logger.Info("Added " + mobyModels.Count + " moby models");

            Logger.Info("Parsing tie models...");
            tieModels = engineParser.GetTieModels();
            Logger.Info("Added " + tieModels.Count + " tie models");

            Logger.Info("Parsing shrub models...");
            shrubModels = engineParser.GetShrubModels();
            Logger.Info("Added " + shrubModels.Count + " shrub models");

            Logger.Info("Parsing weapons...");
            weaponModels = engineParser.GetWeapons(decoder);
            Logger.Info("Added " + weaponModels.Count + " weapons");

            Logger.Info("Parsing textures...");
            textures = engineParser.GetTextures();
            Logger.Info("Added " + textures.Count + " textures");

            Logger.Info("Parsing ties...");
            ties = engineParser.GetTies(tieModels);
            Logger.Info("Added " + ties.Count + " ties");

            Logger.Info("Parsing Shrubs...");
            shrubs = engineParser.GetShrubs(shrubModels);
            Logger.Info("Added " + shrubs.Count + " Shrubs");

            Logger.Info("Parsing Lights...");
            lights = engineParser.GetLights();
            Logger.Info("Added " + lights.Count + " lights");

            Logger.Info("Parsing terrain elements...");
            terrains = engineParser.GetTerrainModels();
            Logger.Info("Added " + terrains?.Count + " terrain elements");

            Logger.Info("Parsing player animations...");
            playerAnimations = engineParser.GetPlayerAnimations((MobyModel)mobyModels[0]);
            Logger.Info("Added " + playerAnimations?.Count + " player animations");

            uiElements = engineParser.GetUiElements();
            Logger.Info("Added " + uiElements?.Count + " ui elements");

            lightConfig = engineParser.GetLightConfig();
            textureConfigMenus = engineParser.GetTextureConfigMenu();
            collisionModel = engineParser.GetCollisionModel();
        }

        void ParseGameplayData(GameplayParser gameplayParser) {
            Logger.Info("Parsing Level variables...");
            levelVariables = gameplayParser.GetLevelVariables();

            Logger.Info("Parsing mobs...");
            mobs = gameplayParser.GetMobies(game, mobyModels);
            Logger.Info("Added " + mobs?.Count + " mobs");

            Logger.Info("Parsing splines...");
            splines = gameplayParser.GetSplines();
            //Logger.Info("Added " + splines.Count + " splines");

            Logger.Info("Parsing languages...");
            english = gameplayParser.GetEnglish();
            lang2 = gameplayParser.GetLang2();
            french = gameplayParser.GetFrench();
            german = gameplayParser.GetGerman();
            spanish = gameplayParser.GetSpanish();
            italian = gameplayParser.GetItalian();
            lang7 = gameplayParser.GetLang7();
            lang8 = gameplayParser.GetLang8();

            Logger.Info("Parsing other gameplay assets...");
            unk6 = gameplayParser.GetUnk6();
            unk7 = gameplayParser.GetUnk7();
            unk13 = gameplayParser.GetUnk13();
            unk17 = gameplayParser.GetUnk17();
            unk14 = gameplayParser.GetUnk14();

            tieData = gameplayParser.GetTieData(ties.Count);
            shrubData = gameplayParser.getShrubData(shrubs.Count);

            type04s = gameplayParser.GetType04s();
            type0Cs = gameplayParser.GetType0Cs();
            type64s = gameplayParser.GetType64s();
            type68s = gameplayParser.GetType68s();
            type7Cs = gameplayParser.GetType7Cs();
            type80s = gameplayParser.GetType80();
            type88s = gameplayParser.GetType88s();
            type50s = gameplayParser.GetType50s();
            type5Cs = gameplayParser.GetType5Cs();

            pVars = gameplayParser.GetPvars(mobs);
            cuboids = gameplayParser.GetCuboids();
            gameCameras = gameplayParser.GetGameCameras();

            mobyIds = gameplayParser.GetMobyIds();
            tieIds = gameplayParser.GetTieIds();
            shrubIds = gameplayParser.GetShrubIds();
            occlusionData = gameplayParser.GetOcclusionData();
        }

    }
}
