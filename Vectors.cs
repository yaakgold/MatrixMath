using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldLib;

namespace MatrixMath
{
    class Vectors
    {
        public int Dimension { get; set; }

        public double[] V { get; set; }

        public Vectors(int size)
        {
            V = new double[size];
            Dimension = size;
        }

        public void Display()
        {
            Console.Write("< ");
            for (int i = 0; i < Dimension; i++)
            {
                if(i == Dimension - 1)
                {
                    Console.Write($"{V[i]} >");
                }
                else
                {
                    Console.Write($"{V[i]}, ");
                }
            }
            Console.WriteLine("");
        }

        public Vectors Display(string pref)
        {
            Console.Write(pref);
            Display();
            return this;
        }

        public void InsertValues()
        {
            for (int i = 0; i < Dimension; i++)
            {
                V[i] = Utils.GetConsoleIn<double>($"Enter value for spot: [{i}]: ");
            }

            Display();
        }

        public static Vectors Concat(Vectors v1, Vectors v2)
        {
            Vectors con = new Vectors(v1.Dimension + v2.Dimension);

            for (int i = 0; i < v1.Dimension; i++)
            {
                con.V[i] = v1.V[i];
            }

            for (int i = v1.Dimension; i < con.Dimension; i++)
            {
                con.V[i] = v2.V[i - v1.Dimension];
            }

            return con;
        }

        public static Vectors Add(Vectors v1, Vectors v2)
        {
            Vectors final = new Vectors(v1.Dimension);

            Console.Write("<\t");
            for (int i = 0; i < v1.Dimension; i++)
            {
                final.V[i] = v1.V[i] + v2.V[i];
                Console.Write($"{v1.V[i]} + {v2.V[i]}\t");
            }
            Console.WriteLine(">");

            return final.Display("ADD: ");
        }

        public static Vectors Sub(Vectors v1, Vectors v2)
        {
            Vectors final = new Vectors(v1.Dimension);

            Console.Write("<\t");
            for (int i = 0; i < v1.Dimension; i++)
            {
                final.V[i] = v1.V[i] - v2.V[i];
                Console.Write($"{v1.V[i]} - {v2.V[i]}\t");
            }
            Console.WriteLine(">");

            return final.Display("SUB: ");
        }

        public static Vectors Scale(Vectors v1, double scale)
        {
            Vectors final = new Vectors(v1.Dimension);

            Console.Write("<\t");
            for (int i = 0; i < v1.Dimension; i++)
            {
                final.V[i] = v1.V[i] * scale;
                Console.Write($"{v1.V[i]} * {scale}\t");
            }
            Console.WriteLine(">");

            return final.Display("SCALE: ");
        }

        public static Vectors UnitVect(Vectors v1)
        {
            return Scale(v1, 1 / Norm(v1)).Display("Unit Vector: ");
        }

        public static double Norm(Vectors v1)
        {
            double num = 0;

            for (int i = 0; i < v1.Dimension; i++)
            {
                num += (v1.V[i] * v1.V[i]);
                Console.WriteLine($"{v1.V[i]} * {v1.V[i]} = {v1.V[i] * v1.V[i]}");
            }
            Console.WriteLine($"SQRT({num})");

            return Math.Sqrt(num);
        }

        public static double Dot(Vectors v1, Vectors v2)
        {
            double num = 0;

            for (int i = 0; i < v1.Dimension; i++)
            {
                num += (v1.V[i] * v2.V[i]);
                Console.WriteLine($"{v1.V[i]} * {v2.V[i]} = {v1.V[i] * v2.V[i]}");
            }
            Console.WriteLine($"DOT: {num}");

            return num;
        }

        public static double DotAngle(Vectors v1, Vectors v2)
        {
            double angle = 0;
            Console.WriteLine("Numerator: ");
            double numerator = Dot(v1, v2);
            Console.WriteLine("\nDenomenator: ");
            double denomenator = Norm(v1) * Norm(v2);

            angle = Utils.RadsToDec(Math.Acos(numerator / denomenator));
            Console.WriteLine($"ANGLE: arccos({numerator} / {denomenator}) = {angle}");
            Console.WriteLine("ANGLE: " + angle);

            return angle;
        }

