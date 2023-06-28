# Übung zur Vorlesung Virtual Reality

## Abgabe Aufgabe 3:


## Externals:
-Darth Artisan's Free Trees: siehe Übung 2
-Customizable Skybox https://assetstore.unity.com/packages/2d/textures-materials/sky/customizable-skybox-174576
-Stylized Wood Texture https://assetstore.unity.com/packages/2d/textures-materials/wood/stylized-wood-texture-153499

## Funktionsweise:
In zwei unterschiedlichen Szenen stehen zwei unterschiedliche Parcours zur Auswahl. 
Mit Start einer Szene beginnt der Vogel, nach vorne in Richtung der z-Achse zu fliegen. 
Dabei kann anhand des getrackten Gesichts in der unteren linken Ecke die x- und die y- Achse des Vogels gesteuert werden.
Je stärker man den Kopf von der Mitte in eine Richtung bewegt, desto stärker lenkt der Vogel in diese Richtung.
Die Geschwindigkeit des Vogels lässt sich steuern, in dem man näher an die Kamera geht, bzw. sich von ihr entfernt.

## Erläuterung:
Das Interface zum Steuern des Vogels bietet zwei Degrees of Freedom der Transformation und kein Degree of Freedom der Rotation.
Bei diesen zwei Degrees of Freedom handelt es sich um die x- und die y-Achse, die jeweils mit der Position des erkannten Gesichts gesteuert wird.
Das Facetracking-Skript bietet theoretisch die Möglichkeit, durch Erkennen der Größe des getrackten Gesichts, auch den dritten Degree fo Freedom der Transformation und somit die z-Achse zu steuern.
Wir hatten uns dagegen entschieden, da es einerseits mühselig wäre, sich durchgehend nach vorne zu lehnen, um nach vorne zu fliegen, und es andereseits für einen Parcour in Form eines langen Korridors nicht nötig ist, nach hinten in die z-Achse fliegen zu können.
Jedoch hatten wir uns dazu entschieden, diese Steuerungsoption nicht vollkommen ungenutzt zu lassen, weswegen sich damit stattdessen die Geschwindigkeit manipulieren lässt, mit welcher der Vogel nach vorne in die z-Achse fliegt.
Da das Facetracking-Skript nicht über die Möglichkeit verfügt, die Neigung eines Gesichts zu erkennen (stattdessen bei ausreichender Neigung sogar gar kein Gesicht mehr erkennt),
hatten wir uns dazu entschlossen, keinerlei Degrees of Freedom der Rotation zuzulassen. Angesichts dieser Einschränkungen haben wir uns auf Parcours in Form von langen Korridoren geeinigt, sodass der Vogel gar nicht erst gedreht werden muss.
Um sicher zu gehen, dass der Vogel nicht in der Luft stehen bleibt, sollte zwischenzeitlich kein Gesicht erkannt werden, wird in solchen Fällen stets die Position des zuletzt erkannten Gesichts verwendet, um die Flugrichtung und -geschwindigkeit des Vogels zu bestimmen.
 
Stärken:
-Dass das Schwenken des Kopfes nach links, rechts, oben und unten die Bewegung des Vogels nach links, rechts, unten und oben ähnlich eines Controlsticks steuert, ist eine verständliche Metapher und einfach zu lernen.
-Das Interface funktioniert vollständig ohne Hände, sodass diese sich auch für andere Zwecke nutzen ließen (z.B. Rotation, falls der Vogel nicht nur über die Webcam gesteuert werden sollte).
-Das Interface verfügt über eine geringe Latenz. Ein Bewegen des Kopfes führt zur zügigen Richtungsänderung im Flug des Vogels.

Schwächen:
-Man ist schnell verleitet, beim Bewegen des Kopfes diesen in die gewünschte Richtung zu drehen, was dazu führen kann, dass das Gesicht nicht mehr erkannt wird.
-Da das Gesicht bei näherem Herangehen an die Webcam eine größere Fläche einnimmt, fällt es bei höherer Geschwindigkeit insbesondere schwerer, die y-Achse des Vogels präzise zu steuern, worunter die Robustheit des Interfaces leidet.
-Den Kopf zum Steuern bewegen zu müssen, kann auf längere Zeit anstrengend sein. Das gilt vor allem für das Steuern nach oben und unten, was die Vertikalität möglicher Parcoure erheblich einschränkt. 



