//Camera enums
public enum MoodCamera
{
    FirstFace,
    ThirdFace
}

public enum CameraPlayer
{
    Null,

    RotateSimple,
    Aiming,
}

//Player body enums
public enum Player
{
    Null,

    OpenInventory,
}


public enum HandsPlayer
{
    Null,

    AimingForDoSomething,
    UseSomething,
    CarryBody,
}

public enum HandsPlayerHave
{
    Null,

    RockForThrow,
    Weapon,
    Pistol,
}


public enum LegsPlayer
{
    Null,

    SatDown,
}

public enum SpeedLegsPlayer
{
    Null,

    CrouchWalk,
    Walk,
    Run,
}

//Loot enums

public enum TypeCaliber
{
    Null,
    C7_62MM,
    C5_56MM,
    C9MM,
    C4_45ACP,
    C12x99MM,
    Bite
}

public enum InfoToLoot
{
    Null,

    Add4_45ACP,
    Add9MM,
    Add5_56MM,
    Add7_62MM,
    Add12x99MM,

    Add15HP,
    Add50HP,
    Add100HP,

    Add80Arm,
    Add120Arm,
    Add160Arm

}

public enum LevelObject
{
    FirstLevel,
    SecondLevel,
    ThirdLevel,
}

//Weapons enums
public enum TypeWeapon
{
    Weapon,
    Pistol
}

public enum StateWeapon
{
    Null,

    IsUsing,
    HaveOwner,
    HaventOwher,
}

//Other enums

public enum WhatIsInHand
{
    Null,
    Weapon,
    Loot,

}

