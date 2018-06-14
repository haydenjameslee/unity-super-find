using UnityEngine;
using NUnit.Framework;
using SuperFindPlugin;

[TestFixture]
public class SuperFindTests {

    [Test]
    public void CannotFind() {
        SuperFindTestHelpers.CreateSceneWithRoot();

        var result = SuperFind.Find("Wrong");

        Assert.IsNull(result);
    }

    [Test]
    public void FindsByExactName_Root() {
        SuperFindTestHelpers.CreateSceneWithRoot();

        var result = SuperFind.Find("Root");

        Assert.AreEqual("Root", result.name);
    }

    [Test]
    public void FindsByName_WithSpaces() {
        SuperFindTestHelpers.CreateSceneWithSpacesInName();

        var result = SuperFind.Find("\"Child With Spaces\"");

        Assert.AreEqual("Child With Spaces", result.name);
    }

    [Test]
    public void FindsByExactName_DeepScene() {
        SuperFindTestHelpers.CreateDeepScene();

        var result = SuperFind.Find("Child");

        Assert.AreEqual("Child", result.name);
    }

    [Test]
    public void FindsByExactName_Root_Inactive() {
        SuperFindTestHelpers.CreateSceneWithRoot();
        GameObject.Find("Root").SetActive(false);

        var result = SuperFind.Find("Root");

        Assert.AreEqual("Root", result.name);
    }

    [Test]
    public void FindsByChildNotaton_DeepScene() {
        SuperFindTestHelpers.CreateDeepScene();

        var result = SuperFind.Find("Root Child");

        Assert.AreEqual("Child", result.name);
    }

    [Test]
    public void FindsByChildNotaton_TrickyDeepScene() {
        SuperFindTestHelpers.CreateTrickyDeepScene();

        var result = SuperFind.Find("Root Child");

        Assert.AreEqual("Child", result.name);
    }

    [Test]
    public void FindsByChildNotaton_DeepScene_Inactive() {
        SuperFindTestHelpers.CreateDeepScene();
        GameObject.Find("Child").SetActive(false);
        GameObject.Find("Root").SetActive(false);

        var result = SuperFind.Find("Root Child");

        Assert.AreEqual("Child", result.name);
    }

    [Test]
    public void FindsBySiblingNotation_SiblingScene_first() {
        var target = SuperFindTestHelpers.CreateSiblingScene(6, 0);

        var result = SuperFind.Find("Child(Clone):first");

        Assert.AreSame(target, result);
    }

    [Test]
    public void FindsBySiblingNotation_SiblingScene_last() {
        var target = SuperFindTestHelpers.CreateSiblingScene(6, 5);

        var result = SuperFind.Find("Child(Clone):last");

        Assert.AreEqual(target, result);
    }

    [Test]
    public void FindsBySiblingNotation_SiblingScene_Index0() {
        var target = SuperFindTestHelpers.CreateSiblingScene(6, 0);

        var result = SuperFind.Find("Child(Clone):0");

        Assert.AreEqual(target, result);
    }

    [Test]
    public void FindsBySiblingNotation_SiblingScene_Index3() {
        var target = SuperFindTestHelpers.CreateSiblingScene(6, 3);

        var result = SuperFind.Find("Child(Clone):3");

        Assert.AreEqual(target, result);
    }

    [Test]
    public void FindsBySiblingNotation_SiblingScene_Index5() {
        var target = SuperFindTestHelpers.CreateSiblingScene(6, 5);

        var result = SuperFind.Find("Child(Clone):5");

        Assert.AreEqual(target, result);
    }

    [Test]
    public void FindsBySiblingNotation_MultipleSiblingsScene_3() {
        var target = SuperFindTestHelpers.CreateMultipleSetsofSiblingsScene(3, 6, 3);

        var result = SuperFind.Find("Child(Clone):3");

        Assert.AreSame(target, result);
    }

    [Test]
    public void FindsBySiblingNotation_MultipleSiblingsScene_5() {
        var target = SuperFindTestHelpers.CreateMultipleSetsofSiblingsScene(3, 6, 5);

        var result = SuperFind.Find("Child(Clone):5");

        Assert.AreSame(target, result);
    }
}
