
﻿ <div align="center">

## LEGENOofOuch

<br/> [<img src="https://img.shields.io/badge/프로젝트 기간-2025.02.21~2025.02.27-73abf0?style=flat&logo=&logoColor=white" />]()

---
</div> 

## 📝 프로젝트 소개

모바일 게임 궁수의 전설을 모티브로 한 2D 게임 프로젝트입니다.
이동 키만을 이용하여 플레이할 수 있으며, 이동하지 않을 때만 공격할 수 있습니다.
또한, 캐릭터 성장을 통해 랜덤으로 주어지는 어빌리티(스킬)을 조합하여 성장하는 재미를 제공합니다.

---

## 🎮 게임 기능 개요

| 기능 | 설명 |
|---|---|
| **🎯 이동 및 공격** | 이동 키만을 이용해 캐릭터를 조작할 수 있으며, 이동을 멈추면 자동으로 공격합니다. |
| **🏆 도전과제 시스템** | 다양한 도전과제를 수행하고 보상을 획득할 수 있습니다. |
| **🎲 랜덤 어빌리티 선택** | 레벨 업 시 랜덤으로 주어지는 어빌리티를 조합하여 캐릭터를 성장시킵니다. |
| **🎮 스테이지 클리어 방식** | 적을 처치하고 점점 강력해지는 적들을 상대하며 스테이지를 클리어해야 합니다. |

---

## 📸 화면 구성
|메인 화면|
|:---:|
|<img src="https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/main.png?raw=true" width="700"/>|
|메인화면 설명.|

<br /><br />

|옵션 창|
|:---:|
|<img src="https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/option.png?raw=true" width="700"/>|
|키 바인딩, 사운드 조절|  

<br /><br />

|캐릭터 선택 창|
|:---:|
|<img src="https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/Character.png?raw=true" width="700"/>|
|플레이에 사용될 캐릭터 선택|

<br /><br />

|게임 플레이 장면|
|:---:|
|<img src="https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/play.png?raw=true" width="700"/>|
|게임 플레이 장면 설명|

---

