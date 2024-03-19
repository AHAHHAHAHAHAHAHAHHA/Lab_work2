using System;

public class Vector
{
    private double[] coordinates;
    
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
    
    public static Vector Sum(Vector vector1, Vector vector2)
    {
        if (vector1.coordinates.Length != vector2.coordinates.Length)
        {
            throw new ArgumentException("Вектори повинні мати однакову кількість розмірностей для виконання додавання.");
        }

        double[] result = new double[vector1.coordinates.Length];
        for (int i = 0; i < vector1.coordinates.Length; i++)
        {
            result[i] = vector1.coordinates[i] + vector2.coordinates[i];
        }

        return new Vector(result);
    }
    
    public static Vector Multiply(Vector vector1, Vector vector2)
    {
        if (vector1.coordinates.Length != vector2.coordinates.Length)
        {
            throw new ArgumentException("Вектори повинні мати однакову кількість розмірностей для виконання множення.");
        }

        double[] result = new double[vector1.coordinates.Length];
        for (int i = 0; i < vector1.coordinates.Length; i++)
        {
            result[i] = vector1.coordinates[i] * vector2.coordinates[i];
        }

        return new Vector(result);
    }
    
    public static Vector Difference(Vector vector1, Vector vector2)
    {
        if (vector1.coordinates.Length != vector2.coordinates.Length)
        {
            throw new ArgumentException("Вектори повинні мати однакову кількість розмірностей для обчислення різниці.");
        }

        double[] result = new double[vector1.coordinates.Length];
        for (int i = 0; i < vector1.coordinates.Length; i++)
        {
            result[i] = vector1.coordinates[i] - vector2.coordinates[i];
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
        Vector sum = Vector.Sum(vector1, vector2);
        sum.Print();

        Console.WriteLine("Рiзниця векторiв:");
        Vector difference = Vector.Difference(vector1, vector2);
        difference.Print();

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
    }
}
