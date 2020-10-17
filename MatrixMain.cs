using GoldLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMath
{
    class MatrixMain
    {
        enum eOption
        {
            MULTIPLY_MATRICIES,
            FIND_DETERMINATE,
            RREF,
            QUIT
        }

        static void Main(string[] args)
        {
            bool quit = false;

            while(!quit)
            {
                Console.WriteLine("What would you like to do?");
                eOption choice = (eOption)Utils.GetConsoleMenu(Enum.GetNames(typeof(eOption)));
                Console.Clear();

                switch (choice)
                {
                    case eOption.MULTIPLY_MATRICIES:
                        Matrix mm1 = new Matrix(Utils.GetConsoleInt("Enter num rows for first matrix: "), Utils.GetConsoleInt("Enter num cols for first matrix: "));
                        mm1.InsertValues();
                        Matrix mm2 = new Matrix(Utils.GetConsoleInt("Enter num rows for second matrix: "), Utils.GetConsoleInt("Enter num cols for second matrix: "));
                        mm2.InsertValues();

                        mm1.MultiplyMats(mm2).Display();
                        break;
                    case eOption.FIND_DETERMINATE:
                        int matrixSize = Utils.GetConsoleInt("Enter matrix dimension: ");
                        Matrix md1 = new Matrix(matrixSize, matrixSize);
                        md1.InsertValues();
                        Matrix.Determinant(md1, matrixSize);
                        break;
                    case eOption.RREF:
                        Matrix mr1 = new Matrix(Utils.GetConsoleInt("Enter num rows: "), Utils.GetConsoleInt("Enter num cols: "));
                        mr1.InsertValues();
                        Matrix.RREF(mr1).Display();
                        break;
                    case eOption.QUIT:
                        quit = true;
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
