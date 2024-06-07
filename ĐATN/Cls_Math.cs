using System;
namespace ĐATN
{
    internal class Cls_Math
    {
        private double berong;
        private double chieucao;
        private double lucdoc;
        private double Momen;
        private double Rb;
        private double Rs;
        private double Abv;
        private double ho;
        private double n = 1.5;

        public Cls_Math(double B, double H, double N, double M, double betong, double thep, double a)
        {
            berong = B;
            chieucao = H;
            lucdoc = N;
            Momen = M;
            Rb = betong;
            Abv = a;
            Rs = thep;
            ho = H - Abv;
        }
        private double ea()
        {
            double a = Math.Max((3600 / 600), (chieucao / 30));
            return a;
        }
        private double eo()
        {
            double a = Math.Max(Momen / lucdoc, ea());

            return a;
        }
        private double Za()
        {
            double a = ho - 4;
            return a;
        }
        private double anpha()
        {
            double a = lucdoc / (Rb * berong * ho * berong);
            return a;
        }
        private double anphaM1()
        {
            double a = (lucdoc * (n * eo() + 0.5 * Za())) / (Rb * berong * ho * ho);
            return a;
        }
        private double shi()
        {
            double a = Abv / ho;
            return a;
        }
        public  double As()
        {
            double a = ((Rb * berong * ho) / Rs) * ((anphaM1() - anpha() * (1 - 0.5 * anpha())) / (1 - shi()));
            return a;
        }
    }

}
