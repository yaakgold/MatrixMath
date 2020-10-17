using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldLib;

namespace MatrixMath
{
    class Matrix
    {
        public int Rows { get; set; }

        public int Cols { get; set; }

        public float[,] Mat { get; set; }

        public Matrix(int r, int c)
        {
            Rows = r;
            Cols = c;
            Mat = new float[r, c];
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

        public void InsertValues()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Mat[i, j] = Utils.GetConsoleIn<float>($"Enter value for spot: [{i} {j}]: ");
                }
            }
        }

        public Matrix MultiplyMats(Matrix matrix)
        {
            Matrix final = new Matrix(Rows, matrix.Cols);
            
            for (int i = 0; i < Rows; ++i)
            {
                Console.Write("|");
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

        public static float Determinant(Matrix m, int n)
        {
            float D = 0; // Initialize result 

            // Base case : if matrix  
            // contains single 
            // element 
            if (n == 1)
                return m.Mat[0, 0];

            // To store cofactors 
            Matrix temp = new Matrix(n, n);//new float[n, n];

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
                while(matrix.Mat[i, lead] == 0)
                {
                    i++;
                    if(i == rowCount)
                    {
                        i = r;
                        lead++;
                        if(colCount == lead)
                        {
                            lead--;
                            break;
                        }
                    }
                }
                for (int j = 0; j < colCount; j++)
                {
                    float temp = matrix.Mat[r, j];
                    matrix.Mat[r, j] = matrix.Mat[i, j];
                    matrix.Mat[i, j] = temp;
                }

                Console.WriteLine($"Swap rows {i} and {r}");
                matrix.Display();

                float div = matrix.Mat[r, lead];
                if(div != 0)
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
                    if(j != r)
                    {
                        float sub = matrix.Mat[j, lead];
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
    }
}
