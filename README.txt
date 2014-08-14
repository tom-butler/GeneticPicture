GeneticPictureMaker v1.41

This project uses a genetic algorithm to create a near enough copy of an image.

Examples:
http://www.youtube.com/watch?v=dO05XcXLxGs (older version)

How it works:
The program starts with randomly positioned and coloured brushes. A fitness function that matches the colour difference between the pixel on the templete and on the image is used to score each genome. When all genomes have been scored, the worst genomes are bred off with a chance of mutation. Eventually the program returns a good likeness of the image.

Tuning:
All elements of the genetic algorithm can be tuned, Detailed help is available in the program.


Genetic Picture is an open source application released under GNU GPLv3 for more info see the 'about' section.
The application is developed by Rob Cox, and Tom Butler of 'Soft Computing Artificial Intelligence Group' a special interest group at the University of Canberra.


Directories (Built):

GeneticPicture/GeneticPictureMaker.Exe  - The program
GeneticPicture/GeneticPictureMaker.ini  - Stores the image saving directories
GeneticPicture/Logo.ico                 - Program icon
GeneticPicture/HelpText1.txt            - Stores the in program help Text
GeneticPicture/Liscence.txt             - A Copy of the GNU GPL Liscence
GeneticPicture/Templates                - Stores various useful templates
GeneticPicture/Templates/default.png    - Which must exist and be a 128 by 128 png file
GeneticPicture/Runs/example1.png        - Which is not a template but an example
GeneticPicture/Runs/Gen                 - Save every gen default output
GeneticPicture/Runs/Loop                - Multiple runs default output
GeneticPicture/Runs/Stop                - Stop output
GeneticPicture/Brushes/Pill             - Brushes of coloured 'pills'
GeneticPicture/Brushes/Panel            - Brushes of coloured 'panel'

Directories (Source):

GeneticPicture/README.Txt                                      - This readme
GeneticPicture/Liscence.txt                                    - A Copy of the GNU GPL Liscence
GeneticPicture/Testchash1/bin/Release/GeneticPictureMaker.exe  - The program
GeneticPicture/Testchash1/bin/Release/GeneticPictureMaker.ini  - Stores the image saving directories
GeneticPicture/Testchash1/bin/Release/Logo.ico                 - Program icon
GeneticPicture/Testchash1/bin/Release/HelpText1.txt            - Stores the in program help Text
GeneticPicture/Testchash1/bin/Release/Templates                - Stores various useful templates
GeneticPicture/Testchash1/bin/Release/Templates/default.png    - Which must exist and be a 128 by 128 png file
GeneticPicture/Testchash1/bin/Release/Runs/example1.png        - Which is not a template but an example
GeneticPicture/Testchash1/bin/Release/Runs/Gen                 - Save every gen default output
GeneticPicture/Testchash1/bin/Release/Runs/Loop                - Multiple runs default output
GeneticPicture/Testchash1/bin/Release/Runs/Stop                - Stop output
GeneticPicture/Testchash1/bin/Release/Brushes/Pill             - Brushes of coloured 'pills'
GeneticPicture/Testchash1/bin/Release/Brushes/Panel            - Brushes of coloured 'panel'