using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterLibrary
{
    public enum Emotion //Could make this a complex object... 
    {         // 0: Anger, 1: Anticipation, 2: Disgust, 3: Fear, 4: Joy, 5: Sadness, 6: Surprise, 7: Trust
        Apprehension, //fear
        Fear, // Core-------------------DarkGray | Fear - "Oh!  You startled me.  You never know what's lurking behind the door.  It could be
                                                   // {0} (those darned goblins) (those pesky gremlins) (those scary orcs) 
                                                   
        Terror, //FEAR
        Acceptance, //trust
        Trust, // Core----------------------Blue | Trust
        Admiration, //TRUST
        Serenity, //joy
        Joy, // Core----------------------Yellow | Joy
        Ecstasy, //JOY
        Interest, //anticipation
        Anticipation, // Core----------DarkGreen | Anticipation
        Vigilance, //ANTICIPATION
        Annoyance, //anger
        Anger, // Core-------------------DarkRed | Anger
        Rage, //ANGER
        Boredom, //disgust
        Disgust, // Core-----------------Magenta | Disgust
        Loathing, //DISGUST
        Pensiveness, //sadness
        Sadness, // Core--------------DarkYellow | Sadness
        Grief, //SADNESS
        Distraction, //surprise
        Surprise, // Core-------------------Cyan | Surprise
        Amazement, //SURPRISE
        Submission, //Fear + Trust
        Love, //Trust + Joy
        Optimism, //Joy + Anticipation
        Aggressiveness, //Anticipation + Anger
        Contempt, //Anger + Disgust
        Remorse, //Disgust + Sadness
        Disapproval, //Sadness + Surprise
        Awe, //Surprise + Fear
        Null

             
    }
}
