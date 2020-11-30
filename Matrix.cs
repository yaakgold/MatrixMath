using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GoldLib;

namespace MatrixMath
{
    class Matrix
    {
        public static Matrix ThreeByIdent()
        {
            Matrix m = new Matrix(3, 3);
            Vectors v1 = new Vectors(3);
            v1.V[0] = 1;

            Vectors v2 = new Vectors(3);
            v2.V[1] = 1;

            Vectors v3 = new Vectors(3);
            v3.V[2] = 1;

            m.FillRowsByVect(v1, 0);
            m.FillRowsByVect(v2, 1);
            m.FillRowsByVect(v3, 2);

            return m;
        }

        public int Rows { get; set; }

        public int Cols { get; set; }

        public double[,] Mat { get; set; }

        public Matrix(int r, int c)
        {
            Rows = r;
            Cols = c;
            Mat = new double[r, c];
        }

        public void Display()
        {
            for (int i = 0; i < Rows; i++)
            {
                Console.Write("|\t");
                for (int j = 0; j < Cols; j++)
                {
                    Console.Write(Mat[i, j] + "\t");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("\n");
        }

        public Matrix Display(string message)
        {
            Console.WriteLine(message);

            Display();

            return this;
        }

        public void InsertValues()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Mat[i, j] = Utils.GetConsoleIn<double>($"Enter value for spot: [{i} {j}]: ");
                }
            }

            Display();
        }

        public void FillRowsByVect(Vectors v, int row)
        {
            for (int i = 0; i < Cols; i++)
            {
                Mat[row, i] = v.V[i];
            }
        }

        public Matrix MultiplyMats(Matrix matrix)
        {
            Matrix final = new Matrix(Rows, matrix.Cols);

            for (int i = 0; i < Rows; ++i)
            {
                Console.Write("|\t");
                for (int j = 0; j < matrix.Cols; ++j)
                {
                    for (int k = 0; k < Cols; ++k)
                    {
                        Console.Write($"{Mat[i, k]} * {matrix.Mat[k, j]}\t");
                        final.Mat[i, j] += Mat[i, k] * matrix.Mat[k, j];
                    }
                    Console.Write("\t");
                }
                Console.WriteLine("|");
            }

            return final;
        }

