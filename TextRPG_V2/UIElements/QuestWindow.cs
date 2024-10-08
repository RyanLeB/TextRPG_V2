﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_V2.Shop_Quests;

namespace TextRPG_V2.UIElements
{
    public class QuestWindow : UIWindow
    {
        private List<Quest> quests;

        public QuestWindow(int width, int height, List<Quest> quests) : base(width, height)
        {
            this.quests = quests;
        }

        public void UpdateQuests(List<Quest> quests)
        {
            this.quests = quests;
            ClearContents();
            int linePos = 1;
            foreach (var quest in quests)
            {
                if (!quest.isCompleted)
                {
                    AddLine(linePos++, $"Quest: {quest.Name} - Progress: {quest.GetProgress()}");

                }
                    
            }
        }
    }
}
