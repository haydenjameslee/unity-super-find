using UnityEngine;
using NUnit.Framework;

[TestFixture]
public class SuperFindTests {

    [Test]
    public void CannotFind() {
        SuperFindTestHelpers.CreateSceneWithRoot();

        var result = SuperFind.Find("Wrong");

        Assert.IsNull(result);
    }

    [Test]
    public void FindsByExactName() {
        SuperFindTestHelpers.CreateSceneWithRoot();

        var result = SuperFind.Find("Root");

        Assert.AreEqual("Root", result.name);
    }

    [Test]
    public void FindsByExactName_DeepScene() {
        SuperFindTestHelpers.CreateDeepScene();

        var result = SuperFind.Find("Child");

        Assert.AreEqual("Child", result.name);
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
    public void FindsByExactName_Root_Inactive() {
        SuperFindTestHelpers.CreateSceneWithRoot();
        GameObject.Find("Root").SetActive(false);

        var result = SuperFind.Find("Root");

        Assert.AreEqual("Root", result.name);
    }

    [Test]
    public void FindsByChildNotaton_DeepScene_Inactive() {
        SuperFindTestHelpers.CreateDeepScene();
        GameObject.Find("Child").SetActive(false);
        GameObject.Find("Root").SetActive(false);

        var result = SuperFind.Find("Root Child");

        Assert.AreEqual("Child", result.name);
    }
}
