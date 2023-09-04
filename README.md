# Test-Snake: Documentation

The improvements that I worked on for each proposed area are the following:


## 1. Memory Management

- To improve the memory usage during runtime and the game performance, I created a pool system to manage the spawn of the "Sound" gameobject. Now there is a limit of "Sound" gameobjects that can be spawned and the game will always recycle the ones that are instanciated, avoiding the creatiion of more instances.
- Following the previous point, I used the pool system for the food spawn too, thinking of the possibility to spawn multiple "Food"" gameobjects simultaneously. The food instances will also be recycled and the code will never instanciate more that the limit configured for the pool.
- The pool system can be configured independantly for each pool, so for example the limit for the "Sound" gameobject can be 10 but for the "Food" can be 5. So it could be used in more situations, but for now I felt that it was necessary only for those two cases.

- I decided to remove some usages of .Find() and .GetComponent() functions, because even though they were mostly on the Awake() methods and they were just a few, I prefer to avoid those usages when possible because when the project grows you can end up having too many of those calls at the start of the game and that can translate into a slow start. If possible, I like to assign the references via inspector instead of calling .Find() and .GetComponent in Awake() or Start() methods. However there are some cases where that is not possible and it is better to just call those methods, as I did in some situations in the code. This point doesn't really affect the memory usage, but I decided to do it just for optimization.

- I decided to use TexturePacker to convert the game textures into a single spritesheet, and with that reduce the size that the textures have in the project. So with this approach the game will have a smaller impact in memory usage on the devices. It also makes easier to replace the textures in the project since you would only need to replace the spritesheet and not search for each one of the texture assets.

- I also implemented Asset Bundles in the project for multiple reasons, one of them being that it can reduce the memory usage on the devices too since the game will only download those assets that are going to be used, instead of having all of them always in the build. So this reduces the build size.

- To end this section, I would like to mention that in a real project I would also take a look to the Profiler section in Unity from time to time to keep the memory usage of the game controlled. For the test I just checked it once but due to the small size and complexity of the project I didn't really get useful information, since the memory usage was already small and there was no real need of fixing anything specific that could be seen in the Profiler.



## 2. Making code easier to test, and adding tests

- I decided to add tests for the most important behaviors on the game, which I think are the movement of the snake and the grid position validations.
- So first I reorganized the scripts by folders and added assemblies for each folder, so that the tests can depend only on those assemblies that are required.
- I created both Edit Mode and Play Mode tests. For the Edit Mode there's actually just one test, which consists in testing the functionally of the AddScore method. Every other test that I added needed to be a Play Mode test, and I divided them in two categories: snake movement tests and grid tests. So for the snake movement tests, they check the movement on the snake in the four directions. And for the grid tests, there are tests to check the ValidatePosition method in the four directions, the TrySnakeEatFood method when there is a food item and where there isn't, and the checking of the snake collision. For some of these tests I needed to refactor some parts of the code and move some logic into single methods, so that it allowed me to call those methods from the tests and also to have a better organised and encapsulated code.



## 3. In the future, we plan to start adding LiveOps to the game, so we will need to be able to update the game assets and config without requiring a new release

