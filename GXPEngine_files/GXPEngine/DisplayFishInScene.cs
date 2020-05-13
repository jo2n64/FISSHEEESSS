using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class DisplayFishInScene : GameObject
    {
        Fish[] scene1Fishes, scene2Fishes, scene3Fishes;
        public DisplayFishInScene(int sceneNumber, List<Food> foodList, List<Fish> fishListPerScene)
        {
            scene1Fishes = new Fish[5];
            scene2Fishes = new Fish[5];
            scene3Fishes = new Fish[5];
            switch (sceneNumber)
            {
                case 1:
                    loadScene1(foodList, fishListPerScene);
                    break;
                case 2:
                    loadScene2(foodList, fishListPerScene);
                    break;
                case 3:
                    loadScene3(foodList, fishListPerScene);
                    break;
            }

        }

        void loadScene1(List<Food> foodList, List<Fish> fishListPerScene)
        {
            scene1Fishes[0] = new Fish(foodList, 6, "Fresh water", "European Perch", "It belongs to the genus perch and can\nbe found in most of Europe and Siberia\nbut has also been introduced to Oceania and South Africa.\nIt is typically less than 25 cm long and has a greenish\nbase color with 5 to 9 dark green bars on its body.\nTheir meals mostly consist of worms, insects, and smaller\nfish.",30000,3000,1,1,1);
            scene1Fishes[1] = new Fish(foodList, 6, "Fresh water", "Northern Pike", "Its name translated literally means “water wolf”\nThis name comes from them eating most fishes smaller\nthan them with themsometimes even eating other\nnorthern pikes. They can typically befound in most\n of North America and Europe. Their average length is between\n40 and 55cm and they can be identified\nby their long body and their bright green color", 30000, 3000, 3, 2,1);
            scene1Fishes[2] = new Fish(foodList, 6, "Fresh water", "Walleye", "It is related to the Northern pike which can also be found\nin this aquarium. It is native to most of the United States\nof America and Canada. Walleyes are largely olive and\ngold in color and their unique name comes from the\nposition of their eyes which point outwards as if they are\nlooking at the walls. Their diet mostly consists of smaller fish", 30000, 3000, 5, 3,1);
            scene1Fishes[3] = new Fish(foodList, 7, "Fresh water", "Tench", "It is known for being a quiet enduring fish with it is mostly\nfound in muddy and overgrown lakes in Europe and\neastern Russia where it can survive even with low oxygen concentration.\nIts maximum size is at 70cm and it can be recognized\nby its darker olive-green skin and\nits almost golden color below.", 20000, 3000, 20, 1,5);
            scene1Fishes[4] = new Fish(foodList, 5, "Fresh water", "Sturgeon", "It is quite a rare sight these days as it is considered an\nalmost extinct species. Their unique look has not\nchanged since their earliest fossil record earning it the name of “living fossil”.\nThey can grow up to be between 2 and 3 ½ meters\nand are hunted for their meat and caviar.\nThe Sturgeons range is from subtropical\nto subarctic waters in North America and Eurasia.", 30000, 3000, 50, 1,10);

            fishListPerScene.Add(scene1Fishes[0]);
            fishListPerScene.Add(scene1Fishes[1]);
            fishListPerScene.Add(scene1Fishes[2]);
            fishListPerScene.Add(scene1Fishes[3]);
            fishListPerScene.Add(scene1Fishes[4]);
        }
        void loadScene2(List<Food> foodList, List<Fish> fishListPerScene)
        {
            scene2Fishes[0] = new Fish(foodList, 8, "Sea water", "Clownfish", "is mostly known from the movie “Finding Nemo”. They live in anemones all around the globe since their toxin provides protection against predators. Their average size is between 7 and 8 cm and they consume small plants and algae. They can be easily identified by their bright orange base colour with white stripes.", 40000, 3000, 100, 3, 20);
            scene2Fishes[1] = new Fish(foodList, 8, "Sea water", "Schooling Bannerfish", "is a butterflyfish native to the Indo-Pacific area. It has a large depth range and is usually observed at 5–30 m depth, but has also been observed at depths of up to 210 meters. It feeds on plankton and smaller examples sometimes act as cleaner fish", 40000, 3000, 150, 2, 50);
            scene2Fishes[2] = new Fish(foodList, 7, "Sea water", "Pallette Tang", "is easily identifiable and a favourite among aquarium keepers for its vibrant colors. They live in pairs or in small groups from east Africa to Australia. They mostly feast on algae and it can grow up to 30 cm. ", 40000, 3000, 300, 3, 50);
            scene2Fishes[3] = new Fish(foodList, 4, "Sea water", "Bluespotted Ray", "is a species of stingray that can be found in most of the indian ocean and parts of the western pacific ocean. Its blue spotted back and the blue strip on its tail makes it easily identifiable. Its tail can be used as a weapon but most of the time they flee instead of attacking humans. It is a fairly small ray, not exceeding 35 cm and their main food source consist of crabs and small fish.", 40000, 3000, 500, 2, 100);
            scene2Fishes[4] = new Fish(foodList, 8, "Sea water", "Red Lionfish", "is a highly venomous coral reef fish. The spikes on its back are highly venomous but in most cases not deadly to humans but will cause extreme pain, and possibly headaches, vomiting, and breathing difficulties. Thankfully the spikes are mostly used for defensive purposes only as it mostly lives off smaller fish. It can grow up to a size of 47 cm and can be found in coral reefs on the coast of the USA and the Mediterranean.", 40000, 3000, 800, 3, 100);
            fishListPerScene.Add(scene2Fishes[0]);
            fishListPerScene.Add(scene2Fishes[1]);
            fishListPerScene.Add(scene2Fishes[2]);
            fishListPerScene.Add(scene2Fishes[3]);
            fishListPerScene.Add(scene2Fishes[4]);
        }

        void loadScene3(List<Food> foodList, List<Fish> fishListPerScene)
        {
            scene3Fishes[0] = new Fish(foodList, 4, "Deep water", "Black Swallower", "is a species of deep sea fish with the ability to swallow fish larger than itself by extending his stomach.Black swallowers have been found to have swallowed fish so large that they could not be digested before decomposition set in, and the resulting release of gases forced the swallower to the ocean surface. The fish has a maximum known length of 25 cm and it is very common and widespread at depths of 700–2,745 m in most of the Atlantic Ocean.",60000,3000,100,1,500);
            scene3Fishes[1] = new Fish(foodList, 5, "Deep water", "Pelican Eel", "pingas",60000,3000,2000,3,200);
            scene3Fishes[2] = new Fish(foodList, 4, "Deep water", "Sloane's Viperfish", "pingas",60000,3000,4000,2,500);
            scene3Fishes[3] = new Fish(foodList, 4, "Deep water", "Humpback Anglerfish", "pingas",60000,3000,8000,3,500);
            scene3Fishes[4] = new Fish(foodList, 7, "Deep water", "Vampire Squid", "pingas",60000,3000,20000,1,1000);
            fishListPerScene.Add(scene3Fishes[0]);
            fishListPerScene.Add(scene3Fishes[1]);
            fishListPerScene.Add(scene3Fishes[2]);
            fishListPerScene.Add(scene3Fishes[3]);
            fishListPerScene.Add(scene3Fishes[4]);
        }
    }
}
