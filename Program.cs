using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedAdventureGame
{
    public class Location
    {
        //using this to connect exit locations, for now each location will only have one exit. 
        ArrayList exits = new ArrayList();
        ArrayList Food = new ArrayList();
        ArrayList Weaps = new ArrayList();

        //These next four methods will allow me to add or remove food or weapons from different locations 
        public void addFood(Food food)
        {
            Food.Add(food);
        }

        public void removeFood(Food food)
        {
            Food.Remove(food);
        }

        public void addWeap(Weapons weap)
        {
            Weaps.Add(weap);
        }

        public void removeWeap(Weapons weap)
        {
            Weaps.Add(weap);
        }

        //These two check methods will check to see if the player has an item, after this is checked then I can call
        //add or remove on that object. I do not want to add or drop anything that does not exist.

        public bool checkItemsRemove(string item)
        {
            bool found = false;

            //this will go through the Food array of objects
            //I will then compare the string passed in this method against the Name of each food object
            //if there is a match then that location has that food item. 
            foreach (Food food in Food)
            {
                if (item.Equals(food.Name))
                {
                    Food.Remove(food);
                    Console.WriteLine("You just picked up " + item);
                    found = true;
                    break;
                }
            }

            //this will go through the Weaps array of objects
            //I will then compare the string passed in this method against the Name of each Weapons object
            //if there is a match then that location has that weapon item. 
            foreach (Weapons weap in Weaps)
            {
                if (item.Equals(weap.Name))
                {
                    Weaps.Remove(weap);
                    Console.WriteLine("You just picked up " + item);
                    found = true;
                    break;
                }
            }

            return found;
        }

        //this method will show the different items in each location
        public void showAll()
        {
            string outPutFood = "";

            foreach (Food food in Food)
            {
                outPutFood = outPutFood + food.Name + " ";
            }

            Console.WriteLine("Food in this area: " + outPutFood);

            string outPutWeap = "";

            foreach (Weapons weap in Weaps)
            {
                outPutWeap = outPutWeap + weap.Name + " ";
            }

            Console.WriteLine("Weapons in this area: " + outPutWeap);
        }

        //creating a getter and a setter for name of the location 
        public string Name { get; set; }
        public Location(string name)
        {
            Name = name;
        }

        public void addExit(Location location)
        {
            exits.Add(location);
        }

        //showing the exits in each current location
        public void showExits()
        {
            string tempOut = "";
            foreach (Location exit in exits)
            {
                tempOut = tempOut + exit.Name + " ";
            }
            Console.WriteLine("From here you can go to " + tempOut);
        }

        //checking to see if the current location has that exit to go to
        public bool hasExit(string exit)
        {
            bool has = false;

            foreach (Location ex in exits)
            {
                if (exit.Equals(ex.Name))
                {
                    has = true;
                }
            }

            return has;
        }

    }

    public class Food
    {
        //creating a getter and setter for name and desc 
        public string Name { get; set; }
        public string Desc { get; set; }
        public Food(string name, string desc)
        {
            Name = name;
            Desc = desc;
        }

        public bool checkDrinkable(string drink)
        {
            bool check = false;

            if (Desc.Equals("Liquid") || Desc.Equals("liquid"))
            {
                check = true;
            }

            return check;
        }

        public bool checkEdible(string food)
        {
            bool check = false;

            if (Desc.Equals("Solid") || Desc.Equals("solid"))
            {
                check = true;
            }

            return check;
        }
    }

    public class Player
    {
        //creating a getting and setter for Age and Name
        //for now I will explicitly define the age and name of the player
        //later I will leave it up to the person playing to enter in a valid 
        //name and age to give the game a personal touch. 
        public string Name { get; set; }
        public int Health { get; set; }

        public Player(string name, int health)
        {
            Name = name;
            Health = health;
        }

        //creating an arrayList to store weapons, and an arrayList for holding food
        //after creating that I will create methods to retrieve the list to check inventory and to add items to it 
        ArrayList Food = new ArrayList();
        ArrayList weapons = new ArrayList();
        Location currentLocation;

        //this is used for dropping items 
        public bool checkItem(string item)
        {
            bool found = false;
            bool foundFood = false;
            bool foundWeap = false;
            Food tempF = null;
            Weapons tempW = null;

            //going through weapons
            foreach (Weapons weap in weapons)
            {
                if (item.Equals(weap.Name))
                {
                    foundWeap = true;
                    tempW = weap;
                    break;
                }
            }

            //going through food
            foreach (Food food in Food)
            {
                if (item.Equals(food.Name))
                {
                    foundFood = true;
                    tempF = food;
                    break;
                }
            }

            if (foundFood == true)
            {
                found = true;
                Food.Remove(tempF);
                Console.WriteLine("You just dropped " + item);
            }
            if (foundWeap == true)
            {
                found = true;
                weapons.Remove(tempW);
                Console.WriteLine("You just dropped " + item);
            }

            return found;
        }

        //adding food items 
        public void addFood(Food food)
        {
            Food.Add(food);
        }

        //will first check to see if the player actually has the food item to eat, and whether or not it is edible 
        public void eatFood(Food food)
        {

            foreach (Food f in Food)
            {
                if (f == food)
                {
                    Food.Remove(f);
                    Console.WriteLine("You just ate a " + food.Name);
                    break;
                }
                else
                {
                    Console.WriteLine("You do not have that to eat.");
                }
            }
        }

        public void drinkFood(Food food)
        {

            foreach (Food f in Food)
            {
                if (f == food)
                {
                    Food.Remove(f);
                    Console.WriteLine("You just drank a " + food.Name);
                    break;
                }
            }
        }

        //checking the weapon againts a "Global Weapon Bank" to see if what they want to pick up exists 
        public void addWeapon(Weapons weapon)
        {
            weapons.Add(weapon);
            Console.WriteLine("You just added a " + weapon.Name + ", it has a damage of " + weapon.Damage);
        }

        //checking their inventory to see if the weapon they want to drop is actually in their inventory. 
        public void removeWeapon(Weapons weapon)
        {
            if (weapons.Contains(weapon.Name))
            {
                weapons.Remove(weapon);
                Console.WriteLine("You just dropped " + weapon.Name);
            }
            else
            {
                Console.WriteLine("You do not have that to drop.");
            }
        }

        public void inventory()
        {

            string outPut = "";
            foreach (Food item in Food)
            {
                outPut = outPut + item.Name + " ";
            }
            Console.WriteLine("Food Items: " + outPut);

            string outPut1 = "";

            foreach (Weapons weap in weapons)
            {
                outPut1 = outPut1 + weap.Name + " ";
            }
            Console.WriteLine("Weapons: " + outPut1);


        }

        //setting the current location
        public void setCurrentLocation(Location currLoc)
        {
            currentLocation = currLoc;
            Console.WriteLine("You are currently in: " + currLoc.Name);
            currLoc.showExits();
        }

        //showing the current location, going to be used later with a "help" function 
        public void showCurr()
        {
            Console.WriteLine("Current Location: " + currentLocation.Name);
        }

        public void moveToNewLocation(Location location)
        {
            currentLocation = location;
            Console.WriteLine("Current Location: " + currentLocation.Name);
            currentLocation.showExits();
        }

        public void showSelf()
        {
            Console.WriteLine("You are " + Name + ", you health is at " + Health);
        }

        public bool hasWeap(Weapons weap)
        {
            bool has = false;

            foreach (Weapons wep in weapons)
            {
                if (wep == weap)
                {
                    has = true;
                    break;
                }
            }

            return has;
        }

    }

    public class Weapons
    {
        //getter for the weapon name
        public string Name { get; set; }
        public int Damage { get; set; }

        public Weapons(string name, int damage)
        {
            Name = name;
            Damage = damage;
        }
    }

    public class TextBasedAdventureGame
    {

        public void play()
        {
            //this play method will create the game. It will creat the different location, the weapons, food, the player, connect locations
            //and anything else need to do inorder to set the game up for play. 

            //creating different locaitons
            Location current;
            Location Chicago = new Location("Chicago");
            Location Texas = new Location("Texas");
            Location Iowa = new Location("Iowa");
            Location Florida = new Location("Florida");

            //connecting the exits 
            Chicago.addExit(Texas); /*Connecting them*/ Texas.addExit(Chicago);
            Chicago.addExit(Iowa);  /*Connecting them*/ Iowa.addExit(Chicago);
            Texas.addExit(Florida); /*Connecting them*/ Florida.addExit(Texas);

            //creating food items ==> "Solid" will help me indicate if a player can "Eat" or "Drink" any given food item 
            Food pizza = new Food("Pizza", "Solid");
            Food ItalianBeef = new Food("Beef", "Solid");
            Food BigRed = new Food("BigRed", "Liquid");
            Food Gogurt = new Food("Gogurt", "Liquid");
            Food Shrimp = new Food("Shrimp", "Solid");

            //creating weapons 
            Weapons Bat = new Weapons("Bat", 10);
            Weapons knive = new Weapons("Knive", 20);
            Weapons nunChucks = new Weapons("NunChucks", 15);
            Weapons nailFile = new Weapons("NailFile", 6);

            //adding the food items and weapon items to the different locations 
            Chicago.addFood(pizza);
            Chicago.addFood(ItalianBeef);
            Texas.addFood(BigRed);
            Iowa.addFood(Gogurt);
            Florida.addFood(Shrimp);

            Chicago.addWeap(knive);
            Texas.addWeap(Bat);
            Iowa.addWeap(nunChucks);
            Florida.addWeap(nailFile);

            //starting the Game, I will use a while loop, the Goal is to have the Bat in possession and get to Florida 
            //to each some Shrimp, once you do that the game will end. 

            Console.WriteLine("Enter in your frist Name: ");
            string playerName = Console.ReadLine();
            Console.WriteLine("On a scale from 1 to 100 how well do you feel?");

            //checking to see if the input result is an actual number
            int playerHealth;

            bool checkNum = int.TryParse(Console.ReadLine(), out playerHealth);

            //checking to see if the input is an actual number
            if (checkNum == true)
            {
                //checking to see if the input is less than 0 or greater than 100 
                if (playerHealth <= 0 || playerHealth > 100)
                {
                    Console.WriteLine("That is not a valid response, Ill assume you feel 100% :)");
                    playerHealth = 100;
                }
            }
            else
            {
                Console.WriteLine("That is not a valid response, Ill assume you feel 100% :)");
                playerHealth = 100;
            }

            //creating the player based off of what was entered 
            Player p1 = new Player(playerName, playerHealth);
            //setting the current starting location of the p1
            current = Chicago;

            //now that we have created the player, we can greet them, let then know the objective and start parsing out what they type. 
            Console.WriteLine("Hello " + playerName + "! your health will start at " + playerHealth + ", Welcome to your new adventure.");
            Console.WriteLine("Your goal is simple, you must have in your possesion a Bat,");
            Console.WriteLine("once you have a Bat you must make your way down south ");
            Console.WriteLine("for some shrimp. Its a simple adventure, so no worries.");
            Console.WriteLine("");
            Console.WriteLine("Here are you follwing commands, you can 'pickUp', 'drop',");
            Console.WriteLine("'eat', and 'drink' different items.");
            Console.WriteLine("You can also type 'look' to take a look at your current ");
            Console.WriteLine("location, you can type 'moveTo' to move to a different location");
            Console.WriteLine("as well as type 'inventory' to show what items");
            Console.WriteLine("you have collected from different places you visited.");
            Console.WriteLine("Please note that you can type in Help at any time for assistance.");
            Console.WriteLine("");

            p1.setCurrentLocation(current);

            bool playing = true;

            //parsing out the message the player types to and performing the right actions. 
            while (playing)
            {
                string message = Console.ReadLine();

                //splitting up the message to see if they typed in a mulit word command such as pickUp, eat, drink something, or moveTo.
                //The first element should be moveTo, drop, eat, drink, pickUp, etc
                //The second element is where, or what that we need to check to see if it is valid. 
                string[] parse = message.Split(' ');

                //handling look
                if (message.Equals("look") || message.Equals("look "))
                {
                    p1.showCurr();
                    current.showAll();
                    current.showExits();
                }
                //handling inventory
                else if (message.Equals("inventory") || message.Equals("inventory "))
                {
                    p1.inventory();
                }
                //handling help or Help
                else if (message.Equals("Help") || message.Equals("help"))
                {
                    Console.WriteLine("Here are you follwing commands, you can 'pickUp', 'drop',");
                    Console.WriteLine("'eat', and 'drink' different items.");
                    Console.WriteLine("You can also type 'look' to take a look at your current ");
                    Console.WriteLine("location, you can type 'moveTo' to move to a different location");
                    Console.WriteLine("as well as type 'inventory' to show what items");
                    Console.WriteLine("you have collected from different places you visited.");
                    Console.WriteLine("Please note that you can type in Help at any time for assistance.");
                }
                //handling drop 
                else if (parse.Count() > 1)
                {
                    if (parse[0].Equals("drop") || parse[0].Equals("drop "))
                    {
                        if (p1.checkItem(parse[1]))
                        {
                            //p1.checkItem will remove that item from them
                            //adding that dropped item to any given location 
                            if (parse[1].Equals(knive.Name))
                                current.addWeap(knive);
                            else if (parse[1].Equals(Bat.Name))
                                current.addWeap(Bat);
                            else if (parse[1].Equals(nunChucks.Name))
                                current.addWeap(nunChucks);
                            else if (parse[1].Equals(nailFile.Name))
                                current.addWeap(nailFile);
                            else if (parse[1].Equals(pizza.Name))
                                current.addFood(pizza);
                            else if (parse[1].Equals(BigRed.Name))
                                current.addFood(BigRed);
                            else if (parse[1].Equals(ItalianBeef.Name))
                                current.addFood(ItalianBeef);
                            else if (parse[1].Equals(Shrimp.Name))
                                current.addFood(Shrimp);
                            else if (parse[1].Equals(Gogurt.Name))
                                current.addFood(Gogurt);
                            else
                                Console.WriteLine("I do not recoginze that item, please check you spelling");
                        }
                        else
                        {
                            Console.WriteLine("Invalid command, silly goose!");
                        }
                    }
                    //handling pickUp
                    else if (parse[0].Equals("pickUp") || parse[0].Equals("pickUp "))
                    {
                        //checking to see if the location has that item
                        if (current.checkItemsRemove(parse[1]))
                        {
                            //the current.checkItemsRemove will remove that item from that location
                            //now we have to add that item to the p1 inventory 
                            if (parse[1].Equals(knive.Name))
                                p1.addWeapon(knive);
                            else if (parse[1].Equals(Bat.Name))
                                p1.addWeapon(Bat);
                            else if (parse[1].Equals(nunChucks.Name))
                                p1.addWeapon(nunChucks);
                            else if (parse[1].Equals(nailFile.Name))
                                p1.addWeapon(nailFile);
                            else if (parse[1].Equals(pizza.Name))
                                p1.addFood(pizza);
                            else if (parse[1].Equals(BigRed))
                                p1.addFood(BigRed);
                            else if (parse[1].Equals(ItalianBeef.Name))
                                p1.addFood(ItalianBeef);
                            else if (parse[1].Equals(Shrimp.Name))
                                p1.addFood(Shrimp);
                            else if (parse[1].Equals(Gogurt.Name))
                                p1.addFood(Gogurt);
                            else
                                Console.WriteLine("I do not recoginze that item, please check you spelling");
                        }
                    }
                    else if (parse[0].Equals("moveTo"))
                    {
                        if (parse[1].Equals(Chicago.Name) && current.hasExit(parse[1]))
                        {
                            current = Chicago;
                            p1.setCurrentLocation(current);
                        }
                        else if (parse[1].Equals(Texas.Name) && current.hasExit(parse[1]))
                        {
                            current = Texas;
                            p1.setCurrentLocation(current);
                        }
                        else if (parse[1].Equals(Iowa.Name) && current.hasExit(parse[1]))
                        {
                            current = Iowa;
                            p1.setCurrentLocation(current);
                        }
                        else if (parse[1].Equals(Florida.Name) && current.hasExit(parse[1]))
                        {
                            current = Florida;
                            p1.setCurrentLocation(current);
                        }
                        else
                            Console.WriteLine("Not a valid location, please check your exits and spelling, Thank You");
                    }
                    //handling eating 
                    else if (parse[0].Equals("eat") || parse[0].Equals("Eat"))
                    {
                        //check to see if they have that item and check if its a solid. 
                        if (parse[1].Equals(pizza.Name))
                            p1.eatFood(pizza);
                        else if (parse[1].Equals(ItalianBeef.Name))
                            p1.eatFood(ItalianBeef);
                        else if (parse[1].Equals(Shrimp.Name))
                        {
                            //check to see if the player is in Flordia, and has the Bat for the win
                            if (current.Name.Equals("Florida") && p1.hasWeap(Bat))
                            {
                                p1.eatFood(Shrimp);
                                Console.WriteLine("Congratulations, You just completed your adventure!");
                                playing = false;
                            }
                            else
                                p1.eatFood(Shrimp);

                        }
                        else
                            Console.WriteLine("You cannot eat that.");
                    }
                    //handling drinking
                    else if (parse[0].Equals("drink") || parse[0].Equals("Drink"))
                    {
                        //checking for drinkable items
                        if (parse[1].Equals(BigRed.Name))
                            p1.drinkFood(BigRed);
                        else if (parse[1].Equals(Gogurt.Name))
                            p1.drinkFood(Gogurt);
                        else
                            Console.WriteLine("No, No!");
                    }
                }
                else
                    Console.WriteLine("Invalid Command, Please Check Spelling"); 
                
            }
            Console.ReadLine(); 

        }
    }



    class Program
    {
        static void Main(string[] args)
        {

            TextBasedAdventureGame game = new TextBasedAdventureGame();
            game.play();
        }
    }
}