- As I mentioned in the first section, I implemented Asset Bundles in the project for multiple reasons. This is another of those reasons, since Asset Bundles allow us to update the game assets just by uploading a new asset bundle, and the code will automatically download it and update the assets on the game without needing a new build and, hence, a new release.
- I decided to separate the asset bundles in three different packs: one for sounds, one for UI (texture and prefabs) and one for game configuration.
- I created multiple configuration files. One to store the asset bundle URLs, one to store the downloaded assets from the bundles and one for the actual game configuration (values used in gameplay such as the speed of the snake, the amount of points that the food adds, etc.)
- So I implemented the creation and the download on asset bundles in the game and, for the purposes of the test, I just uploaded them on Google Drive (https://drive.google.com/drive/folders/1JEr19F5tf7xeKQ_4Efi5_H9G1ZXC6zte?usp=sharing). The asset bundles can be created by going to Assets > Create Asset Bundles and they will be generated in the GeneratedAssetBundles folder inside the project's root folder. Those bundles would be uploaded to the Drive folder overriding the previous ones, and that would be all. The game downloads them automatically from the Drive URLs at start and, when it finishes, it shows the Main Menu and the game can start. I would like to mention that currently, due to the way that Drive generates the URLs, they need to be updated on the project every time the asset bundles are updated because the URLs change (so it would require a new build anyways), but ideally the asset bundles would be in an actual server and the URLs would be always the same, so the asset bundles implementation would actually avoid needing a new build.

- As I mentioned, ideally in a real project I would use a real server or backend platform, such as PlayFab or BrainCloud, to make asset and config management easier and more maintanable, and also to be able to manage properly the user data when the game starts to have actual users.
- The asset bundles and the game config implementations probably would need to change depending on the backend platform and the code would need to adapt to it, but I decided to make a simple implementation since I am just using Google Drive as my "server" for the test. But for example, if I were to use PlayFab, the game configuration would need to be downloaded as .json files and each one of the files would need to be parsed into its corresponding class, so I would not download the config from an asset bundle anymore.

- I also convert and assign manually each asset and each config file when I download them from the asset bundles since the project is small, but ideally in a big project with a big amount of config files and assets I would try to make a different approach to automatize it and avoid having an enormous method to do that. But it would also depend on the backend platform used and in how you download the assets and config from it.



## 4. Improve the maintainability and extendability of the code

- I thought of multiple ways in which the game could be extended, and adapted the code to those potential extensions. Those are the following:
- Having different types of food that give different amount of points. I created the Food.cs class to store the food type and the score value, and there could be more values such as the probability in which a specific type spawns. With this I also avoid having the score given as a hard-coded value or having other classes have the responsibility to decide that value, since they just get it from the food item itself. I also created a prefab for the food item so that the food pool knows what to spawn. If there were multiple food prefabs the pool could be easily adapted to spawn them depending on their probability value or just using a random value.
- Having different types of snake, for example, a player snake and an AI snake. I didn't actually create an AI snake and I didn't modify the full gameplay so that it can manage multiple snakes simultaneously because that was not my point, but I just modified the Snake.cs class to make it the "base" snake class and then I created the PlayerSnake.cs as its child class, where all the player logic is stored (some initialization values, the die functionallity and the input handling). Now another type of snake can be easily created and it would automatically have the basic snake functionality (movement, growing, etc.).

- I refactored some code with the intention of separating the logic and data classes from the UI classes, since I reorganized the script folders and created assemblies for them and I wanted to have one assembly for Gameplay(logic and data) and another one for UI. This also allowed me to avoid crossed dependencies, since gameplay classes where depending on the UI classes and also the other way around, and the creation of the assemblies made me realize that easily. Now Gameplay depends on UI but UI doesn't depend on Gameplay.
- Ideally, to improve this point even more, I would use an MVC model or something similar to keep everything organised and simple and to avoid problems such as the mentioned crossed dependencies. It would improve the code readability and maintainability too. But for the test I though that I would need to refactor everything and I don't think that the point of the test was to just change everything and create a new Snake game since it asks to improve the code, so I decided to just adapt it a bit in the parts that I felt appropiate.



## 5. Reduce compile times

- For this section I though of two approaches that can reduce compile times, and both of them have been already mentioned in previous sections: Assemblies and Asset Bundles.

- The assemblies reduce the compilation time since only the modified assemblies will be recompiled when something changes in the project, instead of compilating the full project every time. So this was an extra reason to add assemblies to the project in addition to the scripts reorganization and the tests implementation.

- The asset bundles, as I already mentioned, not only avoid having to make a new build when you only need to change the assets (which obviously reduces development time), but it also reduces compilation times in case of making a new build since the assets will not be included in the build so they will be ignored during compilation.



## 6. In the future, we are considering releasing it on different platforms that might have different controls schemes

- I decided to migrate from the Unity's classic input system to the new input system so that I can specify and configure multiple control schemes depending on different platforms.

- I added three different schemes (Desktop, PlayStation and Xbox) and I added four different controls for the snake movement (WASD, arrow keys, PlayStation controller's D-Pad and Xbox controller's D-Pad) and three different controls for the pause interaction (ESCAPE key, PlayStation controller's options buttton and Xbox controller's menu button).

- So now those interactions work with all of the mentioned inputs. I didn't really change everything in the game so that it actually works in all of those consoles (for example I didn't adapt the UI buttons to be handled with a controller, it only works with the mouse input). This could be done once it is really specified in which platforms the game will run because the UI related input is more specific for each one of them and I didn't feel necessary to just adapt the game to all of them. But for this test I wanted to just change the basic input system so that control schemes are easily added, removed or modified for general input implementation (non-UI).



## Extra point: Other improvements that I thought of but didn't actually implement

I didn't implement this idea because it is not included in the test's proposed sections so I felt that it wasn't necessary to actually implement it for the purposes of the test.

- Making the UI adaptable to different devices and to the level size. The level size can be configured and the snake will move inside the full level considering that size, but the screen doesn't actually adapt to that size.



## GitHub repository
You can find the GitHub repository that I created for the project in the following link: https://github.com/andreaprez/Snake-Test
