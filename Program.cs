using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;


namespace Newton_s_method_for_solving_systems_of_equations
{
    class Program
    {
        //X[0]=x, X[1]=y
        public static Vector<double> f(Vector<double> X)//вектор значений
        {
            Vector<double> f = DenseVector.Create(X.Count, 0);
             
             f[0] = X[0] + X[0] * Math.Pow(X[1], 3) - 9;
             f[1] = X[0] * X[1] + X[0] * Math.Pow(X[1], 2) - 6;

             /*f[0] = X[0] + Math.Sin(X[1]) * Math.Pow(X[0],2) - 20;
             f[1] = Math.Pow(X[0],2)-X[1];*/

            return f;
        }

        public static Matrix<double> deritavef(Vector<double> X)//матрица производных от f
        {
            Matrix<double> df = DenseMatrix.Create(X.Count, X.Count, 0);

           df[0, 0] = 1 + Math.Pow(X[1], 3);
            df[0, 1] = 3 * Math.Pow(X[1], 2) * X[0];
            df[1, 0] = X[1] + Math.Pow(X[1], 2);
            df[1, 1] = X[0] + 2 * X[1] * X[0];

            /*df[0, 0] = 1+2*X[0]*Math.Sin(X[1]);
            df[0, 1] = Math.Pow(X[0],2)*Math.Cos(X[0]);
            df[1, 0] = 2*X[0];
            df[1, 1] = -1;*/

            return df;
        }

        

        static void Main(string[] args)
        {
            Vector<double> X = DenseVector.Build.Random(2);
            Vector<double> Xlast;
            Matrix<double> W = DenseMatrix.Create(X.Count, X.Count, 0);//Матрица производных(Якоби)-заполняем нулями
            Console.WriteLine($"X0=[{string.Join(" , ", X)}]");//вывод начальных значений вектора
            Vector<double> dX = DenseVector.Build.Random(2);
            double Dx = double.MaxValue;//контроль изменения вектора дял выхода из цикла при нахождении всех решений 
            while (Dx > 1e-10) 
            {
                Xlast = X;
                W = deritavef(X);
                X -= 0.1 * W.Inverse() * f(Xlast); //W.Inverse- обратная матрица якоби(по формуле). Для лучшей сходимости будем умножать на 0.1(шаг приближения)
                dX = X - Xlast; //крутая перегрузка для работы с векторами 
                Dx = dX.SumMagnitudes();// сумма по модулю
            }
            Console.WriteLine("X="+ X);
            Console.WriteLine("F(x)=" + f(X)) ;
            Console.ReadKey();

        }
    }
}
