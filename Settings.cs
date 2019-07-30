using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcedGenV2
{
    //Настройки приложения
    public static class Settings
    {
        //Количество тайлов в длину 
        public static int TilesWide => 30;
        //Количество тайлов в высоту
        public static int TilesHeight => 15;

        //Ветвистость уровня. Чем меньше число, тем компактнее уровень
        public static int Branching => 30;
        //Максимальное число сгенерированных тайлов
        public static int MaxTotalCount => 100;

        //Вероятность вовращения true для вычислений. Число обратное вероятности
        public static int BoolChance => 4;

    }
}
