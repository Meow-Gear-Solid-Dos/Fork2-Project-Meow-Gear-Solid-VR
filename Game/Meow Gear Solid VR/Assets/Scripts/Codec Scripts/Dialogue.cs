using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string callerName = "Professor Meow";

    // Text uses 3 to 10 lines
    /*[TextArea(3, 10)]
    public string[] sentences;*/

    // Default dialogue dictionary
    // Updates after certain points in game is reached
    public Dictionary<int, List<string>> defDialogue = new Dictionary<int, List<string>>()
    {
        //First event is a mission reminder.
        { 0, new List<string> {"Go on, what are you doing!", "Head towards the elevator and begin your mission."} },
        //Second Event is after you've found your gun
        { 1, new List<string> {"Glad you called! (I was getting a little lonely...)", "Ah!", "That's right, your mission.",
        "Head further into the base, there should be a Hangar where the dogs are keeping 'Meow Gear.'", "Make sure you keep an eye out for any gaurds patroling the area!",
        "Thankfully you have your handy dandy wrist mounted radar!", "Give it a look before you enter a room, you never know what's hiding in the back.",
        "Oh and by the way Paws, as you know Weapons and Equipment will be OSP (on site procurement).", "This is a weapons warehouse though.", "So I'm sure you'll be able to find whatever you need!"} },
        { 2, new List<string> {"Keep an eye out!", "As you head further into the base, the more enemies there are.", "Keep you eyes on your map,","and try to stay out of trouble!"} },
        
        { 3, new List<string> {"Watchout for open spaces!", "Large open areas can be both a blessing and a curse", "Big spaces do mean more places to stay out of sight from the gaurds,",
        "But that also mean more gaurds will be patrolling.","Keep your senses sharp, Paws!","And make sure you keep using that handy Soliton Radar on your wrist!"} },
        { 5, new List<string> {"Go on, what are you doing!", "Head towards the door and stop Meow Gear!"} }

    };
    
    // Event dialogue dictionary
    public Dictionary<string, List<string>> eventDialogue = new Dictionary<string, List<string>>()
    {
        //Plays when the player enters the elevator for the first time
        {"Event01", new List<string> {"Come in Agent Paws, this is the Professor.", "I see you've already infiltrated the Dog's base.",
        "Age hasn't slowed you down one bit!", "I'll be keeping in touch with you using the Codec system.", "If you have any questions, give my number a call using the 'Call' Button.",
        "Oh also that reminds me.","The 'Call' button is bound to the 'B' button on your handy dandy Oculus Touch controller!","Anyways, I'll leave you be for now. Goodluck out there!"} },

        {"Event02", new List<string> {"Hey, great work out there!", "Oh by the way, have you run into any weapons yet?", "I'm sure by now there's got to have been at least one basic pistol.",
        "That reminds me!", "If you see any object that's spinning, that means you can pick it up and add it to your inventory!","Oh and you can't see it, but to your right is your holster.",
        "Put any item you get tired of holding in it!", "Now get out there and find some pickups!", "See if there's any spinning guns or med kits nearby!"} },

        {"Event03", new List<string> {"Excellent work, Paws!","Just beyond that door is the next level of your mission", "Be careful, while the area is much more open outside...",
        "It also has significantly more gaurds patrolling!", "Hopefully you found some useful tools here to make things easier out there.", "You got this!"} },
        
        {"Event04", new List<string> {"Excellent work, Paws!"} },

        {"Event05", new List<string> {"Great work out there Paws!","Just beyond that door is Meow Gear.", "You got this!"} },

        {"CodecTrigger1", new List<string> {"Why are you running."} }
    };

    public Dictionary<int, List<string>> finalDialogue = new Dictionary<int, List<string>>()
    {
        //First event is a mission reminder.
        { 0, new List<string> {"Ah yes...", "Hello... Brother...", "It would appear you finally made it to me and...", "the nuclear weapon, MEOW GEAR!",
        "I'm glad you've already done me the favor of removing your gear.", "Now we can have a battle to decide who is the superior Cat.",
        "I wont lose so easily, you better give it your all!"} }
    };
    
    public Dictionary<string, List<string>> specialDialogue = new Dictionary<string, List<string>>()
    {
        //First event is a mission reminder.
        {"Special02", new List<string> {"Paws!", "Look over there! Do you see it?", "That's not just any box...", "That's a field agent's dream!",
        "What? Don't look at me like that.", "I'm sure you more than anyone else knows just how useful a box can be", "I'm sure I don't have to tell you twice - but go pick it up!"} },
        
        {"Special01", new List<string> {"Paws! This is a great hiding spot.", "You're quite literally right under your enemy's noses. How daring.", "Do you see on top of the crate?",
        "It's a tennis ball. I'm sure if you throw it, ", "any gaurds in the are will come chasing for it!","Ha! I guess gaurd dog or not, dogs will be dogs."} },

        {"Special03", new List<string> {"What's this?", "Looks like you've found a hidden passage", "Maybe it leads to an armory. It never hurts to get a few more supplies.",
        "Why don't you go check it out!"} }
    };
}
