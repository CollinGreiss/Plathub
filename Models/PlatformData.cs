using IGDB.Models;

namespace Plathub.Models;
public class PlatformData {

    public enum GamePlatform {

        _1292AdvancedProgrammableVideoSystem = 139,
        A3DOInteractiveMultiplayer = 50,
        AcornArchimedes = 116,
        AcornElectron = 134,
        Amiga = 16,
        AmigaCD32 = 114,
        AmstradCPC = 25,
        AmstradPCW = 154,
        AnalogueElectronics = 100,
        Android = 34,
        AppleII = 75,
        AppleIIGS = 115,
        Arcade = 52,
        Atari2600 = 59,
        Atari5200 = 66,
        Atari7800 = 60,
        Atari8_bit = 65,
        AtariJaguar = 62,
        AtariLynx = 61,
        AtariST_STE = 63,
        BBCMicrocomputerSystem = 69,
        BlackBerryOS = 73,
        CallAComputerTimeSharedMainframeComputerSystem = 107,
        CDCCyber70 = 109,
        ColecoVision = 68,
        Commodore16 = 93,
        Commodore64_128_MAX = 15,
        CommodoreCDTV = 158,
        CommodorePET = 90,
        CommodorePlus_4 = 94,
        CommodoreVIC_20 = 71,
        Daydream = 164,
        DECGT40 = 98,
        DOS = 13,
        Dragon32_64 = 153,
        Dreamcast = 23,
        EDSAC = 102,
        FairchildChannelF = 127,
        FamilyComputer = 99,
        FamilyComputerDiskSystem = 51,
        FerrantiNimrodComputer = 101,
        FM_7 = 152,
        FMTowns = 118,
        GameBoy = 33,
        GameBoyAdvance = 24,
        GameBoyColor = 22,
        GameCube = 21,
        GoogleStadia = 170,
        HP2100 = 104,
        HP3000 = 105,
        HyperNeoGeo64 = 135,
        ImlacPDS_1 = 111,
        Intellivision = 67,
        iOS = 39,
        Linux = 3,
        LegacyMobileDevice = 55,
        Mac = 14,
        Microcomputer = 112,
        Microvision = 89,
        MSX = 27,
        MSX2 = 53,
        N_Gage = 42,
        NeoGeoAES = 80,
        NeoGeoCD = 136,
        NeoGeoMVS = 79,
        NeoGeoPocket = 119,
        NeoGeoPocketColor = 120,
        NewNintendo3DS = 137,
        Nintendo3DS = 37,
        Nintendo64 = 4,
        NintendoDS = 20,
        NintendoEntertainmentSystem = 18,
        NintendoGameCube = 21,
        NintendoPlayStation = 131,
        NintendoSwitch = 130,
        Nuon = 122,
        Odyssey = 88,
        Odyssey2_VideopacG7000 = 133,
        OculusVR = 162,
        Ouya = 72,
        PC_50XFamily = 142,
        PC_8800Series = 125,
        PC_MicrosoftWindows = 6,
        PC_Engine = 86,
        PC_EngineCD = 150,
        PDP_1 = 95,
        PDP_10 = 96,
        PDP_11 = 108,
        PDP_7 = 103,
        PDP_8 = 97,
        PhilipsCD_i = 117,
        PlayStation = 7,
        PlayStation2 = 8,
        PlayStation3 = 9,
        PlayStation4 = 48,
        PlayStation5 = 167,
        PlayStationPortable = 38,
        PlayStationVita = 46,
        PlayStationVR = 165,
        Pokémonmini = 166,
        Sega32X = 30,
        SegaCD = 78,
        SegaGameGear = 35,
        SegaMasterSystem_MarkIII = 64,
        SegaMegaDrive_Genesis = 29,
        SegaSaturn = 32,
        SG_1000 = 84,
        SharpX1 = 77,
        SharpX68000 = 121,
        SteamVR = 163,
        SuperFamicom = 58,
        SuperNintendoEntertainmentSystem = 19,
        SwanCrystal = 124,
        TapwaveZodiac = 44,
        TexasInstrumentsTI_99 = 129,
        ThomsonMO5 = 156,
        TRS_80 = 126,
        TRS_80ColorComputer = 151,
        Turbografx_16_PCEngine = 86,
        Turbografx_16_PCEngineCD = 150,
        Vectrex = 70,
        VC4000 = 138,
        VirtualBoy = 87,
        VirtualConsole = 47,
        WebBrowser = 82,
        Wii = 5,
        WiiU = 41,
        WindowsMixedReality = 161,
        WindowsPhone = 74,
        WonderSwan = 57,
        WonderSwanColor = 123,
        Xbox = 11,
        Xbox360 = 12,
        XboxOne = 49,
        XboxSeriesX_S = 169,
        ZX_Spectrum = 26
    }





    public static string GetPlatformQuery( GamePlatform[]? platforms ) {

        var query = "platforms = (" + (int) platforms[0];

        for ( int i = 1; i < platforms.Length; i++ ) {

            query += (int) platforms[i];

        }

        query += ")";

        return query;

    }

    public static GamePlatform[]? GetPlatforms( int? id ) {

        if ( id == null ) return null;
        return GetPlatforms( new int[] { (int) id } );

    }

    public static GamePlatform[] GetPlatforms( int[] ids ) {

        GamePlatform[] genres = new GamePlatform[ids.Length];

        for ( int i = 0; i < ids.Length; i++ ) {

            genres[i] = (GamePlatform) ids[i];

        }

        return genres;

    }

}
