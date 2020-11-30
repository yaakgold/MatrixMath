using GoldLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMath
{
    class CalcMain
    {
        enum eOptions
        {
            MATRIX,
            VECTORS,
            TRANSLATION,
            QUIT
        }

        enum eOptionMat
        {
            MULTIPLY_MATRICIES,
            FIND_DETERMINATE,
            RREF,
            QUIT
        }

        enum eOptionVec
        {
            ADD,
            SUB,
            SCALE,
            NORM,
            DISTANCE,
            UNIT_VECTOR,
            DOT,
            DOT_ANGLE,
            PROJECT,
            CROSS,
            CROSS_ANGLE,
            TRIPLE_PROD,
            QUIT
        }

        enum eOptionTra
        {
            DET_ORTHO,
            ROT_MAT_X,
            ROT_MAT_Y,
            ROT_MAT_Z,
            SOLVE_FOR_X, //x = A-1 * B
            QUIT
        }

        static void Main(string[] args)
        {
            bool quit = false;

            while (!quit)
            {
                Console.WriteLine("What would you like to do?");
                eOptions choice = (eOptions)Utils.GetConsoleMenu(Enum.GetNames(typeof(eOptions)));
                Console.Clear();

                switch (choice)
                {
                    case eOptions.MATRIX:
                        MatrixCalcs();
                        break;
                    case eOptions.VECTORS:
                        VecCalcs();
                        break;
                    case eOptions.TRANSLATION:
                        TransformCalcs();
                        break;
                    case eOptions.QUIT:
                        quit = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private static void VecCalcs()
        {
            bool quit = false;

            while (!quit)
            {
                Console.WriteLine("What would you like to do?");
                eOptionVec choice = (eOptionVec)Utils.GetConsoleMenu(Enum.GetNames(typeof(eOptionVec)));
                Console.Clear();

                switch (choice)
                {
                    case eOptionVec.ADD:
                        Console.WriteLine("ADDITION");
                        Vectors v1A = new Vectors(Utils.GetConsoleInt("Enter dimension: "));
                        v1A.InsertValues();
                        Vectors v2A = new Vectors(v1A.Dimension);
                        Console.WriteLine("Vector 2: ");
                        v2A.InsertValues();
                        Vectors.Add(v1A, v2A);
                        break;
                    case eOptionVec.SUB:
                        Console.WriteLine("SUBTRACTION");
                        Vectors v1S = new Vectors(Utils.GetConsoleInt("Enter dimension: "));
                        v1S.InsertValues();
                        Vectors v2S = new Vectors(v1S.Dimension);
                        Console.WriteLine("Vector 2: ");
                        v2S.InsertValues();
                        Vectors.Sub(v1S, v2S);
                        break;
                    case eOptionVec.SCALE:
                        Console.WriteLine("SCALE");
                        Vectors v1Sc = new Vectors(Utils.GetConsoleInt("Enter dimension: "));
                        v1Sc.InsertValues();
                        Vectors.Scale(v1Sc, Utils.GetConsoleIn<double>("Enter scale: "));
                        break;
                    case eOptionVec.NORM:
                        Console.WriteLine("NORM");
                        Vectors v1N = new Vectors(Utils.GetConsoleInt("Enter dimension: "));
                        v1N.InsertValues();
                        Vectors.Norm(v1N);
                        break;
                    case eOptionVec.DISTANCE:
                        Console.WriteLine("DISTANCE");
                        Vectors v1D = new Vectors(Utils.GetConsoleInt("Enter dimension: "));
                        v1D.InsertValues();
                        Vectors v2D = new Vectors(v1D.Dimension);
                        Console.WriteLine("Vector 2: ");
                        v2D.InsertValues();
                        Vectors.Distance(v1D, v2D);
                        break;
                    case eOptionVec.UNIT_VECTOR:
                        Console.WriteLine("UNIT VECTOR");
                        Vectors v1Uv = new Vectors(Utils.GetConsoleInt("Enter dimension: "));
                        v1Uv.InsertValues();
                        Vectors.UnitVect(v1Uv);
                        break;
                    case eOptionVec.DOT:
                        Console.WriteLine("DOT");
                        Vectors v1Do = new Vectors(Utils.GetConsoleInt("Enter dimension: "));
                        v1Do.InsertValues();
                        Vectors v2Do = new Vectors(v1Do.Dimension);
                        Console.WriteLine("Vector 2: ");
                        v2Do.InsertValues();
                        Vectors.Dot(v1Do, v2Do);
                        break;
                    case eOptionVec.DOT_ANGLE:
                        Console.WriteLine("DOT - ANGLE");
                        Vectors v1DA = new Vectors(Utils.GetConsoleInt("Enter dimension: "));
                        v1DA.InsertValues();
                        Vectors v2DA = new Vectors(v1DA.Dimension);
                        Console.WriteLine("Vector 2: ");
                        v2DA.InsertValues();
                        Vectors.DotAngle(v1DA, v2DA);
                        break;
                    case eOptionVec.PROJECT:
                        Console.WriteLine("PROJECT");
                        Vectors v1P = new Vectors(Utils.GetConsoleInt("Enter dimension: "));
                        v1P.InsertValues();
                        Vectors v2P = new Vectors(v1P.Dimension);
                        Console.WriteLine("Vector 2: ");
                        v2P.InsertValues();
                        Vectors.ProjectVect(v1P, v2P);
                        break;
                    case eOptionVec.CROSS:
                        Console.WriteLine("CROSS");
                        Vectors v1C = new Vectors(Utils.GetConsoleInt("Enter dimension: "));
                        v1C.InsertValues();
                        Vectors v2C = new Vectors(v1C.Dimension);
                        Console.WriteLine("Vector 2: ");
                        v2C.InsertValues();
                        Vectors.CrossProd(v1C, v2C);
                        break;
                    case eOptionVec.CROSS_ANGLE:
                        Console.WriteLine("CROSS 0 ANGLE");
                        Vectors v1CA = new Vectors(Utils.GetConsoleInt("Enter dimension: "));
                        v1CA.InsertValues();
                        Vectors v2CA = new Vectors(v1CA.Dimension);
                        Console.WriteLine("Vector 2: ");
                        v2CA.InsertValues();
                        Vectors.CrossAngle(v1CA, v2CA);
                        break;
                    case eOptionVec.TRIPLE_PROD:
                        Console.WriteLine("TRIPLE PRODUCT");
                        Vectors v1T = new Vectors(Utils.GetConsoleInt("Enter dimension: "));
                        v1T.InsertValues();
                        Vectors v2T = new Vectors(v1T.Dimension);
                        Console.WriteLine("Vector 2: ");
                        v2T.InsertValues();
                        Vectors v3T = new Vectors(v1T.Dimension);
                        Console.WriteLine("Vector 3: ");
                        v3T.InsertValues();
                        Vectors.TripleProduct(v1T, v2T, v3T);
                        break;
                    case eOptionVec.QUIT:
                        quit = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private static void MatrixCalcs()
        {
            bool quit = false;

            while (!quit)
            {
                Console.WriteLine("What would you like to do?");
                eOptionMat choice = (eOptionMat)Utils.GetConsoleMenu(Enum.GetNames(typeof(eOptionMat)));
                Console.Clear();

                switch (choice)
                {
                    case eOptionMat.MULTIPLY_MATRICIES:
                        Matrix mm1 = new Matrix(Utils.GetConsoleInt("Enter num rows for first matrix: "), Utils.GetConsoleInt("Enter num cols for first matrix: "));
                        mm1.InsertValues();
                        Matrix mm2 = new Matrix(Utils.GetConsoleInt("Enter num rows for second matrix: "), Utils.GetConsoleInt("Enter num cols for second matrix: "));
                        mm2.InsertValues();

                        mm1.MultiplyMats(mm2).Display();
                        break;
                    case eOptionMat.FIND_DETERMINATE:
                        int matrixSize = Utils.GetConsoleInt("Enter matrix dimension: ");
                        Matrix md1 = new Matrix(matrixSize, matrixSize);
                        md1.InsertValues();
                        Matrix.Determinant(md1, matrixSize);
                        break;
                    case eOptionMat.RREF:
                        Matrix mr1 = new Matrix(Utils.GetConsoleInt("Enter num rows: "), Utils.GetConsoleInt("Enter num cols: "));
                        mr1.InsertValues();
                        Matrix.RREF(mr1).Display();
                        break;
                    case eOptionMat.QUIT:
                        quit = true;
                        break;
                    default:
                        break;
                }

            }
        }

        private static void TransformCalcs()
        {
            bool quit = false;

            while (!quit)
            {
                Console.WriteLine("What would you like to do?");
                eOptionTra choice = (eOptionTra)Utils.GetConsoleMenu(Enum.GetNames(typeof(eOptionTra)));
                Console.Clear();

                switch (choice)
                {
                    case eOptionTra.DET_ORTHO:
                        Console.WriteLine("Testing ortho");
                        Vectors v1O = new Vectors(Utils.GetConsoleInt("Enter dimension: "));
                        v1O.InsertValues();
                        Console.WriteLine("Vector 2");
                        Vectors v2O = new Vectors(Utils.GetConsoleInt("Enter dimension: "));
                        v2O.InsertValues();
                        Console.WriteLine("Vector 3");
                        Vectors v3O = new Vectors(Utils.GetConsoleInt("Enter dimension: "));
                        v3O.InsertValues();

                        Vectors.DetermineOthro(v1O, v2O, v3O);
                        break;
                    case eOptionTra.ROT_MAT_X:
                        Console.WriteLine("Rotation by X axis: ");
                        float degX = Utils.GetConsoleIn<float>("Enter degrees of rotation: ");
                        Matrix.Rot_X(degX);
                        break;
                    case eOptionTra.ROT_MAT_Y:
                        Console.WriteLine("Rotation by Y axis: ");
                        float degY = Utils.GetConsoleIn<float>("Enter degrees of rotation: ");
                        Matrix.Rot_Y(degY);
                        break;
                    case eOptionTra.ROT_MAT_Z:
                        Console.WriteLine("Rotation by Z axis: ");
                        float degZ = Utils.GetConsoleIn<float>("Enter degrees of rotation: ");
                        Matrix.Rot_Z(degZ);
                        break;
                    case eOptionTra.SOLVE_FOR_X:
                        Console.WriteLine("Ax = B (Solve for X)");
                        Matrix a = new Matrix(Utils.GetConsoleInt("Enter num rows for matrix A: "), Utils.GetConsoleInt("Enter num cols for matrix A: "));
                        a.InsertValues();
                        Matrix b = new Matrix(Utils.GetConsoleInt("Enter num rows for matrix B: "), Utils.GetConsoleInt("Enter num cols for matrix B: "));
                        b.InsertValues();

                        Matrix.SolveX(a, b);
                        break;
                    case eOptionTra.QUIT:
                        quit = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
