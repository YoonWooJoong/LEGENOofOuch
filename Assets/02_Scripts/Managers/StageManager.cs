using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageManager : MonoBehaviour
{
    public class CharacterSelcet : MonoBehaviour
    {
        public Image StageImage; //스테이지 이미지 배열
        public Text StageName; // 스테이지 이름
        public Sprite[] StageImages; //스테이지 이미지 배열
        public string[] StageNames; // 스테이지 이름

        private int StageIndex = 0; // 선택된 캐릭터 인덱스



        void UpdateStageUI()
        {
            if (StageImages.Length > 0 && StageNames.Length > 0)
            {
                StageImage.sprite = StageImages[StageIndex];
                StageName.text = StageNames[StageIndex];
            }
        }

        public void NextStage()
        {
            StageIndex = (StageIndex + 1) % StageImages.Length;
            UpdateStageUI();
        }

        public void PreviousStage()
        {
            StageIndex = (StageIndex - 1) % StageImages.Length;
            UpdateStageUI();
        }


        void Start()
        {
            UpdateStageUI();
        }
    }
}
