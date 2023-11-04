using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public static class RandomOfferGenerator
{
    public static int[] GenerateItemValue(int itemValue, int offers, float eval, float informed)
    {
        // Zufallszahlengenerator erstellen
        Random random = new Random();

        //double eval = UnityEngine.Random.Range(minEval, maxEval);
        //double informed = UnityEngine.Random.Range(minInformed, maxInformed);

        double expectedItemValue = eval * itemValue;
        double expectedDeviation = informed * expectedItemValue;

        Debug.Log("Eval Value: " + eval.ToString("F2"));
        Debug.Log("Informed Value: " + informed.ToString("F2"));

        int[] itemValueList = new int[offers];

        for (int i = 0; i < offers; i++)
        {
            double randomNumber = GenerateRandomNumberFromNormalDistribution(expectedItemValue, expectedDeviation, random);

            while (randomNumber > expectedItemValue * 2 || randomNumber < expectedItemValue * 0.75f)
            {
                randomNumber = GenerateRandomNumberFromNormalDistribution(expectedItemValue, expectedDeviation, random);
            }

            itemValueList[i] = Mathf.RoundToInt((float)randomNumber);

            Debug.Log("Generated Item Value: " + Mathf.RoundToInt((float)randomNumber));
        }

        return itemValueList;
    }

    static double GenerateRandomNumberFromNormalDistribution(double mean, double stdDev, Random random)
    {
        double u1 = 1.0 - random.NextDouble(); // Zufallszahl zwischen 0 und 1
        double u2 = 1.0 - random.NextDouble(); // Zufallszahl zwischen 0 und 1
        double stdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); // Zufallszahl aus Standardnormalverteilung

        // Skalieren und verschieben, um den gew�nschten Mittelwert und die Standardabweichung zu erhalten
        return mean + stdDev * stdNormal;
    }
}