## 📂 프로젝트 폴더 구조
```
📦Assets
┣ 📂01_Scenes
┃ ┣ 📜MainScene.unity
┣ 📂02_Scripts
┃ ┣ 📂Ability
┃ ┃ ┣ 📜AbilityBase.cs
┃ ┃ ┣ 📜AbilityController.cs
┃ ┃ ┣ 📜Archer.cs
┃ ┃ ┣ 📜AttackBoost.cs
┃ ┃ ┣ 📜AttackSpeedBoost.cs
┃ ┃ ┣ 📜Blaze.cs
┃ ┃ ┣ 📜BloodThirst.cs
┃ ┃ ┣ 📜CriticalMaster.cs
┃ ┃ ┣ 📜DarkTouch.cs
┃ ┃ ┣ 📜DiagonalShot.cs
┃ ┃ ┣ 📜ExtraLife.cs
┃ ┃ ┣ 📜FireOrb.cs
┃ ┃ ┣ 📜FrontShot.cs
┃ ┃ ┣ 📜Fury.cs
┃ ┃ ┣ 📜HPBoost.cs
┃ ┃ ┣ 📜Invincibility.cs
┃ ┃ ┣ 📜Mage.cs
┃ ┃ ┣ 📜MultiShot.cs
┃ ┃ ┣ 📜PiercingShot.cs
┃ ┃ ┣ 📜SideShot.cs
┃ ┃ ┣ 📜SpeedBoost.cs
┃ ┃ ┣ 📜Spirit.cs
┃ ┃ ┣ 📜WallReflection.cs
┃ ┃ ┣ 📜Warrior.cs
┃ ┣ 📂Achievements
┃ ┃ ┣ 📜Achievements.cs
┃ ┃ ┣ 📜AchievemnetUIController.cs
┃ ┣ 📂Camera
┃ ┃ ┣ 📜CameraManager.cs
┃ ┃ ┣ 📜CameraSetup.cs
┃ ┣ 📂Character
┃ ┃ ┣ 📜AnimationHandler.cs
┃ ┃ ┣ 📜BaseCharacter.cs
┃ ┃ ┣ 📜BossCharacter.cs
┃ ┃ ┣ 📜EnemyCharacter.cs
┃ ┃ ┣ 📜PlayerCharacter.cs
┃ ┃ ┣ 📜PlayerFormChange.cs
┃ ┣ 📂Etc
┃ ┃ ┣ 📜DevilInteraction.cs
┃ ┃ ┣ 📜HpPotion.cs
┃ ┃ ┣ 📜NextStageCollider.cs
┃ ┃ ┣ 📜StageContainer.cs
┃ ┃ ┣ 📜Trade.cs
┃ ┣ 📂Gacha
┃ ┃ ┣ 📜Gacha.cs
┃ ┃ ┣ 📜GachaAbilityController.cs
┃ ┃ ┣ 📜GachaAnimation.cs
┃ ┃ ┣ 📜GachaController.cs
┃ ┣ 📂Managers
┃ ┃ ┣ 📜AbilityManager.cs
┃ ┃ ┣ 📜GachaManager.cs
┃ ┃ ┣ 📜GameManager.cs
┃ ┃ ┣ 📜LevelManager.cs
┃ ┃ ┣ 📜MonsterManager.cs
┃ ┃ ┣ 📜OptionManager.cs
┃ ┃ ┣ 📜ProjectileManager.cs
┃ ┃ ┣ 📜SelectManager.cs
┃ ┃ ┣ 📜SoundManager.cs
┃ ┃ ┣ 📜UIManager.cs
┃ ┣ 📂Projectiles
┃ ┃ ┣ 📜FireOrbController.cs
┃ ┃ ┣ 📜ProjectileController.cs
┃ ┃ ┣ 📜ProjectileEnemyController.cs
┃ ┃ ┣ 📜SurroundController.cs
┃ ┣ 📂Repository
┃ ┃ ┣ 📜AbilityRepositoy.cs
┃ ┣ 📂ScriptableObjects
┃ ┃ ┣ 📜AbilityDataSO.cs
┃ ┣ 📂Utility
┃ ┃ ┣ 📜AbilityDataDownLoader.cs
┃ ┃ ┣ 📜Enum.cs
┣ 📂03_Prefabs
┃ ┣ 📂Abilities
┃ ┃ ┣ 📜HP 부스트.prefab
┃ ┃ ┣ 📜공격 부스트.prefab
┃ ┃ ┣ 📜공격 속도 부스트.prefab
┃ ┃ ┣ 📜관통샷.prefab
┃ ┃ ┣ 📜궁수.prefab
┃ ┃ ┣ 📜멀티 샷.prefab
┃ ┃ ┣ 📜무적.prefab
┃ ┃ ┣ 📜법사.prefab
┃ ┃ ┣ 📜벽 반사.prefab
┃ ┃ ┣ 📜분노.prefab
┃ ┃ ┣ 📜불의 원.prefab
┃ ┃ ┣ 📜블레이즈.prefab
┃ ┃ ┣ 📜사선 화살.prefab
┃ ┃ ┣ 📜속도 오로라.prefab
┃ ┃ ┣ 📜어둠의 접촉.prefab
┃ ┃ ┣ 📜엑스트라 라이프.prefab
┃ ┃ ┣ 📜전방 화살.prefab
┃ ┃ ┣ 📜전사.prefab
┃ ┃ ┣ 📜정령.prefab
┃ ┃ ┣ 📜측면 화살.prefab
┃ ┃ ┣ 📜크리티컬 마스터.prefab
┃ ┃ ┣ 📜피의 갈증.prefab
┃ ┣ 📂Entity
┃ ┃ ┣ 📂Monsters
┃ ┃ ┃ ┣ 📂Boss
┃ ┃ ┃ ┃ ┣ 📜AxeSkeleton.prefab
┃ ┃ ┃ ┃ ┣ 📜OrcRider.prefab
┃ ┃ ┃ ┃ ┣ 📜Werebear.prefab
┃ ┃ ┃ ┣ 📜Orc.prefab
┃ ┃ ┃ ┣ 📜Skeleton.prefab
┃ ┃ ┃ ┣ 📜Slime.prefab
┃ ┃ ┣ 📂NPC
┃ ┃ ┃ ┣ 📜Devil.prefab
┃ ┃ ┣ 📂Player
┃ ┃ ┃ ┣ 📜Player.prefab
┃ ┣ 📂Gacha
┃ ┃ ┣ 📜BackGround.prefab
┃ ┃ ┣ 📜CommonBackGround.prefab
┃ ┃ ┣ 📜Pillar.prefab
┃ ┃ ┣ 📜RareBackGround.prefab
┃ ┃ ┣ 📜Slot.prefab
┃ ┣ 📂Item
┃ ┃ ┣ 📜Potion.prefab
┃ ┣ 📂Maps
┃ ┃ ┣ 📂Castle
┃ ┃ ┃ ┣ 📜NavMesh-Castle01.asset
┃ ┃ ┃ ┣ 📜NavMesh-Castle02.asset
┃ ┃ ┃ ┣ 📜NavMesh-Castle03.asset
┃ ┃ ┃ ┣ 📜NavMesh-CastleBoss.asset
┃ ┃ ┃ ┣ 📜NavMesh-CastleDevil.asset
┃ ┃ ┃ ┣ 📜StageCastle01.prefab
┃ ┃ ┃ ┣ 📜StageCastle02.prefab
┃ ┃ ┃ ┣ 📜StageCastle03.prefab
┃ ┃ ┃ ┣ 📜StageCastleBoss.prefab
┃ ┃ ┃ ┣ 📜StageCastleDevil.prefab
┃ ┃ ┣ 📂Swamp
┃ ┃ ┃ ┣ 📜NavMesh-Swamp01.asset
┃ ┃ ┃ ┣ 📜NavMesh-Swamp02.asset
┃ ┃ ┃ ┣ 📜NavMesh-Swamp03.asset
┃ ┃ ┃ ┣ 📜NavMesh-SwampBoss.asset
┃ ┃ ┃ ┣ 📜NavMesh-SwampDevil.asset
┃ ┃ ┃ ┣ 📜StageSwamp01.prefab
┃ ┃ ┃ ┣ 📜StageSwamp02.prefab
┃ ┃ ┃ ┣ 📜StageSwamp03.prefab
┃ ┃ ┃ ┣ 📜StageSwampBoss.prefab
┃ ┃ ┃ ┣ 📜StageSwampDevil.prefab
┃ ┃ ┣ 📂Volcanic
┃ ┃ ┃ ┣ 📜NavMesh-Volcanic01.asset
┃ ┃ ┃ ┣ 📜NavMesh-Volcanic02.asset
┃ ┃ ┃ ┣ 📜NavMesh-Volcanic03.asset
┃ ┃ ┃ ┣ 📜NavMesh-VolcanicBoss.asset
┃ ┃ ┃ ┣ 📜NavMesh-VolcanicDevil.asset
┃ ┃ ┃ ┣ 📜StageVolcanic01.prefab
┃ ┃ ┃ ┣ 📜StageVolcanic02.prefab
┃ ┃ ┃ ┣ 📜StageVolcanic03.prefab
┃ ┃ ┃ ┣ 📜StageVolcanicBoss.prefab
┃ ┃ ┃ ┣ 📜StageVolcanicDevil.prefab
┃ ┣ 📂Particle
┃ ┃ ┣ 📜Electricity_Splash_6.prefab
┃ ┃ ┣ 📜Health_Up_green.prefab
┃ ┃ ┣ 📜MainMenuParticle.prefab
┃ ┣ 📂Projectiles
┃ ┃ ┣ 📜Arrow.prefab
┃ ┃ ┣ 📜EnemyArrow.prefab
┃ ┃ ┣ 📜Fairy.prefab
┃ ┃ ┣ 📜Fireball.prefab
┃ ┃ ┣ 📜FireOrb.prefab
┃ ┃ ┣ 📜SwordAura.prefab
┣ 📂04_Animations
┃ ┣ 📂Entity
┃ ┃ ┣ 📂Devil
┃ ┃ ┣ 📂Enemy
┃ ┃ ┃ ┣ 📂Boss
┃ ┃ ┣ 📂Player
┃ ┣ 📂Projectiles
┃ ┣ 📂UI
┣ 📂05_Art
┃ ┣ 📂Material
┃ ┣ 📂Shader
┃ ┣ 📂Sprites_
┃ ┃ ┣ 📂Environment
┃ ┃ ┃ ┣ 📂Castle
┃ ┃ ┃ ┃ ┣ 📂Palettes
┃ ┃ ┃ ┃ ┣ 📂TileSprites
┃ ┃ ┃ ┣ 📂Swamp
┃ ┃ ┃ ┃ ┣ 📂Palettes
┃ ┃ ┃ ┃ ┣ 📂TileSprites
┃ ┃ ┃ ┣ 📂Volcanic
┃ ┃ ┃ ┃ ┣ 📂Palettes
┃ ┃ ┃ ┃ ┣ 📂TileSprites
┃ ┃ ┣ 📂Particle
┣ 📂06_UI
┃ ┣ 📂Font
┃ ┣ 📂Image
┃ ┃ ┣ 📂AchievementsIcon
┃ ┃ ┣ 📂InGame
┃ ┃ ┃ ┣ 📂Entity
┃ ┃ ┃ ┣ 📂Gacha
┃ ┃ ┣ 📂MainMenu
┃ ┃ ┃ ┣ 📂Map
┣ 📂07_Audio
┃ ┣ 📂BGM
┃ ┣ 📂SFX
┃ ┃ ┣ 📂UI
┣ 📂08_Data
┃ ┣ 📂ScriptableObjects
┃ ┃ ┣ 📂Abilities
┣ 📂09_Rendering
┃ ┣ 📂Settings
┃ ┃ ┣ 📂Scenes
┣ 📂99_ThirdParty
┃ ┣ 📂118 sprite effects bundle
┃ ┣ 📂EditAseprite
┃ ┣ 📂free-swamp-game-tileset-pixel-art
┃ ┣ 📂Kings and Pigs
┃ ┣ 📂Tiny RPG Character Asset Pack v1.03 -Full 20 Characters
┃ ┣ 📂Top Down Lava Tileset 16x16 Free
┣ 📂Plugins
┃ ┣ 📂Demigiant
┃ ┃ ┣ 📂DOTween
┣ 📂Resources
┃ ┣ 📜DOTweenSettings.asset
```

