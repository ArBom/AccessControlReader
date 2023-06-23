namespace AccessControlReader
{
    public delegate void NewCardRead(UInt32 Id);
    public delegate void StateEvent(EventType eventType, string details);
    public delegate void ErrorEvent(Type senderType, string details, int number, object[] errorTypes);

    public enum EventType { MainState_Unconfig, MainState_Blocked, MainState_Reading, CorrectCard, WrongCard, OpenButton, DoorClosed, DoorOpen, TimeEvent, Error }
    public enum NoiseType { Open, NoGo, Alert, SecurityAlarm, Error }
    public enum AnimationType { Unactive, RFID, Open, OpenAlert, Info, SecurityAlert, Hand, Error };
    public enum ErrorTypeIcon { LAN, SQL, RFID, Hardware, Internal, XML }
}