        public static Matrix Concat(Matrix m1, Matrix m2)
        {
            Matrix final = new Matrix(m1.Rows, m1.Cols + m2.Cols);

            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Cols; j++)
                {
                    final.Mat[i, j] = m1.Mat[i, j];
                }
            }

            for (int i = 0; i < m2.Rows; i++)
            {
                for (int j = m1.Cols; j < m2.Cols + m1.Cols; j++)
                {
                    final.Mat[i, j] = m2.Mat[i, j - m2.Cols];
                }
            }

            return final;
        }

        public static double Determinant(Matrix m, int n)
        {
            double D = 0; // Initialize result 

            // Base case : if matrix  
            // contains single 
            // element 
            if (n == 1)
                return m.Mat[0, 0];

            // To store cofactors 
            Matrix temp = new Matrix(n, n);//new double[n, n];

            // To store sign multiplier 
            int sign = 1;

            // Iterate for each element 
            // of first row 
            for (int f = 0; f < n; f++)
            {
                Console.WriteLine("Cofactor:");
                // Getting Cofactor of mat[0][f] 
                GetCofactor(m, temp, 0, f, n);
                Console.WriteLine($"Scalar: {sign * m.Mat[0, f]}");
                D += sign * m.Mat[0, f] * Determinant(temp, n - 1);


                // terms are to be added with  
                // alternate sign 
                sign = -sign;
            }
            Console.WriteLine("Determinant: " + D + "\n\n");
            return D;
        }

        static void GetCofactor(Matrix mat, Matrix temp, int p, int q, int n)
        {
            int i = 0, j = 0;

            // Looping for each element of  
            // the matrix 
            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < n; col++)
                {

                    // Copying into temporary matrix  
                    // only those element which are  
                    // not in given row and column 
                    if (row != p && col != q)
                    {
                        temp.Mat[i, j++] = mat.Mat[row, col];

                        // Row is filled, so increase  
                        // row index and reset col  
                        //index 
                        if (j == n - 1)
                        {
                            j = 0;
                            i++;
                        }
                    }
                }
            }
            temp.Display();
        }

        public static Matrix RREF(Matrix matrix)
        {
            int lead = 0;
            int rowCount = matrix.Mat.GetLength(0);
            int colCount = matrix.Mat.GetLength(1);

            for (int r = 0; r < rowCount; r++)
            {
                if (colCount <= lead) break;
                int i = r;
                while (matrix.Mat[i, lead] == 0)
                {
                    i++;
                    if (i == rowCount)
                    {
                        i = r;
                        lead++;
                        if (colCount == lead)
                        {
                            lead--;
                            break;
                        }
                    }
                }
                for (int j = 0; j < colCount; j++)
                {
                    double temp = matrix.Mat[r, j];
                    matrix.Mat[r, j] = matrix.Mat[i, j];
                    matrix.Mat[i, j] = temp;
                }

                Console.WriteLine($"Swap rows {i} and {r}");
                matrix.Display();

                double div = matrix.Mat[r, lead];
                if (div != 0)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        matrix.Mat[r, j] /= div;
                    }

                    Console.WriteLine($"Divide row {r} by {div}");
                    matrix.Display();
                }

                for (int j = 0; j < rowCount; j++)
                {
                    if (j != r)
                    {
                        double sub = matrix.Mat[j, lead];
                        for (int k = 0; k < colCount; k++)
                        {
                            matrix.Mat[j, k] -= (sub * matrix.Mat[r, k]);
                        }

                        Console.WriteLine($"(END)Subtract {sub} multiplied by row {r} from row {j}");
                        matrix.Display();
                    }
                }
                lead++;
            }
            return matrix;
        }

        public static Matrix Rot_X(float degs)
        {
            float rads = degs.ToRadians();
            Matrix matrix = new Matrix(3, 3);

            matrix.Mat[0, 0] = 1;

            matrix.Mat[0, 1] = 0;
            matrix.Mat[0, 2] = 0;
            matrix.Mat[1, 0] = 0;
            matrix.Mat[2, 0] = 0;

            matrix.Mat[1, 1] = Math.Cos(rads);
            matrix.Mat[2, 2] = Math.Cos(rads);
            matrix.Mat[1, 2] = -Math.Sin(rads);
            matrix.Mat[2, 1] = Math.Sin(rads);

            return matrix.Display(rads + "");
        }

        public static Matrix Rot_Y(float degs)
        {
            float rads = degs.ToRadians();
            Matrix matrix = new Matrix(3, 3);

            matrix.Mat[1, 1] = 1;

            matrix.Mat[0, 1] = 0;
            matrix.Mat[1, 0] = 0;
            matrix.Mat[1, 2] = 0;
            matrix.Mat[2, 1] = 0;

            matrix.Mat[0, 0] = Math.Cos(rads);
            matrix.Mat[0, 2] = Math.Sin(rads);
            matrix.Mat[2, 0] = -Math.Sin(rads);
            matrix.Mat[2, 2] = Math.Cos(rads);


            return matrix.Display(rads + "");
        }

        public static Matrix Rot_Z(float degs)
        {
            float rads = degs.ToRadians();
            Matrix matrix = new Matrix(3, 3);

            matrix.Mat[2, 2] = 1;

            matrix.Mat[0, 2] = 0;
            matrix.Mat[1, 2] = 0;
            matrix.Mat[2, 0] = 0;
            matrix.Mat[2, 1] = 0;

            matrix.Mat[0, 0] = Math.Cos(rads);
            matrix.Mat[1, 1] = Math.Cos(rads);
            matrix.Mat[0, 1] = -Math.Sin(rads);
            matrix.Mat[1, 0] = Math.Sin(rads);


            return matrix.Display(rads + "");
        }

        public static Matrix SolveX(Matrix a, Matrix b)
        {
            Matrix m = new Matrix(a.Rows, b.Cols);

            Matrix aInv = Concat(a, ThreeByIdent());
            aInv = Matrix.RREF(aInv);

            m = aInv.MultiplyMats(b);

            return m.Display("X: ");
        }
    }
}
