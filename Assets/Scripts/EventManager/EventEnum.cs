
public enum EventEnum  {

    None = -1,
    // client part
    Start_Application = 0,  

    Create_Main_Panel_Window = 1,
    Create_Map_Panel = 2,
    Create_Racers_List = 3,
    Client_CreateRacers = 4,
    Client_CreateRacerController = 5,
    Pin_All_Objects = 6,
    Client_ShowRacerFlag = 7,
    Client_HideRacerFlag = 8,
    Client_SendOvertrakingInfo = 9,
    Client_RacerPositionChanged = 10,

    Create_Video_Patern = 15, 
    Close_Video = 16,
    Move_Patern_Video = 17,
    Create_Video_Map = 18,
    Scale_Main_Video = 19,
    Reset_All = 20,

    First_Racer_Finished_Lap = 23,
    Racer_Finished_Lap = 24,

    Refresh_Race = 30,

    ShowRacerVideo = 40,
    HideRacerVideo = 41,
    RacerVideoClosed = 42,

	ARHit = 50,

    // server part
    Server_CreateRasers = 1003, 
    Server_UpdatePosition = 1005,

    Server_RaceBegin = 1100,
    Server_RaceEnd = 1101,
    Server_OvertakingBegin = 1105,
    Server_OvertakingEnd = 1106,
    Server_LapEnd = 1107,
}
