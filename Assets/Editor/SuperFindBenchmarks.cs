using System.Diagnostics;
using NUnit.Framework;
using SuperFindPlugin;
using UnityEngine;
using Debug = UnityEngine.Debug;

[TestFixture]
public class SuperFindBenchmarks {

    private const int REPS = 10000;

    [Test]
    public void Benchmark_RootScene() {
        SuperFindTestHelpers.CreateSceneWithRoot();

        var watch = new Stopwatch();

        watch.Reset();
        watch.Start();
        for (int i = 0; i < REPS; i++) {
            SuperFind.Find("Root");
        }
        watch.Stop();
        var superFindTime = watch.Elapsed;

        watch.Reset();
        watch.Start();
        for (int i = 0; i < REPS; i++) {
            GameObject.Find("Root");
        }
        watch.Stop();
        var nativeTime = watch.Elapsed;

        Debug.Log("Benchmark_RootScene: Time for GameObject.Find = " + nativeTime.TotalMilliseconds + "ms. Time for SuperFind.Find = " +
                  superFindTime.TotalMilliseconds + "ms for " + REPS + " reps.");
    }

    [Test]
    public void Benchmark_ChildNotation_DeepScene() {
        SuperFindTestHelpers.CreateDeepScene();

        var watch = new Stopwatch();

        watch.Reset();
        watch.Start();
        for (int i = 0; i < REPS; i++) {
            SuperFind.Find("Root Child");
        }
        watch.Stop();
        var superFindTime = watch.Elapsed;

        watch.Reset();
        watch.Start();
        for (int i = 0; i < REPS; i++) {
            GameObject.Find("Root/Middle/Child");
        }
        watch.Stop();
        var nativeTime = watch.Elapsed;

        Debug.Log("Benchmark_ChildNotation_DeepScene: Time for GameObject.Find = " + nativeTime.TotalMilliseconds + "ms. Time for SuperFind.Find = " +
                  superFindTime.TotalMilliseconds + "ms for " + REPS + " reps.");
    }

    [Test]
    public void Benchmark_ChildNotation_TrickyDeepScene() {
        SuperFindTestHelpers.CreateTrickyDeepScene();

        var watch = new Stopwatch();

        watch.Reset();
        watch.Start();
        for (int i = 0; i < REPS; i++) {
            SuperFind.Find("Root Child");
        }
        watch.Stop();
        var superFindTime = watch.Elapsed;

        watch.Reset();
        watch.Start();
        for (int i = 0; i < REPS; i++) {
            GameObject.Find("Root/Middle/Child");
        }
        watch.Stop();
        var nativeTime = watch.Elapsed;

        Debug.Log("Benchmark_ChildNotation_TrickyDeepScene: Time for GameObject.Find = " + nativeTime.TotalMilliseconds + "ms. Time for SuperFind.Find = " +
                  superFindTime.TotalMilliseconds + "ms for " + REPS + " reps.");
    }
}