        public static Vectors ProjectVect(Vectors v1, Vectors v2)
        {
            Console.WriteLine("Numerator: ");
            double num = Dot(v1, v2);
            Console.WriteLine("\nDenomenator: ");
            double den = Dot(v2, v2);
            return Scale(v2, (num / den)).Display("Project Vector: ");
        }

        public static Vectors CrossProd(Vectors v1, Vectors v2)
        {
            Vectors final = new Vectors(v1.Dimension);

            final.V[0] = v1.V[1] * v2.V[2] - v1.V[2] * v2.V[1];
            Console.WriteLine($"{v1.V[1]} * {v2.V[2]} - {v1.V[2]} * {v2.V[1]} = {final.V[0]}");

            final.V[1] = v1.V[2] * v2.V[0] - v1.V[0] * v2.V[2];
            Console.WriteLine($"{v1.V[2]} * {v2.V[0]} - {v1.V[0]} * {v2.V[2]} = {final.V[1]}");

            final.V[2] = v1.V[0] * v2.V[1] - v1.V[1] * v2.V[0];
            Console.WriteLine($"{v1.V[0]} * {v2.V[1]} - {v1.V[1]} * {v2.V[0]} = {final.V[2]}");

            return final.Display("Cross Product: ");
        }

        public static double CrossAngle(Vectors v1, Vectors v2)
        {
            double angle = 0;
            Console.WriteLine("Numerator: ");
            double numerator = Norm(CrossProd(v1, v2));
            Console.WriteLine("\nDenomenator: ");
            double denomenator = Norm(v1) * Norm(v2);

            angle = Utils.RadsToDec(Math.Asin(numerator / denomenator));
            Console.WriteLine($"ANGLE: arcsin({numerator} / {denomenator}) = {angle}");
            return angle;
        }

        public static double Distance(Vectors v1, Vectors v2)
        {
            double num = 0;

            for (int i = 0; i < v1.Dimension; i++)
            {
                num += (v1.V[i] - v2.V[i]) * (v1.V[i] - v2.V[i]);
                Console.WriteLine($"({v1.V[i]} - {v2.V[i]}) * ({v1.V[i]} - {v2.V[i]}) = {(v1.V[i] - v2.V[i]) * (v1.V[i] - v2.V[i])}");
            }
            Console.WriteLine($"SQTR({num})");
            num = Math.Sqrt(num);

            return num;
        }

        public static double TripleProduct(Vectors dot, Vectors cr1, Vectors cr2)
        {
            Matrix matrix = new Matrix(3, dot.Dimension);

            matrix.FillRowsByVect(dot, 0);
            matrix.FillRowsByVect(cr1, 1);
            matrix.FillRowsByVect(cr2, 2);
            matrix.Display();
            return Matrix.Determinant(matrix, dot.Dimension);
        }

        public static void DetermineOthro(Vectors v1, Vectors v2, Vectors v3)
        {
            Matrix mat = new Matrix(3, v1.Dimension);

            mat.FillRowsByVect(v1, 0);
            mat.FillRowsByVect(v2, 1);
            mat.FillRowsByVect(v3, 2);

            mat.Display();

            //Find inverse matrix;
            Matrix inverse = new Matrix(3, 6);
            Vectors vi1 = new Vectors(3);
            vi1.V[0] = 1;
            Vectors vi2 = new Vectors(3);
            vi2.V[1] = 1;
            Vectors vi3 = new Vectors(3);
            vi3.V[2] = 1;

            inverse.FillRowsByVect(Concat(v1, vi1), 0);
            inverse.FillRowsByVect(Concat(v2, vi2), 1);
            inverse.FillRowsByVect(Concat(v3, vi3), 2);

            inverse = Matrix.RREF(inverse).Display("Inverse");

            Matrix invFinal = new Matrix(3, 3);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    invFinal.Mat[i, j] = inverse.Mat[i, j + 3];
                }
            }

            invFinal.Display("Inverse Matrix");

            mat.MultiplyMats(invFinal).Display("Ortho Test");
        }
    }
}
