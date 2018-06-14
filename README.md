# SuperFind

A replacement for Unity's GameObject.Find that supports CSS-like selectors and inactive GameObjects.

SuperFind allows you to find inactive GameObjects, use powerful selectors which make finding a GameObject much simpler and easy to maintain, and gives you a FindAll method to find all GameObjects that match your query.

**Warning**: SuperFind is approximately 10x slower than Unity's (already slow) GameObject.Find method. As with GameObject.Find, you should not use SuperFind in an update loop or any other performance sensitive code path.

## Installation

1. Download the latest .unitypackage in the [releases archive](https://github.com/haydenjameslee/unity-super-find/releases)
2. Open your Unity project
3. Double click on the downloaded package from step 1
4. Import all the files into your project

## Documentation

### Methods

#### SuperFind.Find

`GameObject found = SuperFind.Find(string selector);`

Returns the GameObject that first matches the given selector string in the active scene, including inactive GameObjects. If no GameObjects match the selector string, `null` is returned.

#### SuperFind.FindAll

`GameObject[] found = SuperFind.FindAll(string selector);`

Returns all the GameObjects that match the given selector string in the active scene, including inactive GameObjects. If no GameObjects match the selector string, the returned list will be empty.

### Selectors

Each SuperFind method takes a selector string to target the GameObject(s) you're trying to find. Selector strings can combine any of the following features. Please note that GameObject's with spaces in their name are not currently supported.

* **By name**

Similarly to `GameObject.Find`, pass the exact name of the GameObject.

Eg. `SuperFind.Find("Child");`

* **By wildcard**

Use `*` instead of the GameObject's name to select a GameObject of any name.

Eg. `SuperFind.FindAll("Parent *"); // Returns all the GameObjects under "Parent"`

* **By ascendant**

Pass any number of ascendant's names (in order from furthest to closest) each followed by a space and finish with the name of the GameObject (plus a sibling index if wanted).

Eg. `SuperFind.Find("Grandparent Child"); // Returns a Child with a GameObject named Grandparent as an ascendant`

* **By sibling index**

Add a colon and the sibling index of the GameObject you want to find after the name of the GameObject.

Eg. `SuperFind.Find("Enemy(Clone):3"); // Returns the 4th Enemy in a list of Enemy's`

* **By first sibling**

Add `:first` after the GameObject's name to find the first GameObject of that name in a list of GameObjects with that name.

Eg. `SuperFind.Find("Enemy(Clone):first"); // Returns the first Enemy in a list of Enemy's`

* **By last sibling**

Add `:last` after the GameObject's name to find the last GameObject of that name in a list of GameObjects with that name.

Eg. `SuperFind.Find("Enemy(Clone):last"); // Returns the last Enemy in a list of Enemy's`

* **By component**

Add `:` + the component's name after the GameObject's name to find a GameObject with this Component (or MonoBehaviour)

Eg. `SuperFind.FindAll("Tree:BoxCollider"); // Returns all Tree's with a BoxCollider`

* **By multiple flairs**

Add a component selector and a sibling selector to combine selectors.

Eg. `SuperFind.Find("Tree:BoxCollider:first"); // Returns the first Tree with a BoxCollider`


## Tests

See the `SuperFind/Editor/` directory for a suite of unit tests. Run them in Unity by opening the Unity Test Runner (Window -> Test Runner) and clicking `Run All`.

## Inspector

Find the SuperFind Inspector in Unity by going to `Window -> SuperFindInspector`. This inspector can be used to test selectors in realtime.


## Examples

* Exact name

`SuperFind.Find("One");`

![SuperFind.Find("One");](https://i.imgur.com/7WPWqJT.png)

* Exact name (with space)

`SuperFind.Find("\"Colliders 2\"");`

![SuperFind.Find("\"Colliders 2\"");](https://i.imgur.com/BhdNV6g.png)

* Ascendant

`SuperFind.FindAll("Three Child");`

![SuperFind.FindAll("Three Child");](https://i.imgur.com/zvYC9zf.png)

* Multiple ascendants

`SuperFind.FindAll("Three Mid Child");`

![SuperFind.FindAll("Three Mid Child");](https://i.imgur.com/zvYC9zf.png)

* Sibling index

`SuperFind.Find("Child:3");`

![SuperFind.FindAll("Child:3");](https://i.imgur.com/bdsNUbM.png)

* First sibling

`SuperFind.Find("Child:first");`

![SuperFind.Find("Child:first");](https://i.imgur.com/grZO7vK.png)

* Last sibling

`SuperFind.Find("Child:last");`

![SuperFind.Find("Child:last");](https://i.imgur.com/hudTbOK.png)

* Acendants and sibling index

`SuperFind.FindAll("Three Mid:first Child:3");`

![SuperFind.FindAll("Three Mid:first Child:3");](https://i.imgur.com/bdsNUbM.png)

* Ancestor & Wildcard

`SuperFind.FindAll("Mid *");`

![SuperFind.FindAll("Mid *");](https://i.imgur.com/N8Ehmij.png)

* Ancestor & Wildcard

`SuperFind.FindAll("Three *");`

![SuperFind.FindAll("Three *");](https://i.imgur.com/RaXOSEF.png)

* Component

`SuperFind.Find("Child:BoxCollider");`

![SuperFind.Find("Child:BoxCollider");](https://i.imgur.com/w2N3k2b.png)

* Wildcard & Component

`SuperFind.FindAll("*:BoxCollider");`

![SuperFind.FindAll("*:BoxCollider");](https://i.imgur.com/K1A3Gg1.png)

* Ancestor, Wildcard & Component

`SuperFind.FindAll("Colliders *:BoxCollider");`

![SuperFind.FindAll("Colliders *:BoxCollider");](https://i.imgur.com/GBCAnwe.png)

* Find Ancestor, Wildcard, Component & Last Sibling

`SuperFind.Find("Four *:BoxCollider:last");`

![SuperFind.Find("Four *:BoxCollider:last");](https://i.imgur.com/6Nd1uzW.png)

* FindAll Ancestor, Wildcard, Component & Last Sibling

`SuperFind.FindAll("Four *:BoxCollider:last");`

![SuperFind.FindAll("Four *:BoxCollider:last");](https://i.imgur.com/VxqjIUz.png)

