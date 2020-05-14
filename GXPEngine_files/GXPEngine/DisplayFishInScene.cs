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
            scene1Fishes[0] = new Fish(foodList, 6, "Fresh water", "European Perch", "It belongs to the genus perch and can\nbe found in most of Europe and Siberia,\nbut has also been introduced to Oceania and South\nAfrica. It is typically less than 25 cm long and has a\ngreenish base color with 5 to 9 dark green bars on\nits body. Their meals mostly consist of worms,\ninsects, and smaller fish.",30000,3000,1,1,1);
            scene1Fishes[1] = new Fish(foodList, 6, "Fresh water", "Northern Pike", "Its name translated literally means “water wolf”\nThis name comes from them eating most fishes \nsmaller than them. They can also eat smaller\nnorthern pikes. They can typically be\nfound in most of North America and Europe.\nTheir average length is between 40 and\n55cm and they can be identified\nby their long body and their bright green color", 30000, 3000, 3, 2,1);
            scene1Fishes[2] = new Fish(foodList, 6, "Fresh water", "Walleye", "It is related to the Northern pike which can also be\nfound in this aquarium. It is native to most of the\nUnited States of America and Canada. Walleyes are\nlargely olive and gold in color and their unique name\ncomes from the position of their eyes which point\noutwards as if they arelooking at the walls.\nTheir diet mostly consists of smaller fish", 30000, 3000, 5, 3,1);
            scene1Fishes[3] = new Fish(foodList, 7, "Fresh water", "Tench", "It is known for being a quiet enduring fish with it is\nmostly found in muddy and overgrown lakes in\nEurope and eastern Russia where it can survive\neven with low oxygen concentration. Its maximum\nsize is at 70cm and it can be recognized by its\ndarker olive-green skin and its almost golden\ncolor below.", 30000, 3000, 20, 1,5);
            scene1Fishes[4] = new Fish(foodList, 5, "Fresh water", "Sturgeon", "It is quite a rare sight these days as it is considered an\nalmost extinct species. Their unique look has not\nchanged since their earliest fossil record earning\nit the name of “living fossil”. They can grow up to\nbetween 2 and 3 ½ metersand are hunted\nfor their meat and caviar.\nThe Sturgeons range is from subtropical\nto subarctic waters in North America and Eurasia.", 30000, 3000, 50, 1,10);

            fishListPerScene.Add(scene1Fishes[0]);
            fishListPerScene.Add(scene1Fishes[1]);
            fishListPerScene.Add(scene1Fishes[2]);
            fishListPerScene.Add(scene1Fishes[3]);
            fishListPerScene.Add(scene1Fishes[4]);
        }
        void loadScene2(List<Food> foodList, List<Fish> fishListPerScene)
        {
            scene2Fishes[0] = new Fish(foodList, 8, "Sea water", "Clownfish", "It is mostly known from the movie “Finding Nemo”.\n They live in anemones all around the globe since\ntheir toxin provides protection against predators.\nTheir average size is between 7 and 8 cm and\nthey consume small plants and algae.\nThey can be easily identified by their bright orange\nbase colour with white stripes.", 40000, 3000, 100, 3, 20);
            scene2Fishes[1] = new Fish(foodList, 8, "Sea water", "Schooling Bannerfish", "It is a butterflyfish native to the Indo-Pacific area.\nIt has a large depth range and is usually observed\nat 5–30 m depth, but has also been observed at\ndepths of up to 210 meters. It feeds on plankton\nand smaller examples sometimes act as\ncleaner fish.", 40000, 3000, 150, 2, 50);
            scene2Fishes[2] = new Fish(foodList, 7, "Sea water", "Pallette Tang", "It is easily identifiable and a favourite\namong aquarium keepers for its vibrant colors.\nThey live in pairs or in small groups from\neast Africa to Australia. They mostly feast on\nalgae and it can grow up to 30 cm. ", 40000, 3000, 300, 3, 50);
            scene2Fishes[3] = new Fish(foodList, 4, "Sea water", "Bluespotted Ray", "It is a species of stingray that can be\nfound in most of the indian ocean and parts of\nthe western pacific ocean. Its blue spotted\nback and the blue strip on its tail makes it\neasily identifiable. Its tail can be used as a weapon\nbut most of the time they flee instead of\nattacking humans.It is a fairly small ray, not\nexceeding 35 cm and their main food source consist\nof crabs and small fish.", 40000, 3000, 500, 2, 100);
            scene2Fishes[4] = new Fish(foodList, 8, "Sea water", "Red Lionfish", "It is a highly venomous coral reef fish.\nThe spikes on its back are highly venomous but in\nmost cases not deadly to humans but will cause\nextreme pain, and possibly headaches, vomiting, and\nbreathing difficulties.Thankfully the spikes are\nmostly used for defensive purposesonly\nas it mostly lives off smaller fish. It can grow up\nto a size of 47 cm and can\nbe found incoral reefs on the coast of\nthe USA and the Mediterranean.", 40000, 3000, 800, 3, 100);
            fishListPerScene.Add(scene2Fishes[0]);
            fishListPerScene.Add(scene2Fishes[1]);
            fishListPerScene.Add(scene2Fishes[2]);
            fishListPerScene.Add(scene2Fishes[3]);
            fishListPerScene.Add(scene2Fishes[4]);
        }

        void loadScene3(List<Food> foodList, List<Fish> fishListPerScene)
        {
            scene3Fishes[0] = new Fish(foodList, 4, "Deep water", "Black Swallower", "It is a species of deep sea fish with the ability\nto swallow fish larger than itself by extending\nhis stomach.Black swallowers\nhave been found to have swallowed fish so\nlarge that they could not be digested before\ndecomposition set in, and the resulting release of\ngases forced the swallower to the ocean\nsurface. The fish has a maximum known length of\n25 cm and it is very common and widespread at\ndepths of 700–2,745 m in most of the\nAtlantic Ocean.", 60000,3000,1000,1,500);
            scene3Fishes[1] = new Fish(foodList, 5, "Deep water", "Pelican Eel", "It is a deep-sea eel which takes its name from its\nlarge lower jaw which they can use to swallow prey\nmuch larger than itself. There have not been many\ninteractions with living pelican eels as most specimens\nwere found dead in fishing nets. They can grow up\nto length of 75 cm and can be found in most parts\nof the Atlantic between the depth\nof 500 to 3,000 m.", 60000,3000,2000,3,200);
            scene3Fishes[2] = new Fish(foodList, 4, "Deep water", "Sloane's Viperfish", "It is a deep sea dragonfish which stands out from\nother fish by having one of the highest teeth\nto body size ratio in comparison to other fish.\nThey mostly feast on smaller fish and due to their\nlow energy consumption it is estimated that they\nonly have to eat a fish every 12 days. Its size can\nrange from 64 mm to 260 mm and it can be found\nin most oceans between the depth of\n200 m - 1000 m.", 60000,3000,4000,2,500);
            scene3Fishes[3] = new Fish(foodList, 4, "Deep water", "Humpback Anglerfish", "It is a species of black sea devil that is found most\ncommonly at depths between 100 and 1500\nmeters in most parts of the chinese sea the\nEastern Pacific Ocean.The females have the unique\nability to attract its prey with a small\nbioluminescent light attached to its forehead which\ngives the species its unique name. The mostly\nfeed of smaller fish but can also consume fish\nheavier than itself.", 60000,3000,8000,3,500);
            scene3Fishes[4] = new Fish(foodList, 7, "Deep water", "Vampire Squid", "It is a deep sea squid with a maximum length\nof 30 cm. The literal translation of its name\nmeans 'vampire squid from Hell'. It can be found\nin the depth of 600 to 900 metres. Its most\nunique features are its big red eyes and the way\nit curls its arms up outwards and wraps them\naround its body, turning itself inside-out\nin a way, exposing spikes to deter\npotential enemies..", 60000,3000,20000,1,1000);
            fishListPerScene.Add(scene3Fishes[0]);
            fishListPerScene.Add(scene3Fishes[1]);
            fishListPerScene.Add(scene3Fishes[2]);
            fishListPerScene.Add(scene3Fishes[3]);
            fishListPerScene.Add(scene3Fishes[4]);
        }
    }
}
