using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class DisplayFishInScene : GameObject
{
    private Fish[] scene1Fishes, scene2Fishes, scene3Fishes;
    //------------------------------------------------------------------------
    //                          Counstructor
    //------------------------------------------------------------------------
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
    //----------------------------------------------------------------------------------------------
    //                          loadScene1
    //----------------------------------------------------------------------------------------------
    private void loadScene1(List<Food> foodList, List<Fish> fishListPerScene)
    {
        scene1Fishes[0] = new Fish(foodList, 6, "Fresh water", "European Perch", "It belongs to the genus perch and can\nbe found in most of Europe and Siberia\nbut has also been introduced to Oceania and South\nAfrica. It is typically less than 25 cm long and has a\ngreenish base color with 5 to 9 dark green bars on\nits body. Their meals mostly consist of worms,\ninsects, and smaller fish.", 30000, 3000, 1, 1, 1);
        scene1Fishes[1] = new Fish(foodList, 6, "Fresh water", "Northern Pike", "Its name translated literally means “water wolf”\nThis name comes from them eating most fishes smaller\nthan them with themsometimes even eating other\nnorthern pikes. They can typically befound in most of\nNorth America and Europe. Their average length is\nbetween 40 and 55cm and they can be identified\nby their long body and their bright green color", 30000, 3000, 3, 2, 1);
        scene1Fishes[2] = new Fish(foodList, 6, "Fresh water", "Walleye", "It is related to the Northern pike which can also be\nfound in this aquarium. It is native to most of the\nUnited States of America and Canada. Walleyes are\nlargely olive and gold in color and their unique name\ncomes from the position of their eyes which point\noutwards as if they arelooking at the walls.\nTheir diet mostly consists of smaller fish", 30000, 3000, 5, 3, 1);
        scene1Fishes[3] = new Fish(foodList, 7, "Fresh water", "Tench", "It is known for being a quiet enduring fish with it is\nmostly found in muddy and overgrown lakes in Europe\nand eastern Russia where it can survive even with\nlow oxygen concentration. Its maximum size is at 70cm and it can\nbe recognized by its darker olive-green skin and\nits almost golden color below.", 30000, 3000, 20, 1, 5);
        scene1Fishes[4] = new Fish(foodList, 5, "Fresh water", "Sturgeon", "It is quite a rare sight these days as it is considered an\nalmost extinct species. Their unique look has not\nchanged since their earliest fossil record earning\nit the name of “living fossil”. They can grow up to\nbetween 2 and 3 ½ metersand are hunted\nfor their meat and caviar.\nThe Sturgeons range is from subtropical\nto subarctic waters in North America and Eurasia.", 30000, 3000, 50, 1, 10);

        fishListPerScene.Add(scene1Fishes[0]);
        fishListPerScene.Add(scene1Fishes[1]);
        fishListPerScene.Add(scene1Fishes[2]);
        fishListPerScene.Add(scene1Fishes[3]);
        fishListPerScene.Add(scene1Fishes[4]);
    }
    //----------------------------------------------------------------------------------------------
    //                          loadScene2
    //----------------------------------------------------------------------------------------------
    private void loadScene2(List<Food> foodList, List<Fish> fishListPerScene)
    {
        scene2Fishes[0] = new Fish(foodList, 8, "Sea water", "Clownfish", "It is mostly known from the movie “Finding Nemo”.\n They live in anemones all around the globe since\ntheir toxin provides protection against predators.\nTheir average size is between 7 and 8 cm and\nthey consume small plants and algae.\nThey can be easily identified by their bright orange\nbase colour with white stripes.", 40000, 3000, 100, 3, 20);
        scene2Fishes[1] = new Fish(foodList, 8, "Sea water", "Schooling Bannerfish", "It is a butterflyfish native to the Indo-Pacific area.\nIt has a large depth range and is usually observed\nat 5–30 m depth, but has also been observed at\ndepths of up to 210 meters. It feeds on plankton\nand smaller examples sometimes act as\ncleaner fish.", 40000, 3000, 150, 2, 50);
        scene2Fishes[2] = new Fish(foodList, 7, "Sea water", "Pallette Tang", "It is easily identifiable and a favourite\namong aquarium keepers for its vibrant colors.\nThey live in pairs or in small groups from\neast Africa to Australia. They mostly feast on\nalgae and it can grow up to 30 cm. ", 40000, 3000, 300, 3, 50);
        scene2Fishes[3] = new Fish(foodList, 4, "Sea water", "Bluespotted Ray", "It is a species of stingray that can be\nfound in most of the indian ocean and parts of\nthe western pacific ocean. Its blue spotted\nback and the blue strip on its tail makes it\neasily identifiable. Its tail can be used as a weapon\nbut most of the time they flee instead of\nattacking humans.It is a fairly small ray, not exceeding\n35 cm and their main food source consist\nof crabs and small fish.", 40000, 3000, 500, 2, 100);
        scene2Fishes[4] = new Fish(foodList, 8, "Sea water", "Red Lionfish", "It is a highly venomous coral reef fish.\nThe spikes on its back are highly venomous but in\nmost cases not deadly to humans but will cause\nextreme pain, and possibly headaches, vomiting, and\nbreathing difficulties.Thankfully the spikes are\nmostly used for defensive purposesonly\nas it mostly lives off smaller fish. It can grow up\nto a size of 47 cm and can\nbe found incoral reefs on the coast of\nthe USA and the Mediterranean.", 40000, 3000, 800, 3, 100);
        fishListPerScene.Add(scene2Fishes[0]);
        fishListPerScene.Add(scene2Fishes[1]);
        fishListPerScene.Add(scene2Fishes[2]);
        fishListPerScene.Add(scene2Fishes[3]);
        fishListPerScene.Add(scene2Fishes[4]);
    }
    //-----------------------------------------------------------------------------------------------
    //                          loadScene3
    //-----------------------------------------------------------------------------------------------
    private void loadScene3(List<Food> foodList, List<Fish> fishListPerScene)
    {
        scene3Fishes[0] = new Fish(foodList, 4, "Deep water", "Black Swallower", "It is a species of deep sea fish with\nthe ability to swallow fish larger than itself by\nextending his stomach.Black swallowers have been found to have\nswallowed fish so large that they could not be digested\nbefore decomposition set in, and the resulting release\nof gases forced the swallower to the ocean surface.\nThe fish has a maximum known length of 25 cm and\nit is very common and widespread at depths of\n700–2,745 m in most of the Atlantic Ocean.", 60000, 3000, 1000, 1, 500);
        scene3Fishes[1] = new Fish(foodList, 5, "Deep water", "Pelican Eel", "It is a deep-sea eel which takes its name\nfrom its large lower jaw which they can use to swallow\nprey much larger than itself. There have not been many\ninteractions with living pelican eels as most specimens were\nfound dead in fishing nets. They can grow up to length\nof 75 cm and can be found in most parts of the atlantic\nbetween the depth of 500 to 3,000 m.", 60000, 3000, 2000, 3, 200);
        scene3Fishes[2] = new Fish(foodList, 4, "Deep water", "Sloane's Viperfish", "It is a deep sea dragonfish which stands\nout from other fish by having one of the highest\nteeth to body size ratio in comparison to other fish.\nThey mostly feast on smaller fish and due to their\nlow energy consumption it is estimated that they only\nhave to eat a fish every 12 days. Its size can range\nfrom 64 mm to 260 mm and it can be found in most\noceans between the depth of 200 m - 1000 m.", 60000, 3000, 4000, 2, 500);
        scene3Fishes[3] = new Fish(foodList, 4, "Deep water", "Humpback Anglerfish", "It is a species of black sea devil that is found most\ncommonly at depths between 100 and 1500\nmeters in most parts of the chinese sea the eastern\npacific ocean.The females have the unique ability to\nattract its prey with a small bioluminescent light\nattached to its forehead which gives the species its\nunique name. The mostly feed of smaller fish but can\nalso consume fish heavier than itself.", 60000, 3000, 8000, 3, 500);
        scene3Fishes[4] = new Fish(foodList, 7, "Deep water", "Vampire Squid", "It is a deep sea squid with a maximum length\nof 30 cm. The literal translation of its name\nmeans 'vampire squid from Hell'. It can be found in the depth of\n600 to 900 metres. Its most unique features are its big\nred eyes and the way it curls its arms up outwards and\nwraps them around its body, turning itself inside-out in\na way, exposing spikes to deter potential enemies..", 60000, 3000, 20000, 1, 1000);
        fishListPerScene.Add(scene3Fishes[0]);
        fishListPerScene.Add(scene3Fishes[1]);
        fishListPerScene.Add(scene3Fishes[2]);
        fishListPerScene.Add(scene3Fishes[3]);
        fishListPerScene.Add(scene3Fishes[4]);
    }
}

