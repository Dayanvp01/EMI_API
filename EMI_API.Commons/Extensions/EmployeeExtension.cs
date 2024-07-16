using EMI_API.Commons.Entities;
using static EMI_API.Commons.Enums.PositionType;

namespace EMI_API.Commons.Extensions
{
    public static class EmployeeExtension
    {
        /// <summary>
        ///  Extiende la funcionalidad de la clase Employee
        /// Calcula el bono anual basado en el salario y la posicion actual
        /// si es 1 se realiza el calculo para empleado regular, 2 para empleados gerentes
        /// </summary>
        /// <param name="employee">empleado</param>
        /// <returns>Bono anual del empleado</returns>
        public static decimal CalculateYearlyBonus(this Employee employee)
        {
            decimal result = 0;
            switch (employee.CurrentPosition)
            {
                case (int)PositionTypeEnum.Regular:
                    result = employee.Salary * 0.1m;
                    break;
                case (int)PositionTypeEnum.Manager:
                    result = employee.Salary * 0.2m;
                    break;
                default:
                    break;
            }
            return result;
        }
    }

}