<br />  

---

## 👥 팀원 및 역할

| 팀원 | 역할 | GitHub 링크 |
|---|---|---|
| **김동석 (팀장)** | 가챠, 게임 매니저, 도전과제| [링크](https://github.com/dongsoek-kim)  |
| **김기석** | 플레이어, 몬스터, 아이템, 튜토리얼 | [링크](https://github.com/Kim-giseok) |
| **윤우중** | 투사체, 스킬 | [링크](https://github.com/YoonWooJoong) |
| **박승규** | 바탕화면 UI, 캐릭터 선택 | [링크](https://github.com/tmdrb7214) |
| **최상준** | 스킬, 파일관리, 카메라, 사운드, 옵션 | [링크](https://github.com/Dalsi-0) |

---

## 📌 프로젝트 계획 단계

**아이디어 구상:** 첫 회의를 통해 게임의 핵심 구조를 설계하고, 진행 방식 및 역할을 논의하며 프로젝트의 방향성을 구체화하였습니다.

**기능 구현 계획:** 게임의 기본적인 이동 및 공격 시스템을 먼저 개발하고, 이후에 스킬, UI, 도전과제 등 추가 기능을 점진적으로 구현하였습니다.

<img src="https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/classDesign.png?raw=true" width="700"/>
<img src="https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/flowChart.png?raw=true" width="700"/>
<img src="https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/prototype.png?raw=true" width="700"/>

---

## 🤝 협업 툴

**GitHub:** 코드 버전 관리 및 협업

**Notion:** 프로젝트 문서 정리 및 일정 관리

**Figma:** 구조 설계 및 프로토타이핑

**Google Sheets:** 게임 데이터 관리

---

## 🔧 사용 기술 스택

**개발 엔진:** Unity

**프로그래밍 언어:** C#

**버전 관리:** GitHub

**데이터 관리:** Google Sheets

**라이브러리:** DOTween (애니메이션 및 트윈 효과 적용)

---
## 🤔 기술적 이슈와 해결 과정 

### 문제 : 리포지토리 패턴을 활용한 씬 전환 및 데이터 관리 개선
**📍 원인 분석**  
- 각 Manager의 역할 과다: 매니저 클래스가 데이터 관리까지 담당하며 과도한 책임이 집중되었습니다.  
- 매니저 간 결합도 문제: 여러 매니저 클래스가 서로 직접 참조하면서 결합도가 높아 유지보수가 어려웠습니다.  
- 씬 전환 시 객체 참조 문제: 씬 전환 시 DontDestroyOnLoad로 유지되는 객체가 있는 반면, 일부 객체는 삭제되면서 NullReferenceException 오류가 발생했습니다.  
- Player 상태 유지 어려움: Player가 각 씬마다 개별 배치되어 상태 유지가 어렵고, 데이터 공유가 원활하지 않았습니다.  

**💡 해결 방법**  
✔ 책임 분리: Manager가 직접 데이터 관리를 담당하지 않도록 리포지토리 패턴을 적용하여, 데이터는 각각의 Repository에서 관리하도록 변경했습니다.  
✔ 매니저 간 결합도 감소: 각 매니저가 직접 참조하던 객체를 리포지토리를 통해 접근하도록 수정하여 클래스 간 의존성을 낮추고 유지보수를 용이하게 했습니다.  
✔ 객체 참조 문제 해결: 리포지토리 패턴을 적용하여 DontDestroyOnLoad를 적용할 객체와 씬 전환 시 재생성할 객체를 분리하여 NullReferenceException이 발생하지 않도록 개선했습니다.  
✔ Player 상태 유지 개선: Player를 씬에 직접 배치하는 대신 GameRepository에서 프리팹과 위치 정보를 불러와 동적으로 생성하도록 변경하여 씬 전환 후에도 일관된 상태를 유지할 수 있도록 했습니다.  

**🎯 결과 및 개선 효과**  
✅ 책임 분리: Manager 클래스의 역할을 최소화하고, 데이터 관리를 리포지토리에서 담당하도록 변경하여 유지보수성을 향상시켰습니다.  
✅ 확장성 증가: 새로운 데이터를 추가하거나 변경할 때 기존 코드를 수정할 필요 없이 리포지토리만 수정하면 되도록 구조를 개선했습니다.  
✅ 버그 감소: 씬 변경 시 객체 참조 방식이 명확해져 NullReferenceException 발생 가능성이 줄어들었으며 안정적인 데이터 관리가 가능해졌습니다.  

---

## 📹 플레이 영상

**[![유튜브](https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/main.png?raw=true)](https://youtu.be/0VoYswxdsbM)**

---

## 🕹️ 플레이 링크  
**👉 [플레이하기](https://play.unity.com/ko/games/ea0ef296-8fc8-443e-8812-5eb56b48b2d9/legenoofouch)**

웹 브라우저에서 바로 플레이할 수 있습니다! 🎮   

---


