using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Vector
{
    [JsonPropertyName("coordinates")] private double[] coordinates;

    public Vector()
    {
        this.coordinates = new double[0];
    }

    public Vector(double[] coordinates)
    {
        this.coordinates = coordinates;
    }
    
    public void IncreaseDimension(int additionalDimensions)
    {
        Array.Resize(ref coordinates, coordinates.Length + additionalDimensions);
    }

    public void DecreaseDimension(int reducedDimensions)
    {
        if (reducedDimensions >= coordinates.Length)
        {
            throw new ArgumentException("Кількість зменшених розмірностей не може перевищувати поточні розмірності.");
        }

        Array.Resize(ref coordinates, coordinates.Length - reducedDimensions);
    }
    
    public Vector Sum(Vector vector1)
    {
        double[] result = new double[vector1.coordinates.Length];
        for (int i = 0; i < vector1.coordinates.Length; i++)
        {
            result[i] = vector1.coordinates[i] + coordinates[i];
        }

        return new Vector(result);
    }
    
    public Vector Multiply(Vector vector1)
    {
        if (vector1.coordinates.Length != coordinates.Length)
        {
            throw new ArgumentException("Вектори повинні мати однакову кількість розмірностей для виконання множення.");
        }

        double[] result = new double[vector1.coordinates.Length];
        for (int i = 0; i < vector1.coordinates.Length; i++)
        {
            result[i] = vector1.coordinates[i] * coordinates[i];
        }

        return new Vector(result);
    }
    
    public Vector Difference(Vector vector1)
    {
        if (vector1.coordinates.Length != coordinates.Length)
        {
            throw new ArgumentException("Вектори повинні мати однакову кількість розмірностей для обчислення різниці.");
        }

        double[] result = new double[vector1.coordinates.Length];
        for (int i = 0; i < vector1.coordinates.Length; i++)
        {
            result[i] = vector1.coordinates[i] - coordinates[i];
        }

        return new Vector(result);
    }
    
    public void ScalarMultiply(double scalar)
    {
        for (int i = 0; i < coordinates.Length; i++)
        {
            coordinates[i] *= scalar;
        }
    }
    
    public double Magnitude()
    {
        double sumOfSquares = 0;
        foreach (double coordinate in coordinates)
        {
            sumOfSquares += coordinate * coordinate;
        }
        return Math.Sqrt(sumOfSquares);
    }
    
    public void Print()
    {
        if (coordinates == null)
        {
            Console.WriteLine("Vector coordinates are null.");
            return;
        }

        Console.Write("(");
        for (int i = 0; i < coordinates.Length; i++)
        {
            Console.Write(coordinates[i]);
            if (i < coordinates.Length - 1)
            {
                Console.Write(", ");
            }
        }
        Console.WriteLine(")");
    }

    public void A(string filePath)
    {
        for (int i = 0; i < coordinates.Length; i++)
        {
            File.WriteAllText(filePath, coordinates[i].ToString());
        }
    }
    
    public void SaveToJson(string filePath)
    {
        string json = JsonSerializer.Serialize(this);
        File.WriteAllText(filePath, json);
        Console.WriteLine("Object saved to JSON file successfully.");
    }

    public static Vector LoadFromJson(string filePath)
    {
        string json = File.ReadAllText(filePath);
        Vector vector = JsonSerializer.Deserialize<Vector>(json);
        Console.WriteLine("Object loaded from JSON file successfully.");
        return vector;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Vector vector1 = new Vector(new double[] { 1, 2, 3 });
        Vector vector2 = new Vector(new double[] { 4, 5, 6 });
        
        Console.WriteLine("Вектор 1:");
        vector1.Print();

        Console.WriteLine("Вектор 2:");
        vector2.Print();

        Console.WriteLine("Сума векторiв:");
        vector1.Sum(vector2).Print();

        Console.WriteLine("Рiзниця векторiв:");
        vector1.Difference(vector2).Print();

        Console.WriteLine("Довжина вектора 1: " + vector1.Magnitude());
        Console.WriteLine("Довжина вектора 2: " + vector2.Magnitude());

        Console.WriteLine("Скалярний добуток вектора 1:");
        vector1.ScalarMultiply(2);
        vector1.Print();

        Console.WriteLine("Скалярний добуток вектора 2:");
        vector2.IncreaseDimension(1);
        vector2.Print();

        Console.WriteLine("Зменшення розмiрностi вектора 1:");
        vector1.DecreaseDimension(1);
        vector1.Print();
        
        Console.WriteLine("Зменшення розмiрностi вектора 2:");
        vector2.DecreaseDimension(1);
        vector2.Print();

        string jsonFilePath = "vector.json";
        vector1.SaveToJson(jsonFilePath);
        vector2.SaveToJson(jsonFilePath);
        
        Vector loadedVector = Vector.LoadFromJson(jsonFilePath);
        //loadedVector.Print();
    }
}
