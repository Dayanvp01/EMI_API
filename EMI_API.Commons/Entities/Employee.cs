using static EMI_API.Commons.Enums.PositionType;

namespace EMI_API.Commons.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Salary { get; set; }
        /// <summary>
        /// 1 For Regular, 2 For Manager
        /// </summary>
        public int CurrentPosition { get; set; }

        public List<PositionHistory> PositionHistory { get; set; } = new List<PositionHistory>();

        /// <summary>
        /// Calcula el bono anual basado en el salario y la posicion actual
        /// si es 1 se realiza el calculo para empleado regular, 2 para empleados gerentes
        /// </summary>
        /// <returns>valor del bono</returns>
        public decimal CalculateYearlyBonus()
        {
            decimal result = 0;
            switch (CurrentPosition)
            {
                case (int)PositionTypeEnum.Regular:
                    result = Salary * 0.1m;
                    break;
                case (int)PositionTypeEnum.Manager:
                    result = Salary * 0.2m;
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
