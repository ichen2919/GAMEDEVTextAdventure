using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FindMommyAdventure : MonoBehaviour {


	//public variables that also exist outside of the code (ex in unity)
	public string currentRoom;
	public string myText;
    public bool CSLady;
    public bool storyStart;
    public int frameCount;
    public AudioSource sfxSource;
    public AudioClip blip;
    public AudioClip pickup;    
    

	//variables to store possible room connections.
	//private variables exist within the code
	private string room_north;
	private string room_south;
	private string room_west;
	private string room_east;
    private bool returnDR;
    private bool bathroomQuest;
    private bool gotGlasses;
    private bool manager;
    private bool storyEnd;
    private bool pickupSound;

	// Use this for initialization
	void Start () {

        currentRoom = "Start Screen";

        room_east = "nil";
        room_north = "nil";
        room_south = "nil";
        room_west = "nil";

        CSLady = false;
        storyStart = false;
        returnDR = false;
        bathroomQuest = false;
        gotGlasses = false;
        manager = false;
        storyEnd = false;
        pickupSound = true;
        //change text to read "We ran our scene."
       // myText = "We ran our scene.";
	}
		
	
	// Update is called once per frame
	void Update () {


        room_east = "nil";
        room_north = "nil";
        room_south = "nil";
        room_west = "nil";
        /*****************************************************************************************************************
		****************************************************************************************************************/

        //BEGINNING SCREEN
        if(currentRoom == "Start Screen"){
            myText = "One day while you are shopping with your mom, \n\nPress Space to begin!";
            if (Input.GetKeyDown(KeyCode.Space)){
                currentRoom = "dressing room";
            }
        }
        //PARKING LOT
        else if (currentRoom == "parking lot"){
            myText = "Your minivan is still sitting in the same spot in parking lot.";
            if (storyEnd)
            {
                myText += "You tightly hold onto your mom's hand as the two of you walk towards your minivan and away from the store.";
                myText += "\n\nRelive the experience? Press Space.";
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    CSLady = false;
                    storyStart = false;
                    returnDR = false;
                    bathroomQuest = false;
                    gotGlasses = false;
                    manager = false;
                    storyEnd = false;
                    currentRoom = "Start Screen";
                }
            }
            else
            {
                room_north = "entrance";
            }
		} 
		//ENTRANCE
		else if(currentRoom == "entrance"){
			room_south = "parking lot";
			room_north = "aisle 3";
			room_east = "checkout";
			room_west = "customer service";

			myText = "You are in the entrance of Target";
		}
		//CUSTOMER SERVICE
		else if(currentRoom == "customer service"){
			room_north = "aisle 2";
            room_west = "aisle 1";
			room_east = "entrance";

            //before kid is lost
            if (storyStart != true || returnDR != true)
            {
                myText = "You are in customer service.";
            }
            //customer service is empty
            else if (CSLady != true) {
                myText = "You are in customer service. \nThe desk is empty.";
            }
            //customer service is back in business
            else if (CSLady == true) {
                myText = "You are in customer service.";
                if(storyEnd != true)
                {
                    myText += "\nThe lady from the bathroom is standing at the desk.Talk to her ? \n\nPress SpaceBar to talk.";
                }
                else if (storyEnd)
                {
                    myText += "\nThe lady calls your name on the intercom and from aisle 2 you see your mom running towards you with arms open!";
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    storyEnd = true;
                }
            }
		}
		//CHECKOUT
		else if(currentRoom == "checkout"){
			room_north = "aisle 4";
            room_east = "aisle 5";
			room_west = "entrance";

			myText = "You are in checkout. The cashiers seem very irritable so you decide to find customer service.";
		}
		//AISLE 1
		else if(currentRoom == "aisle 1"){
			room_west = "dressing room";
			room_east = "aisle 2";
			room_south = "customer service";

			myText = "You are in aisle 1.";
		}
		//AISLE 2
		else if(currentRoom == "aisle 2"){
			room_west = "aisle 1";
			room_east = "aisle 3";
			room_south = "customer service";

			myText = "You are in aisle 2.";
		}
		//AISLE 3
		else if(currentRoom == "aisle 3"){
			room_west = "aisle 2";
			room_east = "aisle 4";
			room_south = "entrance";

            if (bathroomQuest)
            {
                myText = "You are in aisle 3. You see a worker.";
                if (gotGlasses)
                {
                    //sound is glitchy :( game freezes up whenever it gets to this part
                    /*sfxSource.clip = pickup;
                    if (!sfxSource.isPlaying && pickupSound == true)
                    {
                        sfxSource.Play();
                        pickupSound = false;
                    }*/
                    myText += "\nYou got the glasses! Return it to the lady in the bathroom.";
                }
                else
                {
                    myText += "\n\nPress Space to talk to him.";
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    gotGlasses = true;
                }
                
            }
            else
            {
                myText = "You are in aisle 3. You see a worker.";
            }
            
		}
		//AISLE 4
		else if(currentRoom == "aisle 4"){
			room_west = "aisle 3";
			room_east = "aisle 5";
			room_south = "checkout";

            if(gotGlasses != true && bathroomQuest)
            {
                myText = "You are in aisle 4. You see a worker.";
                if (manager)
                {
                    myText += "\nYou see that his badge says manager so you quickly run away.";
                }
                else
                {
                    myText += "\n\nPress Space to talk to him.";
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    manager = true;
                }
            }
            else
            {
                myText = "You are in aisle 4. You see a worker.";
            }

        }
		//AISLE 5
		else if(currentRoom == "aisle 5"){
			room_west = "aisle 4";
			room_north = "bathroom";
			room_south = "checkout";

			myText = "You are in aisle 5.";
		}
		//DRESSING ROOM
		else if(currentRoom == "dressing room"){
			room_east = "aisle 1";

            //before kid is lost
            if (storyStart != true)
            {
                myText = "You are in the dressing room. Mommy is still trying on all her clothes. This is BORING.";
            }
            //KID IS LOST first time returning to dressing room
            else if (storyStart == true)
            {
                myText = "You are in the dressing room. \nYou call out for your mommy but no one responds! Mommy is no longer here! Where could she be???";
                returnDR = true;
            }
            
		}
		//BATHROOM
		else if(currentRoom == "bathroom"){
			room_south = "aisle 5";

            //before kid is lost
            if (storyStart != true || returnDR != true){
                myText = "You are in the bathroom. Your bladder seems to be content.";
            }
            //customer service is empty
            else if (CSLady != true){
                myText = "You are in the bathroom. You hear someone mumbling in one of the stalls.";
                if (bathroomQuest)
                {
                    myText += "\nI'm having a terrible day! First my boyfriend breaks up with me and now my contacts have fallen out! I can't see anything.";
                    myText += "Would you be a dear and pick up my glasses from my coworker working in one of the aisles? Be careful not to let my manager know though!";
                    myText += "\n\nPress Space to give the lady her glasses.";
                    if(Input.GetKeyDown(KeyCode.Space))
                    {
                        if (gotGlasses)
                        {
                            CSLady = true;
                        }
                    }
                }
                else if(bathroomQuest != true)
                {
                    myText += "\n\nPress SpaceBar to talk to her.";
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    bathroomQuest = true;
                }
               
            }
            //customer service is back in business
            else if (CSLady == true && returnDR){
                myText = "You are in the bathroom. You bladder seems to be content. The lady in the bathroom has left.";
            }
        }



        /*****************************************************************************************************************
		****************************************************************************************************************/

        //req to get kid "LOST"

        //sound is glitchy :( game freezes whenever it reaches this sound
        /*sfxSource.clip = blip;
        if (!sfxSource.isPlaying)
        {
            sfxSource.Play();
        }*/
        if (Input.anyKeyDown)
        {
            frameCount += 1;
        }
        if (frameCount >= 6)
        {
            storyStart = true;
            if (returnDR != true)
            {
                myText += "\nMom should be done trying on clothes now. Let's head back to the dressing room.";
            }
        }

        //letting player know what task to do
        if (storyEnd)
        {
            myText += "\n\nLeave the store!";
        }
        else if(gotGlasses && CSLady)
        {
            myText += "\n\nContinue searching for mom.";
        }
        else if (bathroomQuest)
        {
            myText += "\n\nGet the glasses from the coworker and return it to the lady in the bathroom.";
        }
        else if (storyStart && returnDR)
        {
            myText += "\n\nWHERE IS MOMMY?";
        }

        // here we're checking for keyboard input
        // if a directional key is pressed we go to the corresponding room.
        myText += "\n\n";
		if (room_north != "nil"){
			myText += "Press Up to go to the " + room_north + "\n";
			if (Input.GetKeyDown(KeyCode.UpArrow)) {				
				currentRoom = room_north;
			}
		}
		if (room_south != "nil"){
            myText += "Press Down to go to the " + room_south + "\n";
            if (Input.GetKeyDown(KeyCode.DownArrow)){				
				currentRoom = room_south;
			}
		}	
		if (room_east != "nil"){
            myText += "Press Right to go to the " + room_east + "\n";
            if (Input.GetKeyDown(KeyCode.RightArrow)){				
				currentRoom = room_east;
			}
		}
		if (room_west != "nil") {
            myText += "Press Left to go to the " + room_west + "\n";
            if (Input.GetKeyDown(KeyCode.LeftArrow)){				
				currentRoom = room_west;
			}
		}


        

        //We are acccesing the text component, then using dot notation to modify the text attribute.
        GetComponent<Text>().text = myText;
	
	}
}
