public enum GameSwitches
{
  // Initial suspect interrogation
  InterrogatedShopCultistOnce,
  InterrogatedAmbitiousCultistOnce,
  InterrogatedCleaningCultistOnce,
  Finished_Interrogation,

  // Party cultist line
  TalkedTo_PartyCultist,
  GotItem_Beer,
  Resolved_PartyCultist,

  // Cat cultist line
  TalkedTo_CatCultist,
  ReleasedCat,
  CatRanAway,
  Resolved_CatCultist,

  // Shop Assistant line
  TalkedTo_ShopAssistant,
  Resolved_ShopAssistantCultist,
  GotItem_LetterForLover,
  BroughtLetterToLover,

  // Chimney line
  InvestigatedChimney,
  Got_Item_EmptyBeerJug,
  GotItem_CleaningWaterInAJug,
  UnlockedRoomBehindChimney,
  GotItem_CatHair,

  // Cleaning-Crew
  TalkedTo_CleaningCrew,
  TalkedTo_CleaningCrewAfterChimneyInvestigation,
  CleaningCrewLuredAway,

  // DeathWishCultist
  TalkedTo_DeathWishCultist,
  DeathWishCultistReadyForSacrifice,
  DeathWishCultistDead,
  GotItem_RitualBlood,
